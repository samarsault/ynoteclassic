using WeifenLuo.WinFormsUI.Docking;

///<summary>
/// IYnote Interface
///</summary>
public interface IYnote
{
    /// <summary>
    /// Open File
    /// </summary>
    /// <param name="name"></param>
    void OpenFile(string name);
    /// <summary>
    /// Save File
    /// </summary>
    /// <param name="edit"></param>
    void SaveEditor(SS.Ynote.Classic.UI.Editor edit);
    /// <summary>
    /// Create New Document
    /// </summary>
    void CreateNewDoc();
    /// <summary>
    /// DockPanel
    /// </summary>
    DockPanel Panel { get; }
}
