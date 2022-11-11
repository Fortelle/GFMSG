using System.Collections;

namespace GFMSG
{
    public class MsgWrapper
    {
        public FileVersion Version { get; set; }
        public List<Entry> Entries { get; set; }
        public string? Name { get; set; }
        public int LanguageCount { get; set; }
        public string? Filepath { get; set; }
        public string? Nodepath { get; set; }

        public bool IsNew { get; set; }
        public bool IsError { get; set; }
        public bool CanSave { get; set; }
        public bool Loaded { get; set; }
        public bool NullFill { get; set; }

        public bool Changed => IsNew || Entries.Any(x => x.Changed);

        public string Group { get; set; }
        public string[] LanguageCodes { get; set; }

        private ushort V1Seed { get; set; }
        private bool V1Compressed { get; set; }

        public MsgWrapper()
        {
            Entries = new();
        }

        public MsgWrapper(string path, FileVersion version) : this()
        {
            Version = version;
            Filepath = path;
            Name = Path.GetFileNameWithoutExtension(path);
        }

        public MsgWrapper(MsgDataV1 msg, string name, string langcode) : this()
        {
            Name = name;
            LanguageCodes = new[] { langcode };
            Version = FileVersion.GenIV;
            Load(msg);
        }

        public MsgWrapper(MsgDataV2 msg, string name, FileVersion version, string[] langcodes) : this()
        {
            Name = name;
            LanguageCodes = langcodes;
            Version = version;
            Load(msg, null);
        }

        public MsgWrapper(MsgDataV2 msg, AHTB ahtb, string name, FileVersion version, string[] langcodes) : this()
        {
            Name = name;
            LanguageCodes = langcodes;
            Version = version;
            Load(msg, ahtb);
        }

        public MsgWrapper(AHTB ahtb, string name, FileVersion version) : this()
        {
            Name = name;
            Version = version;
            Load(ahtb);
        }

        public static MsgWrapper CreateFile(string path, FileVersion version)
        {
            var mw = new MsgWrapper(path, version)
            {
                //HasNameTable = nameTable,
                IsNew = true,
            };
            mw.Save(false);
            return mw;
        }

        public static MsgWrapper OpenFile(string path, FileVersion version)
        {
            var mw = new MsgWrapper(path, version);
            //mw.Load();
            return mw;
        }

        public bool Load()
        {
            if (IsNew) return true;
            if (IsError) return false;
            if (Loaded) return true;
            if (Filepath == null) return true;

            try
            {
                switch (Version)
                {
                    case FileVersion.GenIV:
                        {
                            var msg = new MsgDataV1(Filepath);
                            Load(msg);
                        }
                        break;
                    case FileVersion.GenV:
                        {
                            var msg = new MsgDataV2(Filepath);
                            Load(msg, null);
                        }
                        break;
                    case FileVersion.GenVI:
                        {
                            var msg = new MsgDataV2(Filepath);
                            Load(msg, null);
                        }
                        break;
                    case FileVersion.GenVIII:
                        {
                            var msg = new MsgDataV2(Filepath);
                            var tblPath = Path.ChangeExtension(Filepath, ".tbl");
                            var ahtb = File.Exists(tblPath) ? new AHTB(tblPath) : null;
                            Load(msg, ahtb);
                        }
                        break;
                    default:
                        throw new NotSupportedException();
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Invalid message data file:\n" + Filepath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsError = true;
                return false;
            }
        }

        public string[] GetNames()
        {
            Load();
            return Entries.Select(x => x.Name!).ToArray();
        }

        public Entry[] GetTextEntries()
        {
            Load();
            return  Entries.TakeWhile(x => x.HasText).ToArray();
        }

        public void Load(MsgDataV1 msg)
        {
            LanguageCount = 1;
            V1Seed = msg.Seed;
            V1Compressed = msg.Compressed;

            NullFill = true;

            for (var iEntry = 0; iEntry < msg.Data.Length; iEntry++)
            {
                var seq = Enumerable.Range(0, 1)
                    .Select(iTable => msg.Data[iEntry])
                    .Select(x => {
                        //var symbols = MsgFormatterGenIV.CodesToSymbols(x, out bool nullfill);
                        var sequence = new SymbolSequence(x, GetLanguageCodes(0));
                        //NullFill &= nullfill;
                        return sequence;
                    });
                var entry = new Entry(seq);
                Entries.Add(entry);
            }

            Loaded = true;
        }

        public void Load(MsgDataV2 msg, AHTB? ahtb)
        {
            var data = msg.Entries;

            LanguageCount = data.Length;
            NullFill = true;

            for (var iEntry = 0; iEntry < data[0].Length; iEntry++)
            {
                var seq = Enumerable.Range(0, data.Length)
                    .Select(iTable => data[iTable][iEntry])
                    .Select((x, iTable) => new SymbolSequence(x.Codes, GetLanguageCodes(iTable), x.UserParam) { Name = ahtb?.Entries[iEntry].Text });
                var entry = ahtb is not null
                    ? new Entry(ahtb.Entries[iEntry].Text, ahtb.Entries[iEntry].Hash, seq)
                    : new Entry(seq);
                Entries.Add(entry);
            }

            if (ahtb?.Entries.Length > data[0].Length)
            {
                for (var iEntry = data[0].Length; iEntry < ahtb.Entries.Length; iEntry++)
                {
                    var entry = new Entry(ahtb.Entries[iEntry].Text, ahtb.Entries[iEntry].Hash);
                    Entries.Add(entry);
                }
            }

            Loaded = true;
        }

        public void Load(AHTB ahtb)
        {
            LanguageCount = 0;
            NullFill = true;

            var entries = ahtb.Entries.Select(x => new Entry(x.Text, x.Hash));
            Entries.AddRange(entries);

            Loaded = true;
        }

        public void Save(bool backup)
        {
            Save(Filepath, backup);
        }

        public void Save(string path, bool backup)
        {
            var msgPath = path;
            if (backup && Path.GetExtension(path) != ".tmp")
            {
                var bakMsgPath = msgPath + ".bak";
                if (File.Exists(msgPath) && !File.Exists(bakMsgPath))
                {
                    File.Move(msgPath, bakMsgPath);
                }
            }

            if(Version is FileVersion.GenIV)
            {
                var tempMsgPath = Path.GetTempFileName();
                var data = Entries
                        //.Select(entry => MsgFormatterGenIV.SymbolsToCodes(entry[0].Symbols, entry[0].NullFill))
                        .Select(entry => entry[0].Codes)
                        .ToArray();
                var msg = new MsgDataV1(data, V1Seed)
                {
                    Compressed = V1Compressed,
                };
                msg.Save(tempMsgPath);
                File.Move(tempMsgPath, msgPath, true);
            }
            else if (Version is >= FileVersion.GenV)
            {
                var tempMsgPath = Path.GetTempFileName();
                var data = Enumerable.Range(0, LanguageCount)
                    .Select(iLang => Entries
                        .TakeWhile(x => x.HasText)
                        .Select(entry => new MsgDataV2.StringEntry(entry[iLang].Codes, entry[iLang].Grammatical!.Value)).ToArray()
                        )
                    .ToArray();
                var msg = new MsgDataV2(data);
                msg.Save(tempMsgPath);
                File.Move(tempMsgPath, msgPath, true);
            }

            if (Version is FileVersion.GenVIII)
            {
                var namePath = Path.ChangeExtension(path, ".tbl");
                if (backup)
                {
                    var bakNamePath = namePath + ".bak";
                    if (File.Exists(namePath) && !File.Exists(bakNamePath))
                    {
                        File.Move(namePath, bakNamePath);
                    }
                }

                var tempNamePath = Path.GetTempFileName();
                var names = Entries.Select(x => x.Name!).ToArray();
                var ahtb = new AHTB(names);
                ahtb.Save(tempNamePath);
                File.Move(tempNamePath, namePath, true);
            }

            Entries.ForEach(x => x.Changed = false);
            IsNew = false;
        }

        public string GetLanguageCodes(int index)
        {
            if(LanguageCodes == null || LanguageCodes.Length == 0)
            {
                return "";
            }
            else if (LanguageCodes.Length <= index)
            {
                return LanguageCodes[0];
            }
            else
            {
                return LanguageCodes[index];
            }
        }

        public bool TestSave()
        {
            if (Filepath == null)
            {
                throw new NotSupportedException();
            }

            {
                var msgPath = Filepath;
                var ms = new MemoryStream();
                var bw = new BinaryWriter(ms);
                Load();

                if (Version is FileVersion.GenIV)
                {
                    var data = Entries
                            .Select(entry => entry[0].Codes)
                            .ToArray();
                    var msg = new MsgDataV1(data, V1Seed)
                    {
                        Compressed = V1Compressed,
                    };
                    msg.Save(bw);
                }
                else// if (Version is FileVersion.GenV or FileVersion.GFMSG2WithKeys)
                {
                    var tempMsgPath = Path.GetTempFileName();
                    var data = Enumerable.Range(0, LanguageCount)
                        .Select(iLang => Entries.TakeWhile(x => x.HasText).Select(entry => new MsgDataV2.StringEntry(entry[iLang].Codes, entry[iLang].Grammatical!.Value)).ToArray())
                        .ToArray();
                    var msg = new MsgDataV2(data);
                    msg.Save(bw);
                }

                var bytes1 = File.ReadAllBytes(msgPath);
                var bytes2 = ms.ToArray();

                if (!bytes1.SequenceEqual(bytes2))
                {
                    return false;
                }
            }

            if (Version is FileVersion.GenVIII)
            {
                var namePath = Path.ChangeExtension(Filepath, ".tbl");
                var ms = new MemoryStream();
                var bw = new BinaryWriter(ms);
                var names = GetNames();
                var ahtb = new AHTB(names);
                ahtb.Save(bw);
                var bytes1 = File.ReadAllBytes(namePath);
                var bytes2 = ms.ToArray();

                if (!bytes1.SequenceEqual(bytes2))
                {
                    return false;
                }
            }

            return true;
        }

        public Entry this[string name]
        {
            get
            {
                var index = Entries.FindIndex(x => x.Name == name);
                if (index == -1)
                {
                    throw new IndexOutOfRangeException();
                }
                return this[index];
            }
        }

        public Entry this[ulong hash]
        {
            get
            {
                var index = Entries.FindIndex(x => x.Hash == hash);
                if (index == -1)
                {
                    throw new IndexOutOfRangeException();
                }
                return this[index];
            }
        }

        public Entry this[int index]
        {
            get => Entries[index];
        }

        public class Entry : IEnumerable<SymbolSequence>
        {
            public string? Name { get; set; }
            public ulong? Hash { get; set; } // for faster querying
            public List<SymbolSequence>? Sequences { get; set; }
            public bool HasText => Sequences != null;
            public bool Changed { get; set; }

            protected Entry()
            {
                Changed = false;
            }

            public Entry(string name, ulong hash) : this()
            {
                Name = name;
                Hash = hash;
            }

            public Entry(IEnumerable<SymbolSequence> sequences) : this()
            {
                Sequences = new(sequences);
            }

            public Entry(string name, ulong hash, IEnumerable<SymbolSequence> sequences) : this()
            {
                Name = name;
                Hash = hash;
                Sequences = new(sequences);
            }

            public SymbolSequence this[int index]
            {
                get => Sequences[index];
                set => Sequences[index] = value;
            }

            public IEnumerator<SymbolSequence> GetEnumerator()
            {
                return Sequences.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return Sequences.GetEnumerator();
            }
        }
    }
}
