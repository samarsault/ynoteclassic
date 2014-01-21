using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

/// <summary>
/// Plugin Interface
/// </summary>
public interface IPlugin
{
    /// <summary>
    /// Name
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Version
    /// </summary>
    double Version { get; }
    /// <summary>
    /// Author of Plugin
    /// </summary>
    string Author { get; }
}
/// <summary>
/// IYnote Plugin
/// </summary>
public interface IYnotePlugin : IPlugin
{
    /// <summary>
    /// Ynote Reference
    /// </summary>
    IYnote Ynote { get; set; }
    /// <summary>
    /// MenuItem , if any, else='null'
    /// </summary>
    MenuItem MenuItem { get; }
    /// <summary>
    /// Run Plugin()
    /// </summary>
    void Initialize();
}

public interface IFileTypePlugin
{
    /// <summary>
    /// Open File
    /// </summary>
    /// <param name="file"></param>
    /// <param name="panel"></param>
    void Open(string file, DockPanel panel);
    /// <summary>
    /// Save File
    /// </summary>
    void Save();
    /// <summary>
    /// Supported Extensions for Opening Files
    /// </summary>
    string[] Extensions { get; }
}