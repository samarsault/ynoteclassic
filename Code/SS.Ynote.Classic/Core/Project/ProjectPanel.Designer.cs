using System.ComponentModel;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Core.Project
{
    partial class ProjectPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectPanel));
            this.projtree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.folderMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.fileMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // projtree
            // 
            this.projtree.BackColor = System.Drawing.SystemColors.Control;
            this.projtree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projtree.FullRowSelect = true;
            this.projtree.ImageIndex = 0;
            this.projtree.ImageList = this.imageList1;
            this.projtree.Indent = 20;
            this.projtree.Location = new System.Drawing.Point(0, 0);
            this.projtree.Name = "projtree";
            this.projtree.Scrollable = false;
            this.projtree.SelectedImageIndex = 0;
            this.projtree.ShowLines = false;
            this.projtree.Size = new System.Drawing.Size(267, 367);
            this.projtree.TabIndex = 0;
            this.projtree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.projtree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder_icon&16.png");
            this.imageList1.Images.SetKeyName(1, "doc_empty_icon&16.png");
            // 
            // folderMenu
            // 
            this.folderMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem1,
            this.menuItem9,
            this.menuItem7,
            this.menuItem6,
            this.menuItem5,
            this.menuItem10,
            this.menuItem11});
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Add New File";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "Add New Folder";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 2;
            this.menuItem9.Text = "-";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 3;
            this.menuItem7.Text = "Add Existing File";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 4;
            this.menuItem6.Text = "-";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 5;
            this.menuItem5.Text = "Rename";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 6;
            this.menuItem10.Text = "Open Folder in Explorer";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 7;
            this.menuItem11.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.menuItem11.Text = "Delete";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // fileMenu
            // 
            this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem13,
            this.menuItem16,
            this.menuItem14,
            this.menuItem15,
            this.menuItem3});
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 0;
            this.menuItem13.Text = "Open";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 1;
            this.menuItem16.Text = "Duplicate";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 2;
            this.menuItem14.Text = "Rename..";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 3;
            this.menuItem15.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.menuItem15.Text = "Delete";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 4;
            this.menuItem3.Text = "Open Containing Folder";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // ProjectPanel
            // 
            this.ClientSize = new System.Drawing.Size(267, 367);
            this.Controls.Add(this.projtree);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ProjectPanel";
            this.Text = "Project";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView projtree;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenu folderMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.ContextMenu fileMenu;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItem3;
    }
}