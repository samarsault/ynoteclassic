using System;
using System.IO;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Plugins;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            treeView1.ExpandAll();
            cbdockstyle.Text = SettingsBase.DocumentStyle.ToString();
            comboBox1.SelectedIndex = 0;
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
                case "KeyBindings":
                    tabcontrol.SelectTab(keybindingstab);
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
                case "Plugin Manager":
                    new PluginManager().Show();
                    break;
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Ynote KeyBindings(*.ynotekeybinding)|*.ynotekeybinding";
                dlg.ShowDialog();
                if (dlg.FileName == "") return;
                File.Copy(dlg.FileName, Application.StartupPath + "\\Users\\KeyBindings\\" + Path.GetFileName(dlg.FileName));
            }
        }

        private void tablocation_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                SettingsBase.TabLocation = (DocumentTabStripLocation)(tablocation.SelectedItem);
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void cbdockstyle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                SettingsBase.DocumentStyle = (DocumentStyle) (cbdockstyle.SelectedItem);
            }
            catch (Exception)
            {
                
            }
        }

        private void showfoldinglines_CheckedChanged(object sender, System.EventArgs e)
        {
            SettingsBase.ShowFoldingLines = showfoldinglines.Checked;
        }

        private void showcaret_CheckedChanged(object sender, System.EventArgs e)
        {
            SettingsBase.ShowCaret = showcaret.Checked;
        }

        private void ShowLineNumber_CheckedChanged(object sender, System.EventArgs e)
        {
            SettingsBase.ShowLineNumbers = ShowLineNumber.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            SettingsBase.ShowDocumentMap = checkBox1.Checked;
        }

        private void highlightfoliding_CheckedChanged(object sender, System.EventArgs e)
        {
            SettingsBase.HighlightFolding = highlightfoliding.Checked;
        }

        private void virtualspace_CheckedChanged(object sender, System.EventArgs e)
        {
            SettingsBase.EnableVirtualSpace = virtualspace.Checked;
        }

        private void cmbwordwrapmode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                SettingsBase.WordWrapMode = (WordWrapMode) (cmbwordwrapmode.SelectedItem);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                SettingsBase.BracketsStrategy = (BracketsHighlightStrategy) (comboBox2.SelectedItem);
            }
            catch (Exception)
            {
                
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SettingsBase.KeyBinding = comboBox3.SelectedText;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            SettingsBase.SaveConfiguration();
            Close();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            try
            {
                Directory.Delete(Environment.SpecialFolder.ApplicationData + @"\Ynote Classic\");
            }
            catch (Exception)
            {
                // move();
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            File.WriteAllText(string.Empty, Application.StartupPath + @"\User\Settings.ini");
            SettingsBase.RestoreDefault(Application.StartupPath + @"\User\Settings.ini");
        }

    }
}
