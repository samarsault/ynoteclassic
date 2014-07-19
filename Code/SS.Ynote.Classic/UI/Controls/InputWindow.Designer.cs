namespace SS.Ynote.Classic.UI
{
    partial class InputWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputWindow));
            this.tbInput = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbInput)).BeginInit();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.AutoScrollMinSize = new System.Drawing.Size(6, 18);
            this.tbInput.BackBrush = null;
            this.tbInput.CaretColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbInput.CharHeight = 16;
            this.tbInput.CharWidth = 8;
            this.tbInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbInput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbInput.Font = new System.Drawing.Font("Consolas", 10.25F);
            this.tbInput.HighlightFoldingIndicator = false;
            this.tbInput.IsReplaceMode = false;
            this.tbInput.LeftPadding = 2;
            this.tbInput.Location = new System.Drawing.Point(3, 2);
            this.tbInput.Name = "tbInput";
            this.tbInput.Paddings = new System.Windows.Forms.Padding(1);
            this.tbInput.SameWordsStyle = null;
            this.tbInput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbInput.ShowLineNumbers = false;
            this.tbInput.ShowScrollBars = false;
            this.tbInput.Size = new System.Drawing.Size(585, 20);
            this.tbInput.TabIndex = 2;
            this.tbInput.TextAreaBorder = FastColoredTextBoxNS.TextAreaBorderType.Shadow;
            this.tbInput.Zoom = 100;
            // 
            // InputWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(this.tbInput);
            this.Name = "InputWindow";
            this.Size = new System.Drawing.Size(591, 23);
            ((System.ComponentModel.ISupportInitialize)(this.tbInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox tbInput;

    }
}