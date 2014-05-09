#if TEST
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic
{
    /// <summary>
    /// Describes a Ynote Buffer
    /// </summary>
    public interface IYnoteBuffer
    {
        string Name { get; }
        /// <summary>
        /// The Type
        /// </summary>
        string Type { get; }
        /// <summary>
        /// Text of the Buffer
        /// </summary>
        string Text { get; }
        /// <summary>
        /// Shows The Buffer
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="state"></param>
        void Show(DockPanel panel, DockState state);
    }
}
#endif