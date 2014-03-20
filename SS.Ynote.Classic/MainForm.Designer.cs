using System.Security;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic
{
    partial class MainForm
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
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin2 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient4 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient8 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient9 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient5 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient10 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient11 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient12 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient6 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient13 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient14 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.MenuStrip = new System.Windows.Forms.MainMenu(this.components);
            this.filemenu = new System.Windows.Forms.MenuItem();
            this.NewMenuItem = new System.Windows.Forms.MenuItem();
            this.OpenMenuItem = new System.Windows.Forms.MenuItem();
            this.miopenencoding = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.recentfilesmenu = new System.Windows.Forms.MenuItem();
            this.reopenclosedtab = new System.Windows.Forms.MenuItem();
            this.menuItem68 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.misaveencoding = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.fromrtf = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.pngexport = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.htmlexport = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.ExitMenu = new System.Windows.Forms.MenuItem();
            this.editmenu = new System.Windows.Forms.MenuItem();
            this.UndoMenuItem = new System.Windows.Forms.MenuItem();
            this.RedoMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.CutMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.selectallmenu = new System.Windows.Forms.MenuItem();
            this.clearallmenu = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.menuItem47 = new System.Windows.Forms.MenuItem();
            this.menuItem71 = new System.Windows.Forms.MenuItem();
            this.datetime = new System.Windows.Forms.MenuItem();
            this.fileastext = new System.Windows.Forms.MenuItem();
            this.menuItem53 = new System.Windows.Forms.MenuItem();
            this.filenamemenuitem = new System.Windows.Forms.MenuItem();
            this.fullfilenamemenuitem = new System.Windows.Forms.MenuItem();
            this.menuItem52 = new System.Windows.Forms.MenuItem();
            this.emptycolumns = new System.Windows.Forms.MenuItem();
            this.emptylines = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.findmenu = new System.Windows.Forms.MenuItem();
            this.menuItem85 = new System.Windows.Forms.MenuItem();
            this.replacemenu = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.incrementalsearchmenu = new System.Windows.Forms.MenuItem();
            this.menuItem40 = new System.Windows.Forms.MenuItem();
            this.findinfilesmenu = new System.Windows.Forms.MenuItem();
            this.menuItem58 = new System.Windows.Forms.MenuItem();
            this.increaseindent = new System.Windows.Forms.MenuItem();
            this.decreaseindent = new System.Windows.Forms.MenuItem();
            this.doindent = new System.Windows.Forms.MenuItem();
            this.commentmenu = new System.Windows.Forms.MenuItem();
            this.commentline = new System.Windows.Forms.MenuItem();
            this.uncommentline = new System.Windows.Forms.MenuItem();
            this.menuItem93 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem84 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.gotofirstlinemenu = new System.Windows.Forms.MenuItem();
            this.gotoendmenu = new System.Windows.Forms.MenuItem();
            this.menuItem117 = new System.Windows.Forms.MenuItem();
            this.navforwardmenu = new System.Windows.Forms.MenuItem();
            this.navbackwardmenu = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem103 = new System.Windows.Forms.MenuItem();
            this.menuItem82 = new System.Windows.Forms.MenuItem();
            this.menuItem101 = new System.Windows.Forms.MenuItem();
            this.menuItem81 = new System.Windows.Forms.MenuItem();
            this.movelineup = new System.Windows.Forms.MenuItem();
            this.movelinedown = new System.Windows.Forms.MenuItem();
            this.duplicatelinemenu = new System.Windows.Forms.MenuItem();
            this.menuItem86 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.splitlinemenu = new System.Windows.Forms.MenuItem();
            this.menuItem115 = new System.Windows.Forms.MenuItem();
            this.menuItem69 = new System.Windows.Forms.MenuItem();
            this.menuItem65 = new System.Windows.Forms.MenuItem();
            this.menuItem96 = new System.Windows.Forms.MenuItem();
            this.menuItem90 = new System.Windows.Forms.MenuItem();
            this.removelinemenu = new System.Windows.Forms.MenuItem();
            this.removeemptylines = new System.Windows.Forms.MenuItem();
            this.menuItem67 = new System.Windows.Forms.MenuItem();
            this.foldallmenu = new System.Windows.Forms.MenuItem();
            this.unfoldmenu = new System.Windows.Forms.MenuItem();
            this.menuItem70 = new System.Windows.Forms.MenuItem();
            this.foldselected = new System.Windows.Forms.MenuItem();
            this.unfoldselected = new System.Windows.Forms.MenuItem();
            this.menuItem74 = new System.Windows.Forms.MenuItem();
            this.menuItem75 = new System.Windows.Forms.MenuItem();
            this.menuItem76 = new System.Windows.Forms.MenuItem();
            this.menuItem87 = new System.Windows.Forms.MenuItem();
            this.menuItem79 = new System.Windows.Forms.MenuItem();
            this.menuItem78 = new System.Windows.Forms.MenuItem();
            this.menuItem80 = new System.Windows.Forms.MenuItem();
            this.menuItem77 = new System.Windows.Forms.MenuItem();
            this.menuItem73 = new System.Windows.Forms.MenuItem();
            this.caseuppermenu = new System.Windows.Forms.MenuItem();
            this.caselowermenu = new System.Windows.Forms.MenuItem();
            this.casetitlemenu = new System.Windows.Forms.MenuItem();
            this.swapcase = new System.Windows.Forms.MenuItem();
            this.menuItem64 = new System.Windows.Forms.MenuItem();
            this.Addbookmarkmenu = new System.Windows.Forms.MenuItem();
            this.removebookmarkmenu = new System.Windows.Forms.MenuItem();
            this.gotobookmark = new System.Windows.Forms.MenuItem();
            this.navigatethroughbookmarks = new System.Windows.Forms.MenuItem();
            this.menuItem89 = new System.Windows.Forms.MenuItem();
            this.menuItem91 = new System.Windows.Forms.MenuItem();
            this.menuItem92 = new System.Windows.Forms.MenuItem();
            this.menuItem94 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem88 = new System.Windows.Forms.MenuItem();
            this.menuItem98 = new System.Windows.Forms.MenuItem();
            this.menuItem118 = new System.Windows.Forms.MenuItem();
            this.menuItem119 = new System.Windows.Forms.MenuItem();
            this.menuItem120 = new System.Windows.Forms.MenuItem();
            this.menuItem105 = new System.Windows.Forms.MenuItem();
            this.replacemode = new System.Windows.Forms.MenuItem();
            this.viewmenu = new System.Windows.Forms.MenuItem();
            this.statusbarmenuitem = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.menuItem48 = new System.Windows.Forms.MenuItem();
            this.menuItem49 = new System.Windows.Forms.MenuItem();
            this.menuItem50 = new System.Windows.Forms.MenuItem();
            this.menuItem51 = new System.Windows.Forms.MenuItem();
            this.menuItem54 = new System.Windows.Forms.MenuItem();
            this.menuItem55 = new System.Windows.Forms.MenuItem();
            this.menuItem99 = new System.Windows.Forms.MenuItem();
            this.milanguage = new System.Windows.Forms.MenuItem();
            this.menuItem106 = new System.Windows.Forms.MenuItem();
            this.menuItem116 = new System.Windows.Forms.MenuItem();
            this.menuItem104 = new System.Windows.Forms.MenuItem();
            this.menuItem108 = new System.Windows.Forms.MenuItem();
            this.menuItem111 = new System.Windows.Forms.MenuItem();
            this.menuItem112 = new System.Windows.Forms.MenuItem();
            this.menuItem113 = new System.Windows.Forms.MenuItem();
            this.menuItem114 = new System.Windows.Forms.MenuItem();
            this.menuItem107 = new System.Windows.Forms.MenuItem();
            this.menuItem109 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.wordwrapmenu = new System.Windows.Forms.MenuItem();
            this.menuItem110 = new System.Windows.Forms.MenuItem();
            this.menuItem95 = new System.Windows.Forms.MenuItem();
            this.menuItem97 = new System.Windows.Forms.MenuItem();
            this.menuItem57 = new System.Windows.Forms.MenuItem();
            this.toolsmenu = new System.Windows.Forms.MenuItem();
            this.menuItem72 = new System.Windows.Forms.MenuItem();
            this.CommandPrompt = new System.Windows.Forms.MenuItem();
            this.pluginmanagermenu = new System.Windows.Forms.MenuItem();
            this.menuItem100 = new System.Windows.Forms.MenuItem();
            this.menuItem102 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.CompareMenu = new System.Windows.Forms.MenuItem();
            this.menuItem66 = new System.Windows.Forms.MenuItem();
            this.colorschememenu = new System.Windows.Forms.MenuItem();
            this.menuItem83 = new System.Windows.Forms.MenuItem();
            this.OptionsMenu = new System.Windows.Forms.MenuItem();
            this.macrosmenu = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem42 = new System.Windows.Forms.MenuItem();
            this.menuItem43 = new System.Windows.Forms.MenuItem();
            this.menuItem63 = new System.Windows.Forms.MenuItem();
            this.menuItem56 = new System.Windows.Forms.MenuItem();
            this.mimacros = new System.Windows.Forms.MenuItem();
            this.miscripts = new System.Windows.Forms.MenuItem();
            this.pluginsmenuitem = new System.Windows.Forms.MenuItem();
            this.helpmenu = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem121 = new System.Windows.Forms.MenuItem();
            this.menuItem62 = new System.Windows.Forms.MenuItem();
            this.menuItem60 = new System.Windows.Forms.MenuItem();
            this.menuItem59 = new System.Windows.Forms.MenuItem();
            this.menuItem61 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.aboutmenu = new System.Windows.Forms.MenuItem();
            this.status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.infolabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.langmenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.zoom = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dock = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.incrementalSearcher1 = new SS.Ynote.Classic.Features.Search.IncrementalSearcher();
            this.menuItem122 = new System.Windows.Forms.MenuItem();
            this.menuItem123 = new System.Windows.Forms.MenuItem();
            this.menuItem124 = new System.Windows.Forms.MenuItem();
            this.menuItem125 = new System.Windows.Forms.MenuItem();
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.filemenu,
            this.editmenu,
            this.viewmenu,
            this.toolsmenu,
            this.macrosmenu,
            this.pluginsmenuitem,
            this.helpmenu});
            // 
            // filemenu
            // 
            this.filemenu.Index = 0;
            this.filemenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.NewMenuItem,
            this.OpenMenuItem,
            this.miopenencoding,
            this.menuItem13,
            this.menuItem9,
            this.recentfilesmenu,
            this.reopenclosedtab,
            this.menuItem68,
            this.menuItem7,
            this.misaveencoding,
            this.menuItem8,
            this.menuItem21,
            this.menuItem19,
            this.menuItem15,
            this.menuItem16,
            this.menuItem17,
            this.menuItem20,
            this.menuItem18,
            this.menuItem22,
            this.menuItem23,
            this.menuItem24,
            this.menuItem25,
            this.ExitMenu});
            this.filemenu.Text = "File";
            // 
            // NewMenuItem
            // 
            this.NewMenuItem.Index = 0;
            this.NewMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.NewMenuItem.Text = "New";
            this.NewMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Index = 1;
            this.OpenMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.OpenMenuItem.Text = "Open";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // miopenencoding
            // 
            this.miopenencoding.Index = 2;
            this.miopenencoding.Text = "Open File With Encoding";
            this.miopenencoding.Select += new System.EventHandler(this.miopenencoding_Select);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 3;
            this.menuItem13.Shortcut = System.Windows.Forms.Shortcut.CtrlF5;
            this.menuItem13.Text = "Revert";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 4;
            this.menuItem9.Text = "-";
            // 
            // recentfilesmenu
            // 
            this.recentfilesmenu.Index = 5;
            this.recentfilesmenu.Text = "Recent Files";
            // 
            // reopenclosedtab
            // 
            this.reopenclosedtab.Index = 6;
            this.reopenclosedtab.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftT;
            this.reopenclosedtab.Text = "Reopen Latest File";
            this.reopenclosedtab.Click += new System.EventHandler(this.reopenclosedtab_Click);
            // 
            // menuItem68
            // 
            this.menuItem68.Index = 7;
            this.menuItem68.Text = "-";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 8;
            this.menuItem7.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItem7.Text = "Save";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // misaveencoding
            // 
            this.misaveencoding.Index = 9;
            this.misaveencoding.Text = "Save File With Encoding";
            this.misaveencoding.Select += new System.EventHandler(this.misaveencoding_Select);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 10;
            this.menuItem8.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.menuItem8.Text = "Save As";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 11;
            this.menuItem21.Text = "Save All";
            this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 12;
            this.menuItem19.Text = "-";
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 13;
            this.menuItem15.Shortcut = System.Windows.Forms.Shortcut.AltF1;
            this.menuItem15.Text = "Properties";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 14;
            this.menuItem16.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftO;
            this.menuItem16.Text = "Open Containing Folder";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 15;
            this.menuItem17.Shortcut = System.Windows.Forms.Shortcut.AltBksp;
            this.menuItem17.Text = "Move To Recycle Bin";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 16;
            this.menuItem20.Text = "-";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 17;
            this.menuItem18.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.menuItem18.Text = "Print";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 18;
            this.menuItem22.Text = "-";
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 19;
            this.menuItem23.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fromrtf,
            this.menuItem33});
            this.menuItem23.Text = "Import";
            // 
            // fromrtf
            // 
            this.fromrtf.Index = 0;
            this.fromrtf.Text = "From Rich Text";
            this.fromrtf.Click += new System.EventHandler(this.fromrtf_Click);
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 1;
            this.menuItem33.Text = "Open Directory";
            this.menuItem33.Click += new System.EventHandler(this.menuItem33_Click);
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 20;
            this.menuItem24.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.pngexport,
            this.menuItem28,
            this.htmlexport});
            this.menuItem24.Text = "Export";
            // 
            // pngexport
            // 
            this.pngexport.Index = 0;
            this.pngexport.Text = "Image (PNG/JPG)";
            this.pngexport.Click += new System.EventHandler(this.pngexport_Click);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 1;
            this.menuItem28.Text = "RTF (Rich Text)";
            this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
            // 
            // htmlexport
            // 
            this.htmlexport.Index = 2;
            this.htmlexport.Text = "HTML (Web Page)";
            this.htmlexport.Click += new System.EventHandler(this.htmlexport_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 21;
            this.menuItem25.Text = "-";
            // 
            // ExitMenu
            // 
            this.ExitMenu.Index = 22;
            this.ExitMenu.Text = "Exit";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // editmenu
            // 
            this.editmenu.Index = 1;
            this.editmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.UndoMenuItem,
            this.RedoMenuItem,
            this.menuItem10,
            this.CutMenuItem,
            this.CopyMenuItem,
            this.PasteMenuItem,
            this.menuItem14,
            this.selectallmenu,
            this.clearallmenu,
            this.menuItem36,
            this.menuItem47,
            this.menuItem37,
            this.menuItem58,
            this.commentmenu,
            this.menuItem93,
            this.menuItem81,
            this.menuItem67,
            this.menuItem74,
            this.menuItem73,
            this.menuItem64,
            this.menuItem89,
            this.menuItem105,
            this.replacemode});
            this.editmenu.Text = "Edit";
            // 
            // UndoMenuItem
            // 
            this.UndoMenuItem.Index = 0;
            this.UndoMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.UndoMenuItem.Text = "Undo";
            this.UndoMenuItem.Click += new System.EventHandler(this.UndoMenuItem_Click);
            // 
            // RedoMenuItem
            // 
            this.RedoMenuItem.Index = 1;
            this.RedoMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.RedoMenuItem.Text = "Redo";
            this.RedoMenuItem.Click += new System.EventHandler(this.RedoMenuItem_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 2;
            this.menuItem10.Text = "-";
            // 
            // CutMenuItem
            // 
            this.CutMenuItem.Index = 3;
            this.CutMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.CutMenuItem.Text = "Cut";
            this.CutMenuItem.Click += new System.EventHandler(this.CutMenuItem_Click);
            // 
            // CopyMenuItem
            // 
            this.CopyMenuItem.Index = 4;
            this.CopyMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.CopyMenuItem.Text = "Copy";
            this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // PasteMenuItem
            // 
            this.PasteMenuItem.Index = 5;
            this.PasteMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.PasteMenuItem.Text = "Paste";
            this.PasteMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 6;
            this.menuItem14.Text = "-";
            // 
            // selectallmenu
            // 
            this.selectallmenu.Index = 7;
            this.selectallmenu.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.selectallmenu.Text = "Select All";
            this.selectallmenu.Click += new System.EventHandler(this.selectallmenu_Click);
            // 
            // clearallmenu
            // 
            this.clearallmenu.Index = 8;
            this.clearallmenu.Text = "Clear All";
            this.clearallmenu.Click += new System.EventHandler(this.clearallmenu_Click);
            // 
            // menuItem36
            // 
            this.menuItem36.Index = 9;
            this.menuItem36.Text = "-";
            // 
            // menuItem47
            // 
            this.menuItem47.Index = 10;
            this.menuItem47.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem71,
            this.datetime,
            this.fileastext,
            this.menuItem53,
            this.filenamemenuitem,
            this.fullfilenamemenuitem,
            this.menuItem52,
            this.emptycolumns,
            this.emptylines});
            this.menuItem47.Text = "Insert";
            // 
            // menuItem71
            // 
            this.menuItem71.Index = 0;
            this.menuItem71.Text = "Characters";
            this.menuItem71.Click += new System.EventHandler(this.menuItem71_Click);
            // 
            // datetime
            // 
            this.datetime.Index = 1;
            this.datetime.Text = "Date/Time";
            this.datetime.Click += new System.EventHandler(this.datetime_Click);
            // 
            // fileastext
            // 
            this.fileastext.Index = 2;
            this.fileastext.Text = "File As Text";
            this.fileastext.Click += new System.EventHandler(this.fileastext_Click);
            // 
            // menuItem53
            // 
            this.menuItem53.Index = 3;
            this.menuItem53.Text = "-";
            // 
            // filenamemenuitem
            // 
            this.filenamemenuitem.Index = 4;
            this.filenamemenuitem.Text = "Filename";
            this.filenamemenuitem.Click += new System.EventHandler(this.filenamemenuitem_Click);
            // 
            // fullfilenamemenuitem
            // 
            this.fullfilenamemenuitem.Index = 5;
            this.fullfilenamemenuitem.Text = "Full File Name";
            this.fullfilenamemenuitem.Click += new System.EventHandler(this.fullfilenamemenuitem_Click);
            // 
            // menuItem52
            // 
            this.menuItem52.Index = 6;
            this.menuItem52.Text = "-";
            // 
            // emptycolumns
            // 
            this.emptycolumns.Index = 7;
            this.emptycolumns.Text = "Empty Columns";
            this.emptycolumns.Click += new System.EventHandler(this.emptycolumns_Click);
            // 
            // emptylines
            // 
            this.emptylines.Index = 8;
            this.emptylines.Text = "Empty Lines";
            this.emptylines.Click += new System.EventHandler(this.emptylines_Click);
            // 
            // menuItem37
            // 
            this.menuItem37.Index = 11;
            this.menuItem37.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.findmenu,
            this.menuItem85,
            this.replacemenu,
            this.menuItem44,
            this.incrementalsearchmenu,
            this.menuItem40,
            this.findinfilesmenu});
            this.menuItem37.Text = "Search";
            // 
            // findmenu
            // 
            this.findmenu.Index = 0;
            this.findmenu.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.findmenu.Text = "Find";
            this.findmenu.Click += new System.EventHandler(this.findmenu_Click);
            // 
            // menuItem85
            // 
            this.menuItem85.Index = 1;
            this.menuItem85.Text = "Find Next      [F3]";
            this.menuItem85.Click += new System.EventHandler(this.menuItem85_Click);
            // 
            // replacemenu
            // 
            this.replacemenu.Index = 2;
            this.replacemenu.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
            this.replacemenu.Text = "Replace";
            this.replacemenu.Click += new System.EventHandler(this.replacemenu_Click);
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 3;
            this.menuItem44.Text = "-";
            // 
            // incrementalsearchmenu
            // 
            this.incrementalsearchmenu.Index = 4;
            this.incrementalsearchmenu.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.incrementalsearchmenu.Text = "Incremental Search";
            this.incrementalsearchmenu.Click += new System.EventHandler(this.incrementalsearchmenu_Click);
            // 
            // menuItem40
            // 
            this.menuItem40.Index = 5;
            this.menuItem40.Text = "-";
            // 
            // findinfilesmenu
            // 
            this.findinfilesmenu.Index = 6;
            this.findinfilesmenu.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF;
            this.findinfilesmenu.Text = "Find In Files";
            this.findinfilesmenu.Click += new System.EventHandler(this.findinfilesmenu_Click);
            // 
            // menuItem58
            // 
            this.menuItem58.Index = 12;
            this.menuItem58.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.increaseindent,
            this.decreaseindent,
            this.doindent});
            this.menuItem58.Text = "Indent";
            // 
            // increaseindent
            // 
            this.increaseindent.Index = 0;
            this.increaseindent.Text = "Increase [Tab]";
            this.increaseindent.Click += new System.EventHandler(this.increaseindent_Click);
            // 
            // decreaseindent
            // 
            this.decreaseindent.Index = 1;
            this.decreaseindent.Text = "Decrease  [Shift + Tab]";
            this.decreaseindent.Click += new System.EventHandler(this.decreaseindent_Click);
            // 
            // doindent
            // 
            this.doindent.Index = 2;
            this.doindent.Text = "DoIndent Selection";
            this.doindent.Click += new System.EventHandler(this.doindent_Click);
            // 
            // commentmenu
            // 
            this.commentmenu.Index = 13;
            this.commentmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.commentline,
            this.uncommentline});
            this.commentmenu.Text = "Comment";
            this.commentmenu.Popup += new System.EventHandler(this.commentmenu_Popup);
            // 
            // commentline
            // 
            this.commentline.Index = 0;
            this.commentline.Text = "Comment Line";
            this.commentline.Click += new System.EventHandler(this.commentline_Click);
            // 
            // uncommentline
            // 
            this.uncommentline.Index = 1;
            this.uncommentline.Text = "Uncomment Line";
            this.uncommentline.Click += new System.EventHandler(this.uncommentline_Click);
            // 
            // menuItem93
            // 
            this.menuItem93.Index = 14;
            this.menuItem93.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem27,
            this.menuItem84,
            this.menuItem1,
            this.gotofirstlinemenu,
            this.gotoendmenu,
            this.menuItem117,
            this.navforwardmenu,
            this.navbackwardmenu,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5,
            this.menuItem26,
            this.menuItem34,
            this.menuItem103,
            this.menuItem82,
            this.menuItem101});
            this.menuItem93.Text = "Navigation";
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 0;
            this.menuItem27.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.menuItem27.Text = "Go To Line";
            this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // menuItem84
            // 
            this.menuItem84.Index = 1;
            this.menuItem84.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this.menuItem84.Text = "Switch File";
            this.menuItem84.Click += new System.EventHandler(this.menuItem84_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // gotofirstlinemenu
            // 
            this.gotofirstlinemenu.Index = 3;
            this.gotofirstlinemenu.Text = "Go to First Line";
            this.gotofirstlinemenu.Click += new System.EventHandler(this.gotofirstlinemenu_Click);
            // 
            // gotoendmenu
            // 
            this.gotoendmenu.Index = 4;
            this.gotoendmenu.Text = "Go to the End";
            this.gotoendmenu.Click += new System.EventHandler(this.gotoendmenu_Click);
            // 
            // menuItem117
            // 
            this.menuItem117.Index = 5;
            this.menuItem117.Text = "-";
            // 
            // navforwardmenu
            // 
            this.navforwardmenu.Index = 6;
            this.navforwardmenu.Text = "Navigate Forward";
            this.navforwardmenu.Click += new System.EventHandler(this.navforwardmenu_Click);
            // 
            // navbackwardmenu
            // 
            this.navbackwardmenu.Index = 7;
            this.navbackwardmenu.Text = "Navigate Backward";
            this.navbackwardmenu.Click += new System.EventHandler(this.navbackwardmenu_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 8;
            this.menuItem2.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 9;
            this.menuItem3.Text = "Go to Left Bracket (";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 10;
            this.menuItem4.Text = "Go to Right Bracket )";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 11;
            this.menuItem5.Text = "-";
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 12;
            this.menuItem26.Text = "Go to Left Bracket {";
            this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click);
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 13;
            this.menuItem34.Text = "Go to Left Bracket }";
            this.menuItem34.Click += new System.EventHandler(this.menuItem34_Click);
            // 
            // menuItem103
            // 
            this.menuItem103.Index = 14;
            this.menuItem103.Text = "-";
            // 
            // menuItem82
            // 
            this.menuItem82.Index = 15;
            this.menuItem82.Text = "Go Left Bracket [";
            this.menuItem82.Click += new System.EventHandler(this.menuItem82_Click);
            // 
            // menuItem101
            // 
            this.menuItem101.Index = 16;
            this.menuItem101.Text = "Go Right Bracket ]";
            this.menuItem101.Click += new System.EventHandler(this.menuItem101_Click);
            // 
            // menuItem81
            // 
            this.menuItem81.Index = 15;
            this.menuItem81.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.movelineup,
            this.movelinedown,
            this.duplicatelinemenu,
            this.menuItem86,
            this.menuItem29,
            this.splitlinemenu,
            this.menuItem115,
            this.menuItem69,
            this.menuItem65,
            this.menuItem96,
            this.menuItem90,
            this.removelinemenu,
            this.removeemptylines});
            this.menuItem81.Text = "Line";
            // 
            // movelineup
            // 
            this.movelineup.Index = 0;
            this.movelineup.Shortcut = System.Windows.Forms.Shortcut.AltUpArrow;
            this.movelineup.Text = "Move Line Up";
            this.movelineup.Click += new System.EventHandler(this.movelineup_Click);
            // 
            // movelinedown
            // 
            this.movelinedown.Index = 1;
            this.movelinedown.Shortcut = System.Windows.Forms.Shortcut.AltDownArrow;
            this.movelinedown.Text = "Move Line Down";
            this.movelinedown.Click += new System.EventHandler(this.movelinedown_Click);
            // 
            // duplicatelinemenu
            // 
            this.duplicatelinemenu.Index = 2;
            this.duplicatelinemenu.Text = "Duplicate Line";
            this.duplicatelinemenu.Click += new System.EventHandler(this.duplicatelinemenu_Click);
            // 
            // menuItem86
            // 
            this.menuItem86.Index = 3;
            this.menuItem86.Text = "-";
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 4;
            this.menuItem29.Shortcut = System.Windows.Forms.Shortcut.CtrlJ;
            this.menuItem29.Text = "Join Lines";
            this.menuItem29.Click += new System.EventHandler(this.menuItem29_Click);
            // 
            // splitlinemenu
            // 
            this.splitlinemenu.Index = 5;
            this.splitlinemenu.Text = "Split Line";
            this.splitlinemenu.Click += new System.EventHandler(this.splitlinemenu_Click);
            // 
            // menuItem115
            // 
            this.menuItem115.Index = 6;
            this.menuItem115.Text = "-";
            // 
            // menuItem69
            // 
            this.menuItem69.Index = 7;
            this.menuItem69.Text = "Sort Lines (Reverse)";
            this.menuItem69.Click += new System.EventHandler(this.menuItem69_Click);
            // 
            // menuItem65
            // 
            this.menuItem65.Index = 8;
            this.menuItem65.Text = "Sort Alphabetically";
            this.menuItem65.Click += new System.EventHandler(this.menuItem65_Click);
            // 
            // menuItem96
            // 
            this.menuItem96.Index = 9;
            this.menuItem96.Text = "Sort Lengthwise";
            this.menuItem96.Click += new System.EventHandler(this.menuItem96_Click);
            // 
            // menuItem90
            // 
            this.menuItem90.Index = 10;
            this.menuItem90.Text = "-";
            // 
            // removelinemenu
            // 
            this.removelinemenu.Index = 11;
            this.removelinemenu.Text = "Remove Current Line";
            this.removelinemenu.Click += new System.EventHandler(this.removelinemenu_Click);
            // 
            // removeemptylines
            // 
            this.removeemptylines.Index = 12;
            this.removeemptylines.Text = "Remove Empty Lines";
            this.removeemptylines.Click += new System.EventHandler(this.removeemptylines_Click);
            // 
            // menuItem67
            // 
            this.menuItem67.Index = 16;
            this.menuItem67.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.foldallmenu,
            this.unfoldmenu,
            this.menuItem70,
            this.foldselected,
            this.unfoldselected});
            this.menuItem67.Text = "Folding";
            // 
            // foldallmenu
            // 
            this.foldallmenu.Index = 0;
            this.foldallmenu.Text = "Fold All";
            this.foldallmenu.Click += new System.EventHandler(this.foldallmenu_Click);
            // 
            // unfoldmenu
            // 
            this.unfoldmenu.Index = 1;
            this.unfoldmenu.Text = "Unfold All";
            this.unfoldmenu.Click += new System.EventHandler(this.unfoldmenu_Click);
            // 
            // menuItem70
            // 
            this.menuItem70.Index = 2;
            this.menuItem70.Text = "-";
            // 
            // foldselected
            // 
            this.foldselected.Index = 3;
            this.foldselected.Text = "Fold Selected";
            this.foldselected.Click += new System.EventHandler(this.foldselected_Click);
            // 
            // unfoldselected
            // 
            this.unfoldselected.Index = 4;
            this.unfoldselected.Text = "Unfold Selected";
            this.unfoldselected.Click += new System.EventHandler(this.unfoldselected_Click);
            // 
            // menuItem74
            // 
            this.menuItem74.Index = 17;
            this.menuItem74.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem75,
            this.menuItem76,
            this.menuItem87,
            this.menuItem79,
            this.menuItem78,
            this.menuItem80,
            this.menuItem77});
            this.menuItem74.Text = "Blank Operations";
            // 
            // menuItem75
            // 
            this.menuItem75.Index = 0;
            this.menuItem75.Text = "Trim Trailing Space";
            this.menuItem75.Click += new System.EventHandler(this.menuItem75_Click);
            // 
            // menuItem76
            // 
            this.menuItem76.Index = 1;
            this.menuItem76.Text = "Trim Leading Space";
            this.menuItem76.Click += new System.EventHandler(this.menuItem76_Click);
            // 
            // menuItem87
            // 
            this.menuItem87.Index = 2;
            this.menuItem87.Text = "Trim Punctuations";
            this.menuItem87.Click += new System.EventHandler(this.menuItem87_Click);
            // 
            // menuItem79
            // 
            this.menuItem79.Index = 3;
            this.menuItem79.Text = "Trim Trailing and Leading Space";
            this.menuItem79.Click += new System.EventHandler(this.menuItem79_Click);
            // 
            // menuItem78
            // 
            this.menuItem78.Index = 4;
            this.menuItem78.Text = "EOL to Space";
            this.menuItem78.Click += new System.EventHandler(this.menuItem78_Click);
            // 
            // menuItem80
            // 
            this.menuItem80.Index = 5;
            this.menuItem80.Text = "Space to EOL";
            this.menuItem80.Click += new System.EventHandler(this.menuItem80_Click);
            // 
            // menuItem77
            // 
            this.menuItem77.Index = 6;
            this.menuItem77.Text = "Remove EOL";
            this.menuItem77.Click += new System.EventHandler(this.menuItem77_Click);
            // 
            // menuItem73
            // 
            this.menuItem73.Index = 18;
            this.menuItem73.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.caseuppermenu,
            this.caselowermenu,
            this.casetitlemenu,
            this.swapcase});
            this.menuItem73.Text = "Case";
            // 
            // caseuppermenu
            // 
            this.caseuppermenu.Index = 0;
            this.caseuppermenu.Text = "To Upper";
            this.caseuppermenu.Click += new System.EventHandler(this.caseuppermenu_Click);
            // 
            // caselowermenu
            // 
            this.caselowermenu.Index = 1;
            this.caselowermenu.Text = "To Lower";
            this.caselowermenu.Click += new System.EventHandler(this.caselowermenu_Click);
            // 
            // casetitlemenu
            // 
            this.casetitlemenu.Index = 2;
            this.casetitlemenu.Text = "To Title Case";
            this.casetitlemenu.Click += new System.EventHandler(this.casetitlemenu_Click);
            // 
            // swapcase
            // 
            this.swapcase.Index = 3;
            this.swapcase.Text = "Swap Case";
            this.swapcase.Click += new System.EventHandler(this.swapcase_Click);
            // 
            // menuItem64
            // 
            this.menuItem64.Index = 19;
            this.menuItem64.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Addbookmarkmenu,
            this.removebookmarkmenu,
            this.gotobookmark,
            this.navigatethroughbookmarks});
            this.menuItem64.Text = "Bookmarks";
            // 
            // Addbookmarkmenu
            // 
            this.Addbookmarkmenu.Index = 0;
            this.Addbookmarkmenu.Text = "Add Bookmark";
            this.Addbookmarkmenu.Click += new System.EventHandler(this.Addbookmarkmenu_Click);
            // 
            // removebookmarkmenu
            // 
            this.removebookmarkmenu.Index = 1;
            this.removebookmarkmenu.Text = "Remove Bookmark";
            this.removebookmarkmenu.Click += new System.EventHandler(this.removebookmarkmenu_Click);
            // 
            // gotobookmark
            // 
            this.gotobookmark.Index = 2;
            this.gotobookmark.Text = "Go To Bookmark";
            this.gotobookmark.Click += new System.EventHandler(this.gotobookmark_Click);
            // 
            // navigatethroughbookmarks
            // 
            this.navigatethroughbookmarks.Index = 3;
            this.navigatethroughbookmarks.Text = "Navigate Through Bookmarks";
            this.navigatethroughbookmarks.Click += new System.EventHandler(this.navigatethroughbookmarks_Click);
            // 
            // menuItem89
            // 
            this.menuItem89.Index = 20;
            this.menuItem89.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem91,
            this.menuItem92,
            this.menuItem94,
            this.menuItem38,
            this.menuItem88,
            this.menuItem98,
            this.menuItem118,
            this.menuItem119,
            this.menuItem120});
            this.menuItem89.Text = "Conversions";
            // 
            // menuItem91
            // 
            this.menuItem91.Index = 0;
            this.menuItem91.Text = "To CRLF (Windows)";
            this.menuItem91.Click += new System.EventHandler(this.menuItem91_Click);
            // 
            // menuItem92
            // 
            this.menuItem92.Index = 1;
            this.menuItem92.Text = "To CR (Mac)";
            this.menuItem92.Click += new System.EventHandler(this.menuItem92_Click);
            // 
            // menuItem94
            // 
            this.menuItem94.Index = 2;
            this.menuItem94.Text = "To LF (Unix)";
            this.menuItem94.Click += new System.EventHandler(this.menuItem94_Click);
            // 
            // menuItem38
            // 
            this.menuItem38.Index = 3;
            this.menuItem38.Text = "-";
            // 
            // menuItem88
            // 
            this.menuItem88.Index = 4;
            this.menuItem88.Text = "Spaces To Tab";
            this.menuItem88.Click += new System.EventHandler(this.menuItem88_Click);
            // 
            // menuItem98
            // 
            this.menuItem98.Index = 5;
            this.menuItem98.Text = "Tab To Spaces";
            this.menuItem98.Click += new System.EventHandler(this.menuItem98_Click);
            // 
            // menuItem118
            // 
            this.menuItem118.Index = 6;
            this.menuItem118.Text = "-";
            // 
            // menuItem119
            // 
            this.menuItem119.Index = 7;
            this.menuItem119.Text = "Selection to Hex";
            this.menuItem119.Click += new System.EventHandler(this.menuItem119_Click);
            // 
            // menuItem120
            // 
            this.menuItem120.Index = 8;
            this.menuItem120.Text = "Selection to ASCII";
            this.menuItem120.Click += new System.EventHandler(this.menuItem120_Click);
            // 
            // menuItem105
            // 
            this.menuItem105.Index = 21;
            this.menuItem105.Text = "-";
            // 
            // replacemode
            // 
            this.replacemode.Index = 22;
            this.replacemode.Text = "Switch Editing Mode        [Ins]";
            this.replacemode.Click += new System.EventHandler(this.replacemode_Click);
            // 
            // viewmenu
            // 
            this.viewmenu.Index = 2;
            this.viewmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.statusbarmenuitem,
            this.menuItem46,
            this.menuItem48,
            this.menuItem54,
            this.menuItem55,
            this.menuItem99,
            this.milanguage,
            this.menuItem106,
            this.menuItem116,
            this.menuItem104,
            this.menuItem109,
            this.menuItem6,
            this.wordwrapmenu,
            this.menuItem110,
            this.menuItem95,
            this.menuItem97,
            this.menuItem57});
            this.viewmenu.Text = "View";
            // 
            // statusbarmenuitem
            // 
            this.statusbarmenuitem.Checked = true;
            this.statusbarmenuitem.Index = 0;
            this.statusbarmenuitem.Text = "Status Bar";
            this.statusbarmenuitem.Click += new System.EventHandler(this.statusbarmenuitem_Click);
            // 
            // menuItem46
            // 
            this.menuItem46.Index = 1;
            this.menuItem46.Text = "-";
            // 
            // menuItem48
            // 
            this.menuItem48.Index = 2;
            this.menuItem48.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem49,
            this.menuItem50,
            this.menuItem51});
            this.menuItem48.Text = "Zoom";
            // 
            // menuItem49
            // 
            this.menuItem49.Index = 0;
            this.menuItem49.Text = "Zoom In";
            this.menuItem49.Click += new System.EventHandler(this.menuItem49_Click);
            // 
            // menuItem50
            // 
            this.menuItem50.Index = 1;
            this.menuItem50.Text = "Zoom Out";
            this.menuItem50.Click += new System.EventHandler(this.menuItem50_Click);
            // 
            // menuItem51
            // 
            this.menuItem51.Index = 2;
            this.menuItem51.Text = "Restore Default";
            this.menuItem51.Click += new System.EventHandler(this.menuItem51_Click);
            // 
            // menuItem54
            // 
            this.menuItem54.Index = 3;
            this.menuItem54.Text = "Transparent UI";
            this.menuItem54.Click += new System.EventHandler(this.menuItem54_Click);
            // 
            // menuItem55
            // 
            this.menuItem55.Index = 4;
            this.menuItem55.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.menuItem55.Text = "Full Screen";
            this.menuItem55.Click += new System.EventHandler(this.menuItem55_Click);
            // 
            // menuItem99
            // 
            this.menuItem99.Index = 5;
            this.menuItem99.Text = "-";
            // 
            // milanguage
            // 
            this.milanguage.Index = 6;
            this.milanguage.Text = "Syntax Highlight";
            this.milanguage.Select += new System.EventHandler(this.milanguage_Select);
            // 
            // menuItem106
            // 
            this.menuItem106.Index = 7;
            this.menuItem106.Text = "Snippets";
            this.menuItem106.Click += new System.EventHandler(this.menuItem106_Click);
            // 
            // menuItem116
            // 
            this.menuItem116.Index = 8;
            this.menuItem116.Text = "-";
            // 
            // menuItem104
            // 
            this.menuItem104.Index = 9;
            this.menuItem104.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem108,
            this.menuItem111,
            this.menuItem112,
            this.menuItem113,
            this.menuItem114,
            this.menuItem107});
            this.menuItem104.Text = "Mark Selection";
            // 
            // menuItem108
            // 
            this.menuItem108.Index = 0;
            this.menuItem108.Text = "Using Red Style";
            this.menuItem108.Click += new System.EventHandler(this.menuItem108_Click);
            // 
            // menuItem111
            // 
            this.menuItem111.Index = 1;
            this.menuItem111.Text = "Using Blue Style";
            this.menuItem111.Click += new System.EventHandler(this.menuItem111_Click);
            // 
            // menuItem112
            // 
            this.menuItem112.Index = 2;
            this.menuItem112.Text = "Using Gray Style";
            this.menuItem112.Click += new System.EventHandler(this.menuItem112_Click);
            // 
            // menuItem113
            // 
            this.menuItem113.Index = 3;
            this.menuItem113.Text = "Using Green Style";
            this.menuItem113.Click += new System.EventHandler(this.menuItem113_Click);
            // 
            // menuItem114
            // 
            this.menuItem114.Index = 4;
            this.menuItem114.Text = "Using Yellow Style";
            this.menuItem114.Click += new System.EventHandler(this.menuItem114_Click);
            // 
            // menuItem107
            // 
            this.menuItem107.Index = 5;
            this.menuItem107.Text = "Clear Marked";
            this.menuItem107.Click += new System.EventHandler(this.menuItem107_Click);
            // 
            // menuItem109
            // 
            this.menuItem109.Index = 10;
            this.menuItem109.Text = "Hidden Characters";
            this.menuItem109.Click += new System.EventHandler(this.menuItem109_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 11;
            this.menuItem6.Text = "Show Unsaved Changes";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // wordwrapmenu
            // 
            this.wordwrapmenu.Index = 12;
            this.wordwrapmenu.Text = "Word Wrap";
            this.wordwrapmenu.Click += new System.EventHandler(this.wordwrapmenu_Click);
            // 
            // menuItem110
            // 
            this.menuItem110.Index = 13;
            this.menuItem110.Text = "-";
            // 
            // menuItem95
            // 
            this.menuItem95.Index = 14;
            this.menuItem95.Text = "Project Manager";
            this.menuItem95.Click += new System.EventHandler(this.menuItem95_Click);
            // 
            // menuItem97
            // 
            this.menuItem97.Index = 15;
            this.menuItem97.Text = "-";
            // 
            // menuItem57
            // 
            this.menuItem57.Index = 16;
            this.menuItem57.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.menuItem57.Text = "Split";
            this.menuItem57.Click += new System.EventHandler(this.menuItem57_Click);
            // 
            // toolsmenu
            // 
            this.toolsmenu.Index = 3;
            this.toolsmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem72,
            this.CommandPrompt,
            this.pluginmanagermenu,
            this.menuItem100,
            this.menuItem102,
            this.menuItem122,
            this.menuItem30,
            this.menuItem31,
            this.CompareMenu,
            this.menuItem66,
            this.colorschememenu,
            this.menuItem83,
            this.OptionsMenu});
            this.toolsmenu.Text = "Tools";
            // 
            // menuItem72
            // 
            this.menuItem72.Index = 0;
            this.menuItem72.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP;
            this.menuItem72.Text = "Commander";
            this.menuItem72.Click += new System.EventHandler(this.menuItem72_Click);
            // 
            // CommandPrompt
            // 
            this.CommandPrompt.Index = 1;
            this.CommandPrompt.Text = "Command Prompt";
            this.CommandPrompt.Click += new System.EventHandler(this.CommandPrompt_Click);
            // 
            // pluginmanagermenu
            // 
            this.pluginmanagermenu.Index = 2;
            this.pluginmanagermenu.Text = "Plugin Manager";
            this.pluginmanagermenu.Click += new System.EventHandler(this.pluginmanagermenu_Click);
            // 
            // menuItem100
            // 
            this.menuItem100.Index = 3;
            this.menuItem100.Text = "Keymap Editor";
            this.menuItem100.Click += new System.EventHandler(this.menuItem100_Click);
            // 
            // menuItem102
            // 
            this.menuItem102.Index = 4;
            this.menuItem102.Text = "-";
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 6;
            this.menuItem30.Text = "Execute File";
            this.menuItem30.Click += new System.EventHandler(this.menuItem30_Click);
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 7;
            this.menuItem31.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem32,
            this.menuItem45});
            this.menuItem31.Text = "Run Scripts";
            // 
            // menuItem32
            // 
            this.menuItem32.Index = 0;
            this.menuItem32.Text = "Run";
            this.menuItem32.Click += new System.EventHandler(this.menuItem32_Click);
            // 
            // menuItem45
            // 
            this.menuItem45.Index = 1;
            this.menuItem45.Text = "Editor";
            this.menuItem45.Click += new System.EventHandler(this.menuItem45_Click);
            // 
            // CompareMenu
            // 
            this.CompareMenu.Index = 8;
            this.CompareMenu.Text = "Compare";
            this.CompareMenu.Click += new System.EventHandler(this.CompareMenu_Click);
            // 
            // menuItem66
            // 
            this.menuItem66.Index = 9;
            this.menuItem66.Text = "-";
            // 
            // colorschememenu
            // 
            this.colorschememenu.Index = 10;
            this.colorschememenu.Text = "Color Scheme";
            this.colorschememenu.Select += new System.EventHandler(this.colorschememenu_Select);
            // 
            // menuItem83
            // 
            this.menuItem83.Index = 11;
            this.menuItem83.Text = "-";
            // 
            // OptionsMenu
            // 
            this.OptionsMenu.Index = 12;
            this.OptionsMenu.Text = "Options";
            this.OptionsMenu.Click += new System.EventHandler(this.OptionsMenu_Click);
            // 
            // macrosmenu
            // 
            this.macrosmenu.Index = 4;
            this.macrosmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem35,
            this.menuItem39,
            this.menuItem41,
            this.menuItem42,
            this.menuItem43,
            this.menuItem63,
            this.menuItem56,
            this.mimacros,
            this.miscripts});
            this.macrosmenu.Text = "Macros";
            // 
            // menuItem35
            // 
            this.menuItem35.Index = 0;
            this.menuItem35.Text = "Start / Stop Recording";
            this.menuItem35.Click += new System.EventHandler(this.menuItem35_Click);
            // 
            // menuItem39
            // 
            this.menuItem39.Index = 1;
            this.menuItem39.Text = "-";
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 2;
            this.menuItem41.Text = "Playback Macro";
            this.menuItem41.Click += new System.EventHandler(this.menuItem41_Click);
            // 
            // menuItem42
            // 
            this.menuItem42.Index = 3;
            this.menuItem42.Text = "Execute Macro Multiple Times";
            this.menuItem42.Click += new System.EventHandler(this.menuItem42_Click);
            // 
            // menuItem43
            // 
            this.menuItem43.Index = 4;
            this.menuItem43.Text = "Save Recorded Macro";
            this.menuItem43.Click += new System.EventHandler(this.menuItem43_Click);
            // 
            // menuItem63
            // 
            this.menuItem63.Index = 5;
            this.menuItem63.Text = "Clear Macro Data";
            this.menuItem63.Click += new System.EventHandler(this.menuItem63_Click);
            // 
            // menuItem56
            // 
            this.menuItem56.Index = 6;
            this.menuItem56.Text = "-";
            // 
            // mimacros
            // 
            this.mimacros.Index = 7;
            this.mimacros.Text = "Macros";
            this.mimacros.Select += new System.EventHandler(this.menuItem38_Select);
            // 
            // miscripts
            // 
            this.miscripts.Index = 8;
            this.miscripts.Text = "Scripts";
            this.miscripts.Select += new System.EventHandler(this.miscripts_Select);
            // 
            // pluginsmenuitem
            // 
            this.pluginsmenuitem.Index = 5;
            this.pluginsmenuitem.Text = "Plugins";
            this.pluginsmenuitem.Visible = false;
            // 
            // helpmenu
            // 
            this.helpmenu.Index = 6;
            this.helpmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem11,
            this.menuItem121,
            this.menuItem62,
            this.menuItem60,
            this.menuItem59,
            this.menuItem61,
            this.menuItem12,
            this.aboutmenu});
            this.helpmenu.Text = "Help";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 0;
            this.menuItem11.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItem11.Text = "Wiki";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem121
            // 
            this.menuItem121.Index = 1;
            this.menuItem121.Text = "Check For Updates";
            this.menuItem121.Click += new System.EventHandler(this.menuItem121_Click);
            // 
            // menuItem62
            // 
            this.menuItem62.Index = 2;
            this.menuItem62.Text = "-";
            // 
            // menuItem60
            // 
            this.menuItem60.Index = 3;
            this.menuItem60.Text = "Resources";
            // 
            // menuItem59
            // 
            this.menuItem59.Index = 4;
            this.menuItem59.Text = "Plugin Central";
            // 
            // menuItem61
            // 
            this.menuItem61.Index = 5;
            this.menuItem61.Text = "Facebook";
            this.menuItem61.Click += new System.EventHandler(this.menuItem61_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 6;
            this.menuItem12.Text = "-";
            // 
            // aboutmenu
            // 
            this.aboutmenu.Index = 7;
            this.aboutmenu.Shortcut = System.Windows.Forms.Shortcut.ShiftF1;
            this.aboutmenu.Text = "About";
            this.aboutmenu.Click += new System.EventHandler(this.aboutmenu_Click);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.lb1,
            this.infolabel,
            this.toolStripStatusLabel1,
            this.langmenu,
            this.toolStripStatusLabel3,
            this.zoom});
            this.status.Location = new System.Drawing.Point(0, 323);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(748, 22);
            this.status.TabIndex = 3;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel2.Text = "Ready";
            // 
            // lb1
            // 
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(200, 17);
            this.lb1.Spring = true;
            // 
            // infolabel
            // 
            this.infolabel.Name = "infolabel";
            this.infolabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // langmenu
            // 
            this.langmenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.langmenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.langmenu.Name = "langmenu";
            this.langmenu.Size = new System.Drawing.Size(42, 20);
            this.langmenu.Text = "Text";
            this.langmenu.Click += new System.EventHandler(this.langmenu_Click);
            this.langmenu.MouseEnter += new System.EventHandler(this.langmenu_MouseEnter);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // zoom
            // 
            this.zoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.zoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem8,
            this.toolStripMenuItem7,
            this.toolStripMenuItem6,
            this.toolStripMenuItem5,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2});
            this.zoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(52, 20);
            this.zoom.Text = "Zoom";
            this.zoom.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.zoom_DropDownItemClicked);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem8.Text = "500";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem7.Text = "400";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem6.Text = "300";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem5.Text = "200";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem4.Text = "150";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem3.Text = "100";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem2.Text = "50";
            // 
            // dock
            // 
            this.dock.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.dock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dock.DockBottomPortion = 0.4D;
            this.dock.Location = new System.Drawing.Point(0, 0);
            this.dock.Name = "dock";
            this.dock.Size = new System.Drawing.Size(748, 284);
            dockPanelGradient4.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient4.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin2.DockStripGradient = dockPanelGradient4;
            tabGradient8.EndColor = System.Drawing.SystemColors.Control;
            tabGradient8.StartColor = System.Drawing.SystemColors.Control;
            tabGradient8.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin2.TabGradient = tabGradient8;
            autoHideStripSkin2.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            dockPanelSkin2.AutoHideStripSkin = autoHideStripSkin2;
            tabGradient9.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient9.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient9.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient2.ActiveTabGradient = tabGradient9;
            dockPanelGradient5.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient5.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient2.DockStripGradient = dockPanelGradient5;
            tabGradient10.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient10.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient10.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient2.InactiveTabGradient = tabGradient10;
            dockPaneStripSkin2.DocumentGradient = dockPaneStripGradient2;
            dockPaneStripSkin2.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            tabGradient11.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient11.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient11.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient11.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient2.ActiveCaptionGradient = tabGradient11;
            tabGradient12.EndColor = System.Drawing.SystemColors.Control;
            tabGradient12.StartColor = System.Drawing.SystemColors.Control;
            tabGradient12.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient2.ActiveTabGradient = tabGradient12;
            dockPanelGradient6.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient6.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient2.DockStripGradient = dockPanelGradient6;
            tabGradient13.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient13.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient13.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient13.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient2.InactiveCaptionGradient = tabGradient13;
            tabGradient14.EndColor = System.Drawing.Color.Transparent;
            tabGradient14.StartColor = System.Drawing.Color.Transparent;
            tabGradient14.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient2.InactiveTabGradient = tabGradient14;
            dockPaneStripSkin2.ToolWindowGradient = dockPaneStripToolWindowGradient2;
            dockPanelSkin2.DockPaneStripSkin = dockPaneStripSkin2;
            this.dock.Skin = dockPanelSkin2;
            this.dock.TabIndex = 0;
            this.dock.ActiveDocumentChanged += new System.EventHandler(this.dock_ActiveDocumentChanged);
            // 
            // incrementalSearcher1
            // 
            this.incrementalSearcher1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.incrementalSearcher1.Location = new System.Drawing.Point(0, 284);
            this.incrementalSearcher1.Name = "incrementalSearcher1";
            this.incrementalSearcher1.Size = new System.Drawing.Size(748, 39);
            this.incrementalSearcher1.TabIndex = 4;
            this.incrementalSearcher1.Visible = false;
            // 
            // menuItem122
            // 
            this.menuItem122.Index = 5;
            this.menuItem122.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem123,
            this.menuItem124,
            this.menuItem125});
            this.menuItem122.Text = "Search";
            // 
            // menuItem123
            // 
            this.menuItem123.Index = 0;
            this.menuItem123.Text = "Google";
            this.menuItem123.Click += new System.EventHandler(this.menuItem123_Click);
            // 
            // menuItem124
            // 
            this.menuItem124.Index = 1;
            this.menuItem124.Text = "Bing";
            this.menuItem124.Click += new System.EventHandler(this.menuItem124_Click);
            // 
            // menuItem125
            // 
            this.menuItem125.Index = 2;
            this.menuItem125.Text = "Wikipedia";
            this.menuItem125.Click += new System.EventHandler(this.menuItem125_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 345);
            this.Controls.Add(this.dock);
            this.Controls.Add(this.incrementalSearcher1);
            this.Controls.Add(this.status);
            this.IsMdiContainer = true;
            this.Menu = this.MenuStrip;
            this.Name = "MainForm";
            this.Text = "Ynote Classic";
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.MainMenu MenuStrip;
        #endregion

        private System.Windows.Forms.MenuItem filemenu;
        private System.Windows.Forms.MenuItem editmenu;
        private System.Windows.Forms.MenuItem viewmenu;
        private System.Windows.Forms.MenuItem toolsmenu;
        private System.Windows.Forms.MenuItem helpmenu;
        private System.Windows.Forms.MenuItem NewMenuItem;
        private System.Windows.Forms.MenuItem OpenMenuItem;
        private System.Windows.Forms.MenuItem UndoMenuItem;
        private System.Windows.Forms.MenuItem RedoMenuItem;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem CutMenuItem;
        private System.Windows.Forms.MenuItem CopyMenuItem;
        private System.Windows.Forms.MenuItem PasteMenuItem;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem aboutmenu;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem statusbarmenuitem;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.MenuItem menuItem20;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItem22;
        private System.Windows.Forms.MenuItem menuItem23;
        private System.Windows.Forms.MenuItem fromrtf;
        private System.Windows.Forms.MenuItem menuItem33;
        private System.Windows.Forms.MenuItem menuItem24;
        private System.Windows.Forms.MenuItem menuItem28;
        private System.Windows.Forms.MenuItem pngexport;
        private System.Windows.Forms.MenuItem htmlexport;
        private System.Windows.Forms.MenuItem menuItem25;
        private System.Windows.Forms.MenuItem ExitMenu;
        private System.Windows.Forms.MenuItem selectallmenu;
        private System.Windows.Forms.MenuItem clearallmenu;
        private System.Windows.Forms.MenuItem menuItem36;
        private System.Windows.Forms.MenuItem menuItem37;
        private System.Windows.Forms.MenuItem findmenu;
        private System.Windows.Forms.MenuItem replacemenu;
        private System.Windows.Forms.MenuItem menuItem44;
        private System.Windows.Forms.MenuItem incrementalsearchmenu;
        private System.Windows.Forms.MenuItem menuItem40;
        private System.Windows.Forms.MenuItem findinfilesmenu;
        private System.Windows.Forms.MenuItem menuItem47;
        private System.Windows.Forms.MenuItem datetime;
        private System.Windows.Forms.MenuItem fileastext;
        private System.Windows.Forms.MenuItem menuItem53;
        private System.Windows.Forms.MenuItem filenamemenuitem;
        private System.Windows.Forms.MenuItem fullfilenamemenuitem;
        private System.Windows.Forms.MenuItem menuItem52;
        private System.Windows.Forms.MenuItem emptycolumns;
        private System.Windows.Forms.MenuItem emptylines;
        private System.Windows.Forms.MenuItem menuItem46;
        private System.Windows.Forms.MenuItem menuItem48;
        private System.Windows.Forms.MenuItem menuItem49;
        private System.Windows.Forms.MenuItem menuItem50;
        private System.Windows.Forms.MenuItem menuItem51;
        private System.Windows.Forms.MenuItem menuItem54;
        private System.Windows.Forms.MenuItem menuItem55;
        private System.Windows.Forms.MenuItem menuItem57;
        private System.Windows.Forms.MenuItem menuItem58;
        private System.Windows.Forms.MenuItem increaseindent;
        private System.Windows.Forms.MenuItem decreaseindent;
        private System.Windows.Forms.MenuItem CommandPrompt;
        private System.Windows.Forms.MenuItem pluginmanagermenu;
        private System.Windows.Forms.MenuItem menuItem66;
        private System.Windows.Forms.MenuItem OptionsMenu;
        private System.Windows.Forms.MenuItem menuItem62;
        private System.Windows.Forms.MenuItem menuItem60;
        private System.Windows.Forms.MenuItem menuItem59;
        private System.Windows.Forms.MenuItem menuItem61;
        private System.Windows.Forms.MenuItem menuItem67;
        private System.Windows.Forms.MenuItem foldallmenu;
        private System.Windows.Forms.MenuItem unfoldmenu;
        private System.Windows.Forms.MenuItem menuItem70;
        private System.Windows.Forms.MenuItem foldselected;
        private System.Windows.Forms.MenuItem unfoldselected;
        private System.Windows.Forms.MenuItem menuItem73;
        private System.Windows.Forms.MenuItem caseuppermenu;
        private System.Windows.Forms.MenuItem caselowermenu;
        private System.Windows.Forms.MenuItem casetitlemenu;
        private System.Windows.Forms.MenuItem menuItem81;
        private System.Windows.Forms.MenuItem removelinemenu;
        private System.Windows.Forms.MenuItem movelineup;
        private System.Windows.Forms.MenuItem movelinedown;
        private System.Windows.Forms.MenuItem menuItem86;
        private System.Windows.Forms.MenuItem duplicatelinemenu;
        private System.Windows.Forms.MenuItem splitlinemenu;
        private System.Windows.Forms.MenuItem menuItem90;
        private System.Windows.Forms.MenuItem removeemptylines;
        private System.Windows.Forms.MenuItem menuItem93;
        private System.Windows.Forms.MenuItem gotofirstlinemenu;
        private System.Windows.Forms.MenuItem gotoendmenu;
        private System.Windows.Forms.MenuItem navforwardmenu;
        private System.Windows.Forms.MenuItem navbackwardmenu;
        private System.Windows.Forms.MenuItem menuItem99;
        private System.Windows.Forms.MenuItem menuItem102;
        private System.Windows.Forms.MenuItem CompareMenu;
        private System.Windows.Forms.MenuItem menuItem64;
        private System.Windows.Forms.MenuItem Addbookmarkmenu;
        private System.Windows.Forms.MenuItem removebookmarkmenu;
        private System.Windows.Forms.MenuItem gotobookmark;
        private System.Windows.Forms.MenuItem navigatethroughbookmarks;
        private System.Windows.Forms.MenuItem menuItem105;
        private System.Windows.Forms.MenuItem menuItem109;
        private System.Windows.Forms.MenuItem wordwrapmenu;
        private System.Windows.Forms.MenuItem menuItem110;
        private System.Windows.Forms.MenuItem commentmenu;
        private System.Windows.Forms.MenuItem commentline;
        private System.Windows.Forms.MenuItem uncommentline;
        private System.Windows.Forms.MenuItem doindent;
        private System.Windows.Forms.MenuItem replacemode;
        private System.Windows.Forms.MenuItem swapcase;
        private System.Windows.Forms.ToolStripStatusLabel lb1;
        private System.Windows.Forms.ToolStripStatusLabel infolabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton zoom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.MenuItem milanguage;
        private System.Windows.Forms.MenuItem macrosmenu;
        private System.Windows.Forms.MenuItem menuItem35;
        private System.Windows.Forms.MenuItem menuItem39;
        private System.Windows.Forms.MenuItem menuItem41;
        private System.Windows.Forms.MenuItem menuItem42;
        private System.Windows.Forms.MenuItem menuItem56;
        private System.Windows.Forms.MenuItem menuItem43;
        private System.Windows.Forms.MenuItem menuItem63;
        private System.Windows.Forms.MenuItem menuItem30;
        private System.Windows.Forms.MenuItem menuItem31;
        private System.Windows.Forms.MenuItem pluginsmenuitem;
        private System.Windows.Forms.MenuItem recentfilesmenu;
        private System.Windows.Forms.MenuItem reopenclosedtab;
        private System.Windows.Forms.MenuItem menuItem68;
        private System.Windows.Forms.MenuItem colorschememenu;
        private System.Windows.Forms.MenuItem menuItem27;
        private System.Windows.Forms.MenuItem menuItem29;
        private System.Windows.Forms.MenuItem menuItem65;
        private System.Windows.Forms.MenuItem menuItem69;
        private System.Windows.Forms.MenuItem menuItem71;
        private System.Windows.Forms.MenuItem menuItem72;
        private System.Windows.Forms.MenuItem menuItem74;
        private System.Windows.Forms.MenuItem menuItem75;
        private System.Windows.Forms.MenuItem menuItem76;
        private System.Windows.Forms.MenuItem menuItem78;
        private System.Windows.Forms.MenuItem menuItem79;
        private System.Windows.Forms.MenuItem menuItem80;
        private System.Windows.Forms.MenuItem menuItem83;
        private System.Windows.Forms.MenuItem menuItem77;
        private System.Windows.Forms.MenuItem menuItem84;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem85;
        private System.Windows.Forms.MenuItem menuItem87;
        private System.Windows.Forms.MenuItem menuItem89;
        private System.Windows.Forms.MenuItem menuItem91;
        private System.Windows.Forms.MenuItem menuItem92;
        private System.Windows.Forms.MenuItem menuItem94;
        private System.Windows.Forms.MenuItem miopenencoding;
        private System.Windows.Forms.MenuItem misaveencoding;
        private System.Windows.Forms.MenuItem menuItem95;
        private System.Windows.Forms.MenuItem menuItem97;
        private System.Windows.Forms.MenuItem mimacros;
        private System.Windows.Forms.MenuItem menuItem38;
        private System.Windows.Forms.MenuItem menuItem88;
        private System.Windows.Forms.MenuItem menuItem98;
        private System.Windows.Forms.MenuItem menuItem100;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dock;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private SS.Ynote.Classic.Features.Search.IncrementalSearcher incrementalSearcher1;
        private System.Windows.Forms.MenuItem miscripts;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.MenuItem menuItem32;
        private System.Windows.Forms.MenuItem menuItem45;
        private System.Windows.Forms.ToolStripDropDownButton langmenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem26;
        private System.Windows.Forms.MenuItem menuItem34;
        private System.Windows.Forms.MenuItem menuItem103;
        private System.Windows.Forms.MenuItem menuItem82;
        private System.Windows.Forms.MenuItem menuItem101;
        private System.Windows.Forms.MenuItem menuItem104;
        private System.Windows.Forms.MenuItem menuItem107;
        private System.Windows.Forms.MenuItem menuItem108;
        private System.Windows.Forms.MenuItem menuItem111;
        private System.Windows.Forms.MenuItem menuItem112;
        private System.Windows.Forms.MenuItem menuItem113;
        private System.Windows.Forms.MenuItem menuItem114;
        private System.Windows.Forms.MenuItem menuItem117;
        private MenuItem menuItem96;
        private MenuItem menuItem115;
        private MenuItem menuItem106;
        private MenuItem menuItem116;
        private MenuItem menuItem118;
        private MenuItem menuItem119;
        private MenuItem menuItem120;
        private MenuItem menuItem121;
        private MenuItem menuItem122;
        private MenuItem menuItem123;
        private MenuItem menuItem124;
        private MenuItem menuItem125;
    }
}

