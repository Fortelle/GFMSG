using System.Diagnostics;

namespace GFMSG
{
    // GAMEFREAK Library String System

    /*
     * MsgData
     *   HeaderBlock
     *   LanguageBlock
     *     StringParameter[]
     *     StringData[]
     *   LanguageBlock
     *     StringParameter[]
     *     StringData[]
     *   ...
     */

    public class MsgData
    {
        private const ushort MASK = 0x2983;

        private HeaderBlock Header;
        private LanguageBlock[] LanguageBlocks;

        // languages[]/entries[]/(codes[], GrammaticalAttribute)
        public (ushort[], GrammaticalAttribute)[][] GetData()
        {
            return LanguageBlocks.Select(x => {
                return x.Entries.Select((y, i) =>
                {
                    return (y, x.Paramaters[i].UserParam);
                }).ToArray();
            }).ToArray();
        }

        public MsgData()
        {

        }

        public MsgData(string filename)
        {
            using var ms = File.OpenRead(filename);
            using var br = new BinaryReader(ms);
            Load(br);
        }

        public MsgData(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);
            Load(br);
        }

        public MsgData((ushort[], GrammaticalAttribute)[][] data)
        {
            var header = new HeaderBlock
            {
                LanguageNumber = (ushort)data.Length,
                StringNumberPerLanguage = (ushort)data[0].Length,
                MaxLanguageBlockSize = 0,
                Reserved = DataCoding.Coded,
                LanguageBlockOffsets = new uint[data.Length]
            };
            uint headerSize = 12u + 4u * header.LanguageNumber;
            uint langOffset = headerSize;

            var languageBlocks = new LanguageBlock[header.LanguageNumber];
            for (var langIndex = 0; langIndex < header.LanguageNumber; langIndex++)
            {
                header.LanguageBlockOffsets[langIndex] = langOffset;
                uint strOffset = 4u + 8u * header.StringNumberPerLanguage;
                var lb = new LanguageBlock();
                lb.Paramaters = new StringParameter[header.StringNumberPerLanguage];
                for (var strIndex = 0; strIndex < header.StringNumberPerLanguage; strIndex++)
                {
                    ushort strLength = (ushort)data[langIndex][strIndex].Item1.Length;
                    lb.Paramaters[strIndex] = new StringParameter()
                    {
                        Offset = strOffset,
                        Length = strLength,
                        UserParam = data[langIndex][strIndex].Item2,
                    };
                    strOffset += 2u * strLength;
                    if (strLength % 2 == 1) strOffset += 2;
                }
                lb.Size = strOffset;
                lb.Entries = data[langIndex].Select(x => x.Item1).ToArray();
                langOffset += strOffset;
                languageBlocks[langIndex] = lb;
            }

            header.MaxLanguageBlockSize = languageBlocks.Max(x => x.Size);

            Header = header;
            LanguageBlocks = languageBlocks;
        }

        private void Load(BinaryReader br)
        {
            // Header block
            Header = new HeaderBlock();
            Header.LanguageNumber = br.ReadUInt16();
            Header.StringNumberPerLanguage = br.ReadUInt16();
            Header.MaxLanguageBlockSize = br.ReadUInt32();
            Header.Reserved = (DataCoding)br.ReadUInt32();
            Header.LanguageBlockOffsets = Enumerable
                .Range(0, Header.LanguageNumber)
                .Select(_ => br.ReadUInt32())
                .ToArray();

            // Language block
            LanguageBlocks = new LanguageBlock[Header.LanguageNumber];
            for (var langIndex = 0; langIndex < Header.LanguageNumber; langIndex++)
            {
                var lb = new LanguageBlock();
                lb.Size = br.ReadUInt32();
                lb.Paramaters = new StringParameter[Header.StringNumberPerLanguage];
                for(var strIndex = 0; strIndex < Header.StringNumberPerLanguage; strIndex++)
                {
                    lb.Paramaters[strIndex] = new StringParameter()
                    {
                        Offset = br.ReadUInt32(),
                        Length = br.ReadUInt16(),
                        UserParam = new GrammaticalAttribute(br.ReadUInt16()),
                    };
                }
                var list = new List<ushort[]>(Header.StringNumberPerLanguage);
                for (var strIndex = 0; strIndex < Header.StringNumberPerLanguage; strIndex++)
                {
                    br.BaseStream.Seek(Header.LanguageBlockOffsets[langIndex] + lb.Paramaters[strIndex].Offset, SeekOrigin.Begin);
                    var codes = Enumerable.Repeat(0, lb.Paramaters[strIndex].Length).Select(x => br.ReadUInt16()).ToArray();
                    if (Header.Reserved == DataCoding.Coded)
                    {
                        codes = Decode(codes, strIndex);
                    }
                    list.Add(codes);
                }
                lb.Entries = list.ToArray();
                LanguageBlocks[langIndex] = lb;
            }
        }

        public void Save(string path)
        {
            using var fs = File.OpenWrite(path);
            using var bw = new BinaryWriter(fs);
            Save(bw);
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write(Header.LanguageNumber);
            bw.Write(Header.StringNumberPerLanguage);
            bw.Write(Header.MaxLanguageBlockSize);
            bw.Write((uint)Header.Reserved);
            for (var langIndex = 0; langIndex < Header.LanguageBlockOffsets.Length; langIndex++)
            {
                bw.Write(Header.LanguageBlockOffsets[langIndex]);
            }

            for (var langIndex = 0; langIndex < LanguageBlocks.Length; langIndex++)
            {
                bw.Write(LanguageBlocks[langIndex].Size);
                for (var strIndex = 0; strIndex < LanguageBlocks[langIndex].Paramaters.Length; strIndex++)
                {
                    bw.Write(LanguageBlocks[langIndex].Paramaters[strIndex].Offset);
                    bw.Write(LanguageBlocks[langIndex].Paramaters[strIndex].Length);
                    bw.Write(LanguageBlocks[langIndex].Paramaters[strIndex].UserParam.ToUshort());
                }
                for (var strIndex = 0; strIndex < LanguageBlocks[langIndex].Entries.Length; strIndex++)
                {
                    var data = LanguageBlocks[langIndex].Entries[strIndex];
                    if (Header.Reserved == DataCoding.Coded)
                    {
                        data = Encode(data, strIndex);
                    }
                    foreach (var u16 in data)
                    {
                        bw.Write(u16);
                    }
                    if (LanguageBlocks[langIndex].Paramaters[strIndex].Length % 2 == 1)
                    {
                        bw.Write((byte)0);
                        bw.Write((byte)0);
                    }
                }
            }
        }

        private static ushort[] Decode(ushort[] encodedData, int strIndex)
        {
            var decodedData = new ushort[encodedData.Length];
            var mask = (ushort)((MASK * (strIndex + 3)) & 0xFFFF);
            for (var i = 0; i < encodedData.Length; i++) {
                decodedData[i] = (ushort)(encodedData[i] ^ mask);
                mask = (ushort)(((mask & 0xE000) >> 13) | ((mask & 0x1FFF) << 3));
            }
            return decodedData;
        }

        private static ushort[] Encode(ushort[] decodedData, int strIndex)
        {
            return Decode(decodedData, strIndex);
        }

        private struct HeaderBlock
        {
            public ushort LanguageNumber; // language(or column) number
            public ushort StringNumberPerLanguage; // string(or row) number
            public uint MaxLanguageBlockSize;
            public DataCoding Reserved;
            public uint[] LanguageBlockOffsets;
        }

        private struct LanguageBlock
        {
            public uint Size;
            public StringParameter[] Paramaters;
            public ushort[][] Entries;
        }

        private struct StringParameter
        {
            public uint Offset;
            public ushort Length; // with eom
            public GrammaticalAttribute UserParam;
        }
    }

}