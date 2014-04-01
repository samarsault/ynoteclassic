using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class FindInFiles : DockContent
    {
        private readonly IYnote _ynote;

        public FindInFiles(IYnote ynote)
        {
            InitializeComponent();
            cmbsearchoptions.DataSource = cbSearchIn.DataSource = Enum.GetValues(typeof (SearchOption));
            cmbsearchoptions.SelectedIndex = 1;
            cbSearchIn.SelectedIndex = 1;
            _ynote = ynote;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var f = new FolderBrowserDialog())
            {
                f.ShowDialog();
                if (!string.IsNullOrEmpty(f.SelectedPath))
                    txtdir.Text = f.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lvresults.Items.Clear();
            if (txtdir.Text == "$docs")
            {
                var files =
                    (from Editor doc in _ynote.Panel.Documents where doc.IsSaved select doc.Name).ToArray();
                if (cbRegex.Checked)
                {
                    var options = cbCase.Checked ? RegexOptions.IgnoreCase : RegexOptions.None;
                    FindInDocumentsWithRegex(files, txtstring.Text, options);
                }
                else
                    FindInDocuments(files, txtstring.Text, cbCase.Checked);
            }
            else
            {
                if (cbRegex.Checked)
                    FindReferencesWithRegex(txtdir.Text, txtstring.Text, textBox5.Text,
                        (SearchOption) cmbsearchoptions.SelectedItem);
                else
                    FindReferences(txtdir.Text, txtstring.Text, textBox5.Text,
                        (SearchOption) cmbsearchoptions.SelectedItem);
            }
            tabControl1.SelectedIndex = 2;
        }

        private static bool FileExists(IYnote ynote, string file)
        {
            try
            {
                return ynote.Panel.Documents.Cast<Editor>().Any(doc => doc.Name == file);
            }
            catch (Exception e)
            {
                ;
            }
            return false;
        }

        private void FindInDocuments(IEnumerable<string> files, string searchString, bool ignoreCase)
        {
            foreach (string file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    int lineNumber = 1;
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
            foreach (string file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    int lineNumber = 1;
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
            if (searchPath != "$docs" && Directory.Exists(searchPath))
            {
                //searchpattern = "*.*";
                string[] files = Directory.GetFiles(searchPath, searchpattern, option);

                // Loop through all the files in the specified directory & in all sub-directories
                foreach (string file in files)
                {
                    using (var reader = new StreamReader(file))
                    {
                        int lineNumber = 1;
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
            if (searchPath != "$docs" && Directory.Exists(searchPath))
            {
                //searchpattern = "*.*";
                string[] files = Directory.GetFiles(searchPath, searchpattern, option);

                // Loop through all the files in the specified directory & in all sub-directories
                foreach (string file in files)
                {
                    using (var reader = new StreamReader(file))
                    {
                        int lineNumber = 1;
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
                    editor.tb.Navigate(Convert.ToInt32(lvresults.SelectedItems[0].SubItems[1].Text) - 1);
                }
                else
                {
                    foreach (Editor document in _ynote.Panel.Documents.Cast<Editor>().Where(document => document.Name == lvresults.SelectedItems[0].SubItems[0].Text))
                    {
                        document.Show();
                        document.tb.Navigate(Convert.ToInt32(lvresults.SelectedItems[0].SubItems[1].Text) - 1);
                    }
                }
            }
            catch
            {
                _ynote.OpenFile(lvresults.SelectedItems[0].SubItems[0].Text);
            }
        }

        private void ReplaceInFiles(IEnumerable<string> filePaths, string searchText, string replaceText,
            bool ignoreCase)
        {
            try
            {
                foreach (string file in filePaths)
                {
                    var lines = File.ReadAllLines(file);
                    var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains(searchText, comparison))
                        {
                            lines[i] = lines[i].Replace(searchText, replaceText);
                            lvresults.Items.Add(
                              new ListViewItem(new[] { file, i.ToString(), FileExists(_ynote, file).ToString() }));
                        }
                    }
                    File.WriteAllLines(file, lines);
                }
            }
            catch
            {
                ;
            }
        }

        private void ReplaceInFilesWithRegex(IEnumerable<string> filePaths, string searchText, string replaceText,
            RegexOptions options)
        {
            try
            {
                foreach (string file in filePaths)
                {
                    var lines = File.ReadAllLines(file);
                    for (int i = 0; i < lines.Length; i++)
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var browser = new FolderBrowserDialog())
            {
                var ok = browser.ShowDialog() == DialogResult.OK;
                if (ok)
                    tbReplaceDir.Text = browser.SelectedPath;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (tbReplaceFilter.Text == "*.*")
            {
                MessageBox.Show(
                    "Error : Invalid Filter\r\n The Filter is not accepted as it can cause performance and system problems\r\nPlease specify a valid file filter",
                    "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var files = tbReplaceDir.Text == "$docs"
? (from Editor doc in _ynote.Panel.Documents where doc.IsSaved select doc.Name).ToArray()
: Directory.GetFiles(tbReplaceDir.Text, tbReplaceFilter.Text);
            BeginInvoke((MethodInvoker)(() =>
            {
                if (cbRegex.Checked)
                {
                    var options = cbReplaceICase.Checked ? RegexOptions.IgnoreCase : RegexOptions.None;
                    ReplaceInFilesWithRegex(files, tbReplaceFind.Text, tbReplaceWith.Text, options);
                }
                else
                    ReplaceInFiles(files, tbReplaceFind.Text, tbReplaceWith.Text, cbReplaceICase.Checked);
            }));
            tabControl1.SelectedIndex = 2;
        }
    }
}