namespace SS.Ynote.Classic.Features.Search
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
            this.cbsubdir = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbReplace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCase = new System.Windows.Forms.CheckBox();
            this.cbRegex = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbdir = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbsubdir
            // 
            this.cbsubdir.AutoSize = true;
            this.cbsubdir.Checked = true;
            this.cbsubdir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbsubdir.Location = new System.Drawing.Point(321, 88);
            this.cbsubdir.Name = "cbsubdir";
            this.cbsubdir.Size = new System.Drawing.Size(82, 17);
            this.cbsubdir.TabIndex = 48;
            this.cbsubdir.Text = "Sub Folders";
            this.cbsubdir.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(328, 154);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 47;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(247, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 46;
            this.button2.Text = "Replace All";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbReplace
            // 
            this.tbReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReplace.Location = new System.Drawing.Point(86, 60);
            this.tbReplace.Name = "tbReplace";
            this.tbReplace.Size = new System.Drawing.Size(280, 20);
            this.tbReplace.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Replace : ";
            // 
            // cbCase
            // 
            this.cbCase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCase.AutoSize = true;
            this.cbCase.Location = new System.Drawing.Point(166, 38);
            this.cbCase.Name = "cbCase";
            this.cbCase.Size = new System.Drawing.Size(83, 17);
            this.cbCase.TabIndex = 43;
            this.cbCase.Text = "Ignore Case";
            this.cbCase.UseVisualStyleBackColor = true;
            // 
            // cbRegex
            // 
            this.cbRegex.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbRegex.AutoSize = true;
            this.cbRegex.Location = new System.Drawing.Point(86, 38);
            this.cbRegex.Name = "cbRegex";
            this.cbRegex.Size = new System.Drawing.Size(57, 17);
            this.cbRegex.TabIndex = 42;
            this.cbRegex.Text = "Regex";
            this.cbRegex.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Filter : ";
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(86, 112);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(89, 20);
            this.tbFilter.TabIndex = 40;
            this.tbFilter.Text = "*.*";
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(166, 154);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 39;
            this.btnFind.Text = "Find All";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(276, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbdir
            // 
            this.tbdir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbdir.Location = new System.Drawing.Point(86, 86);
            this.tbdir.Name = "tbdir";
            this.tbdir.Size = new System.Drawing.Size(184, 20);
            this.tbdir.TabIndex = 37;
            this.tbdir.Text = "$Open_Files";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Directory : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Find :";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(86, 12);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(280, 20);
            this.tbFind.TabIndex = 34;
            this.tbFind.KeyDown += tbFind_KeyDown;
            // 
            // FindInFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 189);
            this.Controls.Add(this.cbsubdir);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbReplace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbCase);
            this.Controls.Add(this.cbRegex);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbFilter);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbdir);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbFind);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FindInFiles";
            this.Text = "Find In Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.CheckBox cbsubdir;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbReplace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbCase;
        private System.Windows.Forms.CheckBox cbRegex;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbdir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbFind;

    }
}