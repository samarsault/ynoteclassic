#region

using System;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.Features.Project
{
    public partial class Wizard : Form
    {
        private readonly IProjectPanel _project;

        public Wizard(IProjectPanel project)
        {
            InitializeComponent();
            _project = project;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog {Filter = "Ynote Project Files (*.ynoteproj)|*.ynoteproj"};
            dialog.ShowDialog();
            if (dialog.FileName == "") return;
            txtfilename.Text = dialog.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Batch Files (*.bat)(*.cmd)|*.bat;*.cmd|Executables (*.exe)|*.exe"
            };
            dialog.ShowDialog();
            if (dialog.FileName == "") return;
            txtbuild.Text = dialog.FileName;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtbuild.Enabled = checkBox1.Checked;
            label2.Enabled = checkBox1.Checked;
            button2.Enabled = checkBox1.Checked;
        }

        private void BuildProject()
        {
            var proj = new YnoteProject
            {
                Folder = txtfolder.Text,
                ProjectName = txtprojname.Text,
                ProjectFile = txtfilename.Text,
                ProjectType = "Custom"
            };
            if (checkBox1.Checked)
                proj.BuildFile = txtbuild.Text;
            proj.MakeProjectFile(proj.ProjectFile);
            _project.OpenProject(txtfilename.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuildProject();
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var browser = new FolderBrowserDialog();
            browser.ShowDialog();
            if (browser.SelectedPath == null) return;
            txtfolder.Text = browser.SelectedPath;
        }
    }
}