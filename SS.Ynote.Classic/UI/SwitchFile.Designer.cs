namespace SS.Ynote.Classic.UI
{
    partial class SwitchFile
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.completemenu = new AutocompleteMenuNS.AutocompleteMenu();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.completemenu.SetAutocompleteMenu(this.textBox1, this.completemenu);
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textBox1.Location = new System.Drawing.Point(1, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(502, 29);
            this.textBox1.TabIndex = 0;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.KeyDown += this.textBox1_KeyDown;
            // 
            // completemenu
            // 
            this.completemenu.AppearInterval = 20;
            this.completemenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.completemenu.ImageList = null;
            this.completemenu.Items = new string[0];
            this.completemenu.LeftPadding = 10;
            this.completemenu.MaximumSize = new System.Drawing.Size(503, 200);
            this.completemenu.SearchPattern = ".*";
            this.completemenu.TargetControlWrapper = null;
            // 
            // SwitchFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 29);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SwitchFile";
            this.Text = "Console";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private AutocompleteMenuNS.AutocompleteMenu completemenu;
    }
}