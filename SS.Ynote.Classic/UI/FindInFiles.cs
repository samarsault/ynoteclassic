using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class FindInFiles : DockContent
    {
        public FindInFiles()
        {
            InitializeComponent();
            cmbsearchoptions.DataSource = Enum.GetValues(typeof (SearchOption));
            comboBox1.DataSource = Enum.GetValues(typeof (SearchOption));
            cmbsearchoptions.SelectedIndex = 1;
            comboBox1.SelectedIndex = 1;
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
            textBox2.Clear();
            var allsearch = new List<string>();
            FindReferences(allsearch, txtdir.Text, txtstring.Text, textBox5.Text,
                (SearchOption) cmbsearchoptions.SelectedItem);

            if (allsearch.Count != 0)
                foreach (string item in allsearch)
                    textBox2.Text += item + "\r\n";
            else
                textBox2.Text = "Nothing Found";

            tabControl1.SelectedIndex = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            var allsearch = new List<string>();
            ReplaceInFiles(allsearch, textBox1.Text, textBox3.Text, textBox4.Text, "*.*",
                (SearchOption) cmbsearchoptions.SelectedItem);
            if (allsearch.Count != 0)
                foreach (string item in allsearch)
                    textBox2.Text += item + "\r\n";
            else
                textBox2.Text = "Nothing Found";

            tabControl1.SelectedIndex = 1;
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

        /// <summary>
        ///     Finds References in Directory
        /// </summary>
        /// <param name="output"></param>
        /// <param name="searchPath"></param>
        /// <param name="searchString"></param>
        /// <param name="searchpattern"></param>
        /// <param name="option"></param>
        private static void FindReferences(ICollection<string> output, string searchPath, string searchString,
            string searchpattern, SearchOption option)
        {
            if (Directory.Exists(searchPath))
            {
                //searchpattern = "*.*";
                string[] files = Directory.GetFiles(searchPath, searchpattern, option);

                string line = string.Empty;

                // Loop through all the files in the specified directory & in all sub-directories
                foreach (string file in files)
                {
                    using (var reader = new StreamReader(file))
                    {
                        int lineNumber = 1;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                            {
                                output.Add(string.Format("{0}: Line : {1}", file, lineNumber));
                            }

                            lineNumber++;
                        }
                    }
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