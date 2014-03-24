using System;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Packages
{
    public partial class InputUrl : Form
    {
        public InputUrl()
        {
            InitializeComponent();
        }

        public YnoteOnlinePackage GeneratedPackage { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var package = new YnoteOnlinePackage {PackageUrl = textBox1.Text};
            GeneratedPackage = package;
            Close();
        }
    }
}