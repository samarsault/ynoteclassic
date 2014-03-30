namespace SS.Ynote.Classic.UI
{
    partial class FindInFiles
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpFind = new System.Windows.Forms.TabPage();
            this.cbCase = new System.Windows.Forms.CheckBox();
            this.cbRegex = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.cmbsearchoptions = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtdir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtstring = new System.Windows.Forms.TextBox();
            this.tpReplace = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.tbReplaceWith = new System.Windows.Forms.TextBox();
            this.cbReplaceICase = new System.Windows.Forms.CheckBox();
            this.cbReplaceRegex = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbReplaceFilter = new System.Windows.Forms.TextBox();
            this.cbSearchIn = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbReplaceDir = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbReplaceFind = new System.Windows.Forms.TextBox();
            this.tpResults = new System.Windows.Forms.TabPage();
            this.lvresults = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tpFind.SuspendLayout();
            this.tpReplace.SuspendLayout();
            this.tpResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpFind);
            this.tabControl1.Controls.Add(this.tpReplace);
            this.tabControl1.Controls.Add(this.tpResults);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(669, 197);
            this.tabControl1.TabIndex = 0;
            // 
            // tpFind
            // 
            this.tpFind.Controls.Add(this.cbCase);
            this.tpFind.Controls.Add(this.cbRegex);
            this.tpFind.Controls.Add(this.label8);
            this.tpFind.Controls.Add(this.textBox5);
            this.tpFind.Controls.Add(this.cmbsearchoptions);
            this.tpFind.Controls.Add(this.label4);
            this.tpFind.Controls.Add(this.btnFind);
            this.tpFind.Controls.Add(this.button1);
            this.tpFind.Controls.Add(this.txtdir);
            this.tpFind.Controls.Add(this.label2);
            this.tpFind.Controls.Add(this.label1);
            this.tpFind.Controls.Add(this.txtstring);
            this.tpFind.Location = new System.Drawing.Point(4, 22);
            this.tpFind.Name = "tpFind";
            this.tpFind.Padding = new System.Windows.Forms.Padding(3);
            this.tpFind.Size = new System.Drawing.Size(661, 171);
            this.tpFind.TabIndex = 0;
            this.tpFind.Text = "Find In Files";
            this.tpFind.UseVisualStyleBackColor = true;
            // 
            // cbCase
            // 
            this.cbCase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCase.AutoSize = true;
            this.cbCase.Location = new System.Drawing.Point(13, 134);
            this.cbCase.Name = "cbCase";
            this.cbCase.Size = new System.Drawing.Size(83, 17);
            this.cbCase.TabIndex = 16;
            this.cbCase.Text = "Ignore Case";
            this.cbCase.UseVisualStyleBackColor = true;
            // 
            // cbRegex
            // 
            this.cbRegex.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbRegex.AutoSize = true;
            this.cbRegex.Location = new System.Drawing.Point(102, 134);
            this.cbRegex.Name = "cbRegex";
            this.cbRegex.Size = new System.Drawing.Size(57, 17);
            this.cbRegex.TabIndex = 12;
            this.cbRegex.Text = "Regex";
            this.cbRegex.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Filter : ";
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.Location = new System.Drawing.Point(91, 69);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(510, 20);
            this.textBox5.TabIndex = 10;
            this.textBox5.Text = "*.*";
            // 
            // cmbsearchoptions
            // 
            this.cmbsearchoptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbsearchoptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsearchoptions.FormattingEnabled = true;
            this.cmbsearchoptions.Location = new System.Drawing.Point(91, 95);
            this.cmbsearchoptions.Name = "cmbsearchoptions";
            this.cmbsearchoptions.Size = new System.Drawing.Size(510, 21);
            this.cmbsearchoptions.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Search In : ";
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(512, 127);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(89, 29);
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(574, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtdir
            // 
            this.txtdir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdir.Location = new System.Drawing.Point(91, 43);
            this.txtdir.Name = "txtdir";
            this.txtdir.Size = new System.Drawing.Size(477, 20);
            this.txtdir.TabIndex = 3;
            this.txtdir.Text = "$docs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Directory : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find :";
            // 
            // txtstring
            // 
            this.txtstring.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtstring.Location = new System.Drawing.Point(91, 17);
            this.txtstring.Name = "txtstring";
            this.txtstring.Size = new System.Drawing.Size(510, 20);
            this.txtstring.TabIndex = 0;
            // 
            // tpReplace
            // 
            this.tpReplace.Controls.Add(this.label9);
            this.tpReplace.Controls.Add(this.tbReplaceWith);
            this.tpReplace.Controls.Add(this.cbReplaceICase);
            this.tpReplace.Controls.Add(this.cbReplaceRegex);
            this.tpReplace.Controls.Add(this.label3);
            this.tpReplace.Controls.Add(this.tbReplaceFilter);
            this.tpReplace.Controls.Add(this.cbSearchIn);
            this.tpReplace.Controls.Add(this.label5);
            this.tpReplace.Controls.Add(this.btnReplace);
            this.tpReplace.Controls.Add(this.btnBrowse);
            this.tpReplace.Controls.Add(this.tbReplaceDir);
            this.tpReplace.Controls.Add(this.label6);
            this.tpReplace.Controls.Add(this.label7);
            this.tpReplace.Controls.Add(this.tbReplaceFind);
            this.tpReplace.Location = new System.Drawing.Point(4, 22);
            this.tpReplace.Name = "tpReplace";
            this.tpReplace.Padding = new System.Windows.Forms.Padding(3);
            this.tpReplace.Size = new System.Drawing.Size(661, 171);
            this.tpReplace.TabIndex = 2;
            this.tpReplace.Text = "Replace In Files";
            this.tpReplace.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Replace With :";
            // 
            // tbReplaceWith
            // 
            this.tbReplaceWith.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReplaceWith.Location = new System.Drawing.Point(106, 42);
            this.tbReplaceWith.Name = "tbReplaceWith";
            this.tbReplaceWith.Size = new System.Drawing.Size(531, 20);
            this.tbReplaceWith.TabIndex = 29;
            // 
            // cbReplaceICase
            // 
            this.cbReplaceICase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbReplaceICase.AutoSize = true;
            this.cbReplaceICase.Location = new System.Drawing.Point(28, 133);
            this.cbReplaceICase.Name = "cbReplaceICase";
            this.cbReplaceICase.Size = new System.Drawing.Size(83, 17);
            this.cbReplaceICase.TabIndex = 28;
            this.cbReplaceICase.Text = "Ignore Case";
            this.cbReplaceICase.UseVisualStyleBackColor = true;
            // 
            // cbReplaceRegex
            // 
            this.cbReplaceRegex.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbReplaceRegex.AutoSize = true;
            this.cbReplaceRegex.Location = new System.Drawing.Point(117, 133);
            this.cbReplaceRegex.Name = "cbReplaceRegex";
            this.cbReplaceRegex.Size = new System.Drawing.Size(57, 17);
            this.cbReplaceRegex.TabIndex = 27;
            this.cbReplaceRegex.Text = "Regex";
            this.cbReplaceRegex.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(479, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Filter : ";
            // 
            // tbReplaceFilter
            // 
            this.tbReplaceFilter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tbReplaceFilter.Location = new System.Drawing.Point(515, 70);
            this.tbReplaceFilter.Name = "tbReplaceFilter";
            this.tbReplaceFilter.Size = new System.Drawing.Size(122, 20);
            this.tbReplaceFilter.TabIndex = 25;
            this.tbReplaceFilter.Text = "*.*";
            // 
            // cbSearchIn
            // 
            this.cbSearchIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSearchIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchIn.FormattingEnabled = true;
            this.cbSearchIn.Location = new System.Drawing.Point(106, 97);
            this.cbSearchIn.Name = "cbSearchIn";
            this.cbSearchIn.Size = new System.Drawing.Size(531, 21);
            this.cbSearchIn.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Search In : ";
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplace.Location = new System.Drawing.Point(548, 126);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(89, 29);
            this.btnReplace.TabIndex = 22;
            this.btnReplace.Text = "Replace All";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(445, 68);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(34, 23);
            this.btnBrowse.TabIndex = 21;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbReplaceDir
            // 
            this.tbReplaceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReplaceDir.Location = new System.Drawing.Point(104, 70);
            this.tbReplaceDir.Name = "tbReplaceDir";
            this.tbReplaceDir.Size = new System.Drawing.Size(335, 20);
            this.tbReplaceDir.TabIndex = 20;
            this.tbReplaceDir.Text = "$docs";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Directory : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Find :";
            // 
            // tbReplaceFind
            // 
            this.tbReplaceFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReplaceFind.Location = new System.Drawing.Point(106, 16);
            this.tbReplaceFind.Name = "tbReplaceFind";
            this.tbReplaceFind.Size = new System.Drawing.Size(531, 20);
            this.tbReplaceFind.TabIndex = 17;
            // 
            // tpResults
            // 
            this.tpResults.Controls.Add(this.lvresults);
            this.tpResults.Location = new System.Drawing.Point(4, 22);
            this.tpResults.Name = "tpResults";
            this.tpResults.Padding = new System.Windows.Forms.Padding(3);
            this.tpResults.Size = new System.Drawing.Size(661, 171);
            this.tpResults.TabIndex = 1;
            this.tpResults.Text = "Results";
            this.tpResults.UseVisualStyleBackColor = true;
            // 
            // lvresults
            // 
            this.lvresults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvresults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvresults.FullRowSelect = true;
            this.lvresults.GridLines = true;
            this.lvresults.Location = new System.Drawing.Point(3, 3);
            this.lvresults.Name = "lvresults";
            this.lvresults.Size = new System.Drawing.Size(655, 165);
            this.lvresults.TabIndex = 0;
            this.lvresults.UseCompatibleStateImageBehavior = false;
            this.lvresults.View = System.Windows.Forms.View.Details;
            this.lvresults.DoubleClick += new System.EventHandler(this.lvresults_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File";
            this.columnHeader1.Width = 561;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Line";
            this.columnHeader2.Width = 71;
            // 
            // FindInFiles
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 197);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FindInFiles";
            this.ShowIcon = false;
            this.Text = "Search In Files";
            this.tabControl1.ResumeLayout(false);
            this.tpFind.ResumeLayout(false);
            this.tpFind.PerformLayout();
            this.tpReplace.ResumeLayout(false);
            this.tpReplace.PerformLayout();
            this.tpResults.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpFind;
        private System.Windows.Forms.TabPage tpResults;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtdir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtstring;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ComboBox cmbsearchoptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ListView lvresults;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox cbRegex;
        private System.Windows.Forms.CheckBox cbCase;
        private System.Windows.Forms.TabPage tpReplace;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbReplaceWith;
        private System.Windows.Forms.CheckBox cbReplaceICase;
        private System.Windows.Forms.CheckBox cbReplaceRegex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbReplaceFilter;
        private System.Windows.Forms.ComboBox cbSearchIn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbReplaceDir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbReplaceFind;
    }
}