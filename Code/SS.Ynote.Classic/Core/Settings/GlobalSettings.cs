//#define PORTABLE
#if PORTABLE
    using System.Windows.Forms;
#else
    using System;
#endif
using System.IO;
using FastColoredTextBoxNS;
using Newtonsoft.Json;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.Core.Settings
{
    internal class GlobalSettings
    {
#if !PORTABLE
        internal static readonly string SettingsDir =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Ynote Classic\";
#else
        internal static readonly string SettingsDir = Application.StartupPath + @"\Package\";
#endif
#if DEVBUILD
        public static int BuildNumber;
#endif

        /// <summary>
        ///     Loads Properties
        /// </summary>
        /// <param name="file"></param>
        public static GlobalProperties Load(string file)
        {
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                return JsonConvert.DeserializeObject<GlobalProperties>(json);
            }
            RestoreDefault(file);
            return Load(file);
        }
        /// <summary>
        /// Restore Default Properties
        /// </summary>
        /// <param name="file"></param>
        private static void RestoreDefault(string file)
        {
            var prop = new GlobalProperties();
            prop.AutoCompleteBrackets = true;
            prop.EnableVirtualSpace = false;
            prop.FontFamily = "Consolas";
            prop.FontSize = 9.75f;
            prop.DefaultEncoding = 1251;
            prop.DocumentStyle = DocumentStyle.DockingMdi;
            prop.TabLocation = DocumentTabStripLocation.Top;
            prop.FoldingStrategy = FindEndOfFoldingBlockStrategy.Strategy1;
            prop.BracketsStrategy = BracketsHighlightStrategy.Strategy2;
            prop.MinimizeToTray = false;
            prop.ShowChangedLine = false;
            prop.BlockCaret = false;
            prop.ImeMode = false;
            prop.HiddenChars = false;
            prop.LineInterval = 0;
            prop.ShowToolBar = false;
            prop.ShowStatusBar = true;
            prop.ShowMenuBar = true;
            prop.ThemeFile = "User\\Themes\\Default.ynotetheme";
            prop.HighlightFolding = true;
            prop.ShowFoldingLines = true;
            prop.ShowCaret = true;
            prop.UseTabs = false;
            prop.Zoom = 100;
            prop.WordWrap = false;
            prop.ShowRuler = false;
            prop.ShowDocumentMap = true;
            prop.PaddingWidth = 17;
            prop.RecentFileNumber = 15;
            prop.ScrollBars = true;
            prop.ShowLineNumbers = true;
            prop.TabSize = 4;
            File.WriteAllText(file, SerializeProperties(prop));
        }

        private static string SerializeProperties(GlobalProperties properties)
        {
            return JsonConvert.SerializeObject(properties, Formatting.Indented);
        }

        public static void Save(GlobalProperties properties, string file)
        {
            string serialized = SerializeProperties(properties);
            File.WriteAllText(file, serialized);
        }
    }
}