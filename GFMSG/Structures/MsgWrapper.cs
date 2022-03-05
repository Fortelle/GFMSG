using System.Collections;

namespace GFMSG
{
    public class MsgWrapper
    {
        public List<Entry> Entries { get; set; }
        public string? Name { get; set; }
        public int LanguageNumber { get; set; }
        public string? Filepath { get; set; }
        public string? Nodepath { get; set; }

        public bool IsNew { get; set; }
        public bool IsError { get; set; }
        public bool CanSave { get; set; }
        public bool Loaded { get; set; }
        public bool HasNameTable { get; set; }
        public bool NullFill { get; set; }

        public bool Changed => IsNew || Entries.Any(x => x.Changed);

        public string LanguageCode { get; set; }

        public MsgWrapper()
        {
            Entries = new();
        }

        public MsgWrapper(string path) : this()
        {
            Filepath = path;
            Name = Path.GetFileNameWithoutExtension(path);
        }

        public MsgWrapper(MsgData msg, string name) : this()
        {
            Load(msg, null);
            Name = name;
        }

        public static MsgWrapper CreateFile(string path, bool save)
        {
            var mw = new MsgWrapper(path)
            {
                HasNameTable = true,
                IsNew = true,
            };
            if (save)
            {
                mw.Save(false);
            }
            return mw;
        }

        public static MsgWrapper OpenFile(string path)
        {
            var mw = new MsgWrapper(path);
            mw.Load();
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
                var msg = new MsgData(Filepath);
                var tblPath = Path.ChangeExtension(Filepath, ".tbl");
                var ahtb = File.Exists(tblPath) ? new AHTB(tblPath) : null;
                Load(msg, ahtb);
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

        public (ushort[], GrammaticalAttribute)[][] GetData()
        {
            Load();
            return Enumerable.Range(0, LanguageNumber)
                .Select(iLang => Entries.TakeWhile(x => x.HasText).Select(entry => (entry[iLang].ToCodes(), entry[iLang].Grammatical)).ToArray())
                .ToArray();
        }

        public Entry[] GetTextEntries()
        {
            Load();
            return  Entries.TakeWhile(x => x.HasText).ToArray();
        }

        public void Load(MsgData msg, AHTB? ahtb)
        {
            HasNameTable = ahtb is not null;

            var data = msg.GetData();

            LanguageNumber = data.Length;

            for (var iEntry = 0; iEntry < data[0].Length; iEntry++)
            {
                var seq = Enumerable.Range(0, data.Length)
                    .Select(iTable => data[iTable][iEntry])
                    .Select(x => new SymbolSequence(x.Item1, x.Item2) { Name = ahtb?.Entries[iEntry].Text })
                    ;
                var entry = HasNameTable
                    ? new Entry(ahtb.Entries[iEntry].Text, seq)
                    : new Entry(seq);
                Entries.Add(entry);
            }

            if (HasNameTable && ahtb.Entries.Length > data[0].Length)
            {
                for (var iEntry = data[0].Length; iEntry < ahtb.Entries.Length; iEntry++)
                {
                    var entry = new Entry(ahtb.Entries[iEntry].Text);
                    Entries.Add(entry);
                }
            }

            NullFill = Entries.Any(x => x.HasText)
                && Entries.All(x => !x.HasText || x.Sequences.All(y => (y.Symbols.Length % 2 == 0)
                && y.Symbols[0..(y.Symbols.Length / 2)].All(z => z is not CharSymbol cs || cs.Code != 0)
                && y.Symbols[(y.Symbols.Length / 2)..].All(z => z is CharSymbol cs && cs.Code == 0)
                ));

            Loaded = true;
        }

        public void Save(bool backup)
        {
            Save(Filepath, backup);
        }

        public void Save(string path, bool backup)
        {
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

                var tempMsgPath = Path.GetTempFileName();
                var data = Enumerable.Range(0, LanguageNumber)
                    .Select(iLang => Entries.TakeWhile(x => x.HasText).Select(entry => (entry[iLang].ToCodes(), entry[iLang].Grammatical)).ToArray())
                    .ToArray();
                var msg = new MsgData(data);
                msg.Save(tempMsgPath);
                File.Move(tempMsgPath, msgPath, true);
            }

            if (HasNameTable)
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
                var data = GetData();
                var msg = new MsgData(data);
                msg.Save(bw);
                var bytes1 = File.ReadAllBytes(msgPath);
                var bytes2 = ms.ToArray();

                if (!bytes1.SequenceEqual(bytes2))
                {
                    return false;
                }
            }

            if (HasNameTable)
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

        public Entry this[int index]
        {
            get => Entries[index];
        }

        public class Entry : IEnumerable<SymbolSequence>
        {
            public string? Name { get; set; }
            public List<SymbolSequence>? Sequences { get; set; }
            public bool HasText => Sequences != null;
            public bool Changed { get; set; }

            protected Entry()
            {
                Changed = false;
            }

            public Entry(string name) : this()
            {
                Name = name;
            }

            public Entry(IEnumerable<SymbolSequence> sequences) : this()
            {
                Sequences = new(sequences);
            }

            public Entry(string name, IEnumerable<SymbolSequence> sequences) : this()
            {
                Name = name;
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
