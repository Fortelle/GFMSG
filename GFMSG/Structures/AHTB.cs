using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace GFMSG
{
    // Asynchronous Hash TaBle (Fnv1a)
    public class AHTB
    {
        public static readonly Regex TextRegex = new(@"\\u(....)|.");

        public record Entry(ulong Hash, string Text);

        public Entry[] Entries;

        public AHTB()
        {
        }

        public AHTB(string filename)
        {
            using var fs = File.OpenRead(filename);
            using var br = new BinaryReader(fs);

            Load(br);
        }

        public AHTB(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);

            Load(br);
        }

        public AHTB(string[] text)
        {
            Entries = text.Select(x => new Entry(FnvHash.Fnv1a_64(Unescape(x)), x)).ToArray();
        }

        public void Load(BinaryReader br)
        {
            var magic = br.ReadChars(4);
            Debug.Assert(new string(magic) == "AHTB");

            uint lineCount = br.ReadUInt32();
            var list = new List<Entry>((int)lineCount);
            for (var i = 0; i < lineCount; i++)
            {
                ulong hash = br.ReadUInt64();
                ushort length = br.ReadUInt16();
                byte[] charData = br.ReadBytes(length);
                Debug.Assert(charData[^1] == 0x00);
                var text = Escape(charData[..^1]);
                list.Add(new(hash, text));
            }

            if(list.Count > 0) // ensure the hashing algorithm is fnv1a
            {
                Debug.Assert(list[0].Hash == FnvHash.Fnv1a_64(Unescape(list[0].Text)));
            }

            Entries = list.ToArray();
        }

        public void Save(string path)
        {
            using var fs = File.OpenWrite(path);
            using var bw = new BinaryWriter(fs);
            Save(bw);
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write("AHTB".ToArray());
            bw.Write((uint)Entries.Length);
            foreach (var entry in Entries)
            {
                var data = Unescape(entry.Text);
                bw.Write(entry.Hash);
                bw.Write((ushort)(data.Length + 1));
                bw.Write(data);
                bw.Write((byte)0);
            }
        }

        private static string Escape(byte[] chars)
        {
            return string.Join("", chars
                .Select(x => (char)x)
                .Select(x => char.IsControl(x) ? $"\\u{(byte)x:X4}" : $"{x}")
                .ToArray());
        }

        private static byte[] Unescape(string text)
        {
            return TextRegex.Matches(text)
                .Select(x => x.Groups[1].Length > 1 ? Convert.ToByte(x.Groups[1].Value, 16 ) : (byte)x.Value[0])
                .ToArray();
        }
    }
}
