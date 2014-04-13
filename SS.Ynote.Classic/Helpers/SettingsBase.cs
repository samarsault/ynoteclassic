//==================================
//
// Copyright (C) 2014 Samarjeet Singh (singh.samarjeet.27@gmail.com)
//
//===================================
//#define PORTABLE
using System.IO;
using System.Text;
using FastColoredTextBoxNS;
using Nini.Config;
using WeifenLuo.WinFormsUI.Docking;

#if PORTABLE
    using System.Windows.Forms;
#else
    using System;
#endif
/// <summary>
/// Contains all the Settings for Ynote
/// </summary>
public static class SettingsBase
{
#if !PORTABLE
    internal static readonly string SettingsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Ynote_Classic\";
#else
    internal static readonly string SettingsDir = Application.StartupPath + @"\User\";
#endif
/// <summary>
    ///     Show Hidden Chars
    /// </summary>
    public static string ThemeFile { get; set; }

    /// <summary>
    ///     Document Style
    /// </summary>ry>
    public static bool HiddenChars { get; set; }

    ///<summary>
    ///     Get the Tab Location
    ///</summary>
    internal static DocumentStyle DocumentStyle { get; set; }

    /// <summary>
    ///   /// <summary>
    ///     Show Folding Lines
    internal static DocumentTabStripLocation TabLocation { get; set; }

    /// <sum/// <summary>
    ///     Show Caret
    /// </summary>
    internal static bool ShowFoldingLines { get; set; }
/// <summary>
    ///     Highlight Folding
    /// </summary>
    internal static bool ShowCaret { get; set; }

    /// </// <summary>
    ///     Show Document Map
    public static bool HighlightFolding { get; set; }

    /// /// <summary>
    ///     Show Ruler
    /// </summary>ry>
    public static bool ShowDocumentMap { get; set; }

    /// <summary>
    ///     Whether to Show Line Numbers
    internal static bool ShowRuler { get; set; }

    /// <summary>
  /// <summary>
    ///     Whether to Enable Virtual Space
    internal static bool ShowLineNumbers { get; set; }

    /// <summary>
    /// /// <summary>
    ///     Folding Strategy
    internal static bool EnableVirtualSpace { get; set; }
    /// <summary>
    ///     Foldi/// <summary>
    ///     The Bracket Highlight Strategy
    internal static FindEndOfFoldingBlockStrategy FoldingStrategy { get; set; }

    /// <summary>
    ///     The Bracket High/// <summary>
    ///     Padding Width
    internal static BracketsHighlightStrategy BracketsStrategy { get; set; }

  /// <summary>
    ///     Line Interval
    /// </summary>y>
    public static int PaddingWidth { get; set; }

  /// <summary>
    ///     Show Status Bar
    /// </summary>
    public static int LineInterval { get; set; }

    /// <summary>
    ///  Show Status Bar
    /// </summary>
    internal static bool ShowStatusBar { get; set; }

   /// <summary>
   /// Show MenuBar
   /// </summary>
    internal static bool ShowMenuBar { get; private set; }

    /// <summary>
    /// Font-Family
    /// </summary>
    internal  static string FontFamily { get; private set; }
    /// <summary>
    /// No. of RecentFiles
    /// </summary>
    internal static int RecentFileNumber { get; set; }

    /// <summary>
    /// Font Size
    ///  </summary>
    internal static float FontSize { get; private set; }

    /// <summary>
    ///     Zoom
    /// </summary>
    internal static int TabSize { get; set; }

    /// <summary>
    ///     Autocomplete Brackets
    /// </summary>
    internal static int Zoom { get; set; }
    /// <summary>
    ///     Loads Settings
    /// </summary>
    internal static bool AutoCompleteBrackets { get; set; }
    /// <summary>
    ///     Checks if WordWrap is on
    /// </summary>
    internal static bool WordWrap { get; set; }
    /// <summary>
    /// Gets The Default Encoding for Saving Document
    /// </summary>
    internal static int DefaultEncoding { get; set; }
    /// <summary>
    /// Show the Tool Bar
    /// </summary>
    internal static bool ShowToolBar { get; set; }
    /// <summary>
    ///     Loads Settings
    /// </summary>
    internal static void LoadSettings()
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
                DefaultEncoding = source.Configs["Ynote"].GetInt("Encoding");
                ShowStatusBar = source.Configs["Ynote"].GetBoolean("StatusBar");
                ShowToolBar = source.Configs["Ynote"].GetBoolean("ToolBar");
                ShowMenuBar = source.Configs["Ynote"].GetBoolean("MenuBar");
                FontFamily = source.Configs["Ynote"].Get("FontFamily");
                WordWrap = source.Configs["Ynote"].GetBoolean("Wordwrap");
                FontSize = source.Configs["Ynote"].GetFloat("FontSize");
                TabSize = source.Configs["Ynote"].GetInt("TabSize");
                RecentFileNumber = source.Configs["Ynote"].GetInt("RecentFilesNo");
                Zoom = source.Configs["Ynote"].GetInt("Zoom");
            }
            else
            {
                File.WriteAllText(SettingsDir + "Settings.ini", null);
                RestoreDefault();
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
        source.Configs["Ynote"].Set("RecentFilesNo", RecentFileNumber);
        source.Configs["Ynote"].Set("StatusBar", ShowStatusBar);
        source.Configs["Ynote"].Set("ToolBar", ShowToolBar);
        source.Configs["Ynote"].Set("MenuBar", ShowMenuBar);
        source.Configs["Ynote"].Set("FontFamily", FontFamily);
        source.Configs["Ynote"].Set("FontSize", FontSize);
        source.Configs["Ynote"].Set("Encoding", DefaultEncoding);
        source.Configs["Ynote"].Set("Wordwrap", WordWrap);
        source.Configs["Ynote"].Set("Zoom", Zoom);
        source.Save();
    }

    static void RestoreDefault()
    {
        IConfigSource source = new IniConfigSource(SettingsDir + "Settings.ini");
        var config = source.AddConfig("Ynote");
        config.Set("ThemeFile", SettingsDir + @"\Themes\Default.ynotetheme");
        config.Set("ShowHiddenCharacters", false);
        config.Set("DocumentStyle", DocumentStyle.DockingMdi);
        config.Set("TabLocation", DocumentTabStripLocation.Top);
        config.Set("BracketStrategy", BracketsHighlightStrategy.Strategy2);
        config.Set("FoldingStrategy", FindEndOfFoldingBlockStrategy.Strategy1);
        config.Set("ShowCaret", true);
        config.Set("ShowDocumentMap", true);
        config.Set("ShowRuler", false);
        config.Set("ShowFoldingLines", true);
        config.Set("ShowLineNumbers", true);
        config.Set("AutocompleteBrackets", true);
        config.Set("EnableVirtualSpace", false);
        config.Set("HighlightFolding", true);
        config.Set("PaddingWidth", 18);
        config.Set("LineInterval", 0);
        config.Set("RecentFilesNo", 15);
        config.Set("MenuBar", true);
        config.Set("StatusBar", true);
        config.Set("ToolBar", false);
        config.Set("Wordwrap", false);
        config.Set("Encoding", Encoding.Default.CodePage);
        config.Set("FontFamily", "Consolas");
        config.Set("FontSize", 9.75F);
        config.Set("TabSize", 4);
        config.Set("Zoom", 100);
        source.Save();
    }
}