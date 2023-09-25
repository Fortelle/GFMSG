namespace GFMSG.GUI;

public partial class ExportForm : Form
{
    public object Wrappers { get; set; }
    public MsgFormatter Formatter { get; set; }
    public StringOptions Options { get; set; }

    public ExportForm()
    {
        InitializeComponent();

        foreach (var format in Enum.GetValues<StringFormat>())
        {
            cmbFormat.Items.Add(format);
        }

        cmbFileFormat.SelectedIndex = 0;
    }

    public ExportForm(MsgWrapper wrapper, MsgFormatter formatter, StringOptions options) : this()
    {
        Wrappers = wrapper;
        Formatter = formatter;
        Options = options;
        chkMerge.Visible = false;
    }

    public ExportForm(MsgWrapper[] wrappers, MsgFormatter formatter, StringOptions options) : this()
    {
        Wrappers = wrappers;
        Formatter = formatter;
        Options = options;
        chkMerge.Visible = true;
    }

    public ExportForm(MultilingualWrapper[] wrappers, MsgFormatter formatter, StringOptions options) : this()
    {
        Wrappers = wrappers;
        Formatter = formatter;
        Options = options;
        chkMerge.Visible = false;
    }

    private ExportOptions GetOptions()
    {
        return new ExportOptions()
        {
            StringOptions = Options,
            Extension = cmbFileFormat.Text,
            Merged = chkMerge.Checked,
            IncludeId = chkIncludeId.Checked,
        };
    }

    private void Change()
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

        cmbFormat.SelectedItem = Options.Format;

        txtPreview.SelectionStart = txtPreview.TextLength;
        txtPreview.SelectionLength = 0;

        txtPreview.Select();
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

    private void ExportForm_Load(object sender, EventArgs e)
    {
        Change();
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        Change();
    }

    private void chkMerge_CheckedChanged(object sender, EventArgs e)
    {
        Change();
    }

    private void btnStringOptions_Click(object sender, EventArgs e)
    {
        using var frm = new StringOptionsForm(Options);
        if (frm.ShowDialog(this) != DialogResult.OK) return;
        Options = frm.GetValue();
        Change();
    }

    private void cmbFormat_SelectionChangeCommitted(object sender, EventArgs e)
    {
        Options = Options with { Format = (StringFormat)cmbFormat.SelectedItem };
        Change();
    }

    private void cmbFileFormat_SelectionChangeCommitted(object sender, EventArgs e)
    {
        Change();
    }
}
