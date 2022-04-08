using System.Data;

namespace GFMSG.GUI
{
    public partial class MsgForm : Form
    {
        public MsgFormatter Formatter;
        public CellInfo Cell;

        private bool Loaded = false;
        private bool Changed = false;

        public bool AllowEdit { get; set; }

        public MsgForm()
        {
            InitializeComponent();
        }

        public MsgForm(CellInfo cell, MsgFormatter con) : this()
        {
            Formatter = con;
            Cell = cell;

            txtIndex.Text = cell.Row.ToString();

            if (cell.Entry.Name != null)
            {
                txtName.Text = cell.Entry.Name;
            }
            else
            {
                txtName.Visible = false;
                txtHash.Visible = false;
            }

            if (cell.Entry.HasText)
            {
                txtMarkup.Text = con.Format(cell.Sequence, new StringOptions(StringFormat.Markup, cell.Language));

                var rawText = con.Format(cell.Sequence, new StringOptions(StringFormat.Raw, cell.Language));
                //var convertedRawText = con.Format(GetNewSequence(), new StringOptions(StringFormat.Raw, cell.LanguageCode));
                //AllowEdit = rawText == convertedRawText;
                txtRaw.Text = rawText;
                txtLength.Text = string.Format("{0} bytes", cell.Sequence.Codes.Length);

                if (cell.Sequence.Grammatical.HasValue)
                {
                    new[] { radGender0, radGender1, radGender2, radGender3 }[(ushort)cell.Sequence.Grammatical.Value.Gender].Checked = true;
                    new[] { radInitialSound0, radInitialSound1, radInitialSound2, radInitialSound3 }[(ushort)cell.Sequence.Grammatical.Value.InitialSound].Checked = true;

                    chkIsUncountable.Checked = cell.Sequence.Grammatical.Value.IsUncountable;
                    chkIsAlwaysPlural.Checked = cell.Sequence.Grammatical.Value.IsAlwaysPlural;
                    nudExtraAttribute.Value = cell.Sequence.Grammatical.Value.ExtraAttribute;
                    nudExtraAttribute2.Value = cell.Sequence.Grammatical.Value.ExtraAttribute2;

                    grpGrammar.Visible = true;
                }
                else
                {
                    grpGrammar.Visible = false;
                }

                ApplyChange(false);
            }
            else
            {
                tlpContent.Visible = false;
            }

            Loaded = true;
        }

        private void MsgForm_Load(object sender, EventArgs e)
        {
            txtMarkup.SelectionStart = txtMarkup.TextLength;
            txtMarkup.SelectionLength = 0;

            btnClose.Select();
        }

        private void txtMarkup_TextChanged(object sender, EventArgs e)
        {
            if (Loaded)
            {
                tmrEditDelay.Tag = 0;
                tmrEditDelay.Start();
                Changed = true;
                btnOK.Enabled = true;
            }
        }

        private void tmrEditDelay_Tick(object sender, EventArgs e)
        {
            tmrEditDelay.Tag = (int)tmrEditDelay.Tag + tmrEditDelay.Interval;
            if((int)tmrEditDelay.Tag >= 1000)
            {
                ApplyChange(true);
                tmrEditDelay.Stop();
            }
        }

        public SymbolSequence GetNewSequence()
        {
            var symbols = Formatter.MarkupToSymbols(txtMarkup.Text, Cell.Language);
            var codes = Formatter.GetCodes(symbols, false); // !!!
            var gramma = new GrammaticalAttribute()
            {
                Gender = (GrammaticalGender)Array.FindIndex(new[] { radGender0, radGender1, radGender2, radGender3 }, x => x.Checked),
                InitialSound = (GrammaticalInitialSound)Array.FindIndex(new[] { radInitialSound0, radInitialSound1, radInitialSound2, radInitialSound3 }, x => x.Checked),
                IsUncountable = chkIsUncountable.Checked,
                IsAlwaysPlural = chkIsUncountable.Checked,
                ExtraAttribute = (ushort)nudExtraAttribute.Value,
                ExtraAttribute2 = (ushort)nudExtraAttribute2.Value,
            };
            return new SymbolSequence(codes, Cell.Language, gramma)
            {
                Name = txtName.Visible ? txtName.Text : null,
            };
        }

        private void ApplyChange(bool overwriteRaw)
        {
            SymbolSequence? sc = null;

            try
            {
                sc = GetNewSequence();
            }
            catch
            {
                txtPreview.Text = "";
                lblError.Visible = true;
                return;
            }

            var preview = Formatter.Format(sc, new StringOptions(StringFormat.Plain, Cell.Language));
            txtPreview.Text = preview.Replace("\n", "\r\n");
            lblError.Visible = false;

            if (overwriteRaw)
            {
                txtRaw.Text = Formatter.Format(sc, new StringOptions(StringFormat.Raw, Cell.Language));
                txtLength.Text = string.Format("{0} bytes", sc.Codes.Length);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            var hash = FnvHash.Fnv1a_64(txtName.Text);
            txtHash.Text = $"0x{hash:X8}";

            if (Loaded)
            {
                Changed = true;
                btnOK.Enabled = true;
            }
        }

        private void MsgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.DialogResult == DialogResult.Yes)
            {
                if (Changed)
                {
                    try
                    {
                        var sc = GetNewSequence();
                        Cell.Entry.Name = sc.Name;
                        Cell.Entry.Hash = FnvHash.Fnv1a_64(sc.Name);
                        Cell.Entry.Sequences[Cell.Index] = sc;
                        Cell.Entry.Changed = true;
                    }
                    catch
                    {
                        MessageBox.Show("syntax error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }
    }
}
