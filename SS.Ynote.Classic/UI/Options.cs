using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Features.Syntax;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            if (!Directory.Exists(SettingsBase.SettingsDir))
                Directory.CreateDirectory(SettingsBase.SettingsDir);
            tvBrowser.ExpandAll();
            InitSettings();
            BuildLangList();
            tvBrowser.SelectedNode = tvBrowser.Nodes["TabsNode"];
        }

        /// <summary>
        ///     Initialize Settings
        /// </summary>
        private void InitSettings()
        {
            cbdockstyle.Text = SettingsBase.DocumentStyle.ToString();
            comboBox2.Text = SettingsBase.BracketsStrategy == BracketsHighlightStrategy.Strategy2 ? "Inside" : "Outside";
            tablocation.Text = SettingsBase.TabLocation.ToString();
            checkBox1.Checked = SettingsBase.ShowDocumentMap;
            cbBrackets.Checked = SettingsBase.AutoCompleteBrackets;
            ShowLineNumber.Checked = SettingsBase.ShowLineNumbers;
            showcaret.Checked = SettingsBase.ShowCaret;
            showfoldinglines.Checked = SettingsBase.ShowFoldingLines;
            virtualspace.Checked = SettingsBase.EnableVirtualSpace;
            highlightfoliding.Checked = SettingsBase.HighlightFolding;
            tbpaddingwidth.Text = SettingsBase.PaddingWidth.ToString();
            tblineinterval.Text = SettingsBase.LineInterval.ToString();
            comboBox1.Text = SettingsBase.FoldingStrategy.ToString();
            tabsize.Value = SettingsBase.TabSize;
            cbruler.Checked = SettingsBase.ShowRuler;
            numrecent.Value = SettingsBase.RecentFileNumber;
            cbSysTray.Checked = SettingsBase.MinimizeToTray;
            cbmenu.Checked = SettingsBase.ShowMenuBar;
            cbtool.Checked = SettingsBase.ShowToolBar;
            cbstatus.Checked = SettingsBase.ShowStatusBar;
            cbHighlightSameWords.Checked = SettingsBase.HighlightSameWords;
            cbIME.Checked = SettingsBase.IMEMode;
            cbBlockCursor.Checked = SettingsBase.BlockCursor;
            BuildEncodingList();
        }

        void BuildEncodingList()
        {
            foreach (var encoding in Encoding.GetEncodings())
            {
                if (encoding.CodePage == Encoding.GetEncoding(SettingsBase.DefaultEncoding).CodePage)
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
                    tabcontrol.SelectTab(FileExtensionsPage);
                    break;
                case "Encoding":tabcontrol.SelectTab(encodingpage);
                    break;
                case "Manage":
                    tabcontrol.SelectTab(ClearPage);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsBase.FoldingStrategy = comboBox1.Text.ToEnum<FindEndOfFoldingBlockStrategy>();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SettingsBase.BracketsStrategy = comboBox2.Text == "Inside" ? BracketsHighlightStrategy.Strategy2 : BracketsHighlightStrategy.Strategy1;
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
            SettingsBase.RecentFileNumber = Convert.ToInt32(numrecent.Value);
            SettingsBase.SaveConfiguration();
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
                File.Delete(SettingsBase.SettingsDir + "Settings.ini");
                Application.Restart();
            }
        }

        private void cbruler_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.ShowRuler = cbruler.Checked;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(SettingsBase.SettingsDir + "Extensions.xml");
        }

        private static IDictionary<string, string[]> BuildReverseDictionary()
        {
            var dic = new Dictionary<string, string[]>();
            using (var reader = XmlReader.Create(SettingsBase.SettingsDir + "Extensions.xml"))
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
            SettingsBase.AutoCompleteBrackets = cbBrackets.Checked;
        }

        private void btnScriptCache_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var file in Directory.GetFiles(SettingsBase.SettingsDir + @"Scripts\", "*.cache"))
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
                File.Delete(SettingsBase.SettingsDir + "Recent.info");
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
            SettingsBase.DefaultEncoding = item.EncodingInfo.CodePage;
            lblencoding.Text = item.EncodingInfo.DisplayName;
        }

        private void cbSysTray_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.MinimizeToTray = cbSysTray.Checked;
        }

        private void cbHighlightSameWords_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.HighlightSameWords = cbHighlightSameWords.Checked;
        }

        private void cbIME_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.IMEMode = cbIME.Checked;
        }

        private void cbBlockCursor_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.BlockCursor = cbBlockCursor.Checked;
        }

        private void cbmenu_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.ShowMenuBar = cbmenu.Checked;
        }

        private void cbtool_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.ShowToolBar = cbmenu.Checked;
        }

        private void cbstatus_CheckedChanged(object sender, EventArgs e)
        {
            SettingsBase.ShowStatusBar = cbmenu.Checked;
        }
    }

    internal class EncodingItem
    {
        internal EncodingItem(EncodingInfo info)
        {
            EncodingInfo = info;
        }
        /// <summary>
        /// Encoding Info 
        /// </summary>
        internal EncodingInfo EncodingInfo { get; private set; }
        /// <summary>
        /// To String Override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return EncodingInfo.DisplayName;
        }
    }
}
