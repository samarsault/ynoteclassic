namespace SS.Ynote.Classic.UI
{
    partial class Options
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tabs");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Environment", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("MISC");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("KeyBindings");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Text Editor", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("File Extensions");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Files", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("General Settings");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Plugin Manager");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Plugins", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Manage");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Data", new System.Windows.Forms.TreeNode[] {
            treeNode12});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabcontrol = new WizardTabControl();
            this.tabsettingpage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbdockstyle = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tablocation = new System.Windows.Forms.ComboBox();
            this.GeneralPage = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.virtualspace = new System.Windows.Forms.CheckBox();
            this.highlightfoliding = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ShowLineNumber = new System.Windows.Forms.CheckBox();
            this.cmbwordwrapmode = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.showcaret = new System.Windows.Forms.CheckBox();
            this.showfoldinglines = new System.Windows.Forms.CheckBox();
            this.EditingPage = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tblineinterval = new SS.Ynote.Classic.NumericTextBox();
            this.tbpaddingwidth = new SS.Ynote.Classic.NumericTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.FileExtensionsPage = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.label22 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label21 = new System.Windows.Forms.Label();
            this.ClearPage = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Plugins = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.keybindingstab = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabcontrol.SuspendLayout();
            this.tabsettingpage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GeneralPage.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.EditingPage.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.FileExtensionsPage.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.ClearPage.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.Plugins.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.keybindingstab.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.FullRowSelect = true;
            this.treeView1.Indent = 27;
            this.treeView1.Location = new System.Drawing.Point(5, 31);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "TabsNode";
            treeNode1.Text = "Tabs";
            treeNode2.Name = "EnvironmentNode";
            treeNode2.Text = "Environment";
            treeNode3.Name = "TextEditorGeneralNode";
            treeNode3.Text = "General";
            treeNode4.Name = "TextEditorEditing";
            treeNode4.Text = "MISC";
            treeNode5.Name = "Node1";
            treeNode5.Text = "KeyBindings";
            treeNode6.Name = "TextEditorNode";
            treeNode6.Text = "Text Editor";
            treeNode7.Name = "FileExtensionsNode";
            treeNode7.Text = "File Extensions";
            treeNode8.Name = "Node21";
            treeNode8.Text = "Files";
            treeNode9.Name = "General";
            treeNode9.Text = "General Settings";
            treeNode10.Name = "PluginManagerNode";
            treeNode10.Text = "Plugin Manager";
            treeNode11.Name = "Node30";
            treeNode11.Text = "Plugins";
            treeNode12.Checked = true;
            treeNode12.Name = "ClearDataNode";
            treeNode12.Text = "Manage";
            treeNode13.Name = "DataNode";
            treeNode13.Text = "Data";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode6,
            treeNode8,
            treeNode11,
            treeNode13});
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(169, 318);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(458, 355);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(546, 355);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "      Options";
            // 
            // tabcontrol
            // 
            this.tabcontrol.Controls.Add(this.tabsettingpage);
            this.tabcontrol.Controls.Add(this.GeneralPage);
            this.tabcontrol.Controls.Add(this.EditingPage);
            this.tabcontrol.Controls.Add(this.FileExtensionsPage);
            this.tabcontrol.Controls.Add(this.ClearPage);
            this.tabcontrol.Controls.Add(this.Plugins);
            this.tabcontrol.Controls.Add(this.keybindingstab);
            this.tabcontrol.Location = new System.Drawing.Point(180, 4);
            this.tabcontrol.Multiline = true;
            this.tabcontrol.Name = "tabcontrol";
            this.tabcontrol.SelectedIndex = 0;
            this.tabcontrol.Size = new System.Drawing.Size(448, 345);
            this.tabcontrol.TabIndex = 1;
            // 
            // tabsettingpage
            // 
            this.tabsettingpage.BackColor = System.Drawing.SystemColors.Control;
            this.tabsettingpage.Controls.Add(this.groupBox1);
            this.tabsettingpage.Location = new System.Drawing.Point(4, 22);
            this.tabsettingpage.Name = "tabsettingpage";
            this.tabsettingpage.Padding = new System.Windows.Forms.Padding(3);
            this.tabsettingpage.Size = new System.Drawing.Size(440, 319);
            this.tabsettingpage.TabIndex = 0;
            this.tabsettingpage.Text = "Tabs";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbdockstyle);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tablocation);
            this.groupBox1.Location = new System.Drawing.Point(44, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 148);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // cbdockstyle
            // 
            this.cbdockstyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdockstyle.FormattingEnabled = true;
            this.cbdockstyle.Items.AddRange(new object[] {
            "DockingSdi",
            "DockingMdi"});
            this.cbdockstyle.Location = new System.Drawing.Point(113, 70);
            this.cbdockstyle.Name = "cbdockstyle";
            this.cbdockstyle.Size = new System.Drawing.Size(189, 21);
            this.cbdockstyle.TabIndex = 10;
            this.cbdockstyle.SelectedIndexChanged += new System.EventHandler(this.cbdockstyle_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Dock Style : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tab Location : ";
            // 
            // tablocation
            // 
            this.tablocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tablocation.FormattingEnabled = true;
            this.tablocation.Items.AddRange(new object[] {
            "Top",
            "Bottom"});
            this.tablocation.Location = new System.Drawing.Point(113, 38);
            this.tablocation.Name = "tablocation";
            this.tablocation.Size = new System.Drawing.Size(189, 21);
            this.tablocation.TabIndex = 6;
            this.tablocation.SelectedIndexChanged += new System.EventHandler(this.tablocation_SelectedIndexChanged);
            // 
            // GeneralPage
            // 
            this.GeneralPage.BackColor = System.Drawing.SystemColors.Control;
            this.GeneralPage.Controls.Add(this.groupBox7);
            this.GeneralPage.Location = new System.Drawing.Point(4, 22);
            this.GeneralPage.Name = "GeneralPage";
            this.GeneralPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralPage.Size = new System.Drawing.Size(440, 319);
            this.GeneralPage.TabIndex = 2;
            this.GeneralPage.Text = "General";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.virtualspace);
            this.groupBox7.Controls.Add(this.highlightfoliding);
            this.groupBox7.Controls.Add(this.checkBox1);
            this.groupBox7.Controls.Add(this.comboBox2);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.comboBox1);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.ShowLineNumber);
            this.groupBox7.Controls.Add(this.cmbwordwrapmode);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.showcaret);
            this.groupBox7.Controls.Add(this.showfoldinglines);
            this.groupBox7.Location = new System.Drawing.Point(16, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(408, 283);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Text Editor";
            // 
            // virtualspace
            // 
            this.virtualspace.AutoSize = true;
            this.virtualspace.Location = new System.Drawing.Point(192, 55);
            this.virtualspace.Name = "virtualspace";
            this.virtualspace.Size = new System.Drawing.Size(125, 17);
            this.virtualspace.TabIndex = 19;
            this.virtualspace.Text = "Enable Virtual Space";
            this.virtualspace.UseVisualStyleBackColor = true;
            this.virtualspace.CheckedChanged += new System.EventHandler(this.virtualspace_CheckedChanged);
            // 
            // highlightfoliding
            // 
            this.highlightfoliding.AutoSize = true;
            this.highlightfoliding.Checked = true;
            this.highlightfoliding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.highlightfoliding.Location = new System.Drawing.Point(192, 32);
            this.highlightfoliding.Name = "highlightfoliding";
            this.highlightfoliding.Size = new System.Drawing.Size(148, 17);
            this.highlightfoliding.TabIndex = 18;
            this.highlightfoliding.Text = "Highlight Folding Indicator";
            this.highlightfoliding.UseVisualStyleBackColor = true;
            this.highlightfoliding.CheckedChanged += new System.EventHandler(this.highlightfoliding_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(20, 101);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(129, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "Show Document Map";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Strategy1",
            "Strategy2"});
            this.comboBox2.Location = new System.Drawing.Point(162, 217);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(179, 21);
            this.comboBox2.TabIndex = 16;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Bracket Highlight Stratergy :";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Strategy1"});
            this.comboBox1.Location = new System.Drawing.Point(125, 180);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(216, 21);
            this.comboBox1.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Folding Strategy : ";
            // 
            // ShowLineNumber
            // 
            this.ShowLineNumber.AutoSize = true;
            this.ShowLineNumber.Checked = true;
            this.ShowLineNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowLineNumber.Location = new System.Drawing.Point(20, 78);
            this.ShowLineNumber.Name = "ShowLineNumber";
            this.ShowLineNumber.Size = new System.Drawing.Size(121, 17);
            this.ShowLineNumber.TabIndex = 12;
            this.ShowLineNumber.Text = "Show Line Numbers";
            this.ShowLineNumber.UseVisualStyleBackColor = true;
            this.ShowLineNumber.CheckedChanged += new System.EventHandler(this.ShowLineNumber_CheckedChanged);
            // 
            // cmbwordwrapmode
            // 
            this.cmbwordwrapmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbwordwrapmode.FormattingEnabled = true;
            this.cmbwordwrapmode.Location = new System.Drawing.Point(125, 143);
            this.cmbwordwrapmode.Name = "cmbwordwrapmode";
            this.cmbwordwrapmode.Size = new System.Drawing.Size(216, 21);
            this.cmbwordwrapmode.TabIndex = 5;
            this.cmbwordwrapmode.SelectedIndexChanged += new System.EventHandler(this.cmbwordwrapmode_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Word Wrap Mode : ";
            // 
            // showcaret
            // 
            this.showcaret.AutoSize = true;
            this.showcaret.Checked = true;
            this.showcaret.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showcaret.Location = new System.Drawing.Point(20, 55);
            this.showcaret.Name = "showcaret";
            this.showcaret.Size = new System.Drawing.Size(81, 17);
            this.showcaret.TabIndex = 1;
            this.showcaret.Text = "Show Caret";
            this.showcaret.UseVisualStyleBackColor = true;
            this.showcaret.CheckedChanged += new System.EventHandler(this.showcaret_CheckedChanged);
            // 
            // showfoldinglines
            // 
            this.showfoldinglines.AutoSize = true;
            this.showfoldinglines.Checked = true;
            this.showfoldinglines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showfoldinglines.Location = new System.Drawing.Point(20, 32);
            this.showfoldinglines.Name = "showfoldinglines";
            this.showfoldinglines.Size = new System.Drawing.Size(118, 17);
            this.showfoldinglines.TabIndex = 0;
            this.showfoldinglines.Text = "Show Folding Lines";
            this.showfoldinglines.UseVisualStyleBackColor = true;
            this.showfoldinglines.CheckedChanged += new System.EventHandler(this.showfoldinglines_CheckedChanged);
            // 
            // EditingPage
            // 
            this.EditingPage.BackColor = System.Drawing.SystemColors.Control;
            this.EditingPage.Controls.Add(this.groupBox10);
            this.EditingPage.Location = new System.Drawing.Point(4, 22);
            this.EditingPage.Name = "EditingPage";
            this.EditingPage.Padding = new System.Windows.Forms.Padding(3);
            this.EditingPage.Size = new System.Drawing.Size(440, 319);
            this.EditingPage.TabIndex = 3;
            this.EditingPage.Text = "Editing";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label3);
            this.groupBox10.Controls.Add(this.tblineinterval);
            this.groupBox10.Controls.Add(this.tbpaddingwidth);
            this.groupBox10.Controls.Add(this.label17);
            this.groupBox10.Controls.Add(this.label14);
            this.groupBox10.Controls.Add(this.label16);
            this.groupBox10.Location = new System.Drawing.Point(113, 67);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(209, 146);
            this.groupBox10.TabIndex = 10;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "General";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "pixels";
            // 
            // tblineinterval
            // 
            this.tblineinterval.AllowSpace = false;
            this.tblineinterval.Location = new System.Drawing.Point(66, 63);
            this.tblineinterval.Name = "tblineinterval";
            this.tblineinterval.Size = new System.Drawing.Size(65, 20);
            this.tblineinterval.TabIndex = 2;
            // 
            // tbpaddingwidth
            // 
            this.tbpaddingwidth.AllowSpace = false;
            this.tbpaddingwidth.Location = new System.Drawing.Point(95, 34);
            this.tbpaddingwidth.Name = "tbpaddingwidth";
            this.tbpaddingwidth.Size = new System.Drawing.Size(38, 20);
            this.tbpaddingwidth.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(137, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "pixels";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 37);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Padding Width : ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 70);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Interval : ";
            // 
            // FileExtensionsPage
            // 
            this.FileExtensionsPage.BackColor = System.Drawing.SystemColors.Control;
            this.FileExtensionsPage.Controls.Add(this.groupBox16);
            this.FileExtensionsPage.Location = new System.Drawing.Point(4, 22);
            this.FileExtensionsPage.Name = "FileExtensionsPage";
            this.FileExtensionsPage.Padding = new System.Windows.Forms.Padding(3);
            this.FileExtensionsPage.Size = new System.Drawing.Size(440, 319);
            this.FileExtensionsPage.TabIndex = 5;
            this.FileExtensionsPage.Text = "File Extensions";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.linkLabel6);
            this.groupBox16.Controls.Add(this.label22);
            this.groupBox16.Controls.Add(this.listBox1);
            this.groupBox16.Controls.Add(this.listBox2);
            this.groupBox16.Controls.Add(this.label21);
            this.groupBox16.Location = new System.Drawing.Point(6, 6);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(452, 303);
            this.groupBox16.TabIndex = 5;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "File Extensions";
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Location = new System.Drawing.Point(24, 264);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(179, 13);
            this.linkLabel6.TabIndex = 4;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "Edit Languages and their Extensions";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(240, 30);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Extension";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(18, 46);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(155, 199);
            this.listBox1.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(243, 46);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(167, 199);
            this.listBox2.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(15, 30);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(55, 13);
            this.label21.TabIndex = 2;
            this.label21.Text = "Language";
            // 
            // ClearPage
            // 
            this.ClearPage.BackColor = System.Drawing.SystemColors.Control;
            this.ClearPage.Controls.Add(this.groupBox14);
            this.ClearPage.Location = new System.Drawing.Point(4, 22);
            this.ClearPage.Name = "ClearPage";
            this.ClearPage.Padding = new System.Windows.Forms.Padding(3);
            this.ClearPage.Size = new System.Drawing.Size(440, 319);
            this.ClearPage.TabIndex = 7;
            this.ClearPage.Text = "Clear Data";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.button6);
            this.groupBox14.Controls.Add(this.button5);
            this.groupBox14.Controls.Add(this.button3);
            this.groupBox14.Location = new System.Drawing.Point(22, 6);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(394, 200);
            this.groupBox14.TabIndex = 6;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Clear";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(110, 95);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(185, 50);
            this.button6.TabIndex = 3;
            this.button6.Text = "Restore to Default Settings";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(203, 28);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(185, 50);
            this.button5.TabIndex = 2;
            this.button5.Text = "Clear Recent Files";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(185, 46);
            this.button3.TabIndex = 0;
            this.button3.Text = "Clear Application Data";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Plugins
            // 
            this.Plugins.BackColor = System.Drawing.SystemColors.Control;
            this.Plugins.Controls.Add(this.groupBox4);
            this.Plugins.Controls.Add(this.groupBox3);
            this.Plugins.Location = new System.Drawing.Point(4, 22);
            this.Plugins.Name = "Plugins";
            this.Plugins.Padding = new System.Windows.Forms.Padding(3);
            this.Plugins.Size = new System.Drawing.Size(440, 319);
            this.Plugins.TabIndex = 8;
            this.Plugins.Text = "Plugins";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.linkLabel3);
            this.groupBox4.Controls.Add(this.linkLabel2);
            this.groupBox4.Controls.Add(this.linkLabel1);
            this.groupBox4.Location = new System.Drawing.Point(97, 143);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(239, 110);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Resources";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(81, 66);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(64, 13);
            this.linkLabel3.TabIndex = 2;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Plugin Docs";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(143, 33);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(70, 13);
            this.linkLabel2.TabIndex = 1;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Create Plugin";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(19, 33);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(93, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Ynote Plugins Site";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox5);
            this.groupBox3.Location = new System.Drawing.Point(66, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 110);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "General";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(83, 47);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(132, 17);
            this.checkBox5.TabIndex = 0;
            this.checkBox5.Text = "Enable Plugin Loading";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // keybindingstab
            // 
            this.keybindingstab.BackColor = System.Drawing.SystemColors.Control;
            this.keybindingstab.Controls.Add(this.label7);
            this.keybindingstab.Controls.Add(this.linkLabel4);
            this.keybindingstab.Controls.Add(this.comboBox3);
            this.keybindingstab.Controls.Add(this.label6);
            this.keybindingstab.Location = new System.Drawing.Point(4, 22);
            this.keybindingstab.Name = "keybindingstab";
            this.keybindingstab.Padding = new System.Windows.Forms.Padding(3);
            this.keybindingstab.Size = new System.Drawing.Size(440, 319);
            this.keybindingstab.TabIndex = 9;
            this.keybindingstab.Text = "keybindings";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(188, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "OR";
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.Location = new System.Drawing.Point(144, 159);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(128, 17);
            this.linkLabel4.TabIndex = 2;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Import Keymap File";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(165, 73);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(217, 21);
            this.comboBox3.TabIndex = 1;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Switch KeyBindings : ";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 386);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabcontrol);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tabcontrol.ResumeLayout(false);
            this.tabsettingpage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GeneralPage.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.EditingPage.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.FileExtensionsPage.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.ClearPage.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.Plugins.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.keybindingstab.ResumeLayout(false);
            this.keybindingstab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private WizardTabControl tabcontrol;
        private System.Windows.Forms.TabPage tabsettingpage;
        private System.Windows.Forms.TabPage GeneralPage;
        private System.Windows.Forms.TabPage EditingPage;
        private System.Windows.Forms.TabPage FileExtensionsPage;
        private System.Windows.Forms.TabPage ClearPage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tablocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ShowLineNumber;
        private System.Windows.Forms.ComboBox cmbwordwrapmode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox showcaret;
        private System.Windows.Forms.CheckBox showfoldinglines;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbdockstyle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox highlightfoliding;
        private NumericTextBox tblineinterval;
        private NumericTextBox tbpaddingwidth;
        private System.Windows.Forms.CheckBox virtualspace;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TabPage Plugins;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage keybindingstab;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label6;
    }
}