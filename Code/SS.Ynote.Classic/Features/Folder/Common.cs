using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Project
{
    /// <summary>
    ///     ProjectNodeType
    /// </summary>
    public enum FolderNodeType
    {
        /// <summary>
        ///     The Node Type is a Folder
        /// </summary>
        Folder,

        /// <summary>
        ///     The Node Type is a File
        /// </summary>
        File
    }

    /// <summary>
    ///     Extended TreeNode for some properties
    /// </summary>
    public class ExTreeNode : TreeNode
    {
        public ExTreeNode(string text, string name,
            int imageIndex, int selimageindex,
            object tag, FolderNodeType projectNodeType)
        {
            Text = text;
            Tag = tag;
            Name = name;
            Type = projectNodeType;
            ImageIndex = imageIndex;
            SelectedImageIndex = selimageindex;
        }

        /// <summary>
        ///     The Node Type
        /// </summary>
        public FolderNodeType Type { get; private set; }
    }
}