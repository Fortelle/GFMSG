namespace GFMSG.GUI
{
    partial class ExplorerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExplorerForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBatchExport = new System.Windows.Forms.ToolStripMenuItem();
            this.devToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSaveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAnalyzeCharSymbols = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAnalyzeTagSymbols = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslTableCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslEntryCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLanguageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLanguage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsddbLanguage = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbFormat = new System.Windows.Forms.ToolStripDropDownButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpFiles = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.cmbMultilingual = new System.Windows.Forms.ComboBox();
            this.tpSearch = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstSearch = new System.Windows.Forms.ListView();
            this.chSearchContent = new System.Windows.Forms.ColumnHeader();
            this.chSearchFile = new System.Windows.Forms.ColumnHeader();
            this.cmbSearchType = new System.Windows.Forms.ComboBox();
            this.lstContent = new System.Windows.Forms.ListView();
            this.chIndex = new System.Windows.Forms.ColumnHeader();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chText = new System.Windows.Forms.ColumnHeader();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpFiles.SuspendLayout();
            this.tpSearch.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.dataToolStripMenuItem,
            this.devToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.toolStripSeparator3,
            this.tsmiOpen,
            this.tsmiOpenFolder,
            this.tsmiOpenMessage,
            this.toolStripSeparator4,
            this.tsmiSave,
            this.tsmiSaveAs,
            this.tsmiSaveAll,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(44, 24);
            this.tsmiFile.Text = "File";
            // 
            // tsmiNew
            // 
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.Size = new System.Drawing.Size(221, 24);
            this.tsmiNew.Text = "New";
            this.tsmiNew.Click += new System.EventHandler(this.tsmiNew_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(218, 6);
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(221, 24);
            this.tsmiOpen.Text = "Open file";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiOpenFolder
            // 
            this.tsmiOpenFolder.Name = "tsmiOpenFolder";
            this.tsmiOpenFolder.Size = new System.Drawing.Size(221, 24);
            this.tsmiOpenFolder.Text = "Open folder";
            this.tsmiOpenFolder.Click += new System.EventHandler(this.tsmiOpenFolder_Click);
            // 
            // tsmiOpenMessage
            // 
            this.tsmiOpenMessage.Name = "tsmiOpenMessage";
            this.tsmiOpenMessage.Size = new System.Drawing.Size(221, 24);
            this.tsmiOpenMessage.Text = "Open message folder";
            this.tsmiOpenMessage.Click += new System.EventHandler(this.tsmiOpenMessage_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(218, 6);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(221, 24);
            this.tsmiSave.Text = "Save";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiSaveAs
            // 
            this.tsmiSaveAs.Name = "tsmiSaveAs";
            this.tsmiSaveAs.Size = new System.Drawing.Size(221, 24);
            this.tsmiSaveAs.Text = "Save as";
            this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
            // 
            // tsmiSaveAll
            // 
            this.tsmiSaveAll.Name = "tsmiSaveAll";
            this.tsmiSaveAll.Size = new System.Drawing.Size(221, 24);
            this.tsmiSaveAll.Text = "Save all";
            this.tsmiSaveAll.Click += new System.EventHandler(this.tsmiSaveAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(218, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(221, 24);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem1,
            this.toolStripSeparator2,
            this.tsmiExport,
            this.tsmiBatchExport});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // importToolStripMenuItem1
            // 
            this.importToolStripMenuItem1.Enabled = false;
            this.importToolStripMenuItem1.Name = "importToolStripMenuItem1";
            this.importToolStripMenuItem1.Size = new System.Drawing.Size(163, 24);
            this.importToolStripMenuItem1.Text = "Import";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(160, 6);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(163, 24);
            this.tsmiExport.Text = "Export";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // tsmiBatchExport
            // 
            this.tsmiBatchExport.Name = "tsmiBatchExport";
            this.tsmiBatchExport.Size = new System.Drawing.Size(163, 24);
            this.tsmiBatchExport.Text = "Batch export";
            this.tsmiBatchExport.Click += new System.EventHandler(this.tsmiBatchExport_Click);
            // 
            // devToolStripMenuItem
            // 
            this.devToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testSaveToolStripMenuItem,
            this.testSaveAllToolStripMenuItem,
            this.tsmiAnalyzeTagSymbols,
            this.tsmiAnalyzeCharSymbols});
            this.devToolStripMenuItem.Name = "devToolStripMenuItem";
            this.devToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.devToolStripMenuItem.Text = "Dev";
            // 
            // testSaveToolStripMenuItem
            // 
            this.testSaveToolStripMenuItem.Name = "testSaveToolStripMenuItem";
            this.testSaveToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.testSaveToolStripMenuItem.Text = "Test save";
            this.testSaveToolStripMenuItem.Click += new System.EventHandler(this.testSaveToolStripMenuItem_Click);
            // 
            // testSaveAllToolStripMenuItem
            // 
            this.testSaveAllToolStripMenuItem.Name = "testSaveAllToolStripMenuItem";
            this.testSaveAllToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.testSaveAllToolStripMenuItem.Text = "Test save all";
            this.testSaveAllToolStripMenuItem.Click += new System.EventHandler(this.testSaveAllToolStripMenuItem_Click);
            // 
            // tsmiAnalyzeCharSymbols
            // 
            this.tsmiAnalyzeCharSymbols.Name = "tsmiAnalyzeCharSymbols";
            this.tsmiAnalyzeCharSymbols.Size = new System.Drawing.Size(222, 24);
            this.tsmiAnalyzeCharSymbols.Text = "Analyze CharSymbols";
            this.tsmiAnalyzeCharSymbols.Click += new System.EventHandler(this.tsmiAnalyzeCharSymbols_Click);
            // 
            // tsmiAnalyzeTagSymbols
            // 
            this.tsmiAnalyzeTagSymbols.Name = "tsmiAnalyzeTagSymbols";
            this.tsmiAnalyzeTagSymbols.Size = new System.Drawing.Size(222, 24);
            this.tsmiAnalyzeTagSymbols.Text = "Analyze TagSymbols";
            this.tsmiAnalyzeTagSymbols.Click += new System.EventHandler(this.tsmiAnalyzeTagSymbols_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslTableCount,
            this.tsslEntryCount,
            this.tsslLanguageLabel,
            this.tsslLanguage,
            this.tsddbLanguage,
            this.tsddbFormat});
            this.statusStrip1.Location = new System.Drawing.Point(0, 703);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslTableCount
            // 
            this.tsslTableCount.Name = "tsslTableCount";
            this.tsslTableCount.Size = new System.Drawing.Size(67, 21);
            this.tsslTableCount.Text = "Tables: 0";
            // 
            // tsslEntryCount
            // 
            this.tsslEntryCount.Name = "tsslEntryCount";
            this.tsslEntryCount.Size = new System.Drawing.Size(69, 21);
            this.tsslEntryCount.Text = "Entries: 0";
            // 
            // tsslLanguageLabel
            // 
            this.tsslLanguageLabel.Name = "tsslLanguageLabel";
            this.tsslLanguageLabel.Size = new System.Drawing.Size(79, 21);
            this.tsslLanguageLabel.Text = "Language:";
            // 
            // tsslLanguage
            // 
            this.tsslLanguage.Name = "tsslLanguage";
            this.tsslLanguage.Size = new System.Drawing.Size(152, 21);
            this.tsslLanguage.Text = "toolStripStatusLabel2";
            // 
            // tsddbLanguage
            // 
            this.tsddbLanguage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbLanguage.Image = ((System.Drawing.Image)(resources.GetObject("tsddbLanguage.Image")));
            this.tsddbLanguage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbLanguage.Name = "tsddbLanguage";
            this.tsddbLanguage.Size = new System.Drawing.Size(208, 24);
            this.tsddbLanguage.Text = "toolStripDropDownButton1";
            // 
            // tsddbFormat
            // 
            this.tsddbFormat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbFormat.Image = ((System.Drawing.Image)(resources.GetObject("tsddbFormat.Image")));
            this.tsddbFormat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbFormat.Name = "tsddbFormat";
            this.tsddbFormat.Size = new System.Drawing.Size(208, 24);
            this.tsddbFormat.Text = "toolStripDropDownButton1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstContent);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Size = new System.Drawing.Size(1008, 675);
            this.splitContainer1.SplitterDistance = 252;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpFiles);
            this.tabControl1.Controls.Add(this.tpSearch);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(252, 675);
            this.tabControl1.TabIndex = 1;
            // 
            // tpFiles
            // 
            this.tpFiles.Controls.Add(this.treeView1);
            this.tpFiles.Controls.Add(this.cmbMultilingual);
            this.tpFiles.Location = new System.Drawing.Point(4, 29);
            this.tpFiles.Name = "tpFiles";
            this.tpFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tpFiles.Size = new System.Drawing.Size(244, 642);
            this.tpFiles.TabIndex = 0;
            this.tpFiles.Text = "Files";
            this.tpFiles.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 31);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(238, 608);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // cmbMultilingual
            // 
            this.cmbMultilingual.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbMultilingual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMultilingual.FormattingEnabled = true;
            this.cmbMultilingual.Location = new System.Drawing.Point(3, 3);
            this.cmbMultilingual.Name = "cmbMultilingual";
            this.cmbMultilingual.Size = new System.Drawing.Size(238, 28);
            this.cmbMultilingual.TabIndex = 1;
            // 
            // tpSearch
            // 
            this.tpSearch.Controls.Add(this.tableLayoutPanel1);
            this.tpSearch.Location = new System.Drawing.Point(4, 29);
            this.tpSearch.Name = "tpSearch";
            this.tpSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpSearch.Size = new System.Drawing.Size(244, 642);
            this.tpSearch.TabIndex = 1;
            this.tpSearch.Text = "Search";
            this.tpSearch.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtSearch, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstSearch, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbSearchType, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(238, 636);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Location = new System.Drawing.Point(3, 35);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(232, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(160, 67);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 26);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lstSearch
            // 
            this.lstSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSearchContent,
            this.chSearchFile});
            this.lstSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSearch.FullRowSelect = true;
            this.lstSearch.Location = new System.Drawing.Point(3, 99);
            this.lstSearch.Name = "lstSearch";
            this.lstSearch.Size = new System.Drawing.Size(232, 534);
            this.lstSearch.TabIndex = 3;
            this.lstSearch.UseCompatibleStateImageBehavior = false;
            this.lstSearch.View = System.Windows.Forms.View.Details;
            this.lstSearch.SelectedIndexChanged += new System.EventHandler(this.lstSearch_SelectedIndexChanged);
            this.lstSearch.Click += new System.EventHandler(this.lstSearch_Click);
            // 
            // chSearchContent
            // 
            this.chSearchContent.Text = "Content";
            this.chSearchContent.Width = 180;
            // 
            // chSearchFile
            // 
            this.chSearchFile.Text = "File";
            // 
            // cmbSearchType
            // 
            this.cmbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchType.FormattingEnabled = true;
            this.cmbSearchType.Items.AddRange(new object[] {
            "Name",
            "Raw",
            "Markup",
            "Plain"});
            this.cmbSearchType.Location = new System.Drawing.Point(3, 3);
            this.cmbSearchType.Name = "cmbSearchType";
            this.cmbSearchType.Size = new System.Drawing.Size(121, 28);
            this.cmbSearchType.TabIndex = 4;
            // 
            // lstContent
            // 
            this.lstContent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndex,
            this.chName,
            this.chText});
            this.lstContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstContent.FullRowSelect = true;
            this.lstContent.GridLines = true;
            this.lstContent.Location = new System.Drawing.Point(0, 0);
            this.lstContent.Name = "lstContent";
            this.lstContent.Size = new System.Drawing.Size(752, 675);
            this.lstContent.TabIndex = 0;
            this.lstContent.UseCompatibleStateImageBehavior = false;
            this.lstContent.View = System.Windows.Forms.View.Details;
            this.lstContent.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.lstContent.MarginChanged += new System.EventHandler(this.listView1_MarginChanged);
            this.lstContent.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // chIndex
            // 
            this.chIndex.Text = "Index";
            // 
            // chName
            // 
            this.chName.Text = "Name";
            // 
            // chText
            // 
            this.chText.Text = "Text";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "MsgData files (*.dat)|*.dat|All files (*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "MsgData files (*.dat)|*.dat|All files (*.*)|*.*";
            // 
            // ExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ExplorerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GFMSG";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExplorerForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpFiles.ResumeLayout(false);
            this.tpSearch.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem tsmiFile;
        private ToolStripMenuItem tsmiNew;
        private ToolStripMenuItem tsmiOpen;
        private ToolStripMenuItem tsmiSave;
        private ToolStripMenuItem tsmiSaveAs;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmiExit;
        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private ListView lstContent;
        private ColumnHeader chIndex;
        private ColumnHeader chName;
        private ColumnHeader chText;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ToolStripStatusLabel tsslTableCount;
        private ToolStripStatusLabel tsslEntryCount;
        private TreeView treeView1;
        private ToolStripMenuItem tsmiOpenFolder;
        private FolderBrowserDialog folderBrowserDialog1;
        private TabControl tabControl1;
        private TabPage tpFiles;
        private TabPage tpSearch;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtSearch;
        private Button btnSearch;
        private ListView lstSearch;
        private ColumnHeader chSearchContent;
        private ColumnHeader chSearchFile;
        private ToolStripMenuItem devToolStripMenuItem;
        private ToolStripMenuItem dataToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem tsmiExport;
        private ToolStripMenuItem testSaveToolStripMenuItem;
        private ComboBox cmbSearchType;
        private ToolStripMenuItem tsmiAnalyzeCharSymbols;
        private ComboBox cmbMultilingual;
        private ToolStripStatusLabel tsslLanguageLabel;
        private ToolStripStatusLabel tsslLanguage;
        private ToolStripDropDownButton tsddbLanguage;
        private ToolStripMenuItem tsmiAnalyzeTagSymbols;
        private ToolStripMenuItem tsmiOpenMessage;
        private ToolStripMenuItem tsmiSaveAll;
        private ToolStripMenuItem testSaveAllToolStripMenuItem;
        private ToolStripMenuItem tsmiBatchExport;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripDropDownButton tsddbFormat;
    }
}