#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

#endregion

namespace SS.Ynote.Classic.UI
{
    public partial class FindInFiles : DockContent
    {
        private readonly IYnote _ynote;

        public FindInFiles(IYnote ynote)
        {
            InitializeComponent();
            cmbsearchoptions.DataSource = Enum.GetValues(typeof (SearchOption));
            comboBox1.DataSource = Enum.GetValues(typeof (SearchOption));
            cmbsearchoptions.SelectedIndex = 1;
            comboBox1.SelectedIndex = 1;
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
                IList<string> Files =
                    (from Editor doc in _ynote.Panel.Documents where doc.Name != "Editor" select doc.Name).ToList();
                if(cbRegex.Checked)
                    FindInDocumentsWithRegex(Files, txtstring.Text);
                else
                    FindInDocuments(Files, txtstring.Text);
            }
            else
            {
                if(cbRegex.Checked)
                    FindReferencesWithRegex(txtdir.Text, txtstring.Text,textBox5.Text,
                        (SearchOption)cmbsearchoptions.SelectedItem);
                else
                    FindReferences(txtdir.Text, txtstring.Text, textBox5.Text,
                        (SearchOption) cmbsearchoptions.SelectedItem);
            }
            tabControl1.SelectedIndex = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "$docs")
            {
                IList<string> files =
                    (from Editor doc in _ynote.Panel.Documents where doc.Name != "Editor" select doc.Name).ToList();
                if(cbRegex.Checked)
                    ReplaceInDocumentsWithRegex(files, textBox3.Text, textBox4.Text);
                else
                    ReplaceInDocuments(files, textBox3.Text, textBox4.Text);
            }
            else
            {
                if(cbRegex.Checked)
                    ReplaceInDocumentsWithRegex(Directory.GetFiles(textBox1.Text, "*.*"), textBox3.Text, textBox4.Text);
                else
                    ReplaceInDocuments(Directory.GetFiles(textBox1.Text, "*.*"), textBox3.Text, textBox4.Text);
                // var allsearch = new List<string>();
                // ReplaceInFiles(allsearch, textBox1.Text, textBox3.Text, textBox4.Text, "*.*",
                //     (SearchOption) cmbsearchoptions.SelectedItem);
            }
            tabControl1.SelectedIndex = 2;
        }

        private static void ReplaceInFiles(ICollection<string> output, string path, string searchstring,
            string replacestring, string searchpattern, SearchOption option)
        {
            if (Directory.Exists(path))
            {
                //searchpattern = "*.*";
                string[] files = Directory.GetFiles(path, searchpattern, option);

                string line = string.Empty;

                // Loop through all the files in the specified directory & in all sub-directories
                foreach (string file in files)
                {
                    using (var reader = new StreamReader(file))
                    {
                        int lineNumber = 1;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(searchstring, StringComparison.OrdinalIgnoreCase))
                            {
                                line = line.Replace(searchstring, replacestring);
                                output.Add(string.Format("{0}: Line : {1} : Replaced {2} with {3}", file, lineNumber,
                                    searchstring, replacestring));
                            }

                            lineNumber++;
                        }
                    }
                }
            }
        }

        private void ReplaceInDocuments(IEnumerable<string> files, string searchstring, string replacestring)
        {
            //searchpattern = "*.*";
            string line = string.Empty;

            // Loop through all the files in the specified directory & in all sub-directories
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    int lineNumber = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(searchstring, StringComparison.OrdinalIgnoreCase))
                        {
                            line = line.Replace(searchstring, replacestring);
                            lvresults.Items.Add(
                                new ListViewItem(new[]
                                {file, lineNumber.ToString(), FileExists(_ynote, file).ToString()}));
                        }

                        lineNumber++;
                    }
                }
            }
        }
        private void ReplaceInDocumentsWithRegex(IEnumerable<string> files, string searchstring, string replacestring)
        {
            //searchpattern = "*.*";
            string line = string.Empty;

            // Loop through all the files in the specified directory & in all sub-directories
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    int lineNumber = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, searchstring))
                        {
                            line = Regex.Replace(line, searchstring, replacestring);
                            lvresults.Items.Add(
                                new ListViewItem(new[] { file, lineNumber.ToString(), FileExists(_ynote, file).ToString() }));
                        }

                        lineNumber++;
                    }
                }
            }
        }

        private static bool FileExists(IYnote ynote, string file)
        {
            try
            {
                return ynote.Panel.Documents.Cast<Editor>().Any(doc => doc.Name == file);
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e);
#endif
            }
            return false;
        }

        private void FindInDocuments(IEnumerable<string> files, string searchString)
        {
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
        private void FindInDocumentsWithRegex(IEnumerable<string> files, string searchString)
        {
            foreach (string file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    int lineNumber = 1;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, searchString))
                        {
                            lvresults.Items.Add(
                                new ListViewItem(new[] { file, lineNumber.ToString(), FileExists(_ynote, file).ToString() }));
                        }

                        lineNumber++;
                    }
                }
            }
        }

        /// <summary>
        ///     Finds References in Directory
        /// </summary>
        /// <param name="output"></param>
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
                                    new ListViewItem(new[] { file, lineNumber.ToString(), FileExists(_ynote, file).ToString() }));
                            }

                            lineNumber++;
                        }
                    }
                }
            }
        }

        private void lvresults_DoubleClick(object sender, EventArgs e)
        {
            if (lvresults.SelectedItems[0].SubItems[2].Text == "False")
            {
                _ynote.OpenFile(lvresults.SelectedItems[0].SubItems[0].Text);
                var editor = (Editor) (_ynote.Panel.ActiveDocument);
                editor.tb.Navigate(Convert.ToInt32(lvresults.SelectedItems[0].SubItems[1].Text) - 1);
            }
            else
            {
                foreach (Editor document in _ynote.Panel.Documents)
                    if (document.Name == lvresults.SelectedItems[0].SubItems[0].Text)
                    {
                        document.Show();
                        document.tb.Navigate(Convert.ToInt32(lvresults.SelectedItems[0].SubItems[1].Text) - 1);
                    }
            }
        }

        /*   static void AddFileNamesToList(string sourceDir, ICollectionList<string> allFiles)
        {

            var fileEntries = Directory.GetFiles(sourceDir);
            allFiles.AddRange(fileEntries);

            //Recursion    
            string[] subdirectoryEntries = Directory.GetDirectories(sourceDir);
            foreach (string item in subdirectoryEntries)
            {
                // Avoid "reparse points"
                if ((File.GetAttributes(item) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                {
                    AddFileNamesToList(item, allFiles);
                }
            }

        }*/
    }
}