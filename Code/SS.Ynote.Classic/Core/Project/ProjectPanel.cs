using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.Core.Project
{
    /// <summary>
    ///     Ynote Classic Project Panel
    /// </summary>
    public partial class ProjectPanel : DockContent
    {
        #region Private Variables

        /// <summary>
        ///     Ynote Reference to the Object
        /// </summary>
        private readonly IYnote _ynote;

        #endregion

        #region Constructor

        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="ynote">Reference to Ynote</param>
        public ProjectPanel(IYnote ynote)
        {
            InitializeComponent();
            _ynote = ynote;
        }

        public YnoteProject Project
        {
            get { return projtree.Tag as YnoteProject; }
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        ///     Opens a Project
        /// </summary>
        /// <param name="file"></param>
        public void OpenProject(YnoteProject project)
        {
            projtree.Nodes.Clear();
            // initialize the node
            if (!Directory.Exists(project.Path))
                MessageBox.Show(string.Format("Error : Can't find directory : {0}", project.Path), "Folder Manager",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                BeginInvoke((MethodInvoker) (() => ListDirectory(projtree, project)));
            projtree.Tag = project;
        }

        public void CloseProject()
        {
            projtree.Nodes.Clear();
            Close();
        }

        private static void ListDirectory(TreeView treeView, YnoteProject project)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(project.Path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, project.ExcludeDirectories,
                project.ExcludeFileTypes));
        }

        private static ExTreeNode CreateDirectoryNode(DirectoryInfo directoryInfo, string[] excludedir,
            string[] excludefile)
        {
            var directoryNode = new ExTreeNode(directoryInfo.Name, directoryInfo.FullName, 0, 0, FolderNodeType.Folder);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                if (excludedir == null)
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory, excludedir, excludefile));
                else
                {
                    if (!excludedir.Contains(directory.Name))
                        directoryNode.Nodes.Add(CreateDirectoryNode(directory, excludedir, excludefile));
                }
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                if (excludefile == null)
                    directoryNode.Nodes.Add(new ExTreeNode(file.Name, file.FullName, 1, 1, FolderNodeType.File));
                else
                {
                    if (!excludefile.Contains(Path.GetExtension(file.FullName)))
                        directoryNode.Nodes.Add(new ExTreeNode(file.Name, file.FullName, 1, 1, FolderNodeType.File));
                }
            }
            return directoryNode;
        }

        /// <summary>
        ///     Adds a New Folder to existing folder
        /// </summary>
        private void AddNewFolder()
        {
            var path = projtree.SelectedNode as ExTreeNode;
            if (path != null && path.Type == FolderNodeType.Folder)
            {
                using (var util = new FolderUtils())
                {
                    if (util.ShowDialog(this) != DialogResult.OK) return;
                    var dir = Path.Combine(path.Name, util.FileName);
                    var node = new ExTreeNode(util.FileName, dir, 0, 0, FolderNodeType.Folder);
                    Directory.CreateDirectory(dir);
                    projtree.SelectedNode.Nodes.Add(node);
                }
            }
        }

/*
        /// <summary>
        ///     Finds the root Node
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private static TreeNode FindRootNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
                treeNode = treeNode.Parent;
            return treeNode;
        }
*/

        /// <summary>
        ///     Delete File/Folder
        /// </summary>
        private void DeleteFile()
        {
            var node = projtree.SelectedNode;
            var result = MessageBox.Show("Are you sure you want to delete " + node.Name + " ?", "Project",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (IsDir(node.Name) && result == DialogResult.Yes)
            {
                Directory.Delete(node.Name, true);
                projtree.Nodes.Remove(node);
            }
            else if (!IsDir(node.Name) && result == DialogResult.Yes)
            {
                File.Delete(node.Name);
                projtree.Nodes.Remove(node);
            }
        }

        /// <summary>
        ///     Renames Directory
        /// </summary>
        /// <param name="path"></param>
        /// <param name="newpath"></param>
        /// <param name="node"></param>
        private void RenameDirectory(string path, string newpath, ExTreeNode node)
        {
            Directory.Move(path, newpath);
            node.Text = Path.GetFileName(newpath);
            node.Name = newpath;
        }

        /// <summary>
        ///     Rename
        /// </summary>
        /// <param name="path"></param>
        /// <param name="newpath"></param>
        /// <param name="node"></param>
        private static void RenameFile(string path, string newpath, ExTreeNode node)
        {
            File.Move(path, newpath);
            node.Name = newpath;
        }

        /// <summary>
        ///     DoRename
        /// </summary>
        private void DoRename()
        {
            var node = projtree.SelectedNode as ExTreeNode;
            var filename = projtree.SelectedNode.Name;
            var dir = Path.GetDirectoryName(projtree.SelectedNode.Name);
            using (var dlg = new FolderUtils())
            {
                var result = dlg.ShowDialog() == DialogResult.OK;
                if (result)
                {
                    if (node.Type == FolderNodeType.Folder)
                        RenameDirectory(filename, dir + @"\" + dlg.FileName, node);
                    else if (node.Type == FolderNodeType.File)
                        RenameFile(filename, dir + @"\" + dlg.FileName, node);
                    node.Text = dlg.FileName;
                }
            }
        }

        /// <summary>
        ///     Copies a directory from strSource to strDestination
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strDestination"></param>
        private static void CopyDirectory(string strSource, string strDestination)
        {
            if (!Directory.Exists(strDestination))
            {
                Directory.CreateDirectory(strDestination);
            }
            var dirInfo = new DirectoryInfo(strSource);
            var files = dirInfo.GetFiles();
            foreach (var tempfile in files)
            {
                tempfile.CopyTo(Path.Combine(strDestination, tempfile.Name));
            }
            var dirctororys = dirInfo.GetDirectories();
            foreach (var tempdir in dirctororys)
                CopyDirectory(Path.Combine(strSource, tempdir.Name), Path.Combine(strDestination, tempdir.Name));
        }

        /// <summary>
        ///     Add New File
        /// </summary>
        private void AddNewFile()
        {
            try
            {
                var path = projtree.SelectedNode as ExTreeNode;
                if (path != null && path.Type == FolderNodeType.Folder)
                {
                    using (var util = new FolderUtils())
                    {
                        if (util.ShowDialog(this) != DialogResult.OK) return;
                        var file = Path.Combine(path.Name, util.FileName);
                        var node = new ExTreeNode(util.FileName, file, 1, 1, FolderNodeType.File);
                        File.WriteAllText(file, "");
                        projtree.SelectedNode.Nodes.Add(node);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an Error : " + ex.Message, "Project Manager", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        ///     Checks whether a Path is a Directory or File
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsDir(string input)
        {
            return (File.GetAttributes(input) & FileAttributes.Directory)
                   == FileAttributes.Directory;
        }

        #endregion Methods

        #region Events

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node.Name;
            var n = e.Node as ExTreeNode;
            if (n.Type == FolderNodeType.Folder)
                return;
            _ynote.OpenFile(node);
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            AddNewFolder();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            AddNewFile();
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            DeleteFile();
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            DoRename();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            DoRename();
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            if (projtree.SelectedNode == null) return;
            var folder = projtree.SelectedNode.Name;
            Process.Start(folder);
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            var selected = projtree.SelectedNode as ExTreeNode;
            if (selected == null) return;
            var path = selected.Name;
            var fileName = Path.ChangeExtension(path, "") + "-Copy" + Path.GetExtension(path);
            File.Copy(path, fileName);
            var parent = selected.Parent as ExTreeNode;
            if (parent != null && parent.Type == FolderNodeType.Folder)
                parent.Nodes.Add(new ExTreeNode(Path.GetFileName(fileName), fileName, 1, 1, FolderNodeType.File));
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            _ynote.OpenFile(projtree.SelectedNode.Name);
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dlg = new OpenFileDialog())
                {
                    dlg.Filter = "All Files (*.*)|*.*";
                    var res = dlg.ShowDialog() == DialogResult.OK;
                    if (!res) return;
                    var selectedNode = projtree.SelectedNode as ExTreeNode;
                    var dir = selectedNode.Name;
                    var newfile = Path.Combine(dir, Path.GetFileName(dlg.FileName));
                    File.Copy(dlg.FileName, newfile);
                    selectedNode.Nodes.Add(new ExTreeNode(Path.GetFileName(dlg.FileName), newfile, 1, 1,
                        FolderNodeType.File));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Project Manager", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            DeleteFile();
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node
                projtree.SelectedNode = projtree.GetNodeAt(e.X, e.Y);

                if (projtree.SelectedNode != null)
                {
                    var node = projtree.SelectedNode as ExTreeNode;
                    if (node.Type == FolderNodeType.File)
                        fileMenu.Show(projtree, e.Location);
                    else if (node.Type == FolderNodeType.Folder)
                        folderMenu.Show(projtree, e.Location);
                }
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            var node = projtree.SelectedNode;
            var directory = Path.GetDirectoryName(node.Name);
            Process.Start(directory);
        }

        #endregion
    }
}