namespace SS.Ynote.Classic.UI
{
    partial class Shell
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
            this.shellControl = new ConsoleControl.ConsoleControl();
            this.SuspendLayout();
            // 
            // shellControl
            // 
            this.shellControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellControl.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shellControl.IsInputEnabled = true;
            this.shellControl.Location = new System.Drawing.Point(0, 0);
            this.shellControl.Name = "shellControl";
            this.shellControl.SendKeyboardCommandsToProcess = false;
            this.shellControl.ShowDiagnostics = false;
            this.shellControl.Size = new System.Drawing.Size(571, 311);
            this.shellControl.TabIndex = 0;
            this.shellControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.consoleControl1_KeyDown);
            // 
            // Shell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 311);
            this.Controls.Add(this.shellControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Shell";
            this.Text = "Shell";
            this.ResumeLayout(false);

        }

        #endregion

        private ConsoleControl.ConsoleControl shellControl;
    }
}