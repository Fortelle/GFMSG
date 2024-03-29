﻿namespace GFMSG.GUI;

public partial class ChooseModeForm : Form
{
    public ChooseModeForm()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        ShowForm(FileVersion.GenIV);
    }

    private void button2_Click(object sender, EventArgs e)
    {
        ShowForm(FileVersion.GenV);
    }

    private void button3_Click(object sender, EventArgs e)
    {
        ShowForm(FileVersion.GenVIII);
    }

    private void ShowForm(FileVersion version)
    {
        using var form = new ExplorerForm(version, true);
        this.Hide();
        form.ShowDialog();
        this.Show();
    }

    private void ChooseModeForm_Load(object sender, EventArgs e)
    {

    }
}
