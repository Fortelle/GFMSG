using GFMSG.Pokemon;

namespace GFMSG.GUI
{
    public partial class ChooseFormatterForm : Form
    {
        public Type[] FormatterTypes = new[]
            {
                typeof(PokemonMsgFormatter),
                typeof(SunMoonMsgFormatter),
                typeof(UltraSunUltraMoonMsgFormatter),
                typeof(LetsGoMsgFormatter),
                typeof(SwordShieldMsgFormatter),
                typeof(ArceusMsgFormatter),
            };

        public ChooseFormatterForm()
        {
            InitializeComponent();

            foreach(var type in FormatterTypes)
            {
                comboBox1.Items.Add(type.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void ChooseFormatterForm_Load(object sender, EventArgs e)
        {

        }

        public Type GetSelectedType() => FormatterTypes.First(x=>x.Name == comboBox1.Text);
    }
}