namespace SS.Ynote.Classic
{
    using System.Windows.Forms;

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
            this.MenuStrip = new System.Windows.Forms.MainMenu(this.components);
            this.filemenu = new System.Windows.Forms.MenuItem();
            this.NewMenuItem = new System.Windows.Forms.MenuItem();
            this.OpenMenuItem = new System.Windows.Forms.MenuItem();
            this.miopenencoding = new System.Windows.Forms.MenuItem();
            this.revertMenu = new System.Windows.Forms.MenuItem();
            this.seperator19 = new System.Windows.Forms.MenuItem();
            this.recentfilesmenu = new System.Windows.Forms.MenuItem();
            this.reopenclosedtab = new System.Windows.Forms.MenuItem();
            this.seperator1 = new System.Windows.Forms.MenuItem();
            this.savemenu = new System.Windows.Forms.MenuItem();
            this.misaveencoding = new System.Windows.Forms.MenuItem();
            this.misaveas = new System.Windows.Forms.MenuItem();
            this.misaveall = new System.Windows.Forms.MenuItem();
            this.seperator20 = new System.Windows.Forms.MenuItem();
            this.miproperties = new System.Windows.Forms.MenuItem();
            this.miopencontaining = new System.Windows.Forms.MenuItem();
            this.midelete = new System.Windows.Forms.MenuItem();
            this.seperator21 = new System.Windows.Forms.MenuItem();
            this.miprint = new System.Windows.Forms.MenuItem();
            this.seperator22 = new System.Windows.Forms.MenuItem();
            this.miimport = new System.Windows.Forms.MenuItem();
            this.fromrtf = new System.Windows.Forms.MenuItem();
            this.mifromdir = new System.Windows.Forms.MenuItem();
            this.miexport = new System.Windows.Forms.MenuItem();
            this.pngexport = new System.Windows.Forms.MenuItem();
            this.rtfExport = new System.Windows.Forms.MenuItem();
            this.htmlexport = new System.Windows.Forms.MenuItem();
            this.seperator25 = new System.Windows.Forms.MenuItem();
            this.ExitMenu = new System.Windows.Forms.MenuItem();
            this.editmenu = new System.Windows.Forms.MenuItem();
            this.UndoMenuItem = new System.Windows.Forms.MenuItem();
            this.RedoMenuItem = new System.Windows.Forms.MenuItem();
            this.seperator2 = new System.Windows.Forms.MenuItem();
            this.CutMenuItem = new System.Windows.Forms.MenuItem();
            this.CopyMenuItem = new System.Windows.Forms.MenuItem();
            this.PasteMenuItem = new System.Windows.Forms.MenuItem();
            this.seperator3 = new System.Windows.Forms.MenuItem();
            this.micopyas = new System.Windows.Forms.MenuItem();
            this.micopyhtml = new System.Windows.Forms.MenuItem();
            this.micopyrtf = new System.Windows.Forms.MenuItem();
            this.selectallmenu = new System.Windows.Forms.MenuItem();
            this.seperator23 = new System.Windows.Forms.MenuItem();
            this.miinsert = new System.Windows.Forms.MenuItem();
            this.miinschars = new System.Windows.Forms.MenuItem();
            this.midatetime = new System.Windows.Forms.MenuItem();
            this.mifileastext = new System.Windows.Forms.MenuItem();
            this.seperator53 = new System.Windows.Forms.MenuItem();
            this.mifilename = new System.Windows.Forms.MenuItem();
            this.mifullfilename = new System.Windows.Forms.MenuItem();
            this.seperator52 = new System.Windows.Forms.MenuItem();
            this.miemptycolumns = new System.Windows.Forms.MenuItem();
            this.miemptylines = new System.Windows.Forms.MenuItem();
            this.misearch = new System.Windows.Forms.MenuItem();
            this.findmenu = new System.Windows.Forms.MenuItem();
            this.mifindnext = new System.Windows.Forms.MenuItem();
            this.replacemenu = new System.Windows.Forms.MenuItem();
            this.seperator6 = new System.Windows.Forms.MenuItem();
            this.miincrementalsearch = new System.Windows.Forms.MenuItem();
            this.seperator9 = new System.Windows.Forms.MenuItem();
            this.findinfilesmenu = new System.Windows.Forms.MenuItem();
            this.miindent = new System.Windows.Forms.MenuItem();
            this.increaseindent = new System.Windows.Forms.MenuItem();
            this.decreaseindent = new System.Windows.Forms.MenuItem();
            this.doindent = new System.Windows.Forms.MenuItem();
            this.commentmenu = new System.Windows.Forms.MenuItem();
            this.commentline = new System.Windows.Forms.MenuItem();
            this.uncommentline = new System.Windows.Forms.MenuItem();
            this.minav = new System.Windows.Forms.MenuItem();
            this.migotol = new System.Windows.Forms.MenuItem();
            this.miswitchfile = new System.Windows.Forms.MenuItem();
            this.seperator18 = new System.Windows.Forms.MenuItem();
            this.gotofirstlinemenu = new System.Windows.Forms.MenuItem();
            this.gotoendmenu = new System.Windows.Forms.MenuItem();
            this.seperator17 = new System.Windows.Forms.MenuItem();
            this.navforwardmenu = new System.Windows.Forms.MenuItem();
            this.navbackwardmenu = new System.Windows.Forms.MenuItem();
            this.seperator16 = new System.Windows.Forms.MenuItem();
            this.migotoleftbracket = new System.Windows.Forms.MenuItem();
            this.migotorightbracket = new System.Windows.Forms.MenuItem();
            this.seperator15 = new System.Windows.Forms.MenuItem();
            this.mileftbracket2 = new System.Windows.Forms.MenuItem();
            this.migorightbracket2 = new System.Windows.Forms.MenuItem();
            this.seperator14 = new System.Windows.Forms.MenuItem();
            this.migoleftbracket3 = new System.Windows.Forms.MenuItem();
            this.migorightbracket3 = new System.Windows.Forms.MenuItem();
            this.miline = new System.Windows.Forms.MenuItem();
            this.movelineup = new System.Windows.Forms.MenuItem();
            this.movelinedown = new System.Windows.Forms.MenuItem();
            this.duplicatelinemenu = new System.Windows.Forms.MenuItem();
            this.seperator4 = new System.Windows.Forms.MenuItem();
            this.mijoinlines = new System.Windows.Forms.MenuItem();
            this.splitlinemenu = new System.Windows.Forms.MenuItem();
            this.seperator28 = new System.Windows.Forms.MenuItem();
            this.mireverselines = new System.Windows.Forms.MenuItem();
            this.misortalphabet = new System.Windows.Forms.MenuItem();
            this.misortlength = new System.Windows.Forms.MenuItem();
            this.seperator90 = new System.Windows.Forms.MenuItem();
            this.miremovecurrent = new System.Windows.Forms.MenuItem();
            this.removeemptylines = new System.Windows.Forms.MenuItem();
            this.mifolding = new System.Windows.Forms.MenuItem();
            this.foldallmenu = new System.Windows.Forms.MenuItem();
            this.unfoldmenu = new System.Windows.Forms.MenuItem();
            this.seperator70 = new System.Windows.Forms.MenuItem();
            this.foldselected = new System.Windows.Forms.MenuItem();
            this.unfoldselected = new System.Windows.Forms.MenuItem();
            this.miblankops = new System.Windows.Forms.MenuItem();
            this.mitts = new System.Windows.Forms.MenuItem();
            this.mitls = new System.Windows.Forms.MenuItem();
            this.mitpunctuation = new System.Windows.Forms.MenuItem();
            this.mittsandttl = new System.Windows.Forms.MenuItem();
            this.mieoltospace = new System.Windows.Forms.MenuItem();
            this.mispacetoeol = new System.Windows.Forms.MenuItem();
            this.miremoveeol = new System.Windows.Forms.MenuItem();
            this.micase = new System.Windows.Forms.MenuItem();
            this.caseuppermenu = new System.Windows.Forms.MenuItem();
            this.caselowermenu = new System.Windows.Forms.MenuItem();
            this.casetitlemenu = new System.Windows.Forms.MenuItem();
            this.swapcase = new System.Windows.Forms.MenuItem();
            this.mibookmarks = new System.Windows.Forms.MenuItem();
            this.Addbookmarkmenu = new System.Windows.Forms.MenuItem();
            this.removebookmarkmenu = new System.Windows.Forms.MenuItem();
            this.gotobookmark = new System.Windows.Forms.MenuItem();
            this.navigatethroughbookmarks = new System.Windows.Forms.MenuItem();
            this.miconversions = new System.Windows.Forms.MenuItem();
            this.mitocrlf = new System.Windows.Forms.MenuItem();
            this.mitocr = new System.Windows.Forms.MenuItem();
            this.mitolf = new System.Windows.Forms.MenuItem();
            this.seperator12 = new System.Windows.Forms.MenuItem();
            this.mispacestotab = new System.Windows.Forms.MenuItem();
            this.mitabtospaces = new System.Windows.Forms.MenuItem();
            this.seperator11 = new System.Windows.Forms.MenuItem();
            this.miselectiontohex = new System.Windows.Forms.MenuItem();
            this.miselectiontoascii = new System.Windows.Forms.MenuItem();
            this.seperator13 = new System.Windows.Forms.MenuItem();
            this.replacemode = new System.Windows.Forms.MenuItem();
            this.viewmenu = new System.Windows.Forms.MenuItem();
            this.statusbarmenuitem = new System.Windows.Forms.MenuItem();
            this.seperator5 = new System.Windows.Forms.MenuItem();
            this.mizoom = new System.Windows.Forms.MenuItem();
            this.mizoomin = new System.Windows.Forms.MenuItem();
            this.mizoomout = new System.Windows.Forms.MenuItem();
            this.mirestorezoom = new System.Windows.Forms.MenuItem();
            this.mitransparent = new System.Windows.Forms.MenuItem();
            this.mifullscreen = new System.Windows.Forms.MenuItem();
            this.seperator27 = new System.Windows.Forms.MenuItem();
            this.milanguage = new System.Windows.Forms.MenuItem();
            this.misnippets = new System.Windows.Forms.MenuItem();
            this.seperator29 = new System.Windows.Forms.MenuItem();
            this.mimarksel = new System.Windows.Forms.MenuItem();
            this.miredmark = new System.Windows.Forms.MenuItem();
            this.mibluemark = new System.Windows.Forms.MenuItem();
            this.migraymark = new System.Windows.Forms.MenuItem();
            this.migreenmark = new System.Windows.Forms.MenuItem();
            this.mimarkyellow = new System.Windows.Forms.MenuItem();
            this.miclearmarked = new System.Windows.Forms.MenuItem();
            this.mihiddenchars = new System.Windows.Forms.MenuItem();
            this.mishowunsaved = new System.Windows.Forms.MenuItem();
            this.wordwrapmenu = new System.Windows.Forms.MenuItem();
            this.seperator24 = new System.Windows.Forms.MenuItem();
            this.miprojectmanager = new System.Windows.Forms.MenuItem();
            this.seperator26 = new System.Windows.Forms.MenuItem();
            this.misplit = new System.Windows.Forms.MenuItem();
            this.toolsmenu = new System.Windows.Forms.MenuItem();
            this.commandermenu = new System.Windows.Forms.MenuItem();
            this.CommandPrompt = new System.Windows.Forms.MenuItem();
            this.pluginmanagermenu = new System.Windows.Forms.MenuItem();
            this.mikeymapeditor = new System.Windows.Forms.MenuItem();
            this.seperator8 = new System.Windows.Forms.MenuItem();
            this.miwebsearch = new System.Windows.Forms.MenuItem();
            this.migoogle = new System.Windows.Forms.MenuItem();
            this.miwiki = new System.Windows.Forms.MenuItem();
            this.miexecfile = new System.Windows.Forms.MenuItem();
            this.mirunscripts = new System.Windows.Forms.MenuItem();
            this.mirun = new System.Windows.Forms.MenuItem();
            this.mieditor = new System.Windows.Forms.MenuItem();
            this.seperator7 = new System.Windows.Forms.MenuItem();
            this.CompareMenu = new System.Windows.Forms.MenuItem();
            this.micomparewith = new System.Windows.Forms.MenuItem();
            this.seperator66 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.minewsnippet = new System.Windows.Forms.MenuItem();
            this.minewscript = new System.Windows.Forms.MenuItem();
            this.colorschememenu = new System.Windows.Forms.MenuItem();
            this.seperator83 = new System.Windows.Forms.MenuItem();
            this.OptionsMenu = new System.Windows.Forms.MenuItem();
            this.macrosmenu = new System.Windows.Forms.MenuItem();
            this.mirecordmacro = new System.Windows.Forms.MenuItem();
            this.seperator31 = new System.Windows.Forms.MenuItem();
            this.miplaybackmacro = new System.Windows.Forms.MenuItem();
            this.miexecmacromultiple = new System.Windows.Forms.MenuItem();
            this.misaverecordedmacro = new System.Windows.Forms.MenuItem();
            this.miclearmacrodata = new System.Windows.Forms.MenuItem();
            this.seperator32 = new System.Windows.Forms.MenuItem();
            this.mimacros = new System.Windows.Forms.MenuItem();
            this.miscripts = new System.Windows.Forms.MenuItem();
            this.pluginsmenuitem = new System.Windows.Forms.MenuItem();
            this.helpmenu = new System.Windows.Forms.MenuItem();
            this.miwikimenu = new System.Windows.Forms.MenuItem();
            this.miupdates = new System.Windows.Forms.MenuItem();
            this.seperator62 = new System.Windows.Forms.MenuItem();
            this.miresources = new System.Windows.Forms.MenuItem();
            this.miplugincentral = new System.Windows.Forms.MenuItem();
            this.mifb = new System.Windows.Forms.MenuItem();
            this.seperator30 = new System.Windows.Forms.MenuItem();
            this.aboutmenu = new System.Windows.Forms.MenuItem();
            this.status = new System.Windows.Forms.StatusStrip();
            this.mistats = new System.Windows.Forms.ToolStripStatusLabel();
            this.gapfiller2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.infolabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gapfiller1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.langmenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.gapfiller3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.zoom = new System.Windows.Forms.ToolStripDropDownButton();
            this.mizoom500 = new System.Windows.Forms.ToolStripMenuItem();
            this.mizoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.mizoom300 = new System.Windows.Forms.ToolStripMenuItem();
            this.mizoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.mizoom150 = new System.Windows.Forms.ToolStripMenuItem();
            this.mizoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.mizoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.dock = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.incrementalSearcher1 = new SS.Ynote.Classic.Features.Search.IncrementalSearcher();
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
            this.revertMenu,
            this.seperator19,
            this.recentfilesmenu,
            this.reopenclosedtab,
            this.seperator1,
            this.savemenu,
            this.misaveencoding,
            this.misaveas,
            this.misaveall,
            this.seperator20,
            this.miproperties,
            this.miopencontaining,
            this.midelete,
            this.seperator21,
            this.miprint,
            this.seperator22,
            this.miimport,
            this.miexport,
            this.seperator25,
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
            // revertMenu
            // 
            this.revertMenu.Index = 3;
            this.revertMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlF5;
            this.revertMenu.Text = "Revert";
            this.revertMenu.Click += new System.EventHandler(this.mirevert_Click);
            // 
            // seperator19
            // 
            this.seperator19.Index = 4;
            this.seperator19.Text = "-";
            // 
            // recentfilesmenu
            // 
            this.recentfilesmenu.Index = 5;
            this.recentfilesmenu.Text = "Recent Files";
            this.recentfilesmenu.Select += new System.EventHandler(this.recentfilesmenu_Select);
            // 
            // reopenclosedtab
            // 
            this.reopenclosedtab.Index = 6;
            this.reopenclosedtab.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftT;
            this.reopenclosedtab.Text = "Reopen Latest File";
            this.reopenclosedtab.Click += new System.EventHandler(this.reopenclosedtab_Click);
            // 
            // seperator1
            // 
            this.seperator1.Index = 7;
            this.seperator1.Text = "-";
            // 
            // savemenu
            // 
            this.savemenu.Index = 8;
            this.savemenu.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.savemenu.Text = "Save";
            this.savemenu.Click += new System.EventHandler(this.savemenu_Click);
            // 
            // misaveencoding
            // 
            this.misaveencoding.Index = 9;
            this.misaveencoding.Text = "Save File With Encoding";
            this.misaveencoding.Select += new System.EventHandler(this.misaveencoding_Select);
            // 
            // misaveas
            // 
            this.misaveas.Index = 10;
            this.misaveas.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.misaveas.Text = "Save As";
            this.misaveas.Click += new System.EventHandler(this.misaveas_Click);
            // 
            // misaveall
            // 
            this.misaveall.Index = 11;
            this.misaveall.Text = "Save All";
            this.misaveall.Click += new System.EventHandler(this.misaveall_Click);
            // 
            // seperator20
            // 
            this.seperator20.Index = 12;
            this.seperator20.Text = "-";
            // 
            // miproperties
            // 
            this.miproperties.Index = 13;
            this.miproperties.Shortcut = System.Windows.Forms.Shortcut.AltF1;
            this.miproperties.Text = "Properties";
            this.miproperties.Click += new System.EventHandler(this.miproperties_Click);
            // 
            // miopencontaining
            // 
            this.miopencontaining.Index = 14;
            this.miopencontaining.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftO;
            this.miopencontaining.Text = "Open Containing Folder";
            this.miopencontaining.Click += new System.EventHandler(this.miopencontaining_Click);
            // 
            // midelete
            // 
            this.midelete.Index = 15;
            this.midelete.Shortcut = System.Windows.Forms.Shortcut.AltBksp;
            this.midelete.Text = "Move To Recycle Bin";
            this.midelete.Click += new System.EventHandler(this.midelete_Click);
            // 
            // seperator21
            // 
            this.seperator21.Index = 16;
            this.seperator21.Text = "-";
            // 
            // miprint
            // 
            this.miprint.Index = 17;
            this.miprint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.miprint.Text = "Print";
            this.miprint.Click += new System.EventHandler(this.miprint_Click);
            // 
            // seperator22
            // 
            this.seperator22.Index = 18;
            this.seperator22.Text = "-";
            // 
            // miimport
            // 
            this.miimport.Index = 19;
            this.miimport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fromrtf,
            this.mifromdir});
            this.miimport.Text = "Import";
            // 
            // fromrtf
            // 
            this.fromrtf.Index = 0;
            this.fromrtf.Text = "From Rich Text";
            this.fromrtf.Click += new System.EventHandler(this.fromrtf_Click);
            // 
            // mifromdir
            // 
            this.mifromdir.Index = 1;
            this.mifromdir.Text = "Open Directory";
            this.mifromdir.Click += new System.EventHandler(this.mifromdir_Click);
            // 
            // miexport
            // 
            this.miexport.Index = 20;
            this.miexport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.pngexport,
            this.rtfExport,
            this.htmlexport});
            this.miexport.Text = "Export";
            // 
            // pngexport
            // 
            this.pngexport.Index = 0;
            this.pngexport.Text = "Image (PNG/JPG)";
            this.pngexport.Click += new System.EventHandler(this.pngexport_Click);
            // 
            // rtfExport
            // 
            this.rtfExport.Index = 1;
            this.rtfExport.Text = "RTF (Rich Text)";
            this.rtfExport.Click += new System.EventHandler(this.rtfExport_Click);
            // 
            // htmlexport
            // 
            this.htmlexport.Index = 2;
            this.htmlexport.Text = "HTML (Web Page)";
            this.htmlexport.Click += new System.EventHandler(this.htmlexport_Click);
            // 
            // seperator25
            // 
            this.seperator25.Index = 21;
            this.seperator25.Text = "-";
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
            this.seperator2,
            this.CutMenuItem,
            this.CopyMenuItem,
            this.PasteMenuItem,
            this.seperator3,
            this.micopyas,
            this.selectallmenu,
            this.seperator23,
            this.miinsert,
            this.misearch,
            this.miindent,
            this.commentmenu,
            this.minav,
            this.miline,
            this.mifolding,
            this.miblankops,
            this.micase,
            this.mibookmarks,
            this.miconversions,
            this.seperator13,
            this.replacemode});
            this.editmenu.Text = "Edit";
            // 
            // UndoMenuItem
            // 
            this.UndoMenuItem.Index = 0;
            this.UndoMenuItem.Text = "Undo";
            this.UndoMenuItem.Click += new System.EventHandler(this.UndoMenuItem_Click);
            // 
            // RedoMenuItem
            // 
            this.RedoMenuItem.Index = 1;
            this.RedoMenuItem.Text = "Redo";
            this.RedoMenuItem.Click += new System.EventHandler(this.RedoMenuItem_Click);
            // 
            // seperator2
            // 
            this.seperator2.Index = 2;
            this.seperator2.Text = "-";
            // 
            // CutMenuItem
            // 
            this.CutMenuItem.Index = 3;
            this.CutMenuItem.Text = "Cut";
            this.CutMenuItem.Click += new System.EventHandler(this.CutMenuItem_Click);
            // 
            // CopyMenuItem
            // 
            this.CopyMenuItem.Index = 4;
            this.CopyMenuItem.Text = "Copy";
            this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // PasteMenuItem
            // 
            this.PasteMenuItem.Index = 5;
            this.PasteMenuItem.Text = "Paste";
            this.PasteMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
            // 
            // seperator3
            // 
            this.seperator3.Index = 6;
            this.seperator3.Text = "-";
            // 
            // micopyas
            // 
            this.micopyas.Index = 7;
            this.micopyas.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.micopyhtml,
            this.micopyrtf});
            this.micopyas.Text = "Copy As";
            // 
            // micopyhtml
            // 
            this.micopyhtml.Index = 0;
            this.micopyhtml.Text = "Html";
            this.micopyhtml.Click += new System.EventHandler(this.micopyhtml_Click);
            // 
            // micopyrtf
            // 
            this.micopyrtf.Index = 1;
            this.micopyrtf.Text = "Rich Text";
            this.micopyrtf.Click += new System.EventHandler(this.micopyrtf_Click);
            // 
            // selectallmenu
            // 
            this.selectallmenu.Index = 8;
            this.selectallmenu.Text = "Select All";
            this.selectallmenu.Click += new System.EventHandler(this.selectallmenu_Click);
            // 
            // seperator23
            // 
            this.seperator23.Index = 9;
            this.seperator23.Text = "-";
            // 
            // miinsert
            // 
            this.miinsert.Index = 10;
            this.miinsert.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miinschars,
            this.midatetime,
            this.mifileastext,
            this.seperator53,
            this.mifilename,
            this.mifullfilename,
            this.seperator52,
            this.miemptycolumns,
            this.miemptylines});
            this.miinsert.Text = "Insert";
            // 
            // miinschars
            // 
            this.miinschars.Index = 0;
            this.miinschars.Text = "Characters";
            this.miinschars.Click += new System.EventHandler(this.menuItem71_Click);
            // 
            // midatetime
            // 
            this.midatetime.Index = 1;
            this.midatetime.Text = "Date/Time";
            this.midatetime.Click += new System.EventHandler(this.datetime_Click);
            // 
            // mifileastext
            // 
            this.mifileastext.Index = 2;
            this.mifileastext.Text = "File As Text";
            this.mifileastext.Click += new System.EventHandler(this.fileastext_Click);
            // 
            // seperator53
            // 
            this.seperator53.Index = 3;
            this.seperator53.Text = "-";
            // 
            // mifilename
            // 
            this.mifilename.Index = 4;
            this.mifilename.Text = "Filename";
            this.mifilename.Click += new System.EventHandler(this.filenamemenuitem_Click);
            // 
            // mifullfilename
            // 
            this.mifullfilename.Index = 5;
            this.mifullfilename.Text = "Full File Name";
            this.mifullfilename.Click += new System.EventHandler(this.fullfilenamemenuitem_Click);
            // 
            // seperator52
            // 
            this.seperator52.Index = 6;
            this.seperator52.Text = "-";
            // 
            // miemptycolumns
            // 
            this.miemptycolumns.Index = 7;
            this.miemptycolumns.Text = "Empty Columns";
            this.miemptycolumns.Click += new System.EventHandler(this.emptycolumns_Click);
            // 
            // miemptylines
            // 
            this.miemptylines.Index = 8;
            this.miemptylines.Text = "Empty Lines";
            this.miemptylines.Click += new System.EventHandler(this.emptylines_Click);
            // 
            // misearch
            // 
            this.misearch.Index = 11;
            this.misearch.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.findmenu,
            this.mifindnext,
            this.replacemenu,
            this.seperator6,
            this.miincrementalsearch,
            this.seperator9,
            this.findinfilesmenu});
            this.misearch.Text = "Search";
            // 
            // findmenu
            // 
            this.findmenu.Index = 0;
            this.findmenu.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.findmenu.Text = "Find";
            this.findmenu.Click += new System.EventHandler(this.findmenu_Click);
            // 
            // mifindnext
            // 
            this.mifindnext.Index = 1;
            this.mifindnext.Text = "Find Next      [F3]";
            this.mifindnext.Click += new System.EventHandler(this.mifindNext_Click);
            // 
            // replacemenu
            // 
            this.replacemenu.Index = 2;
            this.replacemenu.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
            this.replacemenu.Text = "Replace";
            this.replacemenu.Click += new System.EventHandler(this.replacemenu_Click);
            // 
            // seperator6
            // 
            this.seperator6.Index = 3;
            this.seperator6.Text = "-";
            // 
            // miincrementalsearch
            // 
            this.miincrementalsearch.Index = 4;
            this.miincrementalsearch.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.miincrementalsearch.Text = "Incremental Search";
            this.miincrementalsearch.Click += new System.EventHandler(this.incrementalsearchmenu_Click);
            // 
            // seperator9
            // 
            this.seperator9.Index = 5;
            this.seperator9.Text = "-";
            // 
            // findinfilesmenu
            // 
            this.findinfilesmenu.Index = 6;
            this.findinfilesmenu.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF;
            this.findinfilesmenu.Text = "Find In Files";
            this.findinfilesmenu.Click += new System.EventHandler(this.findinfilesmenu_Click);
            // 
            // miindent
            // 
            this.miindent.Index = 12;
            this.miindent.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.increaseindent,
            this.decreaseindent,
            this.doindent});
            this.miindent.Text = "Indent";
            // 
            // increaseindent
            // 
            this.increaseindent.Index = 0;
            this.increaseindent.Text = "Increase";
            this.increaseindent.Click += new System.EventHandler(this.increaseindent_Click);
            // 
            // decreaseindent
            // 
            this.decreaseindent.Index = 1;
            this.decreaseindent.Text = "Decrease";
            this.decreaseindent.Click += new System.EventHandler(this.decreaseindent_Click);
            // 
            // doindent
            // 
            this.doindent.Index = 2;
            this.doindent.Text = "Indent Selection";
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
            this.commentline.Text = "Comment Selected";
            this.commentline.Click += new System.EventHandler(this.commentline_Click);
            // 
            // uncommentline
            // 
            this.uncommentline.Index = 1;
            this.uncommentline.Text = "Uncomment Selected";
            this.uncommentline.Click += new System.EventHandler(this.commentline_Click);
            // 
            // minav
            // 
            this.minav.Index = 14;
            this.minav.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.migotol,
            this.miswitchfile,
            this.seperator18,
            this.gotofirstlinemenu,
            this.gotoendmenu,
            this.seperator17,
            this.navforwardmenu,
            this.navbackwardmenu,
            this.seperator16,
            this.migotoleftbracket,
            this.migotorightbracket,
            this.seperator15,
            this.mileftbracket2,
            this.migorightbracket2,
            this.seperator14,
            this.migoleftbracket3,
            this.migorightbracket3});
            this.minav.Text = "Navigation";
            // 
            // migotol
            // 
            this.migotol.Index = 0;
            this.migotol.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.migotol.Text = "Go To Line";
            this.migotol.Click += new System.EventHandler(this.migotoline_Click);
            // 
            // miswitchfile
            // 
            this.miswitchfile.Index = 1;
            this.miswitchfile.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this.miswitchfile.Text = "Switch File";
            this.miswitchfile.Click += new System.EventHandler(this.menuItem84_Click);
            // 
            // seperator18
            // 
            this.seperator18.Index = 2;
            this.seperator18.Text = "-";
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
            // seperator17
            // 
            this.seperator17.Index = 5;
            this.seperator17.Text = "-";
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
            // seperator16
            // 
            this.seperator16.Index = 8;
            this.seperator16.Text = "-";
            // 
            // migotoleftbracket
            // 
            this.migotoleftbracket.Index = 9;
            this.migotoleftbracket.Text = "Go to Left Bracket (";
            this.migotoleftbracket.Click += new System.EventHandler(this.migoleftbracket_Click);
            // 
            // migotorightbracket
            // 
            this.migotorightbracket.Index = 10;
            this.migotorightbracket.Text = "Go to Right Bracket )";
            this.migotorightbracket.Click += new System.EventHandler(this.migorightbracket_Click);
            // 
            // seperator15
            // 
            this.seperator15.Index = 11;
            this.seperator15.Text = "-";
            // 
            // mileftbracket2
            // 
            this.mileftbracket2.Index = 12;
            this.mileftbracket2.Text = "Go to Left Bracket {";
            this.mileftbracket2.Click += new System.EventHandler(this.migoleftbracket2_Click);
            // 
            // migorightbracket2
            // 
            this.migorightbracket2.Index = 13;
            this.migorightbracket2.Text = "Go to Right Bracket }";
            this.migorightbracket2.Click += new System.EventHandler(this.migorightbracket2_Click);
            // 
            // seperator14
            // 
            this.seperator14.Index = 14;
            this.seperator14.Text = "-";
            // 
            // migoleftbracket3
            // 
            this.migoleftbracket3.Index = 15;
            this.migoleftbracket3.Text = "Go Left Bracket [";
            this.migoleftbracket3.Click += new System.EventHandler(this.migoleftbracket3_Click);
            // 
            // migorightbracket3
            // 
            this.migorightbracket3.Index = 16;
            this.migorightbracket3.Text = "Go Right Bracket ]";
            this.migorightbracket3.Click += new System.EventHandler(this.migorightbracket3_Click);
            // 
            // miline
            // 
            this.miline.Index = 15;
            this.miline.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.movelineup,
            this.movelinedown,
            this.duplicatelinemenu,
            this.seperator4,
            this.mijoinlines,
            this.splitlinemenu,
            this.seperator28,
            this.mireverselines,
            this.misortalphabet,
            this.misortlength,
            this.seperator90,
            this.miremovecurrent,
            this.removeemptylines});
            this.miline.Text = "Line";
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
            // seperator4
            // 
            this.seperator4.Index = 3;
            this.seperator4.Text = "-";
            // 
            // mijoinlines
            // 
            this.mijoinlines.Index = 4;
            this.mijoinlines.Shortcut = System.Windows.Forms.Shortcut.CtrlJ;
            this.mijoinlines.Text = "Join Lines";
            this.mijoinlines.Click += new System.EventHandler(this.menuItem29_Click);
            // 
            // splitlinemenu
            // 
            this.splitlinemenu.Index = 5;
            this.splitlinemenu.Text = "Split Line";
            this.splitlinemenu.Click += new System.EventHandler(this.splitlinemenu_Click);
            // 
            // seperator28
            // 
            this.seperator28.Index = 6;
            this.seperator28.Text = "-";
            // 
            // mireverselines
            // 
            this.mireverselines.Index = 7;
            this.mireverselines.Text = "Sort Lines (Reverse)";
            this.mireverselines.Click += new System.EventHandler(this.menuItem69_Click);
            // 
            // misortalphabet
            // 
            this.misortalphabet.Index = 8;
            this.misortalphabet.Text = "Sort Alphabetically";
            this.misortalphabet.Click += new System.EventHandler(this.menuItem65_Click);
            // 
            // misortlength
            // 
            this.misortlength.Index = 9;
            this.misortlength.Text = "Sort Lengthwise";
            this.misortlength.Click += new System.EventHandler(this.misortlength_Click);
            // 
            // seperator90
            // 
            this.seperator90.Index = 10;
            this.seperator90.Text = "-";
            // 
            // miremovecurrent
            // 
            this.miremovecurrent.Index = 11;
            this.miremovecurrent.Text = "Remove Current Line";
            this.miremovecurrent.Click += new System.EventHandler(this.removelinemenu_Click);
            // 
            // removeemptylines
            // 
            this.removeemptylines.Index = 12;
            this.removeemptylines.Text = "Remove Empty Lines";
            this.removeemptylines.Click += new System.EventHandler(this.removeemptylines_Click);
            // 
            // mifolding
            // 
            this.mifolding.Index = 16;
            this.mifolding.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.foldallmenu,
            this.unfoldmenu,
            this.seperator70,
            this.foldselected,
            this.unfoldselected});
            this.mifolding.Text = "Folding";
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
            // seperator70
            // 
            this.seperator70.Index = 2;
            this.seperator70.Text = "-";
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
            // miblankops
            // 
            this.miblankops.Index = 17;
            this.miblankops.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mitts,
            this.mitls,
            this.mitpunctuation,
            this.mittsandttl,
            this.mieoltospace,
            this.mispacetoeol,
            this.miremoveeol});
            this.miblankops.Text = "Blank Operations";
            // 
            // mitts
            // 
            this.mitts.Index = 0;
            this.mitts.Text = "Trim Trailing Space";
            this.mitts.Click += new System.EventHandler(this.menuItem75_Click);
            // 
            // mitls
            // 
            this.mitls.Index = 1;
            this.mitls.Text = "Trim Leading Space";
            this.mitls.Click += new System.EventHandler(this.menuItem76_Click);
            // 
            // mitpunctuation
            // 
            this.mitpunctuation.Index = 2;
            this.mitpunctuation.Text = "Trim Punctuations";
            this.mitpunctuation.Click += new System.EventHandler(this.mitrimpunctuation_Click);
            // 
            // mittsandttl
            // 
            this.mittsandttl.Index = 3;
            this.mittsandttl.Text = "Trim Trailing and Leading Space";
            this.mittsandttl.Click += new System.EventHandler(this.menuItem79_Click);
            // 
            // mieoltospace
            // 
            this.mieoltospace.Index = 4;
            this.mieoltospace.Text = "EOL to Space";
            this.mieoltospace.Click += new System.EventHandler(this.menuItem78_Click);
            // 
            // mispacetoeol
            // 
            this.mispacetoeol.Index = 5;
            this.mispacetoeol.Text = "Space to EOL";
            this.mispacetoeol.Click += new System.EventHandler(this.menuItem80_Click);
            // 
            // miremoveeol
            // 
            this.miremoveeol.Index = 6;
            this.miremoveeol.Text = "Remove EOL";
            this.miremoveeol.Click += new System.EventHandler(this.menuItem77_Click);
            // 
            // micase
            // 
            this.micase.Index = 18;
            this.micase.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.caseuppermenu,
            this.caselowermenu,
            this.casetitlemenu,
            this.swapcase});
            this.micase.Text = "Case";
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
            // mibookmarks
            // 
            this.mibookmarks.Index = 19;
            this.mibookmarks.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Addbookmarkmenu,
            this.removebookmarkmenu,
            this.gotobookmark,
            this.navigatethroughbookmarks});
            this.mibookmarks.Text = "Bookmarks";
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
            // miconversions
            // 
            this.miconversions.Index = 20;
            this.miconversions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mitocrlf,
            this.mitocr,
            this.mitolf,
            this.seperator12,
            this.mispacestotab,
            this.mitabtospaces,
            this.seperator11,
            this.miselectiontohex,
            this.miselectiontoascii});
            this.miconversions.Text = "Conversions";
            // 
            // mitocrlf
            // 
            this.mitocrlf.Index = 0;
            this.mitocrlf.Text = "To CRLF (Windows)";
            this.mitocrlf.Click += new System.EventHandler(this.mitocrlf_Click);
            // 
            // mitocr
            // 
            this.mitocr.Index = 1;
            this.mitocr.Text = "To CR (Mac)";
            this.mitocr.Click += new System.EventHandler(this.mitocr_Click);
            // 
            // mitolf
            // 
            this.mitolf.Index = 2;
            this.mitolf.Text = "To LF (Unix)";
            this.mitolf.Click += new System.EventHandler(this.mitolf_Click);
            // 
            // seperator12
            // 
            this.seperator12.Index = 3;
            this.seperator12.Text = "-";
            // 
            // mispacestotab
            // 
            this.mispacestotab.Index = 4;
            this.mispacestotab.Text = "Spaces To Tab";
            this.mispacestotab.Click += new System.EventHandler(this.mispacestotab_Click);
            // 
            // mitabtospaces
            // 
            this.mitabtospaces.Index = 5;
            this.mitabtospaces.Text = "Tab To Spaces";
            this.mitabtospaces.Click += new System.EventHandler(this.mitabtospaces_Click);
            // 
            // seperator11
            // 
            this.seperator11.Index = 6;
            this.seperator11.Text = "-";
            // 
            // miselectiontohex
            // 
            this.miselectiontohex.Index = 7;
            this.miselectiontohex.Text = "Selection to Hex";
            this.miselectiontohex.Click += new System.EventHandler(this.miseltohex_Click);
            // 
            // miselectiontoascii
            // 
            this.miselectiontoascii.Index = 8;
            this.miselectiontoascii.Text = "Selection to ASCII";
            this.miselectiontoascii.Click += new System.EventHandler(this.miseltoASCII_Click);
            // 
            // seperator13
            // 
            this.seperator13.Index = 21;
            this.seperator13.Text = "-";
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
            this.seperator5,
            this.mizoom,
            this.mitransparent,
            this.mifullscreen,
            this.seperator27,
            this.milanguage,
            this.misnippets,
            this.seperator29,
            this.mimarksel,
            this.mihiddenchars,
            this.mishowunsaved,
            this.wordwrapmenu,
            this.seperator24,
            this.miprojectmanager,
            this.seperator26,
            this.misplit});
            this.viewmenu.Text = "View";
            // 
            // statusbarmenuitem
            // 
            this.statusbarmenuitem.Checked = true;
            this.statusbarmenuitem.Index = 0;
            this.statusbarmenuitem.Text = "Status Bar";
            this.statusbarmenuitem.Click += new System.EventHandler(this.statusbarmenuitem_Click);
            // 
            // seperator5
            // 
            this.seperator5.Index = 1;
            this.seperator5.Text = "-";
            // 
            // mizoom
            // 
            this.mizoom.Index = 2;
            this.mizoom.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mizoomin,
            this.mizoomout,
            this.mirestorezoom});
            this.mizoom.Text = "Zoom";
            // 
            // mizoomin
            // 
            this.mizoomin.Index = 0;
            this.mizoomin.Text = "Zoom In";
            this.mizoomin.Click += new System.EventHandler(this.mizoomin_Click);
            // 
            // mizoomout
            // 
            this.mizoomout.Index = 1;
            this.mizoomout.Text = "Zoom Out";
            this.mizoomout.Click += new System.EventHandler(this.mizoomout_Click);
            // 
            // mirestorezoom
            // 
            this.mirestorezoom.Index = 2;
            this.mirestorezoom.Text = "Restore Default";
            this.mirestorezoom.Click += new System.EventHandler(this.mirestoredefault_Click);
            // 
            // mitransparent
            // 
            this.mitransparent.Index = 3;
            this.mitransparent.Text = "Transparent UI";
            this.mitransparent.Click += new System.EventHandler(this.mitransparent_Click);
            // 
            // mifullscreen
            // 
            this.mifullscreen.Index = 4;
            this.mifullscreen.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.mifullscreen.Text = "Full Screen";
            this.mifullscreen.Click += new System.EventHandler(this.mifullscreen_Click);
            // 
            // seperator27
            // 
            this.seperator27.Index = 5;
            this.seperator27.Text = "-";
            // 
            // milanguage
            // 
            this.milanguage.Index = 6;
            this.milanguage.Text = "Syntax Highlight";
            this.milanguage.Select += new System.EventHandler(this.milanguage_Select);
            // 
            // misnippets
            // 
            this.misnippets.Index = 7;
            this.misnippets.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
            this.misnippets.Text = "Snippets";
            this.misnippets.Click += new System.EventHandler(this.misnippets_Click);
            // 
            // seperator29
            // 
            this.seperator29.Index = 8;
            this.seperator29.Text = "-";
            // 
            // mimarksel
            // 
            this.mimarksel.Index = 9;
            this.mimarksel.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miredmark,
            this.mibluemark,
            this.migraymark,
            this.migreenmark,
            this.mimarkyellow,
            this.miclearmarked});
            this.mimarksel.Text = "Mark Selection";
            // 
            // miredmark
            // 
            this.miredmark.Index = 0;
            this.miredmark.Text = "Using Red Style";
            this.miredmark.Click += new System.EventHandler(this.mimarkRed_Click);
            // 
            // mibluemark
            // 
            this.mibluemark.Index = 1;
            this.mibluemark.Text = "Using Blue Style";
            this.mibluemark.Click += new System.EventHandler(this.mimarkblue_Click);
            // 
            // migraymark
            // 
            this.migraymark.Index = 2;
            this.migraymark.Text = "Using Gray Style";
            this.migraymark.Click += new System.EventHandler(this.mimarkgray_Click);
            // 
            // migreenmark
            // 
            this.migreenmark.Index = 3;
            this.migreenmark.Text = "Using Green Style";
            this.migreenmark.Click += new System.EventHandler(this.mimarkgreen_Click);
            // 
            // mimarkyellow
            // 
            this.mimarkyellow.Index = 4;
            this.mimarkyellow.Text = "Using Yellow Style";
            this.mimarkyellow.Click += new System.EventHandler(this.mimarkyellow_Click);
            // 
            // miclearmarked
            // 
            this.miclearmarked.Index = 5;
            this.miclearmarked.Text = "Clear Marked";
            this.miclearmarked.Click += new System.EventHandler(this.miclearMarked_Click);
            // 
            // mihiddenchars
            // 
            this.mihiddenchars.Index = 10;
            this.mihiddenchars.Text = "Hidden Characters";
            this.mihiddenchars.Click += new System.EventHandler(this.mihiddenchars_Click);
            // 
            // mishowunsaved
            // 
            this.mishowunsaved.Index = 11;
            this.mishowunsaved.Text = "Show Unsaved Changes";
            this.mishowunsaved.Click += new System.EventHandler(this.mishowunsaved_Click);
            // 
            // wordwrapmenu
            // 
            this.wordwrapmenu.Index = 12;
            this.wordwrapmenu.Text = "Word Wrap";
            this.wordwrapmenu.Click += new System.EventHandler(this.wordwrapmenu_Click);
            // 
            // seperator24
            // 
            this.seperator24.Index = 13;
            this.seperator24.Text = "-";
            // 
            // miprojectmanager
            // 
            this.miprojectmanager.Index = 14;
            this.miprojectmanager.Text = "Project Manager";
            this.miprojectmanager.Click += new System.EventHandler(this.menuItem95_Click);
            // 
            // seperator26
            // 
            this.seperator26.Index = 15;
            this.seperator26.Text = "-";
            // 
            // misplit
            // 
            this.misplit.Index = 16;
            this.misplit.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.misplit.Text = "Split";
            this.misplit.Click += new System.EventHandler(this.menuItem57_Click);
            // 
            // toolsmenu
            // 
            this.toolsmenu.Index = 3;
            this.toolsmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.commandermenu,
            this.CommandPrompt,
            this.pluginmanagermenu,
            this.mikeymapeditor,
            this.seperator8,
            this.miwebsearch,
            this.miexecfile,
            this.mirunscripts,
            this.seperator7,
            this.CompareMenu,
            this.micomparewith,
            this.seperator66,
            this.menuItem1,
            this.colorschememenu,
            this.seperator83,
            this.OptionsMenu});
            this.toolsmenu.Text = "Tools";
            // 
            // commandermenu
            // 
            this.commandermenu.Index = 0;
            this.commandermenu.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP;
            this.commandermenu.Text = "Commander";
            this.commandermenu.Click += new System.EventHandler(this.commanderMenu_Click);
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
            // mikeymapeditor
            // 
            this.mikeymapeditor.Index = 3;
            this.mikeymapeditor.Text = "Keymap Editor";
            this.mikeymapeditor.Click += new System.EventHandler(this.menuItem100_Click);
            // 
            // seperator8
            // 
            this.seperator8.Index = 4;
            this.seperator8.Text = "-";
            // 
            // miwebsearch
            // 
            this.miwebsearch.Index = 5;
            this.miwebsearch.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.migoogle,
            this.miwiki});
            this.miwebsearch.Text = "Web Search";
            // 
            // migoogle
            // 
            this.migoogle.Index = 0;
            this.migoogle.Text = "Google";
            this.migoogle.Click += new System.EventHandler(this.migoogle_Click);
            // 
            // miwiki
            // 
            this.miwiki.Index = 1;
            this.miwiki.Text = "Wikipedia";
            this.miwiki.Click += new System.EventHandler(this.miwiki_Click);
            // 
            // miexecfile
            // 
            this.miexecfile.Index = 6;
            this.miexecfile.Text = "Execute File";
            this.miexecfile.Click += new System.EventHandler(this.menuItem30_Click);
            // 
            // mirunscripts
            // 
            this.mirunscripts.Index = 7;
            this.mirunscripts.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mirun,
            this.mieditor});
            this.mirunscripts.Text = "Run Scripts";
            // 
            // mirun
            // 
            this.mirun.Index = 0;
            this.mirun.Text = "Run";
            this.mirun.Click += new System.EventHandler(this.mirun_Click);
            // 
            // mieditor
            // 
            this.mieditor.Index = 1;
            this.mieditor.Text = "Editor";
            this.mieditor.Click += new System.EventHandler(this.miruneditor_Click);
            // 
            // seperator7
            // 
            this.seperator7.Index = 8;
            this.seperator7.Text = "-";
            // 
            // CompareMenu
            // 
            this.CompareMenu.Index = 9;
            this.CompareMenu.Text = "Compare";
            this.CompareMenu.Click += new System.EventHandler(this.CompareMenu_Click);
            // 
            // micomparewith
            // 
            this.micomparewith.Index = 10;
            this.micomparewith.Text = "Compare Document With";
            this.micomparewith.Click += new System.EventHandler(this.micomparedocwith_Click);
            // 
            // seperator66
            // 
            this.seperator66.Index = 11;
            this.seperator66.Text = "-";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 12;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.minewsnippet,
            this.minewscript});
            this.menuItem1.Text = "Create New";
            // 
            // minewsnippet
            // 
            this.minewsnippet.Index = 0;
            this.minewsnippet.Text = "Snippet";
            this.minewsnippet.Click += new System.EventHandler(this.minewsnippet_Click);
            // 
            // minewscript
            // 
            this.minewscript.Index = 1;
            this.minewscript.Text = "YnoteScript";
            this.minewscript.Click += new System.EventHandler(this.minewscript_Click);
            // 
            // colorschememenu
            // 
            this.colorschememenu.Index = 13;
            this.colorschememenu.Text = "Color Scheme";
            this.colorschememenu.Select += new System.EventHandler(this.colorschememenu_Select);
            // 
            // seperator83
            // 
            this.seperator83.Index = 14;
            this.seperator83.Text = "-";
            // 
            // OptionsMenu
            // 
            this.OptionsMenu.Index = 15;
            this.OptionsMenu.Text = "Options";
            this.OptionsMenu.Click += new System.EventHandler(this.OptionsMenu_Click);
            // 
            // macrosmenu
            // 
            this.macrosmenu.Index = 4;
            this.macrosmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mirecordmacro,
            this.seperator31,
            this.miplaybackmacro,
            this.miexecmacromultiple,
            this.misaverecordedmacro,
            this.miclearmacrodata,
            this.seperator32,
            this.mimacros,
            this.miscripts});
            this.macrosmenu.Text = "Macros";
            // 
            // mirecordmacro
            // 
            this.mirecordmacro.Index = 0;
            this.mirecordmacro.Text = "Start / Stop Recording";
            this.mirecordmacro.Click += new System.EventHandler(this.mimacrorecord_Click);
            // 
            // seperator31
            // 
            this.seperator31.Index = 1;
            this.seperator31.Text = "-";
            // 
            // miplaybackmacro
            // 
            this.miplaybackmacro.Index = 2;
            this.miplaybackmacro.Text = "Playback Macro";
            this.miplaybackmacro.Click += new System.EventHandler(this.miExecmacro_Click);
            // 
            // miexecmacromultiple
            // 
            this.miexecmacromultiple.Index = 3;
            this.miexecmacromultiple.Text = "Execute Macro Multiple Times";
            this.miexecmacromultiple.Click += new System.EventHandler(this.mimultimacro_Click);
            // 
            // misaverecordedmacro
            // 
            this.misaverecordedmacro.Index = 4;
            this.misaverecordedmacro.Text = "Save Recorded Macro";
            this.misaverecordedmacro.Click += new System.EventHandler(this.misavemacro_Click);
            // 
            // miclearmacrodata
            // 
            this.miclearmacrodata.Index = 5;
            this.miclearmacrodata.Text = "Clear Macro Data";
            this.miclearmacrodata.Click += new System.EventHandler(this.miclearmacro_Click);
            // 
            // seperator32
            // 
            this.seperator32.Index = 6;
            this.seperator32.Text = "-";
            // 
            // mimacros
            // 
            this.mimacros.Index = 7;
            this.mimacros.Text = "Macros";
            this.mimacros.Select += new System.EventHandler(this.mimacros_Select);
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
            this.miwikimenu,
            this.miupdates,
            this.seperator62,
            this.miresources,
            this.miplugincentral,
            this.mifb,
            this.seperator30,
            this.aboutmenu});
            this.helpmenu.Text = "Help";
            // 
            // miwikimenu
            // 
            this.miwikimenu.Index = 0;
            this.miwikimenu.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.miwikimenu.Text = "Wiki";
            this.miwikimenu.Click += new System.EventHandler(this.miwikimenu_Click);
            // 
            // miupdates
            // 
            this.miupdates.Index = 1;
            this.miupdates.Text = "Check For Updates";
            this.miupdates.Click += new System.EventHandler(this.miupdates_Click);
            // 
            // seperator62
            // 
            this.seperator62.Index = 2;
            this.seperator62.Text = "-";
            // 
            // miresources
            // 
            this.miresources.Index = 3;
            this.miresources.Text = "Resources";
            // 
            // miplugincentral
            // 
            this.miplugincentral.Index = 4;
            this.miplugincentral.Text = "Plugin Central";
            // 
            // mifb
            // 
            this.mifb.Index = 5;
            this.mifb.Text = "Facebook";
            this.mifb.Click += new System.EventHandler(this.mifb_Click);
            // 
            // seperator30
            // 
            this.seperator30.Index = 6;
            this.seperator30.Text = "-";
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
            this.mistats,
            this.gapfiller2,
            this.infolabel,
            this.gapfiller1,
            this.langmenu,
            this.gapfiller3,
            this.zoom});
            this.status.Location = new System.Drawing.Point(0, 338);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(767, 22);
            this.status.TabIndex = 3;
            // 
            // mistats
            // 
            this.mistats.Name = "mistats";
            this.mistats.Size = new System.Drawing.Size(39, 17);
            this.mistats.Text = "Ready";
            // 
            // gapfiller2
            // 
            this.gapfiller2.Name = "gapfiller2";
            this.gapfiller2.Size = new System.Drawing.Size(206, 17);
            this.gapfiller2.Spring = true;
            // 
            // infolabel
            // 
            this.infolabel.Name = "infolabel";
            this.infolabel.Size = new System.Drawing.Size(0, 17);
            // 
            // gapfiller1
            // 
            this.gapfiller1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.gapfiller1.Name = "gapfiller1";
            this.gapfiller1.Size = new System.Drawing.Size(206, 17);
            this.gapfiller1.Spring = true;
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
            // gapfiller3
            // 
            this.gapfiller3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.gapfiller3.Name = "gapfiller3";
            this.gapfiller3.Size = new System.Drawing.Size(206, 17);
            this.gapfiller3.Spring = true;
            // 
            // zoom
            // 
            this.zoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.zoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mizoom500,
            this.mizoom400,
            this.mizoom300,
            this.mizoom200,
            this.mizoom150,
            this.mizoom100,
            this.mizoom50});
            this.zoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(52, 20);
            this.zoom.Text = "Zoom";
            this.zoom.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.zoom_DropDownItemClicked);
            // 
            // mizoom500
            // 
            this.mizoom500.Name = "mizoom500";
            this.mizoom500.Size = new System.Drawing.Size(92, 22);
            this.mizoom500.Text = "500";
            // 
            // mizoom400
            // 
            this.mizoom400.Name = "mizoom400";
            this.mizoom400.Size = new System.Drawing.Size(92, 22);
            this.mizoom400.Text = "400";
            // 
            // mizoom300
            // 
            this.mizoom300.Name = "mizoom300";
            this.mizoom300.Size = new System.Drawing.Size(92, 22);
            this.mizoom300.Text = "300";
            // 
            // mizoom200
            // 
            this.mizoom200.Name = "mizoom200";
            this.mizoom200.Size = new System.Drawing.Size(92, 22);
            this.mizoom200.Text = "200";
            // 
            // mizoom150
            // 
            this.mizoom150.Name = "mizoom150";
            this.mizoom150.Size = new System.Drawing.Size(92, 22);
            this.mizoom150.Text = "150";
            // 
            // mizoom100
            // 
            this.mizoom100.Name = "mizoom100";
            this.mizoom100.Size = new System.Drawing.Size(92, 22);
            this.mizoom100.Text = "100";
            // 
            // mizoom50
            // 
            this.mizoom50.Name = "mizoom50";
            this.mizoom50.Size = new System.Drawing.Size(92, 22);
            this.mizoom50.Text = "50";
            // 
            // dock
            // 
            this.dock.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.dock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dock.DockBottomPortion = 0.4D;
            this.dock.Location = new System.Drawing.Point(0, 0);
            this.dock.Name = "dock";
            this.dock.Size = new System.Drawing.Size(767, 299);
            this.dock.TabIndex = 0;
            this.dock.ActiveDocumentChanged += new System.EventHandler(this.dock_ActiveDocumentChanged);
            // 
            // incrementalSearcher1
            // 
            this.incrementalSearcher1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.incrementalSearcher1.Location = new System.Drawing.Point(0, 299);
            this.incrementalSearcher1.Name = "incrementalSearcher1";
            this.incrementalSearcher1.Size = new System.Drawing.Size(767, 39);
            this.incrementalSearcher1.TabIndex = 4;
            this.incrementalSearcher1.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 360);
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
        private System.Windows.Forms.MenuItem seperator2;
        private System.Windows.Forms.MenuItem CutMenuItem;
        private System.Windows.Forms.MenuItem CopyMenuItem;
        private System.Windows.Forms.MenuItem PasteMenuItem;
        private System.Windows.Forms.MenuItem aboutmenu;
        private System.Windows.Forms.MenuItem seperator19;
        private System.Windows.Forms.MenuItem savemenu;
        private System.Windows.Forms.MenuItem misaveas;
        private System.Windows.Forms.MenuItem statusbarmenuitem;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.MenuItem miwikimenu;
        private System.Windows.Forms.MenuItem seperator30;
        private System.Windows.Forms.MenuItem revertMenu;
        private System.Windows.Forms.MenuItem seperator20;
        private System.Windows.Forms.MenuItem miproperties;
        private System.Windows.Forms.MenuItem miopencontaining;
        private System.Windows.Forms.MenuItem midelete;
        private System.Windows.Forms.MenuItem seperator21;
        private System.Windows.Forms.MenuItem miprint;
        private System.Windows.Forms.MenuItem misaveall;
        private System.Windows.Forms.MenuItem seperator22;
        private System.Windows.Forms.MenuItem miimport;
        private System.Windows.Forms.MenuItem fromrtf;
        private System.Windows.Forms.MenuItem mifromdir;
        private System.Windows.Forms.MenuItem miexport;
        private System.Windows.Forms.MenuItem rtfExport;
        private System.Windows.Forms.MenuItem pngexport;
        private System.Windows.Forms.MenuItem htmlexport;
        private System.Windows.Forms.MenuItem seperator25;
        private System.Windows.Forms.MenuItem ExitMenu;
        private System.Windows.Forms.MenuItem selectallmenu;
        private System.Windows.Forms.MenuItem seperator23;
        private System.Windows.Forms.MenuItem misearch;
        private System.Windows.Forms.MenuItem findmenu;
        private System.Windows.Forms.MenuItem replacemenu;
        private System.Windows.Forms.MenuItem seperator6;
        private System.Windows.Forms.MenuItem miincrementalsearch;
        private System.Windows.Forms.MenuItem seperator9;
        private System.Windows.Forms.MenuItem findinfilesmenu;
        private System.Windows.Forms.MenuItem miinsert;
        private System.Windows.Forms.MenuItem midatetime;
        private System.Windows.Forms.MenuItem mifileastext;
        private System.Windows.Forms.MenuItem seperator53;
        private System.Windows.Forms.MenuItem mifilename;
        private System.Windows.Forms.MenuItem mifullfilename;
        private System.Windows.Forms.MenuItem seperator52;
        private System.Windows.Forms.MenuItem miemptycolumns;
        private System.Windows.Forms.MenuItem miemptylines;
        private System.Windows.Forms.MenuItem seperator5;
        private System.Windows.Forms.MenuItem mizoom;
        private System.Windows.Forms.MenuItem mizoomin;
        private System.Windows.Forms.MenuItem mizoomout;
        private System.Windows.Forms.MenuItem mirestorezoom;
        private System.Windows.Forms.MenuItem mitransparent;
        private System.Windows.Forms.MenuItem mifullscreen;
        private System.Windows.Forms.MenuItem misplit;
        private System.Windows.Forms.MenuItem miindent;
        private System.Windows.Forms.MenuItem increaseindent;
        private System.Windows.Forms.MenuItem decreaseindent;
        private System.Windows.Forms.MenuItem CommandPrompt;
        private System.Windows.Forms.MenuItem pluginmanagermenu;
        private System.Windows.Forms.MenuItem seperator66;
        private System.Windows.Forms.MenuItem OptionsMenu;
        private System.Windows.Forms.MenuItem seperator62;
        private System.Windows.Forms.MenuItem miresources;
        private System.Windows.Forms.MenuItem miplugincentral;
        private System.Windows.Forms.MenuItem mifb;
        private System.Windows.Forms.MenuItem mifolding;
        private System.Windows.Forms.MenuItem foldallmenu;
        private System.Windows.Forms.MenuItem unfoldmenu;
        private System.Windows.Forms.MenuItem seperator70;
        private System.Windows.Forms.MenuItem foldselected;
        private System.Windows.Forms.MenuItem unfoldselected;
        private System.Windows.Forms.MenuItem micase;
        private System.Windows.Forms.MenuItem caseuppermenu;
        private System.Windows.Forms.MenuItem caselowermenu;
        private System.Windows.Forms.MenuItem casetitlemenu;
        private System.Windows.Forms.MenuItem miline;
        private System.Windows.Forms.MenuItem miremovecurrent;
        private System.Windows.Forms.MenuItem movelineup;
        private System.Windows.Forms.MenuItem movelinedown;
        private System.Windows.Forms.MenuItem seperator4;
        private System.Windows.Forms.MenuItem duplicatelinemenu;
        private System.Windows.Forms.MenuItem splitlinemenu;
        private System.Windows.Forms.MenuItem seperator90;
        private System.Windows.Forms.MenuItem removeemptylines;
        private System.Windows.Forms.MenuItem minav;
        private System.Windows.Forms.MenuItem gotofirstlinemenu;
        private System.Windows.Forms.MenuItem gotoendmenu;
        private System.Windows.Forms.MenuItem navforwardmenu;
        private System.Windows.Forms.MenuItem navbackwardmenu;
        private System.Windows.Forms.MenuItem seperator27;
        private System.Windows.Forms.MenuItem seperator8;
        private System.Windows.Forms.MenuItem CompareMenu;
        private System.Windows.Forms.MenuItem mibookmarks;
        private System.Windows.Forms.MenuItem Addbookmarkmenu;
        private System.Windows.Forms.MenuItem removebookmarkmenu;
        private System.Windows.Forms.MenuItem gotobookmark;
        private System.Windows.Forms.MenuItem navigatethroughbookmarks;
        private System.Windows.Forms.MenuItem seperator13;
        private System.Windows.Forms.MenuItem mihiddenchars;
        private System.Windows.Forms.MenuItem wordwrapmenu;
        private System.Windows.Forms.MenuItem seperator24;
        private System.Windows.Forms.MenuItem commentmenu;
        private System.Windows.Forms.MenuItem commentline;
        private System.Windows.Forms.MenuItem uncommentline;
        private System.Windows.Forms.MenuItem doindent;
        private System.Windows.Forms.MenuItem replacemode;
        private System.Windows.Forms.MenuItem swapcase;
        private System.Windows.Forms.ToolStripStatusLabel infolabel;
        private System.Windows.Forms.ToolStripStatusLabel gapfiller1;
        private System.Windows.Forms.ToolStripDropDownButton zoom;
        private System.Windows.Forms.ToolStripMenuItem mizoom500;
        private System.Windows.Forms.ToolStripMenuItem mizoom400;
        private System.Windows.Forms.ToolStripMenuItem mizoom300;
        private System.Windows.Forms.ToolStripMenuItem mizoom200;
        private System.Windows.Forms.ToolStripMenuItem mizoom150;
        private System.Windows.Forms.ToolStripMenuItem mizoom100;
        private System.Windows.Forms.ToolStripMenuItem mizoom50;
        private System.Windows.Forms.MenuItem milanguage;
        private System.Windows.Forms.MenuItem macrosmenu;
        private System.Windows.Forms.MenuItem mirecordmacro;
        private System.Windows.Forms.MenuItem seperator31;
        private System.Windows.Forms.MenuItem miplaybackmacro;
        private System.Windows.Forms.MenuItem miexecmacromultiple;
        private System.Windows.Forms.MenuItem seperator32;
        private System.Windows.Forms.MenuItem misaverecordedmacro;
        private System.Windows.Forms.MenuItem miclearmacrodata;
        private System.Windows.Forms.MenuItem miexecfile;
        private System.Windows.Forms.MenuItem mirunscripts;
        private System.Windows.Forms.MenuItem pluginsmenuitem;
        private System.Windows.Forms.MenuItem recentfilesmenu;
        private System.Windows.Forms.MenuItem reopenclosedtab;
        private System.Windows.Forms.MenuItem seperator1;
        private System.Windows.Forms.MenuItem colorschememenu;
        private System.Windows.Forms.MenuItem migotol;
        private System.Windows.Forms.MenuItem mijoinlines;
        private System.Windows.Forms.MenuItem misortalphabet;
        private System.Windows.Forms.MenuItem mireverselines;
        private System.Windows.Forms.MenuItem miinschars;
        private System.Windows.Forms.MenuItem commandermenu;
        private System.Windows.Forms.MenuItem miblankops;
        private System.Windows.Forms.MenuItem mitts;
        private System.Windows.Forms.MenuItem mitls;
        private System.Windows.Forms.MenuItem mieoltospace;
        private System.Windows.Forms.MenuItem mittsandttl;
        private System.Windows.Forms.MenuItem mispacetoeol;
        private System.Windows.Forms.MenuItem seperator83;
        private System.Windows.Forms.MenuItem miremoveeol;
        private System.Windows.Forms.MenuItem miswitchfile;
        private System.Windows.Forms.MenuItem mishowunsaved;
        private System.Windows.Forms.MenuItem mifindnext;
        private System.Windows.Forms.MenuItem mitpunctuation;
        private System.Windows.Forms.MenuItem miconversions;
        private System.Windows.Forms.MenuItem mitocrlf;
        private System.Windows.Forms.MenuItem mitocr;
        private System.Windows.Forms.MenuItem mitolf;
        private System.Windows.Forms.MenuItem miopenencoding;
        private System.Windows.Forms.MenuItem misaveencoding;
        private System.Windows.Forms.MenuItem miprojectmanager;
        private System.Windows.Forms.MenuItem seperator26;
        private System.Windows.Forms.MenuItem mimacros;
        private System.Windows.Forms.MenuItem seperator12;
        private System.Windows.Forms.MenuItem mispacestotab;
        private System.Windows.Forms.MenuItem mitabtospaces;
        private System.Windows.Forms.MenuItem mikeymapeditor;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dock;
        private System.Windows.Forms.ToolStripStatusLabel mistats;
        private SS.Ynote.Classic.Features.Search.IncrementalSearcher incrementalSearcher1;
        private System.Windows.Forms.MenuItem miscripts;
        private System.Windows.Forms.ToolStripStatusLabel gapfiller3;
        private System.Windows.Forms.MenuItem mirun;
        private System.Windows.Forms.MenuItem mieditor;
        private System.Windows.Forms.ToolStripDropDownButton langmenu;
        private System.Windows.Forms.MenuItem seperator18;
        private System.Windows.Forms.MenuItem seperator16;
        private System.Windows.Forms.MenuItem migotoleftbracket;
        private System.Windows.Forms.MenuItem migotorightbracket;
        private System.Windows.Forms.MenuItem seperator15;
        private System.Windows.Forms.MenuItem mileftbracket2;
        private System.Windows.Forms.MenuItem migorightbracket2;
        private System.Windows.Forms.MenuItem seperator14;
        private System.Windows.Forms.MenuItem migoleftbracket3;
        private System.Windows.Forms.MenuItem migorightbracket3;
        private System.Windows.Forms.MenuItem mimarksel;
        private System.Windows.Forms.MenuItem miclearmarked;
        private System.Windows.Forms.MenuItem miredmark;
        private System.Windows.Forms.MenuItem mibluemark;
        private System.Windows.Forms.MenuItem migraymark;
        private System.Windows.Forms.MenuItem migreenmark;
        private System.Windows.Forms.MenuItem mimarkyellow;
        private System.Windows.Forms.MenuItem seperator17;
        private MenuItem misortlength;
        private MenuItem seperator28;
        private MenuItem misnippets;
        private MenuItem seperator29;
        private MenuItem seperator11;
        private MenuItem miselectiontohex;
        private MenuItem miselectiontoascii;
        private MenuItem miupdates;
        private MenuItem miwebsearch;
        private MenuItem migoogle;
        private MenuItem miwiki;
        private ToolStripStatusLabel gapfiller2;
        private MenuItem seperator7;
        private MenuItem micomparewith;
        private MenuItem seperator3;
        private MenuItem micopyas;
        private MenuItem micopyhtml;
        private MenuItem micopyrtf;
        private MenuItem menuItem1;
        private MenuItem minewsnippet;
        private MenuItem minewscript;
    }
}

