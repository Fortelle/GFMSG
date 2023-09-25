using GFMSG.Pokemon;

namespace GFMSG.GUI;

public partial class ChooseFormatterForm : Form
{
    public Dictionary<FileVersion, Type[]> FormatterTypes = new()
    {
        [FileVersion.GenIV] = new []
            {
                typeof(DpMsgFormatter),
            },
        [FileVersion.GenV] = new[]
            {
                typeof(BwMsgFormatter),
            },
        [FileVersion.GenVI] = new []
            {
                typeof(PokemonMsgFormatterV2),
                typeof(SunMoonMsgFormatter),
                typeof(UltraSunUltraMoonMsgFormatter),
            },
        [FileVersion.GenVIII] = new []
            {
                typeof(PokemonMsgFormatterV2),
                typeof(LetsGoMsgFormatter),
                typeof(SwordShieldMsgFormatter),
                typeof(ArceusMsgFormatter),
            },
    };

    public string[] Languages = new []
    {
        "jp",
        "en",
        "fr",
        "it",
        "de",
        "es",
        "ko",
        "zh",
    };

    public ChooseFormatterForm()
    {
        InitializeComponent();
    }

    public ChooseFormatterForm(FileVersion version, bool lang) : this()
    {
        foreach (var type in FormatterTypes[version])
        {
            comboBox1.Items.Add(type.Name);
        }
        comboBox1.SelectedIndex = 0;

        if (lang)
        {
            comboBox2.Items.AddRange(Languages);
            comboBox2.SelectedIndex = 0;
            comboBox2.Visible = true;
        }
        else
        {
            comboBox2.Visible = false;
        }
    }

    private void ChooseFormatterForm_Load(object sender, EventArgs e)
    {

    }

    public Type GetSelectedType() => FormatterTypes.Values.SelectMany(x => x).First(x => x.Name == comboBox1.Text);

    public string GetSelectedLanguage() => comboBox2.Text;

    private void button1_Click(object sender, EventArgs e)
    {

    }
}