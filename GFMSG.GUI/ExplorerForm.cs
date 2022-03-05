using GFMSG.Pokemon;
using System.Text;

namespace GFMSG.GUI
{
    public partial class ExplorerForm : Form
    {
        public MsgFormatter Formatter;
        public MsgWrapper CurrentWrapper;
        public string DirectoryPath { get; set; }

        public List<MsgWrapper> CachedWrappers;
        public MultilingualWrapper[] MultilingualWrappers;

        public ExplorerForm()
        {
            InitializeComponent();

            Formatter = new PokemonMsgFormatter();
            CachedWrappers = new();
            splitContainer1.Panel1Collapsed = true;
            cmbSearchType.SelectedItem = "Markup";
            cmbMultilingual.Visible = false;

            tsslLanguageLabel.Visible = false;
            tsslLanguage.Visible = false;
            tsddbLanguage.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public void LoadMessage(MultilingualCollection collection)
        {
            var wrappers = collection.ToWrappers();
            Formatter = collection.Formatter;
            OpenFolder(wrappers);

            cmbMultilingual.Items.Add("All languages");
            foreach (var langcode in collection.Wrappers.Keys)
            {
                cmbMultilingual.Items.Add(langcode);
            }
            cmbMultilingual.SelectedIndex = 0;
            cmbMultilingual.Visible = true;

            DisableOpen();
        }

        private void DisableOpen()
        {
            tsmiNew.Visible = false;
            tsmiOpen.Visible = false;
            tsmiOpenFolder.Visible = false;
            tsmiOpenMessage.Visible = false;
            tsmiSave.Visible = false;
            tsmiSaveAs.Visible = false;
            tsmiSaveAll.Visible = false;
        }

        private void OpenFile(string path)
        {
            CachedWrappers.Clear();

            treeView1.Nodes.Clear();
            splitContainer1.Panel1Collapsed = true;

            var wrapper = MsgWrapper.OpenFile(path);
            CachedWrappers.Add(wrapper);
            ShowWrapper(wrapper);
        }

        private void OpenFolder(string path)
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

                var wrapper = new MsgWrapper(filepath);
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

        private void OpenFolder(MultilingualWrapper[] wrappers)
        {
            MultilingualWrappers = wrappers;

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
                CachedWrappers.AddRange(wrapper.Wrappers.Values);
            }

            if (treeView1.Nodes.Count > 0)
            {
                treeView1.ExpandAll();
                treeView1.SelectedNode = treeView1.Nodes[0];
            }

            treeView1.EndUpdate();

            splitContainer1.Panel1Collapsed = false;
        }

        private void ShowWrapper(MsgWrapper msg)
        {
            if (msg.Load() == false) return;

            CurrentWrapper = msg;

            lstContent.BeginUpdate();
            lstContent.Clear();
            lstContent.Columns.Add("index", "Index");
            if (msg.HasNameTable)
            {
                lstContent.Columns.Add($"id", $"Id");
            }
            for (var iTable = 0; iTable < msg.LanguageNumber; iTable++)
            {
                lstContent.Columns.Add($"table_{iTable}", $"Table {iTable + 1}", -2);
            }

            var fo = new StringOptions()
            {
                Format = StringFormat.Plain,
                LanguageCode = msg.LanguageCode,
                RemoveLineBreaks = true,
            };

            for (var iEntry = 0; iEntry < msg.Entries.Count; iEntry++)
            {
                var row = lstContent.Items.Add($"{iEntry}");
                if (msg.HasNameTable)
                {
                    var subitem = row.SubItems.Add(msg.Entries[iEntry].Name ?? "");
                    subitem.Name = $"id";
                    subitem.Tag = new CellInfo()
                    {
                        Entry = msg[iEntry],
                        Index = 0,
                        Row = iEntry,
                        LanguageCode = msg.LanguageCode,
                    };
                }
                for (var iTable = 0; iTable < msg.LanguageNumber; iTable++)
                {
                    if (msg[iEntry].HasText)
                    {
                        var symbols = msg[iEntry][iTable];
                        var text = Formatter.Format(symbols, fo);
                        var subitem = row.SubItems.Add(text);
                        subitem.Name = $"table_{iTable}";
                        subitem.Tag = iTable;
                        subitem.Tag = new CellInfo()
                        {
                            Entry = msg[iEntry],
                            Index = iTable,
                            Row = iEntry,
                            LanguageCode = msg.LanguageCode,
                        };
                    }
                }
            }

            lstContent.Columns["index"].Width = -2;
            if (msg.HasNameTable)
            {
                lstContent.Columns["id"].Width = -1;
            }

            lstContent.EndUpdate();

            tsslTableCount.Text = string.Format("Tables: {0}", msg.LanguageNumber);
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
            if (firstWrapper.HasNameTable)
            {
                lstContent.Columns.Add($"id", $"Id");
            }
            foreach (var (langcode, wrapper) in multimsg.Wrappers)
            {
                for (var iTable = 0; iTable < wrapper.LanguageNumber; iTable++)
                {
                    var colName = wrapper.LanguageNumber == 1 ? $"{langcode}" : $"{langcode}[{iTable}]";
                    lstContent.Columns.Add($"table_{langcode}_{iTable}", colName);
                }
            }

            for (var iEntry = 0; iEntry < firstWrapper.Entries.Count; iEntry++)
            {
                var row = lstContent.Items.Add($"{iEntry}");

                if (firstWrapper.HasNameTable)
                {
                    var subitem = row.SubItems.Add(firstWrapper.Entries[iEntry].Name ?? "");
                    subitem.Name = $"id";
                    subitem.Tag = new CellInfo()
                    {
                        Entry = firstWrapper.Entries[iEntry],
                        Index = 0,
                        Row = iEntry,
                        LanguageCode = firstWrapper.LanguageCode,
                    };
                }

                foreach (var (langcode, wrapper) in multimsg.Wrappers)
                {
                    if (wrapper.Entries[iEntry].HasText)
                    {
                        var fo = new StringOptions(StringFormat.Plain, langcode)
                        {
                            RemoveLineBreaks = true,
                        };
                        for (var iTable = 0; iTable < wrapper.LanguageNumber; iTable++)
                        {
                            var symbols = wrapper.Entries[iEntry][iTable];
                            var text = Formatter.Format(symbols, fo);
                            var subitem = row.SubItems.Add(text);
                            subitem.Tag = new CellInfo()
                            {
                                Entry = wrapper.Entries[iEntry],
                                Row = iEntry,
                                Index = iTable,
                                LanguageCode = langcode,
                            };
                        }
                    }
                }
            }

            lstContent.Columns["index"].Width = -2;
            if (firstWrapper.HasNameTable)
            {
                lstContent.Columns["id"].Width = -1;
            }

            lstContent.EndUpdate();

            tsslTableCount.Text = string.Format("Tables: {0}", firstWrapper.LanguageNumber);
            tsslEntryCount.Text = string.Format("Entries: {0}", firstWrapper.Entries.Count);
        }

        private void UpdateRow(ListViewItem row, bool changed)
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
                            var fo = new StringOptions()
                            {
                                Format = StringFormat.Plain,
                                LanguageCode = cell.LanguageCode,
                                RemoveLineBreaks = true,
                            };
                            var text = Formatter.Format(cell.Sequence, fo);
                            subitem.Text = text;
                        }
                        break;
                }
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

        private void tsmiOpenFolder_Click(object sender, EventArgs e)
        {
            if (!CheckDiscard()) return;

            if (folderBrowserDialog1.ShowDialog(this) != DialogResult.OK) return;

            OpenFolder(folderBrowserDialog1.SelectedPath);
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
                    UpdateRow(hitTest.Item, true);
                }
            }
        }

        private void listView1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Search(Func<MsgWrapper.Entry, string?> action)
        {
            lstSearch.BeginUpdate();
            lstSearch.Items.Clear();
            string? searchLang = cmbMultilingual.SelectedIndex > 0 ? cmbMultilingual.Text : null;

            foreach (var wrapper in CachedWrappers)
            {
                if (!string.IsNullOrEmpty(searchLang) && wrapper.LanguageCode != searchLang) continue;
                wrapper.Load();
                for (var iEntry = 0; iEntry < wrapper.Entries.Count; iEntry++)
                {
                    string? result = action(wrapper[iEntry]);
                    if (result != null)
                    {
                        var row = lstSearch.Items.Add(result);
                        row.SubItems.Add(wrapper.Name);
                        row.Tag = (wrapper, iEntry);
                    }
                }
            }
            lstSearch.EndUpdate();
            tabControl1.SelectedTab = tpSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbSearchType.Text == "Name")
            {
                var keyword = txtSearch.Text;
                Search(entry => {
                    if (entry.Name != null && entry.Name.Contains(keyword))
                    {
                        return entry.Name;
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
                    }
                };
                Search(entry => {
                    if (entry.Sequences == null) return null;
                    foreach (var seq in entry)
                    {
                        var text = Formatter.Format(seq, fo);
                        if (keywords.All(x => text.Contains(x)))
                        {
                            return text;
                        }
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
                    UpdateRow(lstContent.Items[i], false);
                }
            }
        }

        private void ExplorerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckDiscard())
            {
                e.Cancel = true;
            }
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            if (!CheckDiscard()) return;

            if (openFileDialog1.ShowDialog(this) != DialogResult.OK) return;

            OpenFile(openFileDialog1.FileName);
        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            if (!CheckDiscard()) return;

            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK) return;
            var mw = MsgWrapper.CreateFile(saveFileDialog1.FileName, true);
            OpenFile(saveFileDialog1.FileName);
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

            Search(entry => {
                if (entry.Sequences == null) return null;
                foreach (var seq in entry)
                {
                    if (seq.Symbols.Any(x =>
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

            Search(entry => {
                if (entry.Sequences == null) return null;
                foreach (var seq in entry)
                {
                    foreach(var x in seq.Symbols)
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
                }
                return null;
            });

            var map = Formatter.Tags.IndexNames;
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

            using var frm = new ChooseFormatterForm();
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            var formatter = (PokemonMsgFormatter)Activator.CreateInstance(frm.GetSelectedType())!;

            var path = folderBrowserDialog1.SelectedPath;
            var mc = new MultilingualCollection();
            mc.Formatter = formatter;
            var langmap = formatter.LanguageMap;
            foreach (var (langcode, foldername) in langmap)
            {
                var langpath = Path.Combine(path, foldername) + "\\";
                var files = Directory.GetFiles(langpath, "*.dat", SearchOption.AllDirectories);
                if (files.Length == 0) continue;
                var wrappers = files.Select(x => new MsgWrapper(x)
                {
                    LanguageCode = langcode,
                    Name = x.Replace(langpath, "").Replace(".dat", "")
                }).ToArray();
                mc.Wrappers.Add(langcode, wrappers);
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
            using var frm = new ExportForm(CurrentWrapper, Formatter);
            frm.ShowDialog(this);
        }

        private void tsmiBatchExport_Click(object sender, EventArgs e)
        {
            if (MultilingualWrappers != null)
            {
                using var frm = new ExportForm(MultilingualWrappers, Formatter);
                frm.ShowDialog(this);
            }
            else
            {
                using var frm = new ExportForm(CachedWrappers.ToArray(), Formatter);
                frm.ShowDialog(this);
            }
        }
    }
}