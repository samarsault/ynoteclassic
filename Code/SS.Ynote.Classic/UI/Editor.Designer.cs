namespace SS.Ynote.Classic.UI
{
    partial class Editor
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
            if (disposing && components != null)
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
            this.codebox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.contextmenu = new System.Windows.Forms.ContextMenu();
            this.tabcontext = new System.Windows.Forms.ContextMenu();
            this.miclose = new System.Windows.Forms.MenuItem();
            this.micloseallbutthis = new System.Windows.Forms.MenuItem();
            this.micloseall = new System.Windows.Forms.MenuItem();
            this.miopencontaining = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.codebox)).BeginInit();
            this.SuspendLayout();
            // 
            // codebox
            // 
            this.codebox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.codebox.AutoCompleteBracketsList = new char[] {'(', ')', '{', '}', '[', ']', '\"', '\"', '\'', '\''};
            this.codebox.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.codebox.BackBrush = null;
            this.codebox.CharHeight = 14;
            this.codebox.CharWidth = 8;
            this.codebox.ContextMenu = this.contextmenu;
            this.codebox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.codebox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this.codebox.IsReplaceMode = false;
            this.codebox.Location = new System.Drawing.Point(179, -1);
            this.codebox.Name = "codebox";
            this.codebox.Paddings = new System.Windows.Forms.Padding(0);
            this.codebox.SameWordsStyle = null;
            this.codebox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.codebox.Size = new System.Drawing.Size(227, 255);
            this.codebox.TabIndex = 0;
            this.codebox.Zoom = 100;
            // 
            // contextmenu
            // 
            this.contextmenu.Popup += new System.EventHandler(this.contextmenu_Popup);
            // 
            // tabcontext
            // 
            this.tabcontext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miclose,
            this.micloseallbutthis,
            this.micloseall,
            this.miopencontaining,
            this.menuItem1,
            this.menuItem2,
            this.menuItem3});
            // 
            // miclose
            // 
            this.miclose.Index = 0;
            this.miclose.Text = "Close";
            this.miclose.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // micloseallbutthis
            // 
            this.micloseallbutthis.Index = 1;
            this.micloseallbutthis.Text = "Close All But This";
            this.micloseallbutthis.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // micloseall
            // 
            this.micloseall.Index = 2;
            this.micloseall.Text = "Close All";
            this.micloseall.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // miopencontaining
            // 
            this.miopencontaining.Index = 3;
            this.miopencontaining.Text = "Open Containing Folder";
            this.miopencontaining.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "-";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 5;
            this.menuItem2.Text = "Copy Filename to Clipboard";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 6;
            this.menuItem3.Text = "Copy Full File Path to Clipboard";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 254);
            this.Controls.Add(this.codebox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Editor";
            this.TabPageContextMenu = this.tabcontext;
            this.Text = "Editor";
            ((System.ComponentModel.ISupportInitialize)(this.codebox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox codebox;
        private System.Windows.Forms.ContextMenu tabcontext;
        private System.Windows.Forms.MenuItem miclose;
        private System.Windows.Forms.MenuItem micloseallbutthis;
        private System.Windows.Forms.MenuItem micloseall;
        private System.Windows.Forms.MenuItem miopencontaining;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.ContextMenu contextmenu;

    }
}