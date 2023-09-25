namespace GFMSG.GUI;

public partial class TextDialog : Form
{
    public TextDialog()
    {
        InitializeComponent();
    }

    public TextDialog(string text) : this()
    {
        textBox1.Text = text;
    }

    public static void Show(string text)
    {
        using var frm = new TextDialog(text);
        frm.ShowDialog();
    }

    private void TextDialog_Load(object sender, EventArgs e)
    {

    }
}
