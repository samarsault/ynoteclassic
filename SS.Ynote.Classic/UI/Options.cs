using FastColoredTextBoxNS;
using Nini.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SS.Ynote.Classic.Features.Syntax;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            treeView1.ExpandAll();
            InitSettings();
            BuildLangList();
        }

        /// <summary>
        ///     Initialize Settings
        /// </summary>
        private void InitSettings()
        {
            cmbwordwrapmode.DataSource = Enum.GetValues(typeof(WordWrapMode));
            cbdockstyle.Text = SettingsBase.DocumentStyle.ToString();
            comboBox2.Text = SettingsBase.BracketsStrategy.ToString();
            cmbwordwrapmode.Text = SettingsBase.WordWrapMode.ToString();
            tablocation.Text = SettingsBase.TabLocation.ToString();
            checkBox1.Checked = SettingsBase.ShowDocumentMap;
            ShowLineNumber.Checked = SettingsBase.ShowLineNumbers;
            showcaret.Checked = SettingsBase.ShowCaret;
            showfoldinglines.Checked = SettingsBase.ShowFoldingLines;
            virtualspace.Checked = SettingsBase.EnableVirtualSpace;
            highlightfoliding.Checked = SettingsBase.HighlightFolding;
            tbpaddingwidth.Text = SettingsBase.PaddingWidth.ToString();
            tblineinterval.Text = SettingsBase.LineInterval.ToString();
            cmbwordwrapmode.Text = SettingsBase.WordWrapMode.ToString();
            comboBox1.Text = SettingsBase.FoldingStrategy.ToString();
            tabsize.Value = SettingsBase.TabSize;
            cbruler.Checked = SettingsBase.ShowRuler;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "Tabs":
                    tabcontrol.SelectTab(tabsettingpage);
                    break;

                case "General":
                    tabcontrol.SelectTab(GeneralPage);
                    break;

                case "MISC":
                    tabcontrol.SelectTab(EditingPage);
                    break;

                case "File Extensions":
                    tabcontrol.SelectTab(FileExtensionsPage);
                    break;

                case "Manage":
                    tabcontrol.SelectTab(ClearPage);
                    break;

                case "General Settings":
                    tabcontrol.SelectTab(Plugins);
                    break;
            }
        }

        private void tablocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SettingsBase.TabLocation = tablocation.Text.ToEnum<DocumentTabStripLocation>();
            }
            catch
            {
                ;
            }
        }

        private void cbdockstyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SettingsBase.DocumentStyle = cbdockstyle.Text.ToEnum<DocumentStyle>();
            }
            catch
            {
                ;
            }
        }

        private void showfoldinglines_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.ShowFoldingLines = showfoldinglines.Checked;
        }

        private void showcaret_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.ShowCaret = showcaret.Checked;
        }

        private void ShowLineNumber_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.ShowLineNumbers = ShowLineNumber.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.ShowDocumentMap = checkBox1.Checked;
        }

        private void highlightfoliding_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.HighlightFolding = highlightfoliding.Checked;
        }

        private void virtualspace_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.EnableVirtualSpace = virtualspace.Checked;
        }

        private void cmbwordwrapmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SettingsBase.WordWrapMode = cmbwordwrapmode.Text.ToEnum<WordWrapMode>();
            }
            catch
            {
                ;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsBase.FoldingStrategy = comboBox1.Text.ToEnum<FindEndOfFoldingBlockStrategy>();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SettingsBase.BracketsStrategy = comboBox2.Text.ToEnum<BracketsHighlightStrategy>();
            }
            catch
            {
                ;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SettingsBase.PaddingWidth = tbpaddingwidth.IntValue;
            SettingsBase.LineInterval = tblineinterval.IntValue;
            SettingsBase.TabSize = Convert.ToInt32(tabsize.Value);
            SettingsBase.SaveConfiguration();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(Environment.SpecialFolder.ApplicationData + @"\Ynote Classic\");
            }
            catch
            {
                ;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            File.WriteAllText(string.Empty, SettingsBase.SettingsDir + "Settings.ini");
            SettingsBase.RestoreDefault(SettingsBase.SettingsDir + "Settings.ini");
        }

        private void cbruler_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.ShowRuler = cbruler.Checked;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(SettingsBase.SettingsDir + "Settings.ini");
        }

        private static IDictionary<string, string[]> BuildReverseDictionary()
        {
            var dic = new Dictionary<string, string[]>();
            IConfigSource source = new IniConfigSource(SettingsBase.SettingsDir + @"Extensions.ini");
              dic.Add("Text", new[]{".txt"});
              dic.Add("CSharp", source.Configs["Extensions"].Get("CSharp").Split('|'));
              dic.Add("VB", source.Configs["Extensions"].Get("VB").Split('|'));
              dic.Add("Javascript", source.Configs["Extensions"].Get("Javascript").Split('|'));
              dic.Add("Java", source.Configs["Extensions"].Get("Java").Split('|'));
              dic.Add("HTML", source.Configs["Extensions"].Get("HTML").Split('|'));
              dic.Add("CSS", source.Configs["Extensions"].Get("CSS").Split('|'));
              dic.Add("CPP", source.Configs["Extensions"].Get("CPP").Split('|'));
              dic.Add("PHP", source.Configs["Extensions"].Get("PHP").Split('|'));
              dic.Add("Lua", source.Configs["Extensions"].Get("Lua").Split('|'));
              dic.Add("Ruby", source.Configs["Extensions"].Get("Ruby").Split('|'));
              dic.Add("Python", source.Configs["Extensions"].Get("Python").Split('|'));
              dic.Add("Pascal", source.Configs["Extensions"].Get("Pascal").Split('|'));
              dic.Add("Lisp", source.Configs["Extensions"].Get("Lisp").Split('|'));
              dic.Add("Batch", source.Configs["Extensions"].Get("Batch").Split('|'));
              dic.Add("C", source.Configs["Extensions"].Get("C").Split('|'));
              dic.Add("Xml", source.Configs["Extensions"].Get("Xml").Split('|'));
              dic.Add("ASP", source.Configs["Extensions"].Get("ASP").Split('|'));
              dic.Add("Actionscript", source.Configs["Extensions"].Get("Actionscript").Split('|'));
              dic.Add("Assembly", source.Configs["Extensions"].Get("Assembly").Split('|'));
              dic.Add("Antlr", source.Configs["Extensions"].Get("Antlr").Split('|'));
              dic.Add("Diff", source.Configs["Extensions"].Get("Diff").Split('|'));
              dic.Add("D", source.Configs["Extensions"].Get("D").Split('|'));
              dic.Add("FSharp", source.Configs["Extensions"].Get("FSharp").Split('|'));
              dic.Add("JSON", source.Configs["Extensions"].Get("JSON").Split('|'));
              dic.Add("Makefile", source.Configs["Extensions"].Get("MakeFile").Split('|'));
              dic.Add("Objective_C", source.Configs["Extensions"].Get("ObjectiveC").Split('|'));
              dic.Add("Perl", source.Configs["Extensions"].Get("Perl").Split('|'));
              dic.Add("QBasic", source.Configs["Extensions"].Get("QBasic").Split('|'));
              dic.Add("SQL", source.Configs["Extensions"].Get("SQL").Split('|'));
              dic.Add("Shell", source.Configs["Extensions"].Get("Shell").Split('|'));
              dic.Add("Scala", source.Configs["Extensions"].Get("Scala").Split('|'));
              dic.Add("Scheme", source.Configs["Extensions"].Get("Scheme").Split('|'));
              dic.Add("INI", source.Configs["Extensions"].Get("INI").Split('|'));
              dic.Add("Yaml", source.Configs["Extensions"].Get("Yaml").Split('|'));
            foreach(var syntax in SyntaxHighlighter.LoadedSyntaxes)
                dic.Add(Path.GetFileNameWithoutExtension(syntax.SysPath), syntax.Extensions);
            return dic;
        }

        private void BuildLangList()
        {
            foreach (var language in Enum.GetValues(typeof(Language)))
                lstlang.Items.Add(language);
            foreach (var syntax in SyntaxHighlighter.LoadedSyntaxes)
                lstlang.Items.Add(Path.GetFileNameWithoutExtension(syntax.SysPath));
            lstlang.SelectedIndexChanged += lstlang_SelectedIndexChanged;
            lstlang.SelectedIndex = 0;
        }

        private void lstlang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lang = lstlang.SelectedItem.ToString();
            var dic = BuildReverseDictionary();
            lstextensions.Items.Clear();
            string[] items;
            dic.TryGetValue(lang, out items);
            if (items == null) return;
            foreach (var item in items)
                lstextensions.Items.Add(item);
        }

        private void cbBrackets_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.AutoCompleteBrackets = cbBrackets.Checked;
        }

        private void btnScriptCache_Click(object sender, EventArgs e)
        {
            try
            { 
                foreach(var file in Directory.GetFiles(SettingsBase.SettingsDir + @"Scripts\", "*.cache"))
                File.Delete(file);
            MessageBox.Show("Script Cache Successfully Cleared !", "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an Error Clearing the Cache\nMessage : " + ex.Message, "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}