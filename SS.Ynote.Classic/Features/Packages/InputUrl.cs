using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Packages
{
    public partial class InputUrl : Form
    {
        public YnoteOnlinePackage GeneratedPackage { get; set; }
        public InputUrl()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var package = new YnoteOnlinePackage() {PackageUrl = textBox1.Text};
            GeneratedPackage = package;
            Close();
        }
    }
}
