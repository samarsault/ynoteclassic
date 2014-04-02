namespace SS.Ynote.Classic.UI
{
    internal partial class BookmarksInfos
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
            this.okbtn = new System.Windows.Forms.Button();
            this.lstbookmarks = new System.Windows.Forms.ListView();
            this.bkname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.linec = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textheader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // okbtn
            // 
            this.okbtn.Location = new System.Drawing.Point(321, 188);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(75, 23);
            this.okbtn.TabIndex = 1;
            this.okbtn.Text = "OK";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // lstbookmarks
            // 
            this.lstbookmarks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.bkname,
            this.linec,
            this.textheader});
            this.lstbookmarks.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbookmarks.FullRowSelect = true;
            this.lstbookmarks.GridLines = true;
            this.lstbookmarks.Location = new System.Drawing.Point(13, 13);
            this.lstbookmarks.MultiSelect = false;
            this.lstbookmarks.Name = "lstbookmarks";
            this.lstbookmarks.Size = new System.Drawing.Size(383, 169);
            this.lstbookmarks.TabIndex = 2;
            this.lstbookmarks.UseCompatibleStateImageBehavior = false;
            this.lstbookmarks.View = System.Windows.Forms.View.Details;
            this.lstbookmarks.DoubleClick += new System.EventHandler(this.lstbookmarks_DoubleClick);
            // 
            // bkname
            // 
            this.bkname.Text = "Name";
            this.bkname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bkname.Width = 84;
            // 
            // linec
            // 
            this.linec.Text = "Line";
            this.linec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.linec.Width = 53;
            // 
            // textheader
            // 
            this.textheader.Text = "Text";
            this.textheader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textheader.Width = 259;
            // 
            // BookmarksInfos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 223);
            this.Controls.Add(this.lstbookmarks);
            this.Controls.Add(this.okbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BookmarksInfos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bookmarks";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.ListView lstbookmarks;
        private System.Windows.Forms.ColumnHeader bkname;
        private System.Windows.Forms.ColumnHeader linec;
        private System.Windows.Forms.ColumnHeader textheader;
    }
}