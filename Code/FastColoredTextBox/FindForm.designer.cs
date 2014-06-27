namespace FastColoredTextBoxNS
{
    partial class FindForm
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
            this.btReplaceAll = new System.Windows.Forms.Button();
            this.btReplace = new System.Windows.Forms.Button();
            this.btHighlightAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btFindNext = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMatchCase = new System.Windows.Forms.CheckBox();
            this.cbWholeWord = new System.Windows.Forms.CheckBox();
            this.cbRegex = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.tbReplace = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btReplaceAll
            // 
            this.btReplaceAll.Location = new System.Drawing.Point(294, 133);
            this.btReplaceAll.Name = "btReplaceAll";
            this.btReplaceAll.Size = new System.Drawing.Size(75, 23);
            this.btReplaceAll.TabIndex = 28;
            this.btReplaceAll.Text = "Replace all";
            this.btReplaceAll.UseVisualStyleBackColor = true;
            this.btReplaceAll.Click += new System.EventHandler(this.btReplaceAll_Click);
            // 
            // btReplace
            // 
            this.btReplace.Location = new System.Drawing.Point(213, 133);
            this.btReplace.Name = "btReplace";
            this.btReplace.Size = new System.Drawing.Size(75, 23);
            this.btReplace.TabIndex = 27;
            this.btReplace.Text = "Replace";
            this.btReplace.UseVisualStyleBackColor = true;
            this.btReplace.Click += new System.EventHandler(this.btReplace_Click);
            // 
            // btHighlightAll
            // 
            this.btHighlightAll.Location = new System.Drawing.Point(294, 104);
            this.btHighlightAll.Name = "btHighlightAll";
            this.btHighlightAll.Size = new System.Drawing.Size(75, 23);
            this.btHighlightAll.TabIndex = 25;
            this.btHighlightAll.Text = "Count";
            this.btHighlightAll.UseVisualStyleBackColor = true;
            this.btHighlightAll.Click += new System.EventHandler(this.btHighlightAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Find: ";
            // 
            // btFindNext
            // 
            this.btFindNext.Location = new System.Drawing.Point(213, 104);
            this.btFindNext.Name = "btFindNext";
            this.btFindNext.Size = new System.Drawing.Size(75, 23);
            this.btFindNext.TabIndex = 22;
            this.btFindNext.Text = "Find next";
            this.btFindNext.UseVisualStyleBackColor = true;
            this.btFindNext.Click += new System.EventHandler(btFindNext_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(294, 162);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 24;
            this.btClose.Text = "Cancel";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbMatchCase);
            this.groupBox1.Controls.Add(this.cbWholeWord);
            this.groupBox1.Controls.Add(this.cbRegex);
            this.groupBox1.Location = new System.Drawing.Point(12, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 95);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // cbMatchCase
            // 
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.Location = new System.Drawing.Point(24, 19);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new System.Drawing.Size(82, 17);
            this.cbMatchCase.TabIndex = 10;
            this.cbMatchCase.Text = "Match case";
            this.cbMatchCase.UseVisualStyleBackColor = true;
            this.cbMatchCase.CheckedChanged += new System.EventHandler(cbMatchCase_CheckedChanged);
            // 
            // cbWholeWord
            // 
            this.cbWholeWord.AutoSize = true;
            this.cbWholeWord.Location = new System.Drawing.Point(24, 42);
            this.cbWholeWord.Name = "cbWholeWord";
            this.cbWholeWord.Size = new System.Drawing.Size(113, 17);
            this.cbWholeWord.TabIndex = 11;
            this.cbWholeWord.Text = "Match whole word";
            this.cbWholeWord.UseVisualStyleBackColor = true;
            // 
            // cbRegex
            // 
            this.cbRegex.AutoSize = true;
            this.cbRegex.Location = new System.Drawing.Point(24, 65);
            this.cbRegex.Name = "cbRegex";
            this.cbRegex.Size = new System.Drawing.Size(57, 17);
            this.cbRegex.TabIndex = 12;
            this.cbRegex.Text = "Regex";
            this.cbRegex.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Replace: ";
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(63, 23);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(306, 21);
            this.tbFind.TabIndex = 31;
            this.tbFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbFind_KeyPress);
            // 
            // tbReplace
            // 
            this.tbReplace.Location = new System.Drawing.Point(63, 55);
            this.tbReplace.Name = "tbReplace";
            this.tbReplace.Size = new System.Drawing.Size(306, 21);
            this.tbReplace.TabIndex = 32;
            this.tbReplace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbReplace_KeyPress);
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(387, 198);
            this.Controls.Add(this.tbReplace);
            this.Controls.Add(this.tbFind);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btReplaceAll);
            this.Controls.Add(this.btReplace);
            this.Controls.Add(this.btHighlightAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btFindNext);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FindForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find and Replace";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btReplaceAll;
        private System.Windows.Forms.Button btReplace;
        private System.Windows.Forms.Button btHighlightAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btFindNext;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbMatchCase;
        private System.Windows.Forms.CheckBox cbWholeWord;
        private System.Windows.Forms.CheckBox cbRegex;
        private System.Windows.Forms.Label label2;
        public  System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.TextBox tbReplace;


    }
}