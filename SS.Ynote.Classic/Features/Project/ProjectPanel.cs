#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SS.Ynote.Classic.Features.RunScript;
using WeifenLuo.WinFormsUI.Docking;

#endregion

namespace SS.Ynote.Classic.Features.Project
{
    public partial class ProjectPanel : DockContent, IProjectPanel
    {
        private readonly IList<YnoteProject> _openprojects;
        private readonly IYnote _ynote;

        public ProjectPanel(IYnote ynote)
        {
            InitializeComponent();
            _ynote = ynote;
            _openprojects = new List<YnoteProject>();
        }

        public void OpenProject(string filename)
        {
            var project = YnoteProject.Read(filename);
            var projectnode = new ExTreeNode(project.ProjectName, project.Folder, 2, 2, project, NodeType.Project)
            {
                ContextMenu = projMenu
            };
            if (!Directory.Exists(project.Folder))
                ListDirectory(projectnode, Path.GetDirectoryName(filename) + @"\" + project.Folder);
            else
                ListDirectory(projectnode, project.Folder);
            treeView1.Nodes.Add(projectnode);
            _openprojects.Add(project);
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wizard = new Wizard(this);
            wizard.ShowDialog();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Ynote Project Files(*.ynoteproj)|*.ynoteproj";
                dlg.ShowDialog();
                if (dlg.FileName == "") return;
                OpenProject(dlg.FileName);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rootnode = FindRootNode(treeView1.SelectedNode);
            _openprojects.Remove((YnoteProject) (rootnode.Tag));
            RefreshProjects();
        }

        private void RefreshProjects()
        {
            treeView1.Nodes.Clear();
            var projects = _openprojects.ToArray();
            _openprojects.Clear();
            foreach (var project in projects)
                OpenProject(project.ProjectFile);
        }

        private void ListDirectory(ExTreeNode iTreeNode, string path)
        {
            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new ExTreeNode(rootDirectory.Name, path, 0, 0, rootDirectory, NodeType.Folder)
            {
                ContextMenu = folderMenu
            };
#if DEBUG
            Debug.WriteLine("value of node is : " + node.Text + " : " + node.Name);
            Debug.WriteLine(iTreeNode.Text + " : " + iTreeNode.Name);
#endif
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo) currentNode.Tag;
                for (int i = 0; i < directoryInfo.GetDirectories().Length; i++)
                {
                    var directory = directoryInfo.GetDirectories()[i];
                    var childDirectoryNode = new ExTreeNode(directory.Name, directory.FullName, 0, 0, directory,
                        NodeType.Folder) {ContextMenu = folderMenu};
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
#if DEBUG
                    Debug.WriteLine(childDirectoryNode.Text + " : " + childDirectoryNode.Name);
#endif
                }
                for (int i = 0; i < directoryInfo.GetFiles().Length; i++)
                {
                    var file = directoryInfo.GetFiles()[i];
                    if (Path.GetExtension(file.FullName) != ".ynoteproj")
                        currentNode.Nodes.Add(new ExTreeNode(file.Name, file.FullName, 1, 1, null, NodeType.File)
                        {
                            ContextMenu = fileMenu
                        });
                }
            }

            iTreeNode.Nodes.Add(node);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node.Name;
            var n = e.Node as ExTreeNode;
            if (n.Type == NodeType.Folder ||
                n.Type == NodeType.Project) return;
            _ynote.OpenFile(node);
        }

        private bool IsDir(string input)
        {
            return (File.GetAttributes(input) & FileAttributes.Directory)
                   == FileAttributes.Directory;
        }

        private void buildProjectToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (buildProjectToolStripMenuItem.DropDownItems.Count != 0) return;
            foreach (var proj in _openprojects)
                buildProjectToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(proj.ProjectName, null,
                    build_click)
                {
                    Name = proj.BuildFile
                });
        }

        private void build_click(object sender, EventArgs e)
        {
            var buildfile = ((ToolStripMenuItem) (sender)).Name;
#if DEBUG
            Debug.WriteLine("Project RunScript : " + buildfile);
#endif
            var console = new Cmd("cmd.exe", "/k " + buildfile);
            console.Show(DockPanel, DockState.DockBottom);
        }

        private void AddNewFolder()
        {
            try
            {
                var path = treeView1.SelectedNode as ExTreeNode;
                if (path.Type == NodeType.Folder)
                {
                    var node = new ExTreeNode("untitled", null, 0, 0, null, NodeType.Folder) {ContextMenu = folderMenu};
                    treeView1.SelectedNode.Nodes.Add(node);
                    node.BeginEdit();
                    treeView1.AfterLabelEdit += (o, args) => Directory.CreateDirectory(path.Name + @"\" + node.Text);
                    node.Name = path + @"\" + node.Text;
                    //Directory.CreateDirectory(path + @"\" + utils.FileName);
                }
            }
            catch (Exception)
            {
            }
        }

        private TreeNode FindRootNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
                treeNode = treeNode.Parent;
            return treeNode;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshProjects();
        }

        private void AddNewFile()
        {
            try
            {
                var path = treeView1.SelectedNode as ExTreeNode;
                if (path.Type == NodeType.Folder)
                {
                    var node = new ExTreeNode("untitled.txt", "", 1, 1, null, NodeType.File) {ContextMenu = fileMenu};
                    treeView1.SelectedNode.Nodes.Add(node);
                    node.BeginEdit();
                    treeView1.AfterLabelEdit += (o, args) => File.WriteAllText(path + @"\" + node.Text, "");
                    node.Name = path + @"\" + node.Text;
                    //Directory.CreateDirectory(path + @"\" + utils.FileName);
                }
            }
            catch (Exception)
            {
                // pass;
            }
        }

        private void DeleteFile()
        {
            var node = treeView1.SelectedNode;
            var result = MessageBox.Show("Are you sure you want to delete " + node.Name + " ?", "Project",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (IsDir(node.Name) && result == DialogResult.Yes)
            {
                Directory.Delete(node.Name);
                treeView1.Nodes.Remove(node);
            }
            else if (!IsDir(node.Name) && result == DialogResult.Yes)
            {
                File.Delete(node.Name);
                treeView1.Nodes.Remove(node);
            }
        }

        private static void RenameDirectory(string path, string newpath, ExTreeNode node)
        {
            Directory.Move(path, newpath);
            node.Name = newpath;
        }

        private static void RenameFile(string path, string newpath, ExTreeNode node)
        {
            File.Move(path, newpath);
            node.Name = newpath;
        }

        private void openExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(treeView1.SelectedNode.Name);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode as ExTreeNode;
            if (node.Type != NodeType.Project) return;
            var buildfile = (node.Tag as YnoteProject).BuildFile;
#if DEBUG
            Debug.WriteLine("Project RunScript : " + buildfile);
#endif
            var console = new Cmd("cmd.exe", "/k " + buildfile);
            console.Show(DockPanel, DockState.DockBottom);
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            RefreshProjects();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            var proj = treeView1.SelectedNode.Tag as YnoteProject;
            var result = MessageBox.Show("Are you sure you want to delete the project ?", "Project Manager",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (proj != null && result == DialogResult.Yes)
            {
                Directory.Delete(proj.Folder);
                File.Delete(proj.Folder);
            }
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
            var node = treeView1.SelectedNode as ExTreeNode;
            var filename = treeView1.SelectedNode.Name;
            var dir = Path.GetDirectoryName(treeView1.SelectedNode.Name);
            node.BeginEdit();
            if (node.Type == NodeType.Folder)
                treeView1.AfterLabelEdit += (o, args) => RenameDirectory(filename, dir + @"\" + node.Text, node);
            else if (node.Type == NodeType.File)
                treeView1.AfterLabelEdit +=
                    (o, args) => RenameFile(filename, dir + @"\" + node.Text, node);
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode as ExTreeNode;
            var filename = treeView1.SelectedNode.Name;
            var dir = Path.GetDirectoryName(treeView1.SelectedNode.Name);
            node.BeginEdit();
            if (node.Type == NodeType.Folder)
                treeView1.AfterLabelEdit += (o, args) => RenameDirectory(filename, dir + @"\" + node.Text, node);
            else if (node.Type == NodeType.File)
                treeView1.AfterLabelEdit +=
                    (o, args) => RenameFile(filename, dir + @"\" + node.Text, node);
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null) return;
            var folder = treeView1.SelectedNode.Name;
            Process.Start(folder);
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            var selected = treeView1.SelectedNode as ExTreeNode;
            if (selected == null) return;
            var path = selected.Name;
            var fileName = Path.ChangeExtension(path, "") + "-Copy" + Path.GetExtension(path);
            File.Copy(path, fileName);
            var parent = selected.Parent as ExTreeNode;
            if (parent != null && parent.Type == NodeType.Folder)
                parent.Nodes.Add(new ExTreeNode(Path.GetFileName(fileName), fileName, 1, 1, null, NodeType.File)
                {
                    ContextMenu = fileMenu
                });
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            _ynote.OpenFile(treeView1.SelectedNode.Name);
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "All Files (*.*)|*.*";
                dlg.ShowDialog();
                if (dlg.FileName == null) return;
                var selected = treeView1.SelectedNode as ExTreeNode;
                if (selected == null) return;
                var path = selected.Name;
                var fileName = dlg.FileName;
                var parent = selected.Parent as ExTreeNode;
                if (parent != null && parent.Type == NodeType.Folder)
                {
                    File.Copy(fileName, parent.Name + Path.GetFileName(fileName));
                    parent.Nodes.Add(new ExTreeNode(Path.GetFileName(fileName), fileName, 1, 1, null, NodeType.File)
                    {
                        ContextMenu = fileMenu
                    });
                }
            }
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            using (var browser = new FolderBrowserDialog())
            {
                browser.ShowDialog();
                var node = treeView1.SelectedNode as ExTreeNode;
                if (node.Type == NodeType.Folder && browser.SelectedPath != null)
                    CopyDirectory(browser.SelectedPath, node.Name + "\\" + Path.GetDirectoryName(browser.SelectedPath));
            }
        }

        private void CopyDirectory(string strSource, string strDestination)
        {
            if (!Directory.Exists(strDestination))
            {
                Directory.CreateDirectory(strDestination);
            }
            var dirInfo = new DirectoryInfo(strSource);
            var files = dirInfo.GetFiles();
            foreach (FileInfo tempfile in files)
            {
                tempfile.CopyTo(Path.Combine(strDestination, tempfile.Name));
            }
            var dirctororys = dirInfo.GetDirectories();
            foreach (DirectoryInfo tempdir in dirctororys)
                CopyDirectory(Path.Combine(strSource, tempdir.Name), Path.Combine(strDestination, tempdir.Name));
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode as ExTreeNode;
            if (node != null) Directory.Delete(node.Name);
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }
    }

    public enum NodeType
    {
        Project,
        Folder,
        File
    }

    /// <summary>
    ///     Extended TreeNode for some properties
    /// </summary>
    public class ExTreeNode : TreeNode
    {
        public ExTreeNode(string text, string name,
            int imageIndex, int selimageindex,
            object tag, NodeType nodeType)
        {
            Text = text;
            Tag = tag;
            Name = name;
            Type = nodeType;
            ImageIndex = imageIndex;
            SelectedImageIndex = selimageindex;
        }

        public NodeType Type { get; set; }
    }
    public class NativeTreeView : TreeView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                                string pszSubIdList);

        protected override void CreateHandle()
        {
            base.CreateHandle();

            SetWindowTheme(this.Handle, "explorer", null);
        }
    }
}