namespace SS.Ynote.Classic.UI
{
    partial class BuildOutput
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
            this.tbout = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbout
            // 
            this.tbout.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbout.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbout.Location = new System.Drawing.Point(0, 0);
            this.tbout.Multiline = true;
            this.tbout.Name = "tbout";
            this.tbout.ReadOnly = true;
            this.tbout.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbout.Size = new System.Drawing.Size(529, 202);
            this.tbout.TabIndex = 0;
            // 
            // BuildOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 202);
            this.Controls.Add(this.tbout);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BuildOutput";
            this.Text = "Build Output";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbout;
    }
}