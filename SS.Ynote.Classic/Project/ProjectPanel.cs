
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SS.Ynote.Classic.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.Project
{
    public partial class ProjectPanel : DockContent, IProject
    {
        private readonly List<YnoteProject> _openprojects;
        private readonly IYnote _ynote;

        public ProjectPanel(IYnote ynote)
        {
            InitializeComponent();
            _ynote = ynote;
            _openprojects = new List<YnoteProject>();
        }

         private void newProjectToolStripMenuItem_Click(object sender, System.EventArgs e)
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

        public void OpenProject(string filename)
        {
            var project = new YnoteProject(filename);
            var projectnode = new TreeNode(project.ProjectName, 2, 2) {Tag = project, Name = project.Folder};
            if (!Directory.Exists(project.Folder))
                ListDirectory(projectnode, Path.GetDirectoryName(filename) + @"\" + project.Folder);
            else
                ListDirectory(projectnode, project.Folder);
            treeView1.Nodes.Add(projectnode);
            _openprojects.Add(project);
        }
         private void RefreshProjects()
         {
             treeView1.Nodes.Clear();
             var projects = _openprojects.ToArray();
             _openprojects.Clear();
             foreach (var project in projects)
                 OpenProject(project.ProjectFile);
         }
         private static void ListDirectory(TreeNode iTreeNode, string path)
         {
             var stack = new Stack<TreeNode>();
             var rootDirectory = new DirectoryInfo(path);
             var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory, Name = path };
             stack.Push(node);

             while (stack.Count > 0)
             {
                 var currentNode = stack.Pop();
                 var directoryInfo = (DirectoryInfo)currentNode.Tag;
                 for (int i = 0; i < directoryInfo.GetDirectories().Length; i++)
                 {
                     var directory = directoryInfo.GetDirectories()[i];
                     var childDirectoryNode = new TreeNode(directory.Name, 0,0) {Tag = directory, Name = directory.FullName};
                     currentNode.Nodes.Add(childDirectoryNode);
                     stack.Push(childDirectoryNode);
                 }
                 for (int i = 0; i < directoryInfo.GetFiles().Length; i++)
                 {
                     var file = directoryInfo.GetFiles()[i];
                     if (Path.GetExtension(file.FullName) != ".ynoteproj")
                         currentNode.Nodes.Add(new TreeNode(file.Name, 1, 1) {Name = file.FullName});
                 }
             }

             iTreeNode.Nodes.Add(node);
         }

         private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
         {
             var node = e.Node.Name;
             if (IsDir(node)) return;
             _ynote.OpenFile(node);
         }

        bool IsDir(string input)
        {
            return (File.GetAttributes(input) & FileAttributes.Directory)
                 == FileAttributes.Directory;
        }

        private void buildProjectToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (buildProjectToolStripMenuItem.DropDownItems.Count != 0) return;
            foreach (var proj in _openprojects)
                buildProjectToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(proj.ProjectName, null,build_click)
                {
                    Name = proj.BuildFile
                });
                
        }

        private void build_click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            var cmd = new Cmd(item.Name);
            cmd.Show(DockPanel, DockState.DockBottom);
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = treeView1.SelectedNode.Name;
            if (IsDir(path))
            {
                var utils = new ProjectUtils(InType.NewFolder, null);
                utils.ShowDialog(this);
                if (utils.FileName == "") return;
                Directory.CreateDirectory(path + @"\" + utils.FileName);
            }
            RefreshProjects();
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

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var savefiledialog = new SaveFileDialog())
            {
                if (IsDir(treeView1.SelectedNode.Name))
                    savefiledialog.InitialDirectory = treeView1.SelectedNode.Name;
                savefiledialog.Filter = "All Files(*.*)|*.*";
                savefiledialog.ShowDialog();
                if (savefiledialog.FileName == "") return;
                File.WriteAllText(savefiledialog.FileName, string.Empty);
                RefreshProjects();
            }
        }

        private void createBuildFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var savefiledialog = new SaveFileDialog())
            {
                if (IsDir(treeView1.SelectedNode.Name))
                    savefiledialog.InitialDirectory = treeView1.SelectedNode.Name;
                savefiledialog.Filter = "Build Files (*.bat)(*.cmd)|*.bat;*.cmd|Executables (*.exe)|*.exe";
                savefiledialog.ShowDialog();
                if (savefiledialog.FileName == "") return;
                File.WriteAllText(savefiledialog.FileName, string.Empty);
                var proj = (YnoteProject) (FindRootNode(treeView1.SelectedNode).Tag);
                proj.BuildFile = savefiledialog.FileName;
                proj.MakeProjectFile(proj.ProjectFile);
                RefreshProjects();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if(IsDir(node.Name))
                Directory.Delete(node.Name);
            else 
                File.Delete(node.Name);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var utildlg = new ProjectUtils(InType.Rename, treeView1.SelectedNode.Text);
            utildlg.ShowDialog();
            RefreshProjects();
        }

        private void openExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(treeView1.SelectedNode.Name);
        }
    }
}
