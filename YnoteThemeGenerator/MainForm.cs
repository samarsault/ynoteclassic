using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using YnoteThemeGenerator.Properties;

namespace YnoteThemeGenerator
{
    internal partial class MainForm : Form
    {
        #region Constructors

        private string OpenedFile;

        public MainForm()
        {
            InitializeComponent();
            OpenedFile = null;
            foreach (object item in Enum.GetValues(typeof (ColorPalette)))
                cmbpalette.Items.Add(item);
            cmbpalette.SelectedIndex = 3;
        }

        private YnoteThemeReader ThemeReader { get; set; }

        #endregion

        #region Overridden Members

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            colorEditorManager.Color = Color.SeaGreen;
            foreach (object obj in Enum.GetValues(typeof (FontStyle)))
                lstfontstyle.Items.Add(obj);
        }

        #endregion

        #region Event Handlers

        private void colorEditorManager_ColorChanged(object sender, EventArgs e)
        {
            try
            {
                panel1.BackColor = colorEditorManager.ColorEditor.Color;
                var item = lstprops.SelectedItems[0].Tag as ThemeKeyValue;
                item.Hex = "#" + colorEditorManager.ColorEditor.Hex;
                lstprops.SelectedItems[0].SubItems[1].Text = "#" + colorEditorManager.ColorEditor.Hex;
            }
            catch (Exception)
            {
                // throw;
            }
        }

        #endregion

        private void menuItem7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ynote Theme Editor\r\n\r\nv1.0 for Ynote Classic 2\r\nCopyright (C) 2014 Samarjeet Singh");
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ynote Themes (*.ynotetheme)|*.ynotetheme";
                ofd.ShowDialog();
                if (ofd.FileName == "") return;
                var reader = new YnoteThemeReader();
                reader.Read(ofd.FileName);
                ThemeReader = reader;
                lstprops.Items.Clear();
                foreach (var key in reader.KeyAssociation)
                {
                    lstprops.Items.Add(
                        new ListViewItem(new[]
                        {key.Key, key.Value.Hex, key.Value.FontStyle.ToString(), key.Value.KeyType.ToString()})
                        {
                            Tag = key.Value
                        });
                }
                OpenedFile = ofd.FileName;
                Text = "Ynote Themes Editor : " + Path.GetFileName(OpenedFile);
            }
        }

        private void lstprops_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var item = lstprops.SelectedItems[0].Tag as ThemeKeyValue;
                colpanel.Enabled = true;
                colorEditorManager.Color = ColorTranslator.FromHtml(item.Hex);
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            using (var browser = new FolderBrowserDialog())
            {
                browser.ShowDialog();
                if (browser.SelectedPath == "") return;
                if (File.Exists(browser.SelectedPath + @"\SS.Ynote.Classic.exe") &&
                    Directory.Exists(browser.SelectedPath + @"\Themes"))
                {
                    Settings.Default.YnoteDir = browser.SelectedPath;
                    Settings.Default.Save();
                    MessageBox.Show("Settings Saved.");
                }
                else
                {
                    MessageBox.Show("Error ! Can't find SS.Ynote.Classic.exe / Themes Directory", "Ynote Theme Editor");
                }
            }
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            if(OpenedFile != null)
                Process.Start(Settings.Default.YnoteDir + @"\SS.Ynote.Classic.exe", OpenedFile);
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(OpenedFile, Settings.Default.YnoteDir + @"\Themes\" + Path.GetFileName(OpenedFile));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            if (OpenedFile == "NewFile")
            {
                using (var sf = new SaveFileDialog())
                {
                    sf.Filter = "Ynote Themes (*.ynotetheme)|*.ynotetheme";
                    sf.ShowDialog();
                    if (sf.FileName == "" || ThemeReader == null) return;
                    ThemeReader.Save(sf.FileName);
                }
            }
            else
            {
                if (ThemeReader == null) return;
                ThemeReader.Save(OpenedFile);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            var reader = new YnoteThemeReader();
            reader.Read(Application.StartupPath + @"\Templates\New.ynotetheme");
            ThemeReader = reader;
            lstprops.Items.Clear();
            foreach (var key in reader.KeyAssociation)
            {
                lstprops.Items.Add(
                    new ListViewItem(new[]
                    {key.Key, key.Value.Hex, key.Value.FontStyle.ToString(), key.Value.KeyType.ToString()})
                    {
                        Tag = key.Value
                    });
            }
            OpenedFile = "NewFile";
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(OpenedFile, Settings.Default.YnoteDir + @"\Themes\" + Path.GetFileName(OpenedFile));
                Process.Start(Settings.Default.YnoteDir + @"\SS.Ynote.Classic.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbpalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            colorGrid.Palette = (ColorPalette) (cmbpalette.SelectedItem);
        }

        private void colorGrid_ColorChanged(object sender, EventArgs e)
        {
            panel1.BackColor = colorEditorManager.ColorEditor.Color;
            try
            {
                var item = lstprops.SelectedItems[0].Tag as ThemeKeyValue;
                item.Hex = "#" + colorEditorManager.ColorEditor.Hex;
                lstprops.SelectedItems[0].SubItems[1].Text = "#" + colorEditorManager.ColorEditor.Hex;
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            panel1.BackColor = colorEditorManager.ColorEditor.Color;
            try
            {
                var item = lstprops.SelectedItems[0].Tag as ThemeKeyValue;
                item.Hex = "#" + colorEditorManager.ColorEditor.Hex;
                lstprops.SelectedItems[0].SubItems[1].Text = "#" + colorEditorManager.ColorEditor.Hex;
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void lstfontstyle_SelectedValueChanged(object sender, EventArgs e)
        {
            var item = lstprops.SelectedItems[0].Tag as ThemeKeyValue;
            if (item.KeyType == KeyType.Style)
            {
                lstprops.SelectedItems[0].SubItems[2].Text = item.FontStyle.ToString();
                item.FontStyle = (FontStyle)(lstfontstyle.SelectedItem);
            }
        }
    }
}