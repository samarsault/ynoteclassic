namespace SS.Ynote.Classic.UI
{
    partial class CommandWindow
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
                this.completemenu.Items = null;
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
            this.tbCmd = new System.Windows.Forms.TextBox();
            this.completemenu = new AutocompleteMenuNS.AutocompleteMenu();
            this.SuspendLayout();
            // 
            // tbCmd
            // 
            this.completemenu.SetAutocompleteMenu(this.tbCmd, this.completemenu);
            this.tbCmd.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbCmd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCmd.Location = new System.Drawing.Point(1, 0);
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Size = new System.Drawing.Size(502, 29);
            this.tbCmd.TabIndex = 0;
            this.tbCmd.Click += new System.EventHandler(this.textBox1_Click);
            this.tbCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(tbCmd_KeyDown);
            // 
            // completemenu
            // 
            this.completemenu.AppearInterval = 20;
            this.completemenu.Font = new System.Drawing.Font("Segoe UI", 12F);
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
            this.Controls.Add(this.tbCmd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "SwitchFile";
            this.Text = "Console";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbCmd;
        private AutocompleteMenuNS.AutocompleteMenu completemenu;
    }
}