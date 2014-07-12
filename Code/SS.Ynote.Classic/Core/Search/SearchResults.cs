using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SS.Ynote.Classic.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.Core.Search
{
    public partial class SearchResults : DockContent
    {
        private readonly IYnote _ynote;

        public SearchResults(IYnote ynote)
        {
            InitializeComponent();
            _ynote = ynote;
        }

        public void FindAll(string dir, bool regex, bool matchcase, string text, string dirpattern, bool subdirs)
        {
            try
            {
                lvresults.Items.Clear();
                if (dir == string.Empty)
                {
                    var files =
                        (from Editor doc in _ynote.Panel.Documents where doc.IsSaved select doc.Name).ToArray();
                    if (regex)
                    {
                        var options = matchcase ? RegexOptions.IgnoreCase : RegexOptions.None;
                        FindInDocumentsWithRegex(files, text, options);
                    }
                    else
                        FindInDocuments(files, text, matchcase);
                }
                else
                {
                    var option = GetOption(subdirs);
                    if (regex)
                        FindReferencesWithRegex(dir, text, dirpattern,
                            option);
                    else
                        FindReferences(dir, text, dirpattern,
                            option);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private SearchOption GetOption(bool subdirectory)
        {
            if (subdirectory)
                return SearchOption.AllDirectories;
            return SearchOption.TopDirectoryOnly;
        }

        private static bool FileExists(IYnote ynote, string file)
        {
            try
            {
                return ynote.Panel.Documents.Cast<Editor>().Any(doc => doc.Name == file);
            }
            catch
            {
            }
            return false;
        }

        private void FindInDocuments(IEnumerable<string> files, string searchString, bool ignoreCase)
        {
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    var lineNumber = 1;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                        if (line.Contains(searchString, comparison))
                        {
                            lvresults.Items.Add(
                                new ListViewItem(new[]
                                {file, lineNumber.ToString(), FileExists(_ynote, file).ToString()}));
                        }

                        lineNumber++;
                    }
                }
            }
        }

        private void FindInDocumentsWithRegex(IEnumerable<string> files, string searchString, RegexOptions options)
        {
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    var lineNumber = 1;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, searchString, options))
                        {
                            lvresults.Items.Add(
                                new ListViewItem(new[]
                                {file, lineNumber.ToString(), FileExists(_ynote, file).ToString()}));
                        }

                        lineNumber++;
                    }
                }
            }
        }

        /// <summary>
        ///     Finds References in Directory
        /// </summary>
        /// <param name="searchPath"></param>
        /// <param name="searchString"></param>
        /// <param name="searchpattern"></param>
        /// <param name="option"></param>
        private void FindReferences(string searchPath, string searchString,
            string searchpattern, SearchOption option)
        {
            if (searchPath != string.Empty && Directory.Exists(searchPath))
            {
                //searchpattern = "*.*";
                var files = Directory.GetFiles(searchPath, searchpattern, option);

                // Loop through all the files in the specified directory & in all sub-directories
                foreach (var file in files)
                {
                    using (var reader = new StreamReader(file))
                    {
                        var lineNumber = 1;
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                            {
                                lvresults.Items.Add(
                                    new ListViewItem(new[]
                                    {file, lineNumber.ToString(), FileExists(_ynote, file).ToString()}));
                            }

                            lineNumber++;
                        }
                    }
                }
            }
        }

        private void FindReferencesWithRegex(string searchPath, string regex,
            string searchpattern, SearchOption option)
        {
            if (searchPath != string.Empty && Directory.Exists(searchPath))
            {
                //searchpattern = "*.*";
                var files = Directory.GetFiles(searchPath, searchpattern, option);

                // Loop through all the files in the specified directory & in all sub-directories
                foreach (var file in files)
                {
                    using (var reader = new StreamReader(file))
                    {
                        var lineNumber = 1;
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (Regex.IsMatch(line, regex))
                            {
                                lvresults.Items.Add(
                                    new ListViewItem(new[]
                                    {file, lineNumber.ToString(), FileExists(_ynote, file).ToString()}));
                            }

                            lineNumber++;
                        }
                    }
                }
            }
        }

        private void lvresults_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvresults.SelectedItems[0].SubItems[2].Text == "False")
                {
                    _ynote.OpenFile(lvresults.SelectedItems[0].SubItems[0].Text);
                    var editor = (Editor) (_ynote.Panel.ActiveDocument);
                    editor.Tb.Navigate(Convert.ToInt32(lvresults.SelectedItems[0].SubItems[1].Text) - 1);
                }
                else
                {
                    foreach (
                        var document in
                            _ynote.Panel.Documents.Cast<Editor>()
                                .Where(document => document.Name == lvresults.SelectedItems[0].SubItems[0].Text))
                    {
                        document.Show();
                        document.Tb.Navigate(lvresults.SelectedItems[0].SubItems[1].Text.ToInt() - 1);
                    }
                }
            }
            catch
            {
                _ynote.OpenFile(lvresults.SelectedItems[0].SubItems[0].Text);
                ((Editor) (_ynote.Panel.ActiveDocument)).Tb.Navigate(
                    lvresults.SelectedItems[0].SubItems[1].Text.ToInt() - 1);
            }
        }

        private void ReplaceInFiles(IEnumerable<string> filePaths, string searchText, string replaceText,
            bool ignoreCase)
        {
            try
            {
                foreach (var file in filePaths)
                {
                    var lines = File.ReadAllLines(file);
                    var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                    for (var i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains(searchText, comparison))
                        {
                            lines[i] = lines[i].Replace(searchText, replaceText);
                            lvresults.Items.Add(
                                new ListViewItem(new[] {file, i.ToString(), FileExists(_ynote, file).ToString()}));
                        }
                    }
                    File.WriteAllLines(file, lines);
                }
            }
            catch
            {
            }
        }

        private void ReplaceInFilesWithRegex(IEnumerable<string> filePaths, string searchText, string replaceText,
            RegexOptions options)
        {
            try
            {
                foreach (var file in filePaths)
                {
                    var lines = File.ReadAllLines(file);
                    for (var i = 0; i < lines.Length; i++)
                    {
                        if (Regex.IsMatch(lines[i], searchText, options))
                        {
                            lines[i] = Regex.Replace(lines[i], searchText, replaceText);
                            lvresults.Items.Add(
                                new ListViewItem(new[] {file, i.ToString(), FileExists(_ynote, file).ToString()}));
                        }
                    }
                    File.WriteAllLines(file, lines);
                }
            }
            catch
            {
            }
        }

        public void ReplaceAll(string dir, string filter, bool regex, bool matchcase, string find, string replace,
            bool subdirs)
        {
            try
            {
                lvresults.Items.Clear();
                if (filter == "*.*")
                {
                    MessageBox.Show(
                        "Error : Invalid Filter\n The Filter is not accepted as it can cause performance and system problems\r\nPlease specify a valid file filter",
                        "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                var files = dir == string.Empty
                    ? (from Editor doc in _ynote.Panel.Documents where doc.IsSaved select doc.Name).ToArray()
                    : Directory.GetFiles(dir, filter, GetOption(subdirs));
                BeginInvoke((MethodInvoker) (() =>
                {
                    if (regex)
                    {
                        var options = matchcase ? RegexOptions.IgnoreCase : RegexOptions.None;
                        ReplaceInFilesWithRegex(files, find, find, options);
                    }
                    else
                        ReplaceInFiles(files, find, find, matchcase);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
    }
}