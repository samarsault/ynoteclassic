using System.IO;
using System.Windows.Forms;
using SS.Ynote.Classic.Core.Project;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.UI;

namespace SS.Ynote.Classic
{
    internal static class Globals
    {
        public static bool DistractionFree { get; set; }
        /// <summary>
        /// Gets the Active Project
        /// </summary>
        public static YnoteProject ActiveProject { get; set; }
        /// <summary>
        ///     The running instance of Ynote
        /// </summary>
        public static IYnote Ynote { get; set; }

        /// <summary>
        ///     Settings
        /// </summary>
        public static GlobalProperties Settings { get; set; }

        /// <summary>
        ///     Exands an abbreviation
        /// </summary>
        /// <param name="abbr"></param>
        /// <param name="ynote"></param>
        /// <returns></returns>
        public static string ExpandAbbr(string abbr, IYnote ynote)
        {
            string expanded = abbr;
            var edit = ynote.Panel.ActiveDocument as Editor;
            if (abbr.Contains("$source"))
            {
                expanded = expanded.Replace("$source", edit.Name);
                expanded = expanded.Replace("$source_dir", Path.GetDirectoryName(edit.Name));
                expanded = expanded.Replace("$source_extension", Path.GetExtension(edit.Name));
                expanded = expanded.Replace("$source_name", edit.Text).Replace("$source", edit.Name);
            }
            if (expanded.Contains("$project"))
            {
                ProjectPanel panel = null;
                foreach (var form in Application.OpenForms)
                    if (form is ProjectPanel)
                        panel = form as ProjectPanel;
                if (panel != null)
                    expanded = expanded.Replace("$project_dir", panel.Project.Path)
                        .Replace("$project_name", panel.Project.Name);
            }
            return expanded;
        }
    }
}