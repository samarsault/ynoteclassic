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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.contextmenu = new System.Windows.Forms.ContextMenu();
            this.cutmenu = new System.Windows.Forms.MenuItem();
            this.copymenu = new System.Windows.Forms.MenuItem();
            this.pastemenu = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.undomenu = new System.Windows.Forms.MenuItem();
            this.redomenu = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.selectallmenu = new System.Windows.Forms.MenuItem();
            this.foldselectedmenu = new System.Windows.Forms.MenuItem();
            this.closemenu = new System.Windows.Forms.MenuItem();
            this.codebox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabcontext = new System.Windows.Forms.ContextMenu();
            this.miclose = new System.Windows.Forms.MenuItem();
            this.micloseallbutthis = new System.Windows.Forms.MenuItem();
            this.micloseall = new System.Windows.Forms.MenuItem();
            this.miopencontaining = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // contextmenu
            // 
            this.contextmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.cutmenu,
            this.copymenu,
            this.pastemenu,
            this.menuItem4,
            this.undomenu,
            this.redomenu,
            this.menuItem7,
            this.selectallmenu,
            this.foldselectedmenu,
            this.closemenu});
            // 
            // cutmenu
            // 
            this.cutmenu.Index = 0;
            this.cutmenu.Text = "Cut";
            this.cutmenu.Click += new System.EventHandler(this.cutemenu_Click);
            // 
            // copymenu
            // 
            this.copymenu.Index = 1;
            this.copymenu.Text = "Copy";
            this.copymenu.Click += new System.EventHandler(this.copymenu_Click);
            // 
            // pastemenu
            // 
            this.pastemenu.Index = 2;
            this.pastemenu.Text = "Paste";
            this.pastemenu.Click += new System.EventHandler(this.pastemenu_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "-";
            // 
            // undomenu
            // 
            this.undomenu.Index = 4;
            this.undomenu.Text = "Undo";
            this.undomenu.Click += new System.EventHandler(this.undomenu_Click);
            // 
            // redomenu
            // 
            this.redomenu.Index = 5;
            this.redomenu.Text = "Redo";
            this.redomenu.Click += new System.EventHandler(this.redomenu_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 6;
            this.menuItem7.Text = "-";
            // 
            // selectallmenu
            // 
            this.selectallmenu.Index = 7;
            this.selectallmenu.Text = "Select All";
            this.selectallmenu.Click += new System.EventHandler(this.selectallmenu_Click);
            // 
            // foldselectedmenu
            // 
            this.foldselectedmenu.Index = 8;
            this.foldselectedmenu.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.foldselectedmenu.Text = "Fold Selected";
            this.foldselectedmenu.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // closemenu
            // 
            this.closemenu.Index = 9;
            this.closemenu.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.closemenu.Text = "Close";
            this.closemenu.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // codebox
            // 
            this.codebox.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.codebox.BackBrush = null;
            this.codebox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.codebox.CharHeight = 14;
            this.codebox.CharWidth = 8;
            this.codebox.ContextMenu = this.contextmenu;
            this.codebox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.codebox.DisabledColor = System.Drawing.Color.FromArgb(100, 100,100,180);
            this.codebox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codebox.Hotkeys = resources.GetString("codebox.Hotkeys");
            this.codebox.IsReplaceMode = false;
            this.codebox.LeftBracket = '(';
            this.codebox.Location = new System.Drawing.Point(0, 0);
            this.codebox.Name = "codebox";
            this.codebox.Paddings = new System.Windows.Forms.Padding(0);
            this.codebox.RightBracket = ')';
            this.codebox.SelectionColor = System.Drawing.Color.FromArgb(60,0,0,255);
            this.codebox.Size = new System.Drawing.Size(284, 262);
            this.codebox.TabIndex = 0;
            this.codebox.Zoom = 100;
            // 
            // tabcontext
            // 
            this.tabcontext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miclose,
            this.micloseallbutthis,
            this.micloseall,
            this.miopencontaining});
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
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.codebox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Editor";
            this.TabPageContextMenu = this.tabcontext;
            this.Text = "Editor";
            this.ResumeLayout(false);

        }
        private FastColoredTextBoxNS.FastColoredTextBox codebox;
        private System.Windows.Forms.ContextMenu contextmenu;
        #endregion

        private System.Windows.Forms.MenuItem cutmenu;
        private System.Windows.Forms.MenuItem copymenu;
        private System.Windows.Forms.MenuItem pastemenu;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem undomenu;
        private System.Windows.Forms.MenuItem redomenu;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem selectallmenu;
        private System.Windows.Forms.MenuItem foldselectedmenu;
        private System.Windows.Forms.MenuItem closemenu;
        private System.Windows.Forms.ContextMenu tabcontext;
        private System.Windows.Forms.MenuItem miclose;
        private System.Windows.Forms.MenuItem micloseallbutthis;
        private System.Windows.Forms.MenuItem micloseall;
        private System.Windows.Forms.MenuItem miopencontaining;

    }
}