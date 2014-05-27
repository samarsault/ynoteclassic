using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.Core.Session
{
    /// <summary>
    ///     A Ynote Session
    /// </summary>
    public class YnoteSession
    {
        // TODO: Implement Ynote Session
        /// <summary>
        ///     List of Open Windows
        /// </summary>
        public DockContent[] OpenWindows { get; set; }

        /// <summary>
        ///     Find Auto complete history
        /// </summary>
        public string[] FindAutoCompleteList { get; set; }

        /// <summary>
        ///     replace autocomplete history
        /// </summary>
        public string[] ReplaceAutoCompleteList { get; set; }

        /// <summary>
        ///     Find In Files AutoComplete History
        /// </summary>
        public string[] FindInFilesAutoCompleteList { get; set; }

        /// <summary>
        ///     Find Regex Checked
        /// </summary>
        public bool FindRegexChecked { get; set; }

        /// <summary>
        ///     Find Case Checked
        /// </summary>
        public bool FindCaseChecked { get; set; }

        /// <summary>
        ///     Find In Files Dialog Regex Checked
        /// </summary>
        public bool FindInFilesRegexChecked { get; set; }

        /// <summary>
        ///     Find In Files
        /// </summary>
        public bool FindInFilesCaseChecked { get; set; }

        /// <summary>
        ///     Recent Files
        /// </summary>
        public string[] RecentFiles { get; set; }

        /// <summary>
        ///     Show Menu Bar
        /// </summary>
        public bool ShowMenu { get; set; }

        /// <summary>
        ///     Show Tool Bar
        /// </summary>
        public bool ShowToolBar { get; set; }

        /// <summary>
        ///     Show Status Bar
        /// </summary>
        public bool ShowStatusBar { get; set; }

        /// <summary>
        ///     Show Document Map
        /// </summary>
        public bool ShowDocumentMap { get; set; }

        /// <summary>
        ///     Show Ruler
        /// </summary>
        public bool ShowRuler { get; set; }

        /// <summary>
        ///     Tab Size
        /// </summary>
        public int TabSize { get; set; }

        /// <summary>
        ///     Whether to Use Tabs
        /// </summary>
        public bool UseTabs { get; set; }

        /// <summary>
        ///     Gets the Default Encoding for the Session
        /// </summary>
        public int DefaultEncoding { get; set; }

        /// <summary>
        ///     Whether to auto complete brackets
        /// </summary>
        public bool AutocompleteBrackets { get; set; }

        /// <summary>
        ///     Whether to enable virtual space
        /// </summary>
        public bool VirtualSpace { get; set; }

        /// <summary>
        ///     Font Family
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        ///     Font Size
        /// </summary>
        public float FontSize { get; set; }

        /// <summary>
        ///     Highlight Same Words
        /// </summary>
        public bool HighlightSameWords { get; set; }

        /// <summary>
        ///     Word Wrap
        /// </summary>
        public bool WordWrap { get; set; }
    }
}