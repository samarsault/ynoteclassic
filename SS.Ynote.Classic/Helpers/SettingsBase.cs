using FastColoredTextBoxNS;
using Nini.Config;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

/// <summary>
///     Contains all the Settings for Ynote
/// </summary>
public static class SettingsBase
{
    /// <summary>
    ///     Settings Directory
    ///     If Portable then same ,
    ///     else = Environment.SpecialFolder.ApplicationData  + "\Ynote Classic\"
    /// </summary>
    public static readonly string SettingsDir = Application.StartupPath + @"\User\";

    /// <summary>
    ///     ThemeFile
    /// </summary>
    public static string ThemeFile { get; set; }

    /// <summary>
    ///     Show Hidden Chars
    /// </summary>
    public static bool HiddenChars { get; set; }

    /// <summary>
    ///     Document Style
    /// </summary>
    public static DocumentStyle DocumentStyle { get; set; }

    /// <summary>
    ///     Get the Tab Location
    /// </summary>
    public static DocumentTabStripLocation TabLocation { get; set; }

    /// <summary>
    ///     Show Folding Lines
    /// </summary>
    public static bool ShowFoldingLines { get; set; }

    /// <summary>
    ///     Show Caret
    /// </summary>
    public static bool ShowCaret { get; set; }

    /// <summary>
    ///     Highlight Folding
    /// </summary>
    public static bool HighlightFolding { get; set; }

    /// <summary>
    ///     Show Document Map
    /// </summary>
    public static bool ShowDocumentMap { get; set; }

    /// <summary>
    ///     Show Ruler
    /// </summary>
    public static bool ShowRuler { get; set; }

    /// <summary>
    ///     Whether to Show Line Numbers
    /// </summary>
    public static bool ShowLineNumbers { get; set; }

    /// <summary>
    ///     Whether to Enable Virtual Space
    /// </summary>
    public static bool EnableVirtualSpace { get; set; }

    /// <summary>
    ///     Get the WordWrap Mode
    /// </summary>
    public static WordWrapMode WordWrapMode { get; set; }

    /// <summary>
    ///     Folding Strategy
    /// </summary>
    public static FindEndOfFoldingBlockStrategy FoldingStrategy { get; set; }

    /// <summary>
    ///     The Bracket Highlight Strategy
    /// </summary>
    public static BracketsHighlightStrategy BracketsStrategy { get; set; }

    /// <summary>
    ///     Padding Width
    /// </summary>
    public static int PaddingWidth { get; set; }

    /// <summary>
    ///     Line Interval
    /// </summary>
    public static int LineInterval { get; set; }

    /// <summary>
    ///     Show Status Bar
    /// </summary>
    public static bool ShowStatusBar { get; set; }

    /// <summary>
    ///     Show Menu Bar
    /// </summary>
    public static bool ShowMenuBar { get; private set; }

    /// <summary>
    ///     Get The FontFamily
    /// </summary>
    public static string FontFamily { get; private set; }

    /// <summary>
    ///     Get the font size
    /// </summary>
    public static float FontSize { get; private set; }

    /// <summary>
    ///     Get the Tab Size
    /// </summary>
    public static int TabSize { get; set; }

    /// <summary>
    ///     Zoom
    /// </summary>
    public static int Zoom { get; set; }

    /// <summary>
    /// Autocomplete Brackets
    /// </summary>
    public static bool AutoCompleteBrackets { get; set; }

    /// <summary>
    ///     Loads Settings
    /// </summary>
    public static void LoadSettings()
    {
        while (true)
        {
            if (File.Exists(SettingsDir + "Settings.ini"))
            {
                IConfigSource source = new IniConfigSource(SettingsDir + "Settings.ini");
                ThemeFile = source.Configs["Ynote"].Get("ThemeFile");
                HiddenChars = source.Configs["Ynote"].GetBoolean("ShowHiddenCharacters");
                DocumentStyle = source.Configs["Ynote"].Get("DocumentStyle").ToEnum<DocumentStyle>();
                TabLocation = source.Configs["Ynote"].Get("TabLocation").ToEnum<DocumentTabStripLocation>();
                WordWrapMode = source.Configs["Ynote"].Get("WordWrapMode").ToEnum<WordWrapMode>();
                BracketsStrategy = source.Configs["Ynote"].Get("BracketStrategy").ToEnum<BracketsHighlightStrategy>();
                FoldingStrategy = source.Configs["Ynote"].Get("FoldingStrategy").ToEnum<FindEndOfFoldingBlockStrategy>();
                ShowCaret = source.Configs["Ynote"].GetBoolean("ShowCaret");
                ShowDocumentMap = source.Configs["Ynote"].GetBoolean("ShowDocumentMap");
                ShowRuler = source.Configs["Ynote"].GetBoolean("ShowRuler");
                AutoCompleteBrackets = source.Configs["Ynote"].GetBoolean("AutocompleteBrackets");
                ShowFoldingLines = source.Configs["Ynote"].GetBoolean("ShowFoldingLines");
                ShowLineNumbers = source.Configs["Ynote"].GetBoolean("ShowLineNumbers");
                EnableVirtualSpace = source.Configs["Ynote"].GetBoolean("EnableVirtualSpace");
                HighlightFolding = source.Configs["Ynote"].GetBoolean("HighlightFolding");
                PaddingWidth = source.Configs["Ynote"].GetInt("PaddingWidth");
                LineInterval = source.Configs["Ynote"].GetInt("LineInterval");
                ShowStatusBar = source.Configs["Ynote"].GetBoolean("StatusBar");
                ShowMenuBar = source.Configs["Ynote"].GetBoolean("MenuBar");
                FontFamily = source.Configs["Ynote"].Get("FontFamily");
                FontSize = source.Configs["Ynote"].GetFloat("FontSize");
                TabSize = source.Configs["Ynote"].GetInt("TabSize");
                Zoom = source.Configs["Ynote"].GetInt("Zoom");
            }
            else
            {
                File.WriteAllText(SettingsDir + "Settings.ini", "");
                RestoreDefault(SettingsDir + "Settings.ini");
                continue;
            }
            break;
        }
    }

    /// <summary>
    ///     Save Configuration
    /// </summary>
    public static void SaveConfiguration()
    {
        IConfigSource source = new IniConfigSource(SettingsDir + "Settings.ini");
        source.Configs["Ynote"].Set("ThemeFile", ThemeFile);
        source.Configs["Ynote"].Set("ShowHiddenCharacters", HiddenChars);
        source.Configs["Ynote"].Set("DocumentStyle", DocumentStyle);
        source.Configs["Ynote"].Set("TabLocation", TabLocation);
        source.Configs["Ynote"].Set("WordWrapMode", WordWrapMode);
        source.Configs["Ynote"].Set("BracketStrategy", BracketsStrategy);
        source.Configs["Ynote"].Set("FoldingStrategy", FoldingStrategy);
        source.Configs["Ynote"].Set("ShowCaret", ShowCaret);
        source.Configs["Ynote"].Set("ShowDocumentMap", ShowDocumentMap);
        source.Configs["Ynote"].Set("ShowRuler", ShowRuler);
        source.Configs["Ynote"].Set("ShowFoldingLines", ShowFoldingLines);
        source.Configs["Ynote"].Set("ShowLineNumbers", ShowLineNumbers);
        source.Configs["Ynote"].Set("AutocompleteBrackets", AutoCompleteBrackets);
        source.Configs["Ynote"].Set("EnableVirtualSpace", EnableVirtualSpace);
        source.Configs["Ynote"].Set("HighlightFolding", HighlightFolding);
        source.Configs["Ynote"].Set("PaddingWidth", PaddingWidth);
        source.Configs["Ynote"].Set("LineInterval", LineInterval);
        source.Configs["Ynote"].Set("StatusBar", ShowStatusBar);
        source.Configs["Ynote"].Set("MenuBar", ShowMenuBar);
        source.Configs["Ynote"].Set("FontFamily", FontFamily);
        source.Configs["Ynote"].Set("FontSize", FontSize);
        source.Configs["Ynote"].Set("TabSize", TabSize);
        source.Configs["Ynote"].Set("Zoom", Zoom);
        source.Save();
    }

    /// <summary>
    ///     Restores Default Settings
    /// </summary>
    /// <param name="configfile"></param>
    public static void RestoreDefault(string configfile)
    {
        IConfigSource source = new IniConfigSource(configfile);
        var config = source.AddConfig("Ynote");
        config.Set("ThemeFile", Application.StartupPath + @"\Themes\Default.ynotetheme");
        config.Set("ShowHiddenCharacters", false);
        config.Set("DocumentStyle", DocumentStyle.DockingMdi);
        config.Set("TabLocation", DocumentTabStripLocation.Top);
        config.Set("WordWrapMode", WordWrapMode.WordWrapControlWidth);
        config.Set("BracketStrategy", BracketsHighlightStrategy.Strategy2);
        source.Configs["Ynote"].Set("FoldingStrategy", FindEndOfFoldingBlockStrategy.Strategy1);
        config.Set("ShowCaret", true);
        config.Set("ShowDocumentMap", true);
        config.Set("ShowRuler", false);
        config.Set("ShowFoldingLines", true);
        config.Set("ShowLineNumbers", true);
        config.Set("ShowChangedLine", false);
        config.Set("AutocompleteBrackets", true);
        config.Set("EnableVirtualSpace", false);
        config.Set("HighlightFolding", true);
        config.Set("PaddingWidth", 18);
        config.Set("LineInterval", 0);
        config.Set("MenuBar", true);
        config.Set("StatusBar", true);
        config.Set("FontFamily", "Consolas");
        config.Set("FontSize", 9.75F);
        config.Set("TabSize", 4);
        config.Set("Zoom", 100);
        source.Save();
    }
}