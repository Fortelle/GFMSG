using System.Diagnostics;

namespace GFMSG
{
    public class MsgDataV1
    {
        public ushort[][] Data;

        public ushort Seed;
        public bool Compressed = false;

        public MsgDataV1()
        {

        }

        public MsgDataV1(string filename)
        {
            using var ms = File.OpenRead(filename);
            using var br = new BinaryReader(ms);
            Load(br);
        }

        public MsgDataV1(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);
            Load(br);
        }

        public MsgDataV1(ushort[][] data, ushort seed)
        {
            Data = data;
            Seed = seed;
        }

        private void Load(BinaryReader br)
        {
            // Header block
            var entryCount = br.ReadUInt16();
            Seed = br.ReadUInt16();

            var parameters = new List<StringParameter>();
            for (int i = 0; i < entryCount; i++)
            {
                uint offset = br.ReadUInt32();
                uint length = br.ReadUInt32();
                var sp = new StringParameter(offset, length);
                sp = DecodeParam(sp, i);
                parameters.Add(sp);
            }

            var text = new List<ushort[]>();
            for (int i = 0; i < entryCount; i++)
            {
                var param = parameters[i];
                var data = Enumerable.Range(0, (int)param.Length)
                    .Select(x => br.ReadUInt16())
                    .ToArray();
                data = Decode(data, i);
                if (data[0] == 0xF100)
                {
                    Compressed = true;
                    data = Decompress(data);
                }
                text.Add(data);
            }

            var header = new HeaderBlock
            {
                EntryCount = entryCount,
                Seed = Seed,
                Parameters = parameters.ToArray()
            };
            Data = text.ToArray();
        }

        public void Save(string path)
        {
            using var fs = File.OpenWrite(path);
            using var bw = new BinaryWriter(fs);
            Save(bw);
        }

        public void Save(BinaryWriter bw)
        {
            int count = Data.Length;
            uint headerSize = (uint)(4 + 8 * count);
            uint strOffset = headerSize;
            var parameters = new StringParameter[Data.Length];
            var data = new ushort[Data.Length][];

            for (var strIndex = 0; strIndex < count; strIndex++)
            {
                ushort[] codes = Encode(Data[strIndex], strIndex);
                if (Compressed)
                {
                    codes = Compress(codes);
                }
                ushort strLength = (ushort)codes.Length;
                var param = new StringParameter()
                {
                    Offset = strOffset,
                    Length = strLength,
                };
                parameters[strIndex] = param;
                data[strIndex] = codes;
                strOffset += 2u * strLength;
            }

            var header = new HeaderBlock
            {
                EntryCount = (ushort)data.Length,
                Seed = Seed,
                Parameters = parameters,
            };

            bw.Write(header.EntryCount);
            bw.Write(header.Seed);
            for (var i = 0; i < header.Parameters.Length; i++)
            {
                var param = DecodeParam(header.Parameters[i], i);
                bw.Write(param.Offset);
                bw.Write(param.Length);
            }

            for (var strIndex = 0; strIndex < data.Length; strIndex++)
            {
                foreach (var u16 in data[strIndex])
                {
                    bw.Write(u16);
                }
            }
        }

        private StringParameter DecodeParam(StringParameter sp, int index)
        {
            uint mask = (uint)((Seed * 0x02FD * (index + 1)) & 0xFFFF);
            mask |= mask << 16;
            uint offset = sp.Offset ^ mask;
            uint length = sp.Length ^ mask;

            return new StringParameter(offset, length);
        }

        private static ushort[] Decode(ushort[] codes, int index)
        {
            ushort mask = (ushort)((0x91BD3 * (index + 1)) & 0xFFFF);
            var buff = new ushort[codes.Length];
            for (int i = 0; i < buff.Length; i++)
            {
                buff[i] = (ushort)(codes[i] ^ mask);
                mask += 0x493D;
            }
            return buff;
        }

        private static ushort[] Encode(ushort[] codes, int index)
        {
            return Decode(codes, index);
        }

        public static ushort CalcCrc(string name)
        {
            uint crc = 0;
            foreach (char c in name)
            {
                uint d = c;
                for (var i = 0; i < 8; i++)
                {
                    crc <<= 1;
                    if ((crc & 0x10000) != 0)
                    {
                        crc ^= 0x8003;
                    }
                    d <<= 1;
                    if ((d & 0x100) != 0)
                    {
                        crc ^= 1;
                    }
                }
            }

            return (ushort)(crc & 0xffff);
        }

        private static ushort[] Decompress(ushort[] codes)
        {
            Debug.Assert(codes[0] == 0xF100);

            var list = new List<ushort>();
            var srcBit = 0;

            int i = 1;
            while(true)
            {
                var expCode = (codes[i] >> srcBit) & 0x01FF;
                srcBit += 9;
                if (srcBit >= 15)
                {
                    i++;
                    srcBit -= 15;
                    if (srcBit > 0)
                    {
                        expCode |= (codes[i] << (9 - srcBit)) & 0x01FF;
                    }
                }
                if (expCode == 0x01FF) break;
                list.Add((ushort)expCode);
            };
            list.Add(0xFFFF);

            return list.ToArray();
        }

        private static ushort[] Compress(ushort[] codes)
        {
            var compressedCodes = codes
                .SelectMany(b => Enumerable.Range(0, 9).Select(i => (b >> i & 1) == 1).ToArray())
                .Chunk(15)
                .Select(chunk => {
                    ushort code = 0;
                    for (var i = 0; i < chunk.Length; i++)
                    {
                        if (chunk[i]) code |= (ushort)(1 << i);
                    }
                    if(chunk.Length < 15)
                    {
                        code |= (ushort)((0xFFFF << chunk.Length) & 0x7fff);
                    }
                    return code;
                })
                .ToArray();

            var list = new List<ushort>();
            list.Add(0xF100);
            list.AddRange(compressedCodes);
            if(((codes.Length * 9) % 15) >= 9)
            {
                list.Add(0xFFFF); // ???
            }

            return list.ToArray();
        }


        private struct HeaderBlock
        {
            public ushort EntryCount;
            public ushort Seed;
            public StringParameter[] Parameters;
        }

        private struct StringParameter
        {
            public uint Offset;
            public uint Length;

            public StringParameter(uint offset, uint length)
            {
                Offset = offset;
                Length = length;
            }
        }
    }

}