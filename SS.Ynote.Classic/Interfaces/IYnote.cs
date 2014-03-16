#region

using SS.Ynote.Classic.UI;
using WeifenLuo.WinFormsUI.Docking;

#endregion

/// <summary>
///     IYnote Interface
/// </summary>
public interface IYnote
{
    /// <summary>
    ///     DockPanel
    /// </summary>
    DockPanel Panel { get; }

    /// <summary>
    ///     Open File
    /// </summary>
    /// <param name="name"></param>
    void OpenFile(string name);

    /// <summary>
    ///     Save File
    /// </summary>
    /// <param name="edit"></param>
    void SaveEditor(Editor edit);

    /// <summary>
    ///     Create New Document
    /// </summary>
    void CreateNewDoc();
}