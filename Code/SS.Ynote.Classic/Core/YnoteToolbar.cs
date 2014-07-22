using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using SS.Ynote.Classic.Core.Settings;

namespace SS.Ynote.Classic.Core
{
    public class YnoteToolbar
    {
        public static bool ToolBarExists()
        {
            string file = Path.Combine(GlobalSettings.SettingsDir, "Toolbar.json");
            return File.Exists(file);
        }

        public static void AddItems(ToolStrip tool)
        {
            var items = GetToolBarItems();
            if (items == null) return;
            foreach (var item in items)
            {
                Image img = Image.FromFile(item.IconFile);
                var button = new ToolStripButton(item.Text,img,(sender, args) => Commander.RunCommand(Globals.Ynote,item.Command));
                tool.Items.Add(button);
            }
        }
        static IEnumerable<ToolBarItem> GetToolBarItems()
        {
            try
            {
                string file = Path.Combine(GlobalSettings.SettingsDir, "Toolbar.json");
                return JsonConvert.DeserializeObject<List<ToolBarItem>>(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Parsing ToolBar File\n " + ex.Message, null, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }
    }

    class ToolBarItem
    {
        /// <summary>
        /// Text / ToolTip of the Tool Bar Item
        /// </summary>
        public string Text;
        /// <summary>
        /// The Icon of the Item
        /// </summary>
        public string IconFile;
        /// <summary>
        /// Command it will run
        /// </summary>
        public string Command;
    }
}
