namespace SS.Ynote.Classic.Extensibility.Packages
{
    partial class PackageMaker
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtoutfile = new System.Windows.Forms.TextBox();
            this.outfilebtn = new System.Windows.Forms.Button();
            this.createpackagebtn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lstfiles = new System.Windows.Forms.ListView();
            this.File = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Output = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Output File :";
            // 
            // txtoutfile
            // 
            this.txtoutfile.Location = new System.Drawing.Point(110, 34);
            this.txtoutfile.Name = "txtoutfile";
            this.txtoutfile.Size = new System.Drawing.Size(379, 20);
            this.txtoutfile.TabIndex = 5;
            // 
            // outfilebtn
            // 
            this.outfilebtn.Location = new System.Drawing.Point(496, 32);
            this.outfilebtn.Name = "outfilebtn";
            this.outfilebtn.Size = new System.Drawing.Size(26, 23);
            this.outfilebtn.TabIndex = 9;
            this.outfilebtn.Text = "...";
            this.outfilebtn.UseVisualStyleBackColor = true;
            this.outfilebtn.Click += new System.EventHandler(this.outfilebtn_Click);
            // 
            // createpackagebtn
            // 
            this.createpackagebtn.Location = new System.Drawing.Point(404, 325);
            this.createpackagebtn.Name = "createpackagebtn";
            this.createpackagebtn.Size = new System.Drawing.Size(117, 27);
            this.createpackagebtn.TabIndex = 10;
            this.createpackagebtn.Text = "Create Package ";
            this.createpackagebtn.UseVisualStyleBackColor = true;
            this.createpackagebtn.Click += new System.EventHandler(this.createpackagebtn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(538, 325);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(95, 27);
            this.cancel_btn.TabIndex = 11;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtoutfile);
            this.groupBox1.Controls.Add(this.outfilebtn);
            this.groupBox1.Location = new System.Drawing.Point(20, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 79);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Package Details";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.lstfiles);
            this.groupBox2.Location = new System.Drawing.Point(20, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(613, 213);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Files";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(565, 60);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 34);
            this.button2.TabIndex = 2;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(565, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstfiles
            // 
            this.lstfiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.File,
            this.Output});
            this.lstfiles.FullRowSelect = true;
            this.lstfiles.GridLines = true;
            this.lstfiles.Location = new System.Drawing.Point(17, 20);
            this.lstfiles.Name = "lstfiles";
            this.lstfiles.Size = new System.Drawing.Size(532, 187);
            this.lstfiles.TabIndex = 0;
            this.lstfiles.UseCompatibleStateImageBehavior = false;
            this.lstfiles.View = System.Windows.Forms.View.Details;
            // 
            // File
            // 
            this.File.Text = "File";
            this.File.Width = 319;
            // 
            // Output
            // 
            this.Output.Text = "Out Location";
            this.Output.Width = 209;
            // 
            // PackageMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 365);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.createpackagebtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PackageMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Package Maker";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.PackageMaker_HelpButtonClicked);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtoutfile;
        private System.Windows.Forms.Button outfilebtn;
        private System.Windows.Forms.Button createpackagebtn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView lstfiles;
        private System.Windows.Forms.ColumnHeader File;
        private System.Windows.Forms.ColumnHeader Output;
    }
}