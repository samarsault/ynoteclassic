using SS.Ynote.Classic.UI.Controls;

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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Text Editor", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("File Extensions");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Encoding");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Files", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Manage");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Data", new System.Windows.Forms.TreeNode[] {
            treeNode9});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabcontrol = new SS.Ynote.Classic.UI.Controls.WizardTabControl();
            this.tabsettingpage = new System.Windows.Forms.TabPage();
            this.groupOther = new System.Windows.Forms.GroupBox();
            this.numrecent = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbdockstyle = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tablocation = new System.Windows.Forms.ComboBox();
            this.GeneralPage = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cbBrackets = new System.Windows.Forms.CheckBox();
            this.cbruler = new System.Windows.Forms.CheckBox();
            this.virtualspace = new System.Windows.Forms.CheckBox();
            this.highlightfoliding = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ShowLineNumber = new System.Windows.Forms.CheckBox();
            this.showcaret = new System.Windows.Forms.CheckBox();
            this.showfoldinglines = new System.Windows.Forms.CheckBox();
            this.EditingPage = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tabsize = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tblineinterval = new SS.Ynote.Classic.UI.Controls.NumericTextBox();
            this.tbpaddingwidth = new SS.Ynote.Classic.UI.Controls.NumericTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.FileExtensionsPage = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.label22 = new System.Windows.Forms.Label();
            this.lstlang = new System.Windows.Forms.ListBox();
            this.lstextensions = new System.Windows.Forms.ListBox();
            this.label21 = new System.Windows.Forms.Label();
            this.ClearPage = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btnScriptCache = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.encodingpage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblencoding = new System.Windows.Forms.Label();
            this.lstencs = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabcontrol.SuspendLayout();
            this.tabsettingpage.SuspendLayout();
            this.groupOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numrecent)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.GeneralPage.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.EditingPage.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabsize)).BeginInit();
            this.FileExtensionsPage.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.ClearPage.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.encodingpage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.FullRowSelect = true;
            this.treeView1.Indent = 27;
            this.treeView1.Location = new System.Drawing.Point(9, 36);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "TabsNode";
            treeNode1.Text = "Tabs";
            treeNode2.Name = "EnvironmentNode";
            treeNode2.Text = "Environment";
            treeNode3.Name = "TextEditorGeneralNode";
            treeNode3.Text = "General";
            treeNode4.Name = "TextEditorEditing";
            treeNode4.Text = "MISC";
            treeNode5.Name = "TextEditorNode";
            treeNode5.Text = "Text Editor";
            treeNode6.Name = "FileExtensionsNode";
            treeNode6.Text = "File Extensions";
            treeNode7.Name = "EncodingNode";
            treeNode7.Text = "Encoding";
            treeNode8.Name = "Node21";
            treeNode8.Text = "Files";
            treeNode9.Checked = true;
            treeNode9.Name = "ClearDataNode";
            treeNode9.Text = "Manage";
            treeNode10.Name = "DataNode";
            treeNode10.Text = "Data";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode5,
            treeNode8,
            treeNode10});
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(169, 313);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(458, 355);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(546, 355);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "      Options";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Location = new System.Drawing.Point(-2, 351);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 36);
            this.panel1.TabIndex = 5;
            // 
            // tabcontrol
            // 
            this.tabcontrol.Controls.Add(this.tabsettingpage);
            this.tabcontrol.Controls.Add(this.GeneralPage);
            this.tabcontrol.Controls.Add(this.EditingPage);
            this.tabcontrol.Controls.Add(this.FileExtensionsPage);
            this.tabcontrol.Controls.Add(this.ClearPage);
            this.tabcontrol.Controls.Add(this.encodingpage);
            this.tabcontrol.Location = new System.Drawing.Point(180, 4);
            this.tabcontrol.Multiline = true;
            this.tabcontrol.Name = "tabcontrol";
            this.tabcontrol.SelectedIndex = 0;
            this.tabcontrol.Size = new System.Drawing.Size(455, 345);
            this.tabcontrol.TabIndex = 1;
            // 
            // tabsettingpage
            // 
            this.tabsettingpage.BackColor = System.Drawing.SystemColors.Control;
            this.tabsettingpage.Controls.Add(this.groupOther);
            this.tabsettingpage.Controls.Add(this.groupBox1);
            this.tabsettingpage.Location = new System.Drawing.Point(4, 22);
            this.tabsettingpage.Name = "tabsettingpage";
            this.tabsettingpage.Padding = new System.Windows.Forms.Padding(3);
            this.tabsettingpage.Size = new System.Drawing.Size(447, 319);
            this.tabsettingpage.TabIndex = 0;
            this.tabsettingpage.Text = "Tabs";
            // 
            // groupOther
            // 
            this.groupOther.Controls.Add(this.numrecent);
            this.groupOther.Controls.Add(this.label6);
            this.groupOther.Location = new System.Drawing.Point(122, 183);
            this.groupOther.Name = "groupOther";
            this.groupOther.Size = new System.Drawing.Size(234, 91);
            this.groupOther.TabIndex = 9;
            this.groupOther.TabStop = false;
            this.groupOther.Text = "Other";
            // 
            // numrecent
            // 
            this.numrecent.Location = new System.Drawing.Point(149, 37);
            this.numrecent.Name = "numrecent";
            this.numrecent.Size = new System.Drawing.Size(47, 20);
            this.numrecent.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "No. of Recent Files : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbdockstyle);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tablocation);
            this.groupBox1.Location = new System.Drawing.Point(15, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 148);
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
            this.cbdockstyle.Location = new System.Drawing.Point(152, 73);
            this.cbdockstyle.Name = "cbdockstyle";
            this.cbdockstyle.Size = new System.Drawing.Size(189, 21);
            this.cbdockstyle.TabIndex = 10;
            this.cbdockstyle.SelectedIndexChanged += new System.EventHandler(this.cbdockstyle_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Dock Style : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 42);
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
            this.tablocation.Location = new System.Drawing.Point(152, 39);
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
            this.GeneralPage.Size = new System.Drawing.Size(447, 319);
            this.GeneralPage.TabIndex = 2;
            this.GeneralPage.Text = "General";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cbBrackets);
            this.groupBox7.Controls.Add(this.cbruler);
            this.groupBox7.Controls.Add(this.virtualspace);
            this.groupBox7.Controls.Add(this.highlightfoliding);
            this.groupBox7.Controls.Add(this.checkBox1);
            this.groupBox7.Controls.Add(this.comboBox2);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.comboBox1);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.ShowLineNumber);
            this.groupBox7.Controls.Add(this.showcaret);
            this.groupBox7.Controls.Add(this.showfoldinglines);
            this.groupBox7.Location = new System.Drawing.Point(16, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(408, 283);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Text Editor";
            // 
            // cbBrackets
            // 
            this.cbBrackets.AutoSize = true;
            this.cbBrackets.Checked = true;
            this.cbBrackets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBrackets.Location = new System.Drawing.Point(193, 101);
            this.cbBrackets.Name = "cbBrackets";
            this.cbBrackets.Size = new System.Drawing.Size(137, 17);
            this.cbBrackets.TabIndex = 21;
            this.cbBrackets.Text = "AutoComplete Brackets";
            this.cbBrackets.UseVisualStyleBackColor = true;
            this.cbBrackets.CheckedChanged += new System.EventHandler(this.cbBrackets_CheckedChanged);
            // 
            // cbruler
            // 
            this.cbruler.AutoSize = true;
            this.cbruler.Location = new System.Drawing.Point(193, 32);
            this.cbruler.Name = "cbruler";
            this.cbruler.Size = new System.Drawing.Size(81, 17);
            this.cbruler.TabIndex = 20;
            this.cbruler.Text = "Show Ruler";
            this.cbruler.UseVisualStyleBackColor = true;
            this.cbruler.CheckedChanged += new System.EventHandler(this.cbruler_CheckedChanged);
            // 
            // virtualspace
            // 
            this.virtualspace.AutoSize = true;
            this.virtualspace.Location = new System.Drawing.Point(193, 78);
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
            this.highlightfoliding.Location = new System.Drawing.Point(193, 55);
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
            this.comboBox2.Location = new System.Drawing.Point(162, 183);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(179, 21);
            this.comboBox2.TabIndex = 16;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 186);
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
            "Strategy1",
            "Strategy2"});
            this.comboBox1.Location = new System.Drawing.Point(125, 146);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(216, 21);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 149);
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
            this.EditingPage.Size = new System.Drawing.Size(447, 319);
            this.EditingPage.TabIndex = 3;
            this.EditingPage.Text = "Editing";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.tabsize);
            this.groupBox10.Controls.Add(this.label8);
            this.groupBox10.Controls.Add(this.label3);
            this.groupBox10.Controls.Add(this.tblineinterval);
            this.groupBox10.Controls.Add(this.tbpaddingwidth);
            this.groupBox10.Controls.Add(this.label17);
            this.groupBox10.Controls.Add(this.label14);
            this.groupBox10.Controls.Add(this.label16);
            this.groupBox10.Location = new System.Drawing.Point(15, 10);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(398, 148);
            this.groupBox10.TabIndex = 10;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "General";
            // 
            // tabsize
            // 
            this.tabsize.Location = new System.Drawing.Point(295, 38);
            this.tabsize.Name = "tabsize";
            this.tabsize.Size = new System.Drawing.Size(43, 20);
            this.tabsize.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(231, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Tab Size : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "pixels";
            // 
            // tblineinterval
            // 
            this.tblineinterval.AllowSpace = false;
            this.tblineinterval.Location = new System.Drawing.Point(84, 75);
            this.tblineinterval.Name = "tblineinterval";
            this.tblineinterval.Size = new System.Drawing.Size(65, 20);
            this.tblineinterval.TabIndex = 2;
            // 
            // tbpaddingwidth
            // 
            this.tbpaddingwidth.AllowSpace = false;
            this.tbpaddingwidth.Location = new System.Drawing.Point(113, 42);
            this.tbpaddingwidth.Name = "tbpaddingwidth";
            this.tbpaddingwidth.Size = new System.Drawing.Size(38, 20);
            this.tbpaddingwidth.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(155, 78);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "pixels";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(27, 45);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Padding Width : ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(27, 78);
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
            this.FileExtensionsPage.Size = new System.Drawing.Size(447, 319);
            this.FileExtensionsPage.TabIndex = 5;
            this.FileExtensionsPage.Text = "File Extensions";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.linkLabel6);
            this.groupBox16.Controls.Add(this.label22);
            this.groupBox16.Controls.Add(this.lstlang);
            this.groupBox16.Controls.Add(this.lstextensions);
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
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
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
            // lstlang
            // 
            this.lstlang.FormattingEnabled = true;
            this.lstlang.Location = new System.Drawing.Point(18, 46);
            this.lstlang.Name = "lstlang";
            this.lstlang.Size = new System.Drawing.Size(155, 199);
            this.lstlang.TabIndex = 0;
            // 
            // lstextensions
            // 
            this.lstextensions.FormattingEnabled = true;
            this.lstextensions.Location = new System.Drawing.Point(243, 46);
            this.lstextensions.Name = "lstextensions";
            this.lstextensions.Size = new System.Drawing.Size(167, 199);
            this.lstextensions.TabIndex = 1;
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
            this.ClearPage.Size = new System.Drawing.Size(447, 319);
            this.ClearPage.TabIndex = 7;
            this.ClearPage.Text = "Clear Data";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.btnScriptCache);
            this.groupBox14.Controls.Add(this.button6);
            this.groupBox14.Controls.Add(this.button5);
            this.groupBox14.Location = new System.Drawing.Point(24, 48);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(394, 182);
            this.groupBox14.TabIndex = 6;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Clear";
            // 
            // btnScriptCache
            // 
            this.btnScriptCache.Location = new System.Drawing.Point(12, 28);
            this.btnScriptCache.Name = "btnScriptCache";
            this.btnScriptCache.Size = new System.Drawing.Size(185, 50);
            this.btnScriptCache.TabIndex = 4;
            this.btnScriptCache.Text = "Clear Scripts Cache";
            this.btnScriptCache.UseVisualStyleBackColor = true;
            this.btnScriptCache.Click += new System.EventHandler(this.btnScriptCache_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(108, 84);
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
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // encodingpage
            // 
            this.encodingpage.BackColor = System.Drawing.SystemColors.Control;
            this.encodingpage.Controls.Add(this.groupBox2);
            this.encodingpage.Location = new System.Drawing.Point(4, 22);
            this.encodingpage.Name = "encodingpage";
            this.encodingpage.Padding = new System.Windows.Forms.Padding(3);
            this.encodingpage.Size = new System.Drawing.Size(447, 319);
            this.encodingpage.TabIndex = 8;
            this.encodingpage.Text = "Encoding";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblencoding);
            this.groupBox2.Controls.Add(this.lstencs);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(26, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 263);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Encoding Settings";
            // 
            // lblencoding
            // 
            this.lblencoding.AutoSize = true;
            this.lblencoding.Location = new System.Drawing.Point(131, 39);
            this.lblencoding.Name = "lblencoding";
            this.lblencoding.Size = new System.Drawing.Size(0, 13);
            this.lblencoding.TabIndex = 2;
            // 
            // lstencs
            // 
            this.lstencs.FormattingEnabled = true;
            this.lstencs.Location = new System.Drawing.Point(30, 74);
            this.lstencs.Name = "lstencs";
            this.lstencs.Size = new System.Drawing.Size(336, 173);
            this.lstencs.TabIndex = 1;
            this.lstencs.DoubleClick += new System.EventHandler(this.lstencs_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Default Encoding : ";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 386);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabcontrol);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tabcontrol.ResumeLayout(false);
            this.tabsettingpage.ResumeLayout(false);
            this.groupOther.ResumeLayout(false);
            this.groupOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numrecent)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GeneralPage.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.EditingPage.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabsize)).EndInit();
            this.FileExtensionsPage.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.ClearPage.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.encodingpage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tablocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ShowLineNumber;
        private System.Windows.Forms.CheckBox showcaret;
        private System.Windows.Forms.CheckBox showfoldinglines;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ListBox lstlang;
        private System.Windows.Forms.ListBox lstextensions;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Button button5;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown tabsize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TreeNode treeNode1;
        private System.Windows.Forms.TreeNode treeNode2;
        private System.Windows.Forms.TreeNode treeNode3;
        private System.Windows.Forms.TreeNode treeNode4;
        private System.Windows.Forms.TreeNode treeNode5;
        private System.Windows.Forms.TreeNode treeNode6;
        private System.Windows.Forms.TreeNode treeNode7;
        private System.Windows.Forms.TreeNode treeNode8;
        private System.Windows.Forms.TreeNode treeNode9;
        private System.Windows.Forms.TreeNode treeNode10;
        private System.Windows.Forms.TreeNode treeNode11;
        private System.Windows.Forms.CheckBox cbruler;
        private System.Windows.Forms.CheckBox cbBrackets;
        private System.Windows.Forms.Button btnScriptCache;
        private System.Windows.Forms.GroupBox groupOther;
        private System.Windows.Forms.NumericUpDown numrecent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage encodingpage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lstencs;
        private System.Windows.Forms.Label lblencoding;
    }
}