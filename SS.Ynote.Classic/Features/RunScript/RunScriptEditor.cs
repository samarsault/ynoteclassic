#region

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.Features.RunScript
{
    public partial class RunScriptEditor : Form
    {
        public RunScriptEditor()
        {
            InitializeComponent();
            PopulateTree();
        }

        private void PopulateTree()
        {
            var node = new TreeNode("Configurations");
            foreach (var file in RunConfiguration.GetConfigurations())
            {
                var config = RunConfiguration.ToRunConfig(file);
                var tn = new TreeNode(config.Name) { Tag = config };
                node.Nodes.Add(tn);
            }
            configTree.Nodes.Add(node);
            configTree.ExpandAll();
        }

        private void configTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                var selectedconfig = e.Node.Tag as RunConfiguration;
                if (selectedconfig == null) return;
                tbName.Text = selectedconfig.Name;
                tbName.Text = e.Node.Text;
                tbArgs.Text = selectedconfig.Arguments;
                tbCmdDir.Text = selectedconfig.CmdDir;
                tbProcess.Text = selectedconfig.Process;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error processing your request\r\nReport : " + ex, "Ynote Classic",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sNode = configTree.SelectedNode.Tag as RunConfiguration;
            if (sNode != null) sNode.EditConfig(tbProcess.Text, tbArgs.Text, tbCmdDir.Text, tbName.Text);
            else
                MessageBox.Show("Error Processing Request : Nothing Selected", "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var item = configTree.SelectedNode;
            var tag = item.Tag as RunConfiguration;
            if (tag != null) File.Delete(tag.GetPath());
#if DEBUG
            Debug.WriteLine(tag.GetPath());
#endif
            configTree.Nodes.Remove(item);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var nd = new TreeNode("untitled") { Tag = new RunConfiguration() };
            configTree.Nodes[0].Nodes.Add(nd);
            nd.BeginEdit();
            configTree.SelectedNode = nd;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            configTree.Nodes.Clear();
            PopulateTree();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Executables (*.exe), (*.bat), (*.cmd)|*.exe;*.bat;*.cmd";
                dlg.ShowDialog();
                if (dlg.FileName != null) tbProcess.Text = dlg.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}