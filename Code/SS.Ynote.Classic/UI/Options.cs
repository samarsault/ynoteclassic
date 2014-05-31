using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Core.Syntax;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            if (!Directory.Exists(YnoteSettings.SettingsDir))
                Directory.CreateDirectory(YnoteSettings.SettingsDir);
            tvBrowser.ExpandAll();
            InitYnoteSettings();
            BuildLangList();
            tvBrowser.SelectedNode = tvBrowser.Nodes["TabsNode"];
        }

        /// <summary>
        ///     Initialize YnoteSettings
        /// </summary>
        private void InitYnoteSettings()
        {
            cbdockstyle.Text = YnoteSettings.DocumentStyle.ToString();
            comboBox2.Text = YnoteSettings.BracketsStrategy == BracketsHighlightStrategy.Strategy2
                ? "Inside"
                : "Outside";
            tablocation.Text = YnoteSettings.TabLocation.ToString();
            checkBox1.Checked = YnoteSettings.ShowDocumentMap;
            cbBrackets.Checked = YnoteSettings.AutoCompleteBrackets;
            ShowLineNumber.Checked = YnoteSettings.ShowLineNumbers;
            showcaret.Checked = YnoteSettings.ShowCaret;
            showfoldinglines.Checked = YnoteSettings.ShowFoldingLines;
            virtualspace.Checked = YnoteSettings.EnableVirtualSpace;
            highlightfoliding.Checked = YnoteSettings.HighlightFolding;
            tbpaddingwidth.Text = YnoteSettings.PaddingWidth.ToString();
            tblineinterval.Text = YnoteSettings.LineInterval.ToString();
            comboBox1.Text = YnoteSettings.FoldingStrategy.ToString();
            tabsize.Value = YnoteSettings.TabSize;
            cbruler.Checked = YnoteSettings.ShowRuler;
            numrecent.Value = YnoteSettings.RecentFileNumber;
            cbSysTray.Checked = YnoteSettings.MinimizeToTray;
            cbmenu.Checked = YnoteSettings.ShowMenuBar;
            cbtool.Checked = YnoteSettings.ShowToolBar;
            cbstatus.Checked = YnoteSettings.ShowStatusBar;
            cbHighlightSameWords.Checked = YnoteSettings.HighlightSameWords;
            cbIME.Checked = YnoteSettings.IMEMode;
            cbBlockCursor.Checked = YnoteSettings.BlockCaret;
            cbTabs.Checked = YnoteSettings.UseTabs;
            cbchangedline.Checked = YnoteSettings.ShowChangedLine;
            cbScrollBars.Checked = YnoteSettings.ScrollBars;
            BuildEncodingList();
        }

        private void BuildEncodingList()
        {
            foreach (var encoding in Encoding.GetEncodings())
            {
                if (encoding.CodePage == Encoding.GetEncoding(YnoteSettings.DefaultEncoding).CodePage)
                    lblencoding.Text = encoding.DisplayName;
                lstencs.Items.Add(new EncodingItem(encoding));
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "Tabs":
                    tabcontrol.SelectTab(tabsettingpage);
                    break;

                case "Other":
                    tabcontrol.SelectTab(tpOther);
                    break;

                case "General":
                    tabcontrol.SelectTab(GeneralPage);
                    break;

                case "MISC":
                    tabcontrol.SelectTab(EditingPage);
                    break;

                case "File Extensions":
                    tabcontrol.SelectTab(FileTypesPage);
                    break;

                case "Encoding":
                    tabcontrol.SelectTab(encodingpage);
                    break;

                case "Manage":
                    tabcontrol.SelectTab(ClearPage);
                    break;

                case "Save YnoteSettings":
                    tabcontrol.SelectTab(savingfile);
                    break;
            }
        }

        private void tablocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                YnoteSettings.TabLocation = tablocation.Text.ToEnum<DocumentTabStripLocation>();
            }
            catch
            {
            }
        }

        private void cbdockstyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                YnoteSettings.DocumentStyle = cbdockstyle.Text.ToEnum<DocumentStyle>();
            }
            catch
            {
            }
        }

        private void showfoldinglines_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ShowFoldingLines = showfoldinglines.Checked;
        }

        private void showcaret_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ShowCaret = showcaret.Checked;
        }

        private void ShowLineNumber_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ShowLineNumbers = ShowLineNumber.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ShowDocumentMap = checkBox1.Checked;
        }

        private void highlightfoliding_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.HighlightFolding = highlightfoliding.Checked;
        }

        private void virtualspace_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.EnableVirtualSpace = virtualspace.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            YnoteSettings.FoldingStrategy = comboBox1.Text.ToEnum<FindEndOfFoldingBlockStrategy>();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                YnoteSettings.BracketsStrategy = comboBox2.Text == "Inside"
                    ? BracketsHighlightStrategy.Strategy2
                    : BracketsHighlightStrategy.Strategy1;
            }
            catch
            {
                ;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            YnoteSettings.PaddingWidth = tbpaddingwidth.IntValue;
            YnoteSettings.LineInterval = tblineinterval.IntValue;
            YnoteSettings.TabSize = Convert.ToInt32(tabsize.Value);
            YnoteSettings.RecentFileNumber = Convert.ToInt32(numrecent.Value);
            YnoteSettings.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var result =
                MessageBox.Show(
                    "You will need to restart the application for changes to take place.\nDo you want to continue ? ",
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                File.Delete(YnoteSettings.SettingsDir + "User.ynotesettings");
                Application.Restart();
            }
        }

        private void cbruler_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ShowRuler = cbruler.Checked;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(YnoteSettings.SettingsDir + "Extensions.xml");
        }

        private static IDictionary<string, string[]> BuildReverseDictionary()
        {
            var dic = new Dictionary<string, string[]>();
            using (var reader = XmlReader.Create(YnoteSettings.SettingsDir + "Extensions.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() && reader.Name == "Key")
                        dic.Add(reader["Language"], reader["Extensions"].Split('|'));
                }
            }
            foreach (var syntax in SyntaxHighlighter.LoadedSyntaxes.Where(syntax => syntax.SysPath != null))
                dic.Add(Path.GetFileNameWithoutExtension(syntax.SysPath), syntax.Extensions);
            return dic;
        }

        private void BuildLangList()
        {
            foreach (var language in Enum.GetValues(typeof (Language)))
                lstlang.Items.Add(language);
            foreach (var syntax in SyntaxHighlighter.LoadedSyntaxes.Where(syntax => syntax.SysPath != null))
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
            YnoteSettings.AutoCompleteBrackets = cbBrackets.Checked;
        }

        private void btnScriptCache_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var file in Directory.GetFiles(YnoteSettings.SettingsDir + @"Scripts\", "*.ysc"))
                    File.Delete(file);
                MessageBox.Show("Script Cache Successfully Cleared !", "Ynote Classic", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an Error Clearing the Cache\nMessage : " + ex.Message, "Ynote Classic",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(YnoteSettings.SettingsDir + "Recent.info");
                MessageBox.Show("Recent Files Successfully cleared. Changes will take place after restart.", null,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Clearing Recent Files \r\n Message : " + ex.Message, null,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lstencs_Click(object sender, EventArgs e)
        {
            if (lstencs.SelectedItem == null) return;
            var item = lstencs.SelectedItem as EncodingItem;
            YnoteSettings.DefaultEncoding = item.EncodingInfo.CodePage;
            lblencoding.Text = item.EncodingInfo.DisplayName;
        }

        private void cbSysTray_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.MinimizeToTray = cbSysTray.Checked;
        }

        private void cbHighlightSameWords_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.HighlightSameWords = cbHighlightSameWords.Checked;
        }

        private void cbIME_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.IMEMode = cbIME.Checked;
        }

        private void cbBlockCursor_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.BlockCaret = cbBlockCursor.Checked;
        }

        private void cbmenu_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ShowMenuBar = cbmenu.Checked;
        }

        private void cbtool_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ShowToolBar = cbmenu.Checked;
        }

        private void cbstatus_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ShowStatusBar = cbmenu.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ynote = Application.OpenForms[1] as IYnote;
            if (ynote == null)
                ynote = Application.OpenForms[0] as IYnote;

            ynote.OpenFile(YnoteSettings.SettingsDir + "User.ynotesettings");
            Close();
        }

        private void cbchangedline_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ShowChangedLine = cbchangedline.Checked;
        }

        private void cbScrollBars_CheckedChanged(object sender, EventArgs e)
        {
            YnoteSettings.ScrollBars = cbScrollBars.Checked;
        }
    }

    internal class EncodingItem
    {
        internal EncodingItem(EncodingInfo info)
        {
            EncodingInfo = info;
        }

        /// <summary>
        ///     Encoding Info
        /// </summary>
        internal EncodingInfo EncodingInfo { get; private set; }

        /// <summary>
        ///     To String Override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return EncodingInfo.DisplayName;
        }
    }
}