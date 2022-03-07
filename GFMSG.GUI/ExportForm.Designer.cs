namespace GFMSG.GUI
{
    partial class ExportForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkMerge = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.chkIncludeId = new System.Windows.Forms.CheckBox();
            this.cmbFileFormat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkRemoveLF = new System.Windows.Forms.CheckBox();
            this.chkIgnoreRuby = new System.Windows.Forms.CheckBox();
            this.chkIgnoreSpeaker = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(16, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(752, 529);
            this.splitContainer1.SplitterDistance = 558;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPreview);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(558, 529);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview:";
            // 
            // txtPreview
            // 
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Location = new System.Drawing.Point(8, 26);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreview.Size = new System.Drawing.Size(542, 495);
            this.txtPreview.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chkMerge, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.cmbFormat, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkIncludeId, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbFileFormat, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(190, 529);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // chkMerge
            // 
            this.chkMerge.AutoSize = true;
            this.chkMerge.Location = new System.Drawing.Point(3, 468);
            this.chkMerge.Name = "chkMerge";
            this.chkMerge.Size = new System.Drawing.Size(158, 24);
            this.chkMerge.TabIndex = 8;
            this.chkMerge.Text = "Merge into one file";
            this.chkMerge.UseVisualStyleBackColor = true;
            this.chkMerge.CheckedChanged += new System.EventHandler(this.chkMerge_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "String format:";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(112, 500);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 26);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cmbFormat
            // 
            this.cmbFormat.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Items.AddRange(new object[] {
            "Raw",
            "Markup",
            "Plain",
            "Html"});
            this.cmbFormat.Location = new System.Drawing.Point(3, 35);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(184, 28);
            this.cmbFormat.TabIndex = 3;
            this.cmbFormat.SelectedIndexChanged += new System.EventHandler(this.cmbFormat_SelectedIndexChanged);
            // 
            // chkIncludeId
            // 
            this.chkIncludeId.AutoSize = true;
            this.chkIncludeId.Location = new System.Drawing.Point(3, 67);
            this.chkIncludeId.Name = "chkIncludeId";
            this.chkIncludeId.Size = new System.Drawing.Size(137, 24);
            this.chkIncludeId.TabIndex = 6;
            this.chkIncludeId.Text = "Include string id";
            this.chkIncludeId.UseVisualStyleBackColor = true;
            this.chkIncludeId.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cmbFileFormat
            // 
            this.cmbFileFormat.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileFormat.FormattingEnabled = true;
            this.cmbFileFormat.Items.AddRange(new object[] {
            ".txt",
            ".json",
            ".csv"});
            this.cmbFileFormat.Location = new System.Drawing.Point(3, 436);
            this.cmbFileFormat.Name = "cmbFileFormat";
            this.cmbFileFormat.Size = new System.Drawing.Size(184, 28);
            this.cmbFileFormat.TabIndex = 5;
            this.cmbFileFormat.SelectedIndexChanged += new System.EventHandler(this.cmbFileFormat_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 413);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "File format:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "String options:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.chkRemoveLF, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkIgnoreRuby, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkIgnoreSpeaker, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 131);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(184, 267);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // chkRemoveLF
            // 
            this.chkRemoveLF.AutoSize = true;
            this.chkRemoveLF.Location = new System.Drawing.Point(3, 3);
            this.chkRemoveLF.Name = "chkRemoveLF";
            this.chkRemoveLF.Size = new System.Drawing.Size(159, 24);
            this.chkRemoveLF.TabIndex = 9;
            this.chkRemoveLF.Text = "Remove line breaks";
            this.chkRemoveLF.UseVisualStyleBackColor = true;
            this.chkRemoveLF.CheckedChanged += new System.EventHandler(this.chkRemoveLF_CheckedChanged);
            // 
            // chkIgnoreRuby
            // 
            this.chkIgnoreRuby.AutoSize = true;
            this.chkIgnoreRuby.Location = new System.Drawing.Point(3, 35);
            this.chkIgnoreRuby.Name = "chkIgnoreRuby";
            this.chkIgnoreRuby.Size = new System.Drawing.Size(106, 24);
            this.chkIgnoreRuby.TabIndex = 10;
            this.chkIgnoreRuby.Text = "Ignore ruby";
            this.chkIgnoreRuby.UseVisualStyleBackColor = true;
            this.chkIgnoreRuby.CheckedChanged += new System.EventHandler(this.chkIgnoreRuby_CheckedChanged);
            // 
            // chkIgnoreSpeaker
            // 
            this.chkIgnoreSpeaker.AutoSize = true;
            this.chkIgnoreSpeaker.Location = new System.Drawing.Point(3, 67);
            this.chkIgnoreSpeaker.Name = "chkIgnoreSpeaker";
            this.chkIgnoreSpeaker.Size = new System.Drawing.Size(128, 24);
            this.chkIgnoreSpeaker.TabIndex = 11;
            this.chkIgnoreSpeaker.Text = "Ignore speaker";
            this.chkIgnoreSpeaker.UseVisualStyleBackColor = true;
            this.chkIgnoreSpeaker.CheckedChanged += new System.EventHandler(this.chkIgnoreSpeaker_CheckedChanged);
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ExportForm";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exporter";
            this.Load += new System.EventHandler(this.ExportForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private TextBox txtPreview;
        private Button btnExport;
        private ToolTip toolTip1;
        private SaveFileDialog saveFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
        private ComboBox cmbFileFormat;
        private ComboBox cmbFormat;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private CheckBox chkIncludeId;
        private Label label2;
        private CheckBox chkMerge;
        private CheckBox chkRemoveLF;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox chkIgnoreRuby;
        private CheckBox chkIgnoreSpeaker;
    }
}