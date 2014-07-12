using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.Core.Syntax;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            if (!Directory.Exists(GlobalSettings.SettingsDir))
                Directory.CreateDirectory(GlobalSettings.SettingsDir);
            tvBrowser.ExpandAll();
            InitSettings();
            BuildLangList();
            tvBrowser.SelectedNode = tvBrowser.Nodes["TabsNode"];
        }

        /// <summary>
        ///     Initialize Globals.Settings
        /// </summary>
        private void InitSettings()
        {
            cbdockstyle.Text = Globals.Settings.DocumentStyle.ToString();
            comboBox2.Text = Globals.Settings.BracketsStrategy == BracketsHighlightStrategy.Strategy2
                ? "Inside"
                : "Outside";
            tablocation.Text = Globals.Settings.TabLocation.ToString();
            checkBox1.Checked = Globals.Settings.ShowDocumentMap;
            cbBrackets.Checked = Globals.Settings.AutoCompleteBrackets;
            ShowLineNumber.Checked = Globals.Settings.ShowLineNumbers;
            showcaret.Checked = Globals.Settings.ShowCaret;
            showfoldinglines.Checked = Globals.Settings.ShowFoldingLines;
            virtualspace.Checked = Globals.Settings.EnableVirtualSpace;
            highlightfoliding.Checked = Globals.Settings.HighlightFolding;
            tbpaddingwidth.Text = Globals.Settings.PaddingWidth.ToString();
            tblineinterval.Text = Globals.Settings.LineInterval.ToString();
            comboBox1.Text = Globals.Settings.FoldingStrategy.ToString();
            tabsize.Value = Globals.Settings.TabSize;
            cbruler.Checked = Globals.Settings.ShowRuler;
            numrecent.Value = Globals.Settings.RecentFileNumber;
            cbSysTray.Checked = Globals.Settings.MinimizeToTray;
            cbmenu.Checked = Globals.Settings.ShowMenuBar;
            cbtool.Checked = Globals.Settings.ShowToolBar;
            cbstatus.Checked = Globals.Settings.ShowStatusBar;
            cbHighlightSameWords.Checked = Globals.Settings.HighlightSameWords;
            cbIME.Checked = Globals.Settings.ImeMode;
            cbBlockCursor.Checked = Globals.Settings.BlockCaret;
            cbTabs.Checked = Globals.Settings.UseTabs;
            cbchangedline.Checked = Globals.Settings.ShowChangedLine;
            cbScrollBars.Checked = Globals.Settings.ScrollBars;
            tbwrap.Text = Globals.Settings.WrapWidth.ToString();
            BuildEncodingList();
        }

        private void BuildEncodingList()
        {
            foreach (var encoding in Encoding.GetEncodings())
            {
                if (encoding.CodePage == Encoding.GetEncoding(Globals.Settings.DefaultEncoding).CodePage)
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

                case "Save Settings":
                    tabcontrol.SelectTab(savingfile);
                    break;
            }
        }

        private void tablocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.Settings.TabLocation = tablocation.Text.ToEnum<DocumentTabStripLocation>();
            }
            catch
            {
            }
        }

        private void cbdockstyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.Settings.DocumentStyle = cbdockstyle.Text.ToEnum<DocumentStyle>();
            }
            catch
            {
            }
        }

        private void showfoldinglines_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ShowFoldingLines = showfoldinglines.Checked;
        }

        private void showcaret_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ShowCaret = showcaret.Checked;
        }

        private void ShowLineNumber_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ShowLineNumbers = ShowLineNumber.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ShowDocumentMap = checkBox1.Checked;
        }

        private void highlightfoliding_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.HighlightFolding = highlightfoliding.Checked;
        }

        private void virtualspace_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.EnableVirtualSpace = virtualspace.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.Settings.FoldingStrategy = comboBox1.Text.ToEnum<FindEndOfFoldingBlockStrategy>();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.Settings.BracketsStrategy = comboBox2.Text == "Inside"
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
            Globals.Settings.PaddingWidth = tbpaddingwidth.IntValue;
            Globals.Settings.LineInterval = tblineinterval.IntValue;
            Globals.Settings.WrapWidth = tbwrap.IntValue;
            Globals.Settings.TabSize = Convert.ToInt32(tabsize.Value);
            Globals.Settings.RecentFileNumber = Convert.ToInt32(numrecent.Value);
            GlobalSettings.Save(Globals.Settings, GlobalSettings.SettingsDir + "User.ynotesettings");
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
                File.Delete(GlobalSettings.SettingsDir + "User.ynotesettings");
                Application.Restart();
            }
        }

        private void cbruler_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ShowRuler = cbruler.Checked;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            Globals.Ynote.OpenFile(GlobalSettings.SettingsDir + "Extensions.xml");
        }

        private static IDictionary<string, string[]> BuildReverseDictionary()
        {
            var dic = new Dictionary<string, string[]>();
            using (var reader = XmlReader.Create(GlobalSettings.SettingsDir + "Extensions.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() && reader.Name == "Key")
                        dic.Add(reader["Language"], reader["Extensions"].Split('|'));
                }
            }
            foreach (var syntax in SyntaxHighlighter.LoadedSyntaxes.Where(syntax => syntax.SysPath != null))
                if(!dic.ContainsKey(syntax.Name))
                    dic.Add(Path.GetFileNameWithoutExtension(syntax.SysPath), syntax.Extensions);
            return dic;
        }

        private void BuildLangList()
        {
            foreach (var language in SyntaxHighlighter.Scopes)
                lstlang.Items.Add(language);
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
            Globals.Settings.AutoCompleteBrackets = cbBrackets.Checked;
        }

        private void btnScriptCache_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var file in Directory.GetFiles(GlobalSettings.SettingsDir + @"Scripts\", "*.ysc"))
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
                File.Delete(GlobalSettings.SettingsDir + "Recent.info");
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
            Globals.Settings.DefaultEncoding = item.EncodingInfo.CodePage;
            lblencoding.Text = item.EncodingInfo.DisplayName;
        }

        private void cbSysTray_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.MinimizeToTray = cbSysTray.Checked;
        }

        private void cbHighlightSameWords_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.HighlightSameWords = cbHighlightSameWords.Checked;
        }

        private void cbIME_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ImeMode = cbIME.Checked;
        }

        private void cbBlockCursor_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.BlockCaret = cbBlockCursor.Checked;
        }

        private void cbmenu_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ShowMenuBar = cbmenu.Checked;
        }

        private void cbtool_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ShowToolBar = cbmenu.Checked;
        }

        private void cbstatus_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ShowStatusBar = cbmenu.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Globals.Ynote.OpenFile(GlobalSettings.SettingsDir + "User.ynotesettings");
            Close();
        }

        private void cbchangedline_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ShowChangedLine = cbchangedline.Checked;
        }

        private void cbScrollBars_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ScrollBars = cbScrollBars.Checked;
        }

        private void cbTabs_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.UseTabs = cbTabs.Checked;
        }

        private void tbwrap_Enter(object sender, EventArgs e)
        {
            int visible = 1000;
            ToolTip tip = new ToolTip();
            tip.Show("Leave it 0 for automatic adjustment",tbwrap,0,-10,visible);
        }

    }

    class EncodingItem
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