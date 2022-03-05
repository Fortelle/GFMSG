namespace GFMSG.GUI
{
    partial class MsgForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtHash = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.tlpGrammar = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nudExtraAttribute = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.radInitialSound0 = new System.Windows.Forms.RadioButton();
            this.radInitialSound1 = new System.Windows.Forms.RadioButton();
            this.radInitialSound2 = new System.Windows.Forms.RadioButton();
            this.radInitialSound3 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.radGender3 = new System.Windows.Forms.RadioButton();
            this.radGender0 = new System.Windows.Forms.RadioButton();
            this.radGender2 = new System.Windows.Forms.RadioButton();
            this.radGender1 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.nudExtraAttribute2 = new System.Windows.Forms.NumericUpDown();
            this.chkIsUncountable = new System.Windows.Forms.CheckBox();
            this.chkIsAlwaysPlural = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtMarkup = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtRaw = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.tmrEditDelay = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpGrammar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExtraAttribute)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExtraAttribute2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tlpContent.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIndex
            // 
            this.txtIndex.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtIndex.Location = new System.Drawing.Point(3, 39);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.ReadOnly = true;
            this.txtIndex.Size = new System.Drawing.Size(190, 25);
            this.txtIndex.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtName.Location = new System.Drawing.Point(3, 111);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(190, 25);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtHash
            // 
            this.txtHash.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtHash.Location = new System.Drawing.Point(3, 183);
            this.txtHash.Name = "txtHash";
            this.txtHash.ReadOnly = true;
            this.txtHash.Size = new System.Drawing.Size(190, 25);
            this.txtHash.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Index:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hash:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtHash, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtIndex, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtLength, 0, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 187F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(196, 545);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 512);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 30);
            this.panel1.TabIndex = 8;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(115, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(34, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Length:";
            // 
            // txtLength
            // 
            this.txtLength.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtLength.Location = new System.Drawing.Point(3, 255);
            this.txtLength.Name = "txtLength";
            this.txtLength.ReadOnly = true;
            this.txtLength.Size = new System.Drawing.Size(190, 25);
            this.txtLength.TabIndex = 10;
            // 
            // tlpGrammar
            // 
            this.tlpGrammar.ColumnCount = 5;
            this.tlpGrammar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGrammar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpGrammar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpGrammar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpGrammar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpGrammar.Controls.Add(this.label8, 1, 0);
            this.tlpGrammar.Controls.Add(this.label9, 1, 1);
            this.tlpGrammar.Controls.Add(this.nudExtraAttribute, 2, 3);
            this.tlpGrammar.Controls.Add(this.label12, 1, 3);
            this.tlpGrammar.Controls.Add(this.tableLayoutPanel2, 2, 1);
            this.tlpGrammar.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tlpGrammar.Controls.Add(this.label7, 3, 3);
            this.tlpGrammar.Controls.Add(this.nudExtraAttribute2, 4, 3);
            this.tlpGrammar.Controls.Add(this.chkIsUncountable, 2, 2);
            this.tlpGrammar.Controls.Add(this.chkIsAlwaysPlural, 3, 2);
            this.tlpGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGrammar.Location = new System.Drawing.Point(3, 21);
            this.tlpGrammar.Name = "tlpGrammar";
            this.tlpGrammar.RowCount = 5;
            this.tlpGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGrammar.Size = new System.Drawing.Size(524, 129);
            this.tlpGrammar.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Gender";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Initial sound";
            // 
            // nudExtraAttribute
            // 
            this.nudExtraAttribute.Location = new System.Drawing.Point(149, 99);
            this.nudExtraAttribute.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudExtraAttribute.Name = "nudExtraAttribute";
            this.nudExtraAttribute.Size = new System.Drawing.Size(120, 25);
            this.nudExtraAttribute.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 20);
            this.label12.TabIndex = 8;
            this.label12.Text = "Extra attribute";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tlpGrammar.SetColumnSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.radInitialSound0, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.radInitialSound1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.radInitialSound2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.radInitialSound3, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(149, 35);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(372, 26);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // radInitialSound0
            // 
            this.radInitialSound0.AutoSize = true;
            this.radInitialSound0.Location = new System.Drawing.Point(3, 3);
            this.radInitialSound0.Name = "radInitialSound0";
            this.radInitialSound0.Size = new System.Drawing.Size(87, 20);
            this.radInitialSound0.TabIndex = 0;
            this.radInitialSound0.TabStop = true;
            this.radInitialSound0.Text = "Consonant";
            this.radInitialSound0.UseVisualStyleBackColor = true;
            // 
            // radInitialSound1
            // 
            this.radInitialSound1.AutoSize = true;
            this.radInitialSound1.Location = new System.Drawing.Point(96, 3);
            this.radInitialSound1.Name = "radInitialSound1";
            this.radInitialSound1.Size = new System.Drawing.Size(68, 20);
            this.radInitialSound1.TabIndex = 1;
            this.radInitialSound1.TabStop = true;
            this.radInitialSound1.Text = "Vowel";
            this.radInitialSound1.UseVisualStyleBackColor = true;
            // 
            // radInitialSound2
            // 
            this.radInitialSound2.AutoSize = true;
            this.radInitialSound2.Location = new System.Drawing.Point(189, 3);
            this.radInitialSound2.Name = "radInitialSound2";
            this.radInitialSound2.Size = new System.Drawing.Size(87, 20);
            this.radInitialSound2.TabIndex = 2;
            this.radInitialSound2.TabStop = true;
            this.radInitialSound2.Text = "Consonant2";
            this.radInitialSound2.UseVisualStyleBackColor = true;
            // 
            // radInitialSound3
            // 
            this.radInitialSound3.AutoSize = true;
            this.radInitialSound3.Enabled = false;
            this.radInitialSound3.Location = new System.Drawing.Point(282, 3);
            this.radInitialSound3.Name = "radInitialSound3";
            this.radInitialSound3.Size = new System.Drawing.Size(31, 20);
            this.radInitialSound3.TabIndex = 3;
            this.radInitialSound3.TabStop = true;
            this.radInitialSound3.Text = " ";
            this.radInitialSound3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tlpGrammar.SetColumnSpan(this.tableLayoutPanel3, 3);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.radGender3, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.radGender0, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.radGender2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.radGender1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(149, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(372, 26);
            this.tableLayoutPanel3.TabIndex = 14;
            // 
            // radGender3
            // 
            this.radGender3.AutoSize = true;
            this.radGender3.Enabled = false;
            this.radGender3.Location = new System.Drawing.Point(282, 3);
            this.radGender3.Name = "radGender3";
            this.radGender3.Size = new System.Drawing.Size(31, 20);
            this.radGender3.TabIndex = 3;
            this.radGender3.TabStop = true;
            this.radGender3.Text = " ";
            this.radGender3.UseVisualStyleBackColor = true;
            // 
            // radGender0
            // 
            this.radGender0.AutoSize = true;
            this.radGender0.Location = new System.Drawing.Point(3, 3);
            this.radGender0.Name = "radGender0";
            this.radGender0.Size = new System.Drawing.Size(87, 20);
            this.radGender0.TabIndex = 0;
            this.radGender0.TabStop = true;
            this.radGender0.Text = "Masculine";
            this.radGender0.UseVisualStyleBackColor = true;
            // 
            // radGender2
            // 
            this.radGender2.AutoSize = true;
            this.radGender2.Location = new System.Drawing.Point(189, 3);
            this.radGender2.Name = "radGender2";
            this.radGender2.Size = new System.Drawing.Size(73, 20);
            this.radGender2.TabIndex = 2;
            this.radGender2.TabStop = true;
            this.radGender2.Text = "Neuter";
            this.radGender2.UseVisualStyleBackColor = true;
            // 
            // radGender1
            // 
            this.radGender1.AutoSize = true;
            this.radGender1.Location = new System.Drawing.Point(96, 3);
            this.radGender1.Name = "radGender1";
            this.radGender1.Size = new System.Drawing.Size(87, 20);
            this.radGender1.TabIndex = 1;
            this.radGender1.TabStop = true;
            this.radGender1.Text = "Feminine";
            this.radGender1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(275, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            // 
            // nudExtraAttribute2
            // 
            this.nudExtraAttribute2.Location = new System.Drawing.Point(401, 99);
            this.nudExtraAttribute2.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudExtraAttribute2.Name = "nudExtraAttribute2";
            this.nudExtraAttribute2.Size = new System.Drawing.Size(120, 25);
            this.nudExtraAttribute2.TabIndex = 11;
            this.nudExtraAttribute2.Visible = false;
            // 
            // chkIsUncountable
            // 
            this.chkIsUncountable.AutoSize = true;
            this.chkIsUncountable.Location = new System.Drawing.Point(149, 67);
            this.chkIsUncountable.Name = "chkIsUncountable";
            this.chkIsUncountable.Size = new System.Drawing.Size(115, 24);
            this.chkIsUncountable.TabIndex = 5;
            this.chkIsUncountable.Text = "Uncountable";
            this.chkIsUncountable.UseVisualStyleBackColor = true;
            // 
            // chkIsAlwaysPlural
            // 
            this.chkIsAlwaysPlural.AutoSize = true;
            this.chkIsAlwaysPlural.Location = new System.Drawing.Point(275, 67);
            this.chkIsAlwaysPlural.Name = "chkIsAlwaysPlural";
            this.chkIsAlwaysPlural.Size = new System.Drawing.Size(117, 24);
            this.chkIsAlwaysPlural.TabIndex = 7;
            this.chkIsAlwaysPlural.Text = "Always plural";
            this.chkIsAlwaysPlural.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tlpContent);
            this.splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(16, 0, 16, 16);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 16, 16);
            this.splitContainer1.Size = new System.Drawing.Size(784, 561);
            this.splitContainer1.SplitterDistance = 568;
            this.splitContainer1.TabIndex = 7;
            // 
            // tlpContent
            // 
            this.tlpContent.ColumnCount = 1;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Controls.Add(this.label4, 0, 0);
            this.tlpContent.Controls.Add(this.label5, 0, 2);
            this.tlpContent.Controls.Add(this.txtPreview, 0, 1);
            this.tlpContent.Controls.Add(this.groupBox1, 0, 5);
            this.tlpContent.Controls.Add(this.tabControl1, 0, 3);
            this.tlpContent.Controls.Add(this.lblError, 0, 4);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(16, 0);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 7;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Size = new System.Drawing.Size(536, 545);
            this.tlpContent.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Preview:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 1);
            this.label5.TabIndex = 1;
            this.label5.Text = "Markup:";
            // 
            // txtPreview
            // 
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Location = new System.Drawing.Point(3, 35);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreview.Size = new System.Drawing.Size(530, 138);
            this.txtPreview.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tlpGrammar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 383);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 153);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grammar:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 179);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(530, 174);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtMarkup);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(522, 141);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Markup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtMarkup
            // 
            this.txtMarkup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMarkup.Location = new System.Drawing.Point(3, 3);
            this.txtMarkup.Multiline = true;
            this.txtMarkup.Name = "txtMarkup";
            this.txtMarkup.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMarkup.Size = new System.Drawing.Size(516, 135);
            this.txtMarkup.TabIndex = 3;
            this.txtMarkup.TextChanged += new System.EventHandler(this.txtMarkup_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtRaw);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(522, 141);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Raw";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtRaw
            // 
            this.txtRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRaw.Location = new System.Drawing.Point(3, 3);
            this.txtRaw.Multiline = true;
            this.txtRaw.Name = "txtRaw";
            this.txtRaw.ReadOnly = true;
            this.txtRaw.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRaw.Size = new System.Drawing.Size(516, 135);
            this.txtRaw.TabIndex = 0;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(3, 356);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(87, 20);
            this.lblError.TabIndex = 11;
            this.lblError.Text = "syntax error";
            // 
            // tmrEditDelay
            // 
            this.tmrEditDelay.Interval = 200;
            this.tmrEditDelay.Tick += new System.EventHandler(this.tmrEditDelay_Tick);
            // 
            // MsgForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MsgForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MsgForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MsgForm_FormClosing);
            this.Load += new System.EventHandler(this.MsgForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tlpGrammar.ResumeLayout(false);
            this.tlpGrammar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExtraAttribute)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExtraAttribute2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tlpContent.ResumeLayout(false);
            this.tlpContent.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBox txtIndex;
        private TextBox txtName;
        private TextBox txtHash;
        private Label label1;
        private Label label2;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private Button btnClose;
        private Button btnOK;
        private TableLayoutPanel tlpContent;
        private Label label4;
        private Label label5;
        private TextBox txtPreview;
        private TextBox txtMarkup;
        private System.Windows.Forms.Timer tmrEditDelay;
        private Label label6;
        private TextBox txtLength;
        private TableLayoutPanel tlpGrammar;
        private Label label8;
        private Label label9;
        private CheckBox chkIsUncountable;
        private CheckBox chkIsAlwaysPlural;
        private Label label12;
        private NumericUpDown nudExtraAttribute;
        private Label label7;
        private NumericUpDown nudExtraAttribute2;
        private RadioButton radGender3;
        private RadioButton radGender2;
        private RadioButton radGender1;
        private RadioButton radGender0;
        private TableLayoutPanel tableLayoutPanel2;
        private RadioButton radInitialSound0;
        private RadioButton radInitialSound1;
        private RadioButton radInitialSound2;
        private RadioButton radInitialSound3;
        private TableLayoutPanel tableLayoutPanel3;
        private GroupBox groupBox1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox txtRaw;
        private Label lblError;
    }
}