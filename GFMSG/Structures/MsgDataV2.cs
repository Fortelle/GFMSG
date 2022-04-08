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

    public class MsgDataV2
    {
        private const ushort MASK = 0x2983;

        // languages[]/entries[]
        public StringEntry[][] Entries { get; set; }

        public MsgDataV2()
        {

        }

        public MsgDataV2(string filename)
        {
            using var ms = File.OpenRead(filename);
            using var br = new BinaryReader(ms);
            Load(br);
        }

        public MsgDataV2(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);
            Load(br);
        }

        public MsgDataV2(StringEntry[][] entries)
        {
            Entries = entries;
        }

        private void Load(BinaryReader br)
        {
            // Header block
            var header = new HeaderBlock();
            header.LanguageNumber = br.ReadUInt16();
            header.StringNumber = br.ReadUInt16();
            header.MaxLanguageBlockSize = br.ReadUInt32();
            header.Reserved = (DataCoding)br.ReadUInt32();
            header.LanguageBlockOffsets = Enumerable
                .Range(0, header.LanguageNumber)
                .Select(_ => br.ReadUInt32())
                .ToArray();

            // Language block
            var languageBlocks = new LanguageBlock[header.LanguageNumber];
            for (var langIndex = 0; langIndex < header.LanguageNumber; langIndex++)
            {
                //Debug.Assert(br.BaseStream.Position == Header.LanguageBlockOffsets[langIndex]);
                br.BaseStream.Seek(header.LanguageBlockOffsets[langIndex], SeekOrigin.Begin);

                var lb = new LanguageBlock();
                lb.Size = br.ReadUInt32();
                lb.Paramaters = new StringParameter[header.StringNumber];
                for(var strIndex = 0; strIndex < header.StringNumber; strIndex++)
                {
                    lb.Paramaters[strIndex] = new StringParameter()
                    {
                        Offset = br.ReadUInt32(),
                        Length = br.ReadUInt16(),
                        UserParam = new GrammaticalAttribute(br.ReadUInt16()),
                    };
                }
                var list = new List<ushort[]>(header.StringNumber);
                for (var strIndex = 0; strIndex < header.StringNumber; strIndex++)
                {
                    br.BaseStream.Seek(header.LanguageBlockOffsets[langIndex] + lb.Paramaters[strIndex].Offset, SeekOrigin.Begin);
                    var codes = Enumerable.Repeat(0, lb.Paramaters[strIndex].Length).Select(x => br.ReadUInt16()).ToArray();
                    if (header.Reserved == DataCoding.Coded)
                    {
                        codes = Decode(codes, strIndex);
                    }
                    list.Add(codes);
                }
                lb.Entries = list.ToArray();
                languageBlocks[langIndex] = lb;
            }

            Entries = languageBlocks.Select(x => {
                return x.Entries.Select((y, i) =>
                {
                    return new StringEntry(y, x.Paramaters[i].UserParam);
                }).ToArray();
            }).ToArray();
        }

        public void Save(string path)
        {
            using var fs = File.OpenWrite(path);
            using var bw = new BinaryWriter(fs);
            Save(bw);
        }

        public void Save(BinaryWriter bw)
        {
            int count = Entries.Length;
            var header = new HeaderBlock
            {
                LanguageNumber = (ushort)count,
                StringNumber = (ushort)Entries[0].Length,
                MaxLanguageBlockSize = 0,
                Reserved = DataCoding.Coded,
                LanguageBlockOffsets = new uint[count]
            };
            uint headerSize = 12u + 4u * header.LanguageNumber;
            uint langOffset = headerSize;

            var languageBlocks = new LanguageBlock[header.LanguageNumber];
            for (var langIndex = 0; langIndex < header.LanguageNumber; langIndex++)
            {
                header.LanguageBlockOffsets[langIndex] = langOffset;
                uint strOffset = 4u + 8u * header.StringNumber;
                var lb = new LanguageBlock();
                lb.Paramaters = new StringParameter[header.StringNumber];
                for (var strIndex = 0; strIndex < header.StringNumber; strIndex++)
                {
                    ushort strLength = (ushort)Entries[langIndex][strIndex].Codes.Length;
                    lb.Paramaters[strIndex] = new StringParameter()
                    {
                        Offset = strOffset,
                        Length = strLength,
                        UserParam = Entries[langIndex][strIndex].UserParam,
                    };
                    strOffset += 2u * strLength;
                    if (strLength % 2 == 1) strOffset += 2;
                }
                lb.Size = strOffset;
                lb.Entries = Entries[langIndex].Select(x => x.Codes).ToArray();
                langOffset += strOffset;
                languageBlocks[langIndex] = lb;
            }

            header.MaxLanguageBlockSize = languageBlocks.Max(x => x.Size);

            bw.Write(header.LanguageNumber);
            bw.Write(header.StringNumber);
            bw.Write(header.MaxLanguageBlockSize);
            bw.Write((uint)header.Reserved);
            for (var langIndex = 0; langIndex < header.LanguageBlockOffsets.Length; langIndex++)
            {
                bw.Write(header.LanguageBlockOffsets[langIndex]);
            }

            for (var langIndex = 0; langIndex < languageBlocks.Length; langIndex++)
            {
                bw.Write(languageBlocks[langIndex].Size);
                for (var strIndex = 0; strIndex < languageBlocks[langIndex].Paramaters.Length; strIndex++)
                {
                    bw.Write(languageBlocks[langIndex].Paramaters[strIndex].Offset);
                    bw.Write(languageBlocks[langIndex].Paramaters[strIndex].Length);
                    bw.Write(languageBlocks[langIndex].Paramaters[strIndex].UserParam.ToUshort());
                }
                for (var strIndex = 0; strIndex < languageBlocks[langIndex].Entries.Length; strIndex++)
                {
                    var data = languageBlocks[langIndex].Entries[strIndex];
                    if (header.Reserved == DataCoding.Coded)
                    {
                        data = Encode(data, strIndex);
                    }
                    foreach (var u16 in data)
                    {
                        bw.Write(u16);
                    }
                    if (languageBlocks[langIndex].Paramaters[strIndex].Length % 2 == 1)
                    {
                        bw.Write((byte)0);
                        bw.Write((byte)0);
                    }
                }
                //todo: check alignment
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
            public ushort StringNumber; // string(or row) number
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

        public record StringEntry(ushort[] Codes, GrammaticalAttribute UserParam);
    }

}