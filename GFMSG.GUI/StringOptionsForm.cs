using System.ComponentModel;

namespace GFMSG.GUI;

public partial class StringOptionsForm : Form
{
    public StringOptionsForm()
    {
        InitializeComponent();
    }

    public StringOptionsForm(StringOptions options) : this()
    {
        propertyGrid1.SelectedObject = options;
    }
    
    public StringOptions GetValue()
    {
        return (StringOptions)propertyGrid1.SelectedObject;
    }

    private void StringOptionsForm_Load(object sender, EventArgs e)
    {

    }

}
