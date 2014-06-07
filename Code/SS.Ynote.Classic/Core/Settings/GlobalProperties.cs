using FastColoredTextBoxNS;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.Core.Settings
{
    public class GlobalProperties
    {
        /// <summary>
        ///     Theme File
        /// </summary>
        public  string ThemeFile { get; set; }

        /// <summary>
        ///     Show Hidden Chars
        /// </summary>
        public  bool HiddenChars { get; set; }

        /// <summary>
        ///     Document Style
        /// </summary>
        public  DocumentStyle DocumentStyle { get; set; }

        /// <summary>
        ///     Get the Tab Location
        /// </summary>
        public  DocumentTabStripLocation TabLocation { get; set; }

        /// <summary>
        ///     Show Folding Lines
        /// </summary>
        public  bool ShowFoldingLines { get; set; }

        /// <summary>
        ///     Show Caret
        /// </summary>
        public  bool ShowCaret { get; set; }

        /// <summary>
        ///     Highlight Folding
        /// </summary>
        public  bool HighlightFolding { get; set; }

        /// <summary>
        ///     Show Document Map
        /// </summary>
        public  bool ShowDocumentMap { get; set; }

        /// <summary>
        ///     Show Ruler
        /// </summary>
        public  bool ShowRuler { get; set; }

        /// <summary>
        ///     Whether to Show Line Numbers
        /// </summary>
        public  bool ShowLineNumbers { get; set; }

        /// <summary>
        ///     Enable Virtual Space
        /// </summary>
        public  bool EnableVirtualSpace { get; set; }

        /// <summary>
        ///     Folding Strategy
        /// </summary>
        public  FindEndOfFoldingBlockStrategy FoldingStrategy { get; set; }

        /// <summary>
        ///     Bracket Highlighting Strategy
        /// </summary>
        public  BracketsHighlightStrategy BracketsStrategy { get; set; }

        /// <summary>
        ///     Line Interval
        /// </summary>
        /// y>
        public  int PaddingWidth { get; set; }

        /// <summary>
        ///     Show Status Bar
        /// </summary>
        public  int LineInterval { get; set; }

        /// <summary>
        ///     Show Status Bar
        /// </summary>
        public  bool ShowStatusBar { get; set; }

        /// <summary>
        ///     Show MenuBar
        /// </summary>
        public  bool ShowMenuBar { get; set; }

        /// <summary>
        ///     Font-Family
        /// </summary>
        public  string FontFamily { get;  set; }

        /// <summary>
        ///     No. of RecentFiles
        /// </summary>
        public  int RecentFileNumber { get; set; }

        /// <summary>
        ///     Font Size
        /// </summary>
        public  float FontSize { get;  set; }

        /// <summary>
        ///     Zoom
        /// </summary>
        public  int TabSize { get; set; }

        /// <summary>
        ///     Autocomplete Brackets
        /// </summary>
        public  int Zoom { get; set; }

        /// <summary>
        ///     Loads Globals.Settings
        /// </summary>
        public  bool AutoCompleteBrackets { get; set; }

        /// <summary>
        ///     Checks if WordWrap is on
        /// </summary>
        public  bool WordWrap { get; set; }

        /// <summary>
        ///     Gets The Default Encoding for Saving Document
        /// </summary>
        public  int DefaultEncoding { get; set; }

        /// <summary>
        ///     Show the Tool Bar
        /// </summary>
        public  bool ShowToolBar { get; set; }

        /// <summary>
        ///     Whether to Minimize app to System Tray
        /// </summary>
        public  bool MinimizeToTray { get; set; }

        /// <summary>
        ///     Whether to Highlight Same Words
        /// </summary>
        public  bool HighlightSameWords { get; set; }

        /// <summary>
        ///     Whether to show Changed Line
        /// </summary>
        public  bool ShowChangedLine { get; set; }

        /// <summary>
        ///     Whether IME mode is on
        /// </summary>
        public  bool ImeMode { get; set; }

        /// <summary>
        ///     Block Cursor is On
        /// </summary>
        public  bool BlockCaret { get; set; }

        /// <summary>
        ///     Whether to use Tabs vs Spaces
        /// </summary>
        public  bool UseTabs { get; set; }

        /// <summary>
        ///     Whether to show Scroll Bars
        /// </summary>
        public  bool ScrollBars { get; set; }
        /// <summary>
        ///     Whether to Load Layout on Startup
        /// </summary>
        public  bool LoadLayout { get; set; }
    }
}