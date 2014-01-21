using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace YnoteThemeGenerator
{
  internal partial class MainForm
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
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.lstfontstyle = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            this.colorWheel = new Cyotek.Windows.Forms.ColorWheel();
            this.lightnessColorSlider = new Cyotek.Windows.Forms.LightnessColorSlider();
            this.colorGrid = new Cyotek.Windows.Forms.ColorGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstprops = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbpalette = new System.Windows.Forms.ComboBox();
            this.colorEditorManager = new Cyotek.Windows.Forms.ColorEditorManager();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.colpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem8,
            this.menuItem11});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5,
            this.menuItem6,
            this.menuItem7});
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItem2.Text = "New Theme";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem3.Text = "Open Theme";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 3;
            this.menuItem5.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItem5.Text = "Save Theme";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 4;
            this.menuItem6.Text = "-";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 5;
            this.menuItem7.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.menuItem7.Text = "Close";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9,
            this.menuItem10,
            this.menuItem13,
            this.menuItem14,
            this.menuItem15});
            this.menuItem8.Text = "Options";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.menuItem9.Text = "Copy And Run Ynote";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.Shortcut = System.Windows.Forms.Shortcut.CtrlF5;
            this.menuItem10.Text = "Copy To Ynote Themes Directory";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 2;
            this.menuItem13.Text = "-";
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 3;
            this.menuItem14.Text = "Set Ynote Directory";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 4;
            this.menuItem15.Text = "Edit With Ynote";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 2;
            this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12});
            this.menuItem11.Text = "Help";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 0;
            this.menuItem12.Text = "About";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // lstfontstyle
            // 
            this.lstfontstyle.FormattingEnabled = true;
            this.lstfontstyle.ItemHeight = 15;
            this.lstfontstyle.Location = new System.Drawing.Point(9, 31);
            this.lstfontstyle.Name = "lstfontstyle";
            this.lstfontstyle.Size = new System.Drawing.Size(105, 124);
            this.lstfontstyle.TabIndex = 24;
            this.lstfontstyle.SelectedValueChanged += new System.EventHandler(this.lstfontstyle_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.colorEditor);
            this.groupBox1.Controls.Add(this.colorWheel);
            this.groupBox1.Controls.Add(this.lightnessColorSlider);
            this.groupBox1.Controls.Add(this.colorGrid);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 339);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color";
            // 
            // colorEditor
            // 
            this.colorEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorEditor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorEditor.Location = new System.Drawing.Point(254, 13);
            this.colorEditor.Name = "colorEditor";
            this.colorEditor.Size = new System.Drawing.Size(320, 290);
            this.colorEditor.TabIndex = 7;
            this.colorEditor.ColorChanged += new System.EventHandler(this.colorEditor_ColorChanged);
            // 
            // colorWheel
            // 
            this.colorWheel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorWheel.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorWheel.Location = new System.Drawing.Point(6, 31);
            this.colorWheel.Name = "colorWheel";
            this.colorWheel.Size = new System.Drawing.Size(192, 147);
            this.colorWheel.TabIndex = 10;
            // 
            // lightnessColorSlider
            // 
            this.lightnessColorSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lightnessColorSlider.Location = new System.Drawing.Point(191, 31);
            this.lightnessColorSlider.Name = "lightnessColorSlider";
            this.lightnessColorSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.lightnessColorSlider.Size = new System.Drawing.Size(41, 147);
            this.lightnessColorSlider.TabIndex = 21;
            // 
            // colorGrid
            // 
            this.colorGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorGrid.AutoAddColors = false;
            this.colorGrid.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None;
            this.colorGrid.EditMode = Cyotek.Windows.Forms.ColorEditingMode.Both;
            this.colorGrid.Location = new System.Drawing.Point(6, 184);
            this.colorGrid.Name = "colorGrid";
            this.colorGrid.Padding = new System.Windows.Forms.Padding(0);
            this.colorGrid.SelectedCellStyle = Cyotek.Windows.Forms.ColorGridSelectedCellStyle.Standard;
            this.colorGrid.ShowCustomColors = false;
            this.colorGrid.Size = new System.Drawing.Size(192, 108);
            this.colorGrid.Spacing = new System.Drawing.Size(0, 0);
            this.colorGrid.TabIndex = 11;
            this.colorGrid.ColorChanged += new System.EventHandler(this.colorGrid_ColorChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lstfontstyle);
            this.groupBox2.Location = new System.Drawing.Point(609, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 178);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Font Style";
            // 
            // lstprops
            // 
            this.lstprops.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstprops.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lstprops.FullRowSelect = true;
            this.lstprops.GridLines = true;
            this.lstprops.HideSelection = false;
            this.lstprops.Location = new System.Drawing.Point(12, 6);
            this.lstprops.MultiSelect = false;
            this.lstprops.Name = "lstprops";
            this.lstprops.Size = new System.Drawing.Size(715, 97);
            this.lstprops.TabIndex = 28;
            this.lstprops.UseCompatibleStateImageBehavior = false;
            this.lstprops.View = System.Windows.Forms.View.Details;
            this.lstprops.SelectedIndexChanged += new System.EventHandler(this.lstprops_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Key";
            this.columnHeader1.Width = 179;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "HexColor";
            this.columnHeader2.Width = 132;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "FontStyle";
            this.columnHeader3.Width = 177;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.Width = 168;
            // 
            // colpanel
            // 
            this.colpanel.Controls.Add(this.panel1);
            this.colpanel.Controls.Add(this.cmbpalette);
            this.colpanel.Controls.Add(this.groupBox1);
            this.colpanel.Controls.Add(this.groupBox2);
            this.colpanel.Enabled = false;
            this.colpanel.Location = new System.Drawing.Point(5, 109);
            this.colpanel.Name = "colpanel";
            this.colpanel.Size = new System.Drawing.Size(729, 342);
            this.colpanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(609, 240);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(118, 45);
            this.panel1.TabIndex = 29;
            // 
            // cmbpalette
            // 
            this.cmbpalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpalette.FormattingEnabled = true;
            this.cmbpalette.Location = new System.Drawing.Point(605, 204);
            this.cmbpalette.Name = "cmbpalette";
            this.cmbpalette.Size = new System.Drawing.Size(121, 23);
            this.cmbpalette.TabIndex = 28;
            this.cmbpalette.SelectedIndexChanged += new System.EventHandler(this.cmbpalette_SelectedIndexChanged);
            // 
            // colorEditorManager
            // 
            this.colorEditorManager.ColorEditor = this.colorEditor;
            this.colorEditorManager.ColorGrid = this.colorGrid;
            this.colorEditorManager.ColorWheel = this.colorWheel;
            this.colorEditorManager.LightnessColorSlider = this.lightnessColorSlider;
            this.colorEditorManager.ColorChanged += new System.EventHandler(this.colorEditorManager_ColorChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 415);
            this.Controls.Add(this.lstprops);
            this.Controls.Add(this.colpanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Menu = this.MainMenu;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ynote Theme Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.colpanel.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private ColorWheel colorWheel;
    private ColorEditor colorEditor;
    private ColorGrid colorGrid;
    private ColorEditorManager colorEditorManager;
    private LightnessColorSlider lightnessColorSlider;
      private MainMenu MainMenu;
      private MenuItem menuItem1;
      private MenuItem menuItem2;
      private MenuItem menuItem3;
      private MenuItem menuItem4;
      private MenuItem menuItem5;
      private MenuItem menuItem6;
      private MenuItem menuItem7;
      private MenuItem menuItem8;
      private MenuItem menuItem9;
      private MenuItem menuItem10;
      private MenuItem menuItem11;
      private MenuItem menuItem12;
      private ListBox lstfontstyle;
      private GroupBox groupBox1;
      private GroupBox groupBox2;
      private MenuItem menuItem13;
      private MenuItem menuItem14;
      private ListView lstprops;
      private ColumnHeader columnHeader1;
      private ColumnHeader columnHeader2;
      private ColumnHeader columnHeader3;
      private ColumnHeader columnHeader4;
      private Panel colpanel;
      private MenuItem menuItem15;
      private ComboBox cmbpalette;
      private Panel panel1;
  }
}