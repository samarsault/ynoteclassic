
using System.Windows.Forms;
using SS.Ynote.Classic.Properties;

namespace SS.Ynote.Classic.Project
{
    public partial class Wizard : Form
    {
        private IProject _project;
        public Wizard(IProject project)
        {
            InitializeComponent();
            _project = project;
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            var dialog = new SaveFileDialog() {Filter = "Ynote Project Files (*.ynoteproj)|*.ynoteproj"};
            dialog.ShowDialog();
            if (dialog.FileName == "") return;
            txtfilename.Text = dialog.FileName;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = "Batch Files (*.bat)(*.cmd)|*.bat;*.cmd|Executables (*.exe)|*.exe" };
            dialog.ShowDialog();
            if (dialog.FileName == "") return;
            txtbuild.Text = dialog.FileName;
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtbuild.Enabled = true;
                label2.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                txtbuild.Enabled = false;
                label2.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void BuildProject()
        {
            var proj = new YnoteProject
            {
                Folder = txtfolder.Text,
                ProjectName = txtprojname.Text,
                ProjectFile = txtfilename.Text
            };
            if(checkBox1.Checked)
                proj.BuildFile = txtbuild.Text;
            proj.MakeProjectFile(proj.ProjectFile);
            _project.OpenProject(txtfilename.Text);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            BuildProject();
            Close();
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            var browser = new FolderBrowserDialog();
            browser.ShowDialog();
            if (browser.SelectedPath == null) return;
            txtfolder.Text = browser.SelectedPath;
        }
    }
}
