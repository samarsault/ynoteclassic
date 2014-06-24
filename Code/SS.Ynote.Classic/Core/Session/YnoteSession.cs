namespace SS.Ynote.Classic.Core.Session
{
    /// <summary>
    ///     A Ynote Session
    /// </summary>
    public class YnoteSession
    {
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
        ///     Recent projects
        /// </summary>
        public string[] RecentProjects { get; set; }
    }
}