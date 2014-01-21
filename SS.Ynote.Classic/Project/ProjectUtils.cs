using System.IO;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Project
{
    internal enum InType
    {
        Rename,
        NewFolder
    }
    public partial class ProjectUtils : Form
    {
        public string FileName { get; private set; }
        private InType Intype { get; set; }

        internal ProjectUtils(InType intype, string renamefile)
        {
            InitializeComponent();
            if (intype == InType.NewFolder)
            {
                textBox2.Visible = false;
                label2.Visible = false;
            }
            else if (intype == InType.Rename)
            {
                Text = "Rename";
                textBox1.Text = renamefile;
            }
            Intype = intype;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if(Intype == InType.NewFolder)
                FileName = textBox1.Text;
            else if (Intype == InType.Rename)
            {
                if(IsDir(textBox1.Text))
                    Directory.Move(textBox1.Text, Path.GetDirectoryName(textBox1.Text) + "\\"+textBox2.Text);
                else
                    File.Move(textBox1.Text, Path.GetDirectoryName(textBox1.Text) + "\\" +textBox1.Text);
            }
        }
        bool IsDir(string input)
        {
            return (File.GetAttributes(input) & FileAttributes.Directory)
                 == FileAttributes.Directory;
        }
    }
}
