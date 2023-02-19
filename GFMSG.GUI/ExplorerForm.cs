using GFMSG.Pokemon;
using System.Text;

namespace GFMSG.GUI
{
    public partial class ExplorerForm : Form
    {
        public MsgFormatter Formatter;
        public MsgWrapper? CurrentWrapper;
        public string DirectoryPath { get; set; }
        public StringOptions CurrentStringOptions { get; set; }
        public FileVersion Version { get; set; }

        public List<MsgWrapper> CachedWrappers;
        public MultilingualWrapper[] MultilingualWrappers;

        public ExplorerForm()
        {
            InitializeComponent();

            CachedWrappers = new();
            splitContainer1.Panel1Collapsed = true;
            cmbSearchType.SelectedItem = "Markup";
            cmbMultilingual.Visible = false;

            tsslLanguageLabel.Visible = false;
            tsslLanguage.Visible = false;
            tsddbLanguage.Visible = false;

            foreach(var format in Enum.GetValues<StringFormat>())
            {
                var tsmi = tsddbFormat.DropDownItems.Add(format.ToString());
                tsmi.Tag = format;
                tsmi.Click += (_, _) =>
                {
                    ChangeOptions(CurrentStringOptions with { Format = format });
                };
                tsddbFormat.DropDownItems.Insert(tsddbFormat.DropDownItems.Count - 3, tsmi);
                if (format == StringFormat.Plain) tsmi.PerformClick();
            }
            tsddbFormat.DropDownOpening += (_, _) =>
            {
                foreach(ToolStripItem item in tsddbFormat.DropDownItems)
                {
                    if(item is ToolStripMenuItem tsmi && tsmi.Tag != null)
                    {
                        tsmi.Checked = (StringFormat)tsmi.Tag == CurrentStringOptions.Format;
                    }
                }
            };

            ChangeOptions(new StringOptions()
            {
                Format = StringFormat.Plain,
                RemoveLineBreaks = true,
            });
        }

        public ExplorerForm(FileVersion version, bool allowOpen) : this()
        {
            Version = version;
            tsslVersion.Text = version.ToString();

            tsmiNew.Visible = allowOpen;
            tsmiOpen.Visible = allowOpen;
            tsmiOpenFolder.Visible = allowOpen;
            tsmiOpenMessage.Visible = allowOpen;
            tsmiSave.Visible = allowOpen;
            tsmiSaveAs.Visible = allowOpen;
            tsmiSaveAll.Visible = allowOpen;

        }

        public ExplorerForm(MultilingualCollection collection) : this(FileVersion.GenVIII, true)
        {
            LoadMessage(collection);
        }

        public void LoadMessage(MsgWrapper[] wrappers, MsgFormatter formatter)
        {
            Formatter = formatter;
            OpenFolder(wrappers);

            cmbMultilingual.Visible = false;
        }

        public void LoadMessage(MultilingualCollection collection)
        {
            var wrappers = collection.TransposeWrappers();
            Formatter = collection.Formatter;
            OpenFolder(wrappers);

            cmbMultilingual.Items.Add("All languages");
            foreach (var langcode in collection.Wrappers.Keys)
            {
                cmbMultilingual.Items.Add(langcode);
            }
            cmbMultilingual.SelectedIndex = 0;
            cmbMultilingual.Visible = true;
        }

        private void OpenFile(string path, string[] langcodes)
        {
            CachedWrappers.Clear();

            treeView1.Nodes.Clear();
            splitContainer1.Panel1Collapsed = true;

            var wrapper = MsgWrapper.OpenFile(path, Version);
            wrapper.LanguageCodes = langcodes;
            CachedWrappers.Add(wrapper);
            ShowWrapper(wrapper);
        }

        private void OpenFolder(string path, string[] langcodes)
        {
            DirectoryPath = path;
            CachedWrappers.Clear();

            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            var files = Directory.GetFiles(path, "*.dat", SearchOption.AllDirectories);
            foreach (var filepath in files)
            {
                var relpath = filepath.Replace(DirectoryPath, "").TrimStart('\\');
                var parent = treeView1.Nodes;
                var folderParts = relpath.Split("\\");
                for (var i = 0; i < folderParts.Length - 1; i++)
                {
                    if (!parent.ContainsKey(folderParts[i]))
                    {
                        parent.Add(folderParts[i], folderParts[i]);
                    }
                    parent = parent[folderParts[i]].Nodes;
                }

                var wrapper = new MsgWrapper(filepath, Version)
                {
                    LanguageCodes = langcodes,
                };
                CachedWrappers.Add(wrapper);

                var node = parent.Add(folderParts[^1], Path.GetFileNameWithoutExtension(folderParts[^1]));
                node.Tag = wrapper;
            }

            if (treeView1.Nodes.Count > 0)
            {
                treeView1.ExpandAll();
                treeView1.SelectedNode = treeView1.Nodes[0];
            }

            treeView1.EndUpdate();

            splitContainer1.Panel1Collapsed = false;
        }

        private void OpenFolder(MultilingualWrapper[] mlwrappers)
        {
            MultilingualWrappers = mlwrappers;

            CachedWrappers.Clear();

            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            foreach (var mlwrapper in mlwrappers)
            {
                var parent = treeView1.Nodes;
                var name = mlwrapper.Name;
                if (name.Contains('\\'))
                {
                    var folderParts = name.Split("\\");
                    for (var i = 0; i < folderParts.Length - 1; i++)
                    {
                        if (!parent.ContainsKey(folderParts[i]))
                        {
                            parent.Add(folderParts[i], folderParts[i]);
                        }
                        parent = parent[folderParts[i]].Nodes;
                    }
                    name = folderParts[^1];
                }
                var node = parent.Add(name, name);
                node.Tag = mlwrapper;
                CachedWrappers.AddRange(mlwrapper.Wrappers.Values);
            }

            if (treeView1.Nodes.Count > 0)
            {
                treeView1.ExpandAll();
                treeView1.SelectedNode = treeView1.Nodes[0];
            }

            treeView1.EndUpdate();

            splitContainer1.Panel1Collapsed = false;
        }

        private void OpenFolder(MsgWrapper[] wrappers)
        {
            CachedWrappers.Clear();

            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            foreach (var wrapper in wrappers)
            {
                var parent = treeView1.Nodes;
                var name = wrapper.Name;
                if (name.Contains('\\'))
                {
                    var folderParts = name.Split("\\");
                    for (var i = 0; i < folderParts.Length - 1; i++)
                    {
                        if (!parent.ContainsKey(folderParts[i]))
                        {
                            parent.Add(folderParts[i], folderParts[i]);
                        }
                        parent = parent[folderParts[i]].Nodes;
                    }
                    name = folderParts[^1];
                }
                var node = parent.Add(name, name);
                node.Tag = wrapper;
                CachedWrappers.Add(wrapper);
            }

            if (treeView1.Nodes.Count > 0)
            {
                treeView1.ExpandAll();
                treeView1.SelectedNode = treeView1.Nodes[0];
            }

            treeView1.EndUpdate();

            splitContainer1.Panel1Collapsed = false;
        }

        private void ChangeOptions(StringOptions options)
        {
            CurrentStringOptions = options;
            tsddbFormat.Text = string.Format("Format: {0}", options.Format);
            RefreshTable();
        }

        private void ShowWrapper(MsgWrapper msg)
        {
            if (msg.Load() == false) return;

            CurrentWrapper = msg;

            lstContent.BeginUpdate();
            lstContent.Clear();
            lstContent.Columns.Add("index", "Index");
            if (msg.Version == FileVersion.GenVIII)
            {
                lstContent.Columns.Add($"id", $"Id");
            }
            for (var iLang = 0; iLang < msg.LanguageCount; iLang++)
            {
                var langcode = msg.GetLanguageCodes(iLang);
                lstContent.Columns.Add($"lang_{langcode}", langcode, -2);
            }


            for (var iEntry = 0; iEntry < msg.Entries.Count; iEntry++)
            {
                var row = lstContent.Items.Add($"{iEntry}");
                if (msg.Version == FileVersion.GenVIII)
                {
                    var subitem = row.SubItems.Add(msg.Entries[iEntry].Name ?? "");
                    subitem.Name = $"id";
                    subitem.Tag = new CellInfo()
                    {
                        Entry = msg[iEntry],
                        Index = 0,
                        Row = iEntry,
                        Language = msg.GetLanguageCodes(0),
                    };
                }
                for (var iLang = 0; iLang < msg.LanguageCount; iLang++)
                {
                    var langcode = msg.GetLanguageCodes(iLang);
                    var options = CurrentStringOptions with
                    {
                        LanguageCode = langcode,
                    };

                    if (msg[iEntry].HasText)
                    {
                        var symbols = msg[iEntry][iLang];
                        var text = Formatter.Format(symbols, options);
                        var subitem = row.SubItems.Add(text);
                        subitem.Name = $"table_{iLang}";
                        subitem.Tag = iLang;
                        subitem.Tag = new CellInfo()
                        {
                            Entry = msg[iEntry],
                            Index = iLang,
                            Row = iEntry,
                            Language = langcode,
                        };
                    }
                }
            }

            lstContent.Columns["index"].Width = -2;
            if (msg.Version == FileVersion.GenVIII)
            {
                lstContent.Columns["id"].Width = -1;
            }

            lstContent.EndUpdate();

            tsslTableCount.Text = string.Format("Tables: {0}", msg.LanguageCount);
            tsslEntryCount.Text = string.Format("Entries: {0}", msg.Entries.Count);
        }

        private void ShowWrapper(MultilingualWrapper multimsg)
        {
            if (multimsg.Wrappers.Values.Any(x => x.Load() == false)) return;

            CurrentWrapper = null;
            var firstWrapper = multimsg.Wrappers.First().Value;

            lstContent.BeginUpdate();
            lstContent.Clear();
            lstContent.Columns.Add("index", "Index");
            if (firstWrapper.Version == FileVersion.GenVIII)
            {
                lstContent.Columns.Add($"id", $"Id");
            }
            foreach (var (key, wrapper) in multimsg.Wrappers)
            {
                for (var iLang = 0; iLang < wrapper.LanguageCount; iLang++)
                {
                    var langcode = wrapper.GetLanguageCodes(iLang);
                    lstContent.Columns.Add($"lang_{langcode}", langcode);
                }
            }

            for (var iEntry = 0; iEntry < firstWrapper.Entries.Count; iEntry++)
            {
                var row = lstContent.Items.Add($"{iEntry}");

                if (firstWrapper.Version == FileVersion.GenVIII)
                {
                    var subitem = row.SubItems.Add(firstWrapper.Entries[iEntry].Name ?? "");
                    subitem.Name = $"id";
                    subitem.Tag = new CellInfo()
                    {
                        Entry = firstWrapper.Entries[iEntry],
                        Index = 0,
                        Row = iEntry,
                        Language = firstWrapper.GetLanguageCodes(0),
                    };
                }

                foreach (var (groupname, wrapper) in multimsg.Wrappers)
                {
                    try
                    {
                        if (wrapper.Entries[iEntry].HasText)
                        {
                            for (var iTable = 0; iTable < wrapper.LanguageCount; iTable++)
                            {
                                var symbols = wrapper.Entries[iEntry][iTable];
                                var langcode = wrapper.GetLanguageCodes(iTable);
                                var options = CurrentStringOptions with
                                {
                                    LanguageCode = langcode,
                                };
                                var text = Formatter.Format(symbols, options);
                                var subitem = row.SubItems.Add(text);
                                subitem.Tag = new CellInfo()
                                {
                                    Entry = wrapper.Entries[iEntry],
                                    Row = iEntry,
                                    Index = iTable,
                                    Language = langcode,
                                };
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show($"An error occurred on file \"{wrapper.Name}\".");
                    }
                }
            }

            lstContent.Columns["index"].Width = -2;
            if (firstWrapper.Version == FileVersion.GenVIII)
            {
                lstContent.Columns["id"].Width = -1;
            }

            lstContent.EndUpdate();

            tsslTableCount.Text = string.Format("Tables: {0}", firstWrapper.LanguageCount);
            tsslEntryCount.Text = string.Format("Entries: {0}", firstWrapper.Entries.Count);
        }

        private void RefreshTable()
        {
            lstContent.BeginUpdate();
            foreach (ListViewItem row in lstContent.Items)
            {
                RefreshRow(row);
            }
            lstContent.EndUpdate();
        }

        private void RefreshRow(ListViewItem row)
        {
            foreach (ListViewItem.ListViewSubItem subitem in row.SubItems)
            {
                switch (subitem.Name)
                {
                    case "index":
                        break;
                    case "id":
                        subitem.Text = ((CellInfo)subitem.Tag).Entry.Name;
                        break;
                    default:
                        if (subitem.Tag is CellInfo cell)
                        {
                            var options = CurrentStringOptions with
                            {
                                LanguageCode = cell.Language,
                            };
                            var text = Formatter.Format(cell.Sequence, options);
                            subitem.Text = text;
                        }
                        break;
                }
            }
        }

        private void RefreshRow(ListViewItem row, bool changed)
        {
            if (changed)
            {
                row.ForeColor = Color.Green;
                row.Text = string.Format("{0} *", row.Index);
            }
            else
            {
                row.ForeColor = SystemColors.WindowText;
                row.Text = string.Format("{0}", row.Index);
            }

            RefreshRow(row);
        }

        private void Search(Func<SymbolSequence, string?> action)
        {
            lstSearch.BeginUpdate();
            lstSearch.Items.Clear();
            string? searchGroup = cmbMultilingual.SelectedIndex > 0 ? cmbMultilingual.Text : null;

            foreach (var wrapper in CachedWrappers)
            {
                if (searchGroup != null && searchGroup != wrapper.Group) continue;
                wrapper.Load();
                for (var iEntry = 0; iEntry < wrapper.Entries.Count; iEntry++)
                {
                    if (!wrapper.Entries[iEntry].HasText) continue;
                    foreach(var seq in wrapper.Entries[iEntry])
                    {
                        string? result = action(seq);
                        if (result != null)
                        {
                            var row = lstSearch.Items.Add(result);
                            row.SubItems.Add(wrapper.Name);
                            row.Tag = (wrapper, iEntry);
                        }
                    }
                }
            }
            lstSearch.EndUpdate();
            tabControl1.SelectedTab = tpSearch;
        }

        private bool CheckDiscard()
        {
            var changedWrappers = CachedWrappers.Where(mw => mw.Changed).ToArray();
            if (changedWrappers.Length > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Do you want to discard your changes to the following files?");
                sb.AppendLine();
                foreach (var mw in changedWrappers)
                {
                    sb.AppendLine("  - " + mw.Name);
                }
                sb.AppendLine();

                var result = MessageBox.Show(this, sb.ToString(), "File changed", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        return true;
                    case DialogResult.No:
                        return true;
                    case DialogResult.Cancel:
                        return false;
                }
            }
            return true;
        }

        private void Save(MsgWrapper wrapper)
        {
            var changedIndex = wrapper == CurrentWrapper
                ? Enumerable.Range(0, wrapper.Entries.Count).Where(i => wrapper.Entries[i].Changed).ToArray()
                : null;

            wrapper.Save(true);

            if (changedIndex != null)
            {
                foreach (var i in changedIndex)
                {
                    RefreshRow(lstContent.Items[i], false);
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ExplorerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckDiscard())
            {
                e.Cancel = true;
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Tag)
            {
                case MsgWrapper mw:
                    ShowWrapper(mw);
                    break;
                case MultilingualWrapper mlw when cmbMultilingual.SelectedIndex == 0:
                    ShowWrapper(mlw);
                    break;
                case MultilingualWrapper mlw when cmbMultilingual.SelectedIndex > 0 && mlw.Wrappers.ContainsKey(cmbMultilingual.Text):
                    ShowWrapper(mlw.Wrappers[cmbMultilingual.Text]);
                    break;
                default:
                    lstContent.Clear();
                    break;
            }
        }

        private (MsgFormatter?, string?) ChooseFormatter(bool haslang)
        {
            using var frm = new ChooseFormatterForm(Version, haslang);
            if (frm.ShowDialog(this) != DialogResult.OK) return (null, null);

            MsgFormatter formatter = Activator.CreateInstance(frm.GetSelectedType())! switch
            {
                DpMsgFormatter v1 => v1,
                PokemonMsgFormatterV2 v2 => v2,
                _ => throw new NotSupportedException(),
            };

            string? lang = null;
            if (haslang)
            {
                lang = frm.GetSelectedLanguage();
            }

            return (formatter, lang);
        }

        private void tsmiOpenFolder_Click(object sender, EventArgs e)
        {
            if (!CheckDiscard()) return;

            if (folderBrowserDialog1.ShowDialog(this) != DialogResult.OK) return;

            var (formatter, lang) = ChooseFormatter(true);
            if (formatter == null) return;
            Formatter = formatter;

            OpenFolder(folderBrowserDialog1.SelectedPath, new[] { lang! });
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var mousePos = lstContent.PointToClient(MousePosition);
            var hitTest = lstContent.HitTest(mousePos);

            if (hitTest.SubItem?.Tag is CellInfo cell)
            {
                using var frm = new MsgForm(cell, Formatter);
                var result = frm.ShowDialog(this);

                if (result == DialogResult.Yes) // entry changed
                {
                    RefreshRow(hitTest.Item, true);
                }
            }
        }

        private void listView1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbSearchType.Text == "Name")
            {
                var keyword = txtSearch.Text;
                Search(seq => {
                    if (seq.Name != null && seq.Name.Contains(keyword))
                    {
                        return seq.Name;
                    }
                    return null;
                });
            }
            else
            {
                var keywords = txtSearch.Text.Split(' ');
                var fo = new StringOptions()
                {
                    Format = cmbSearchType.Text switch
                    {
                        "Raw" => StringFormat.Raw,
                        "Markup" => StringFormat.Markup,
                        "Plain" => StringFormat.Plain,
                        "Html" => StringFormat.Html,
                        _ => throw new NotSupportedException(),
                    },
                };
                Search(seq => {
                    var text = Formatter.Format(seq, fo with { LanguageCode = seq.Language });
                    if (keywords.All(x => text.Contains(x)))
                    {
                        return text;
                    }
                    return null;
                });
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void lstSearch_Click(object sender, EventArgs e)
        {
            var mousePos = lstSearch.PointToClient(MousePosition);
            var hitTest = lstSearch.HitTest(mousePos);
            if (hitTest.Item != null)
            {
                var (mw, index) = ((MsgWrapper, int))hitTest.Item.Tag;
                if (CurrentWrapper != mw)
                {
                    ShowWrapper(mw);
                }
                else
                {
                    lstContent.SelectedItems
                        .Cast<ListViewItem>()
                        .ToList()
                        .ForEach(x => x.Selected = false);
                }
                lstContent.Items[index].Selected = true;
                lstContent.EnsureVisible(index);
            }
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            if (!CheckDiscard()) return;

            if (openFileDialog1.ShowDialog(this) != DialogResult.OK) return;

            var (formatter, lang) = ChooseFormatter(true);
            if (formatter == null) return;
            Formatter = formatter;

            OpenFile(openFileDialog1.FileName, new[] { lang! });
        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            if (!CheckDiscard()) return;

            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK) return;

            var (formatter, lang) = ChooseFormatter(true);
            if (formatter == null) return;
            Formatter = formatter;

            MsgWrapper.CreateFile(saveFileDialog1.FileName, Version);

            OpenFile(saveFileDialog1.FileName, new[] { lang! });
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            if (CurrentWrapper == null) return;

            Save(CurrentWrapper);
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            if (CurrentWrapper == null) return;

            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK) return;

            CurrentWrapper.Save(saveFileDialog1.FileName, true);
        }

        private void testSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(CurrentWrapper != null)
            {
                var result = CurrentWrapper.TestSave();
                if (result)
                {
                    MessageBox.Show("Sucessed");
                }
                else
                {
                    MessageBox.Show("Failed");
                }
            }
        }

        private void lstSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tsmiAnalyzeCharSymbols_Click(object sender, EventArgs e)
        {
            var fo = new StringOptions()
            {
                Format = StringFormat.Markup
            };

            var counters = new Dictionary<ushort, int>();
            var map = Formatter.Chars.PlainMap;

            Search(seq => {
                fo.LanguageCode = seq.Language;
                var ss = Formatter.GetSymbols(seq);
                if (ss.Any(x =>
                {
                    if (x is CharSymbol y && y.IsPrivate)
                    {
                        if (counters.ContainsKey(y.Code))
                        {
                            counters[y.Code]++;
                        }
                        else
                        {
                            counters.Add(y.Code, 1);
                        }
                        return !map.ContainsKey(y.Code);
                    }
                    else
                    {
                        return false;
                    }
                }))
                {
                    return Formatter.Format(seq, fo);
                }
                return null;
            });

            var sb = new StringBuilder();
            foreach (var (code, count) in counters.OrderBy(x=>x.Key))
            {
                sb.AppendLine($"0x{code:X4}({(map.ContainsKey(code) ? map[code] : "undefined")}): {count}");
            }
            TextDialog.Show(sb.ToString()); 
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                btnSearch.PerformClick();
                e.Handled = true;
            }
        }

        private void tsmiAnalyzeTagSymbols_Click(object sender, EventArgs e)
        {
            var fo = new StringOptions()
            {
                Format = StringFormat.Markup
            };

            var counters = new Dictionary<ushort, int>();

            Search(seq => {
                fo.LanguageCode = seq.Language;
                var ss = Formatter.GetSymbols(seq);
                foreach (var x in ss)
                {
                    if (x is TagSymbol y)
                    {
                        if (counters.ContainsKey(y.Code))
                        {
                            counters[y.Code]++;
                        }
                        else
                        {
                            counters.Add(y.Code, 1);
                        }
                    }
                }
                return null;
            });

            var map = Formatter.Tags.Converters;
            var sb = new StringBuilder();
            foreach (var (code, count) in counters.OrderBy(x => x.Key))
            {
                var i = map.FindIndex(x => (x.Group == code >> 8) && (x.Index == (code & 0xFF)));
                sb.AppendLine($"0x{code:X4}({(i > -1 ? map[i].Name : "undefined")}): {count}");
            }
            TextDialog.Show(sb.ToString());
        }

        private void tsmiOpenMessage_Click(object sender, EventArgs e)
        {
            if (!CheckDiscard()) return;

            if (folderBrowserDialog1.ShowDialog(this) != DialogResult.OK) return;

            var (formatter, _) = ChooseFormatter(false);
            if (formatter == null) return;

            var path = folderBrowserDialog1.SelectedPath;
            var mc = new MultilingualCollection();
            Dictionary<string, string[]> langmap;
            if (formatter is DpMsgFormatter v1)
            {
                mc.Formatter = v1;
                langmap = new Dictionary<string, string[]>()
                {
                    ["jpn"] = new[] { "ja-Hrkt" },
                    ["usa"] = new[] { "en-US" },
                    ["fra"] = new[] { "fr" },
                    ["ita"] = new[] { "it" },
                    ["ger"] = new[] { "de" },
                    ["spa"] = new[] { "es" },
                    ["kor"] = new[] { "ko" },
                };
            }
            else if (formatter is PokemonMsgFormatterV2 v2)
            {
                mc.Formatter = v2;
                langmap = new Dictionary<string, string[]>()
                {
                    ["JPN"] = new[] { "ja-Hrkt" },
                    ["JPN_KANJI"] = new[] { "ja-Jpan" },
                    ["English"] = new[] { "en-US" },
                    ["French"] = new[] { "fr" },
                    ["Italian"] = new[] { "it" },
                    ["German"] = new[] { "de" },
                    ["Spanish"] = new[] { "es" },
                    ["Korean"] = new[] { "ko" },
                    ["Simp_Chinese"] = new[] { "zh-Hans" },
                    ["Trad_Chinese"] = new[] { "zh-Hant" },
                };
            }
            else
            {
                throw new NotSupportedException();
            }
            foreach (var (foldername, langcodes) in langmap)
            {
                foreach(var langcode in langcodes)
                {
                    var langpath = Path.Combine(path, foldername);
                    if (!Directory.Exists(langpath)) continue;
                    var files = Directory.GetFiles(langpath, "*.dat", SearchOption.AllDirectories);
                    if (files.Length == 0) continue;
                    var wrappers = files.Select(x => new MsgWrapper(x, Version)
                    {
                        LanguageCodes = new[] { langcode },
                        Name = x.Replace(langpath + "\\", "").Replace(".dat", "")
                    }).ToArray();
                    mc.Wrappers.Add(foldername, wrappers);
                }
            }

            if (mc.Wrappers.Count == 0) return;
            LoadMessage(mc);
        }

        private void tsmiSaveAll_Click(object sender, EventArgs e)
        {
            var changedWrappers = CachedWrappers.Where(mw => mw.Changed).ToArray();
            foreach (var mw in changedWrappers)
            {
                Save(mw);
            }
        }

        private void testSaveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var badfiles = CachedWrappers.Where(mw => mw.TestSave() == false).Select(mw => mw.Filepath).ToArray();

            if (badfiles.Length > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("The following files can not be saved correctly:\n\n");
                sb.AppendJoin('\n', badfiles);
                MessageBox.Show(this, sb.ToString(), "Save test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(this, "All files can be saved correctly.", "Save test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsmiExport_Click(object sender, EventArgs e)
        {
            using var frm = new ExportForm(CurrentWrapper, Formatter, CurrentStringOptions);
            frm.ShowDialog(this);
        }

        private void tsmiBatchExport_Click(object sender, EventArgs e)
        {
            if (MultilingualWrappers != null)
            {
                using var frm = new ExportForm(MultilingualWrappers, Formatter, CurrentStringOptions);
                frm.ShowDialog(this);
            }
            else
            {
                using var frm = new ExportForm(CachedWrappers.ToArray(), Formatter, CurrentStringOptions);
                frm.ShowDialog(this);
            }
        }

        private void tsmiStringOptions_Click(object sender, EventArgs e)
        {
            using var frm = new StringOptionsForm(CurrentStringOptions);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            ChangeOptions(frm.GetValue());
        }

        private void analyzeLettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var counters = new Dictionary<ushort, Dictionary<string, int>>();

            Search(seq => {
                var ss = Formatter.GetSymbols(seq);
                foreach(var s in ss)
                {
                    if (s is LetterSymbol ls)
                    {
                        if (!counters.ContainsKey(ls.Code))
                        {
                            counters.Add(ls.Code, new());
                        }
                        if (!counters[ls.Code].ContainsKey(seq.Language))
                        {
                            counters[ls.Code].Add(seq.Language, 1);
                        }
                        else
                        {
                            counters[ls.Code][seq.Language]++;
                        }
                    }
                }
                return null;
            });

            var sb = new StringBuilder();
            var keys = counters.SelectMany(x => x.Value.Keys).Distinct().ToArray();
            sb.AppendLine($"code\t{string.Join('\t', keys)}");
            foreach (var kv in counters.OrderBy(x => x.Key))
            {
                var cnt = string.Join('\t', keys.Select(x => kv.Value.ContainsKey(x) ? kv.Value[x] : 0));
                sb.AppendLine($"0x{kv.Key:X4}\t{cnt}");
            }
            TextDialog.Show(sb.ToString());
        }
    }
}