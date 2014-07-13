using System.Windows.Forms;

namespace SS.Ynote.Classic.Core.Project
{
    /// <summary>
    ///     ProjectNodeType
    /// </summary>
    public enum ProjectNodeType
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
        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="name"></param>
        /// <param name="imageIndex"></param>
        /// <param name="selimageindex"></param>
        /// <param name="projectNodeType"></param>
        public ExTreeNode(string text, string name,
            ProjectNodeType projectNodeType)
        {
            Text = text;
            Name = name;
            Type = projectNodeType;
            int i = 0;
            if (projectNodeType == ProjectNodeType.File)
                i = 1;
            ImageIndex = i;
            SelectedImageIndex = i;
        }

        /// <summary>
        ///     The Node Type
        /// </summary>
        public ProjectNodeType Type { get; private set; }
    }
}