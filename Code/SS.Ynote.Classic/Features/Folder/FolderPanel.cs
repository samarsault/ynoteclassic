using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.Features.Project
{
    public partial class FolderPanel : DockContent
    {
        #region Private Variables

        /// <summary>
        ///     Gets the List of Open Projects
        /// </summary>
        private readonly IList<string> _openFolders;

        /// <summary>
        ///     Ynote Reference to the Object
        /// </summary>
        private readonly IYnote _ynote;

        private readonly string folders = Settings.SettingsDir + "Folders.ynote";

        #endregion Private Variables

        #region Constructor

        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="ynote">Reference to Ynote</param>
        public FolderPanel(IYnote ynote)
        {
            InitializeComponent();
            _ynote = ynote;
            _openFolders = new List<string>();
            if (File.Exists(folders))
            {
                foreach (var file in File.ReadAllLines(folders))
                    _openFolders.Add(file);
            }
        }

        #endregion Constructor

        #region Methods

        private void OpenFolders()
        {
            if (_openFolders == null)
                return;
            foreach (var folder in _openFolders)
            {
                if (!Directory.Exists(folder) ||
                    string.IsNullOrEmpty(folder)) return;
                OpenFolder(folder);
            }
        }

        /// <summary>
        ///     Opens a project file in the Project Explorer
        /// </summary>
        /// <param name="folder"></param>
        public void OpenFolder(string folder)
        {
            // initialize the node
            var directory = new DirectoryInfo(folder);
            if (!directory.Exists)
                MessageBox.Show(string.Format("Error : Can't find directory : {0}", folder), "Folder Manager",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                BeginInvoke((MethodInvoker)(() => projtree.Nodes.Add(ListDirectory(directory))));
            if (!_openFolders.Contains(folder))
                _openFolders.Add(folder);
        }

        /// <summary>
        ///     Lists a Directory in Project Explorer
        /// </summary>
        /// <param name="path"></param>
        private static ExTreeNode ListDirectory(DirectoryInfo rootDirectory)
        {
            var stack = new Stack<TreeNode>();
            var node = new ExTreeNode(rootDirectory.Name, rootDirectory.FullName, 0, 0, rootDirectory,
                FolderNodeType.Folder);
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                for (var i = 0; i < directoryInfo.GetDirectories().Length; i++)
                {
                    var directory = directoryInfo.GetDirectories()[i];
                    var childDirectoryNode = new ExTreeNode(directory.Name, directory.FullName, 0, 0, directory,
                        FolderNodeType.Folder);
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                for (var i = 0; i < directoryInfo.GetFiles().Length; i++)
                {
                    var file = directoryInfo.GetFiles()[i];
                    if (Path.GetExtension(file.FullName) != ".ynoteproj")
                        currentNode.Nodes.Add(new ExTreeNode(file.Name, file.FullName, 1, 1, null, FolderNodeType.File));
                }
            }

            return node;
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
                    var node = new ExTreeNode(util.FileName, dir, 0, 0, null, FolderNodeType.Folder);
                    Directory.CreateDirectory(dir);
                    projtree.SelectedNode.Nodes.Add(node);
                }
            }
        }

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
                        var node = new ExTreeNode(util.FileName, file, 1, 1, null, FolderNodeType.File);
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
                parent.Nodes.Add(new ExTreeNode(Path.GetFileName(fileName), fileName, 1, 1, null, FolderNodeType.File));
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
                    selectedNode.Nodes.Add(new ExTreeNode(Path.GetFileName(dlg.FileName), newfile, 1, 1, null,
                        FolderNodeType.File));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Project Manager", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            using (var browser = new FolderBrowserDialog())
            {
                browser.ShowDialog();
                var node = projtree.SelectedNode as ExTreeNode;
                if (node.Type == FolderNodeType.Folder && browser.SelectedPath != null)
                {
                    CopyDirectory(browser.SelectedPath,
                        node.Name + "\\" + Path.GetFileName(Path.GetDirectoryName(browser.SelectedPath)));
                    node.Nodes.Add(ListDirectory(new DirectoryInfo(browser.SelectedPath)));
                    // var files = Directory.GetFiles(browser.SelectedPath);
                    // var folderNode = new ExTreeNode(Path.GetFileName(browser.SelectedPath),
                    //     browser.SelectedPath, 0, 0, null, FolderNodeType.Folder);
                    // foreach (var file in files)
                    //     folderNode.Nodes.Add(new ExTreeNode(Path.GetFileName(file),
                    //         file, 1, 1, null, FolderNodeType.File));
                    // node.Nodes.Add(folderNode);
                }
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

        protected override void OnLoad(EventArgs e)
        {
            OpenFolders();
            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            var openfolders = _openFolders.ToArray();
            if (openfolders != null)
                File.WriteAllLines(folders, openfolders);
            base.OnClosed(e);
        }

        #endregion Events
    }
}