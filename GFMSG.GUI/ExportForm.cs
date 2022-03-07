namespace GFMSG.GUI
{
    public partial class ExportForm : Form
    {
        public object Wrappers { get; set; }
        public MsgFormatter Formatter { get; set; }

        public ExportForm()
        {
            InitializeComponent();

            cmbFileFormat.SelectedIndex = 0;
            cmbFormat.SelectedIndex = 2;
        }

        public ExportForm(MsgWrapper wrapper, MsgFormatter formatter) : this()
        {
            Wrappers = wrapper;
            Formatter = formatter;
            chkMerge.Visible = false;
        }

        public ExportForm(MsgWrapper[] wrappers, MsgFormatter formatter) : this()
        {
            Wrappers = wrappers;
            Formatter = formatter;
            chkMerge.Visible = true;
        }

        public ExportForm(MultilingualWrapper[] wrappers, MsgFormatter formatter) : this()
        {
            Wrappers = wrappers;
            Formatter = formatter;
            chkMerge.Visible = false;
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            Change();

        }

        public ExportOptions GetOptions()
        {
            Enum.TryParse(cmbFormat.Text, out StringFormat format);
            var stringOptions = new StringOptions()
            {
                Format = format,
                RemoveLineBreaks = chkRemoveLF.Checked,
                IgnoreRuby = chkIgnoreRuby.Checked,
                IgnoreSpeaker = chkIgnoreSpeaker.Checked,
            };
            return new ExportOptions()
            {
                StringOptions = stringOptions,
                Extension = cmbFileFormat.Text,
                Merged = chkMerge.Checked,
                IncludeId = chkIncludeId.Checked,
            };
        }

        public void Change()
        {
            var options = GetOptions();
            switch (Wrappers)
            {
                case MsgWrapper wrapper:
                    {
                        txtPreview.Text = ExportHelper.Export(wrapper, Formatter, options);
                    }
                    break;
                case MsgWrapper[] wrappers:
                    {
                        txtPreview.Text = ExportHelper.Export(wrappers.Take(3).ToArray(), Formatter, options);
                    }
                    break;
                case MultilingualWrapper[] wrappers:
                    {
                        txtPreview.Text = ExportHelper.Export(wrappers.First().Wrappers.First().Value, Formatter, options);
                    }
                    break;
            }

            txtPreview.SelectionStart = txtPreview.TextLength;
            txtPreview.SelectionLength = 0;

            txtPreview.Select();
        }

        public void Save()
        {

        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            var options = GetOptions();
            bool singleFile = (Wrappers is MsgWrapper) || options.Merged;
            if (singleFile)
            {
                saveFileDialog1.Filter = options.Extension switch
                {
                    ".txt" => "Text files(*.txt)|*.txt",
                    ".json" => "Json files(*.json)|*.json",
                    _ => throw new NotSupportedException(),
                };
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                options.Path = saveFileDialog1.FileName;
            }
            else
            {
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;
                options.Path = folderBrowserDialog1.SelectedPath;
            }

            switch (Wrappers)
            {
                case MsgWrapper wrapper:
                    {
                        ExportHelper.Export(wrapper, Formatter, options);
                    }
                    break;
                case MsgWrapper[] wrappers:
                    {
                        ExportHelper.Export(wrappers, Formatter, options);
                    }
                    break;
                case MultilingualWrapper[] wrappers:
                    {
                        ExportHelper.Export(wrappers, Formatter, options);
                    }
                    break;
                default:
                    throw new NotSupportedException();

            }
        }

        private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Change();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Change();
        }

        private void cmbFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Change();
        }

        private void chkMerge_CheckedChanged(object sender, EventArgs e)
        {
            Change();
        }

        private void chkRemoveLF_CheckedChanged(object sender, EventArgs e)
        {
            Change();
        }

        private void chkIgnoreRuby_CheckedChanged(object sender, EventArgs e)
        {
            Change();
        }

        private void chkIgnoreSpeaker_CheckedChanged(object sender, EventArgs e)
        {
            Change();
        }
    }
}
