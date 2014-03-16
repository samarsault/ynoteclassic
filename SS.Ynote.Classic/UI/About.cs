#region

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.UI
{
    public partial class About : BaseFormGradient
    {
        public About()
        {
            InitializeComponent();
            LostFocus += About_LostFocus;
            if (File.Exists(Application.StartupPath + @"\License.txt"))
                textBox1.Text = File.ReadAllText(Application.StartupPath + @"\License.txt");
        }

        private void About_LostFocus(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://ynoteclassic.codeplex.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://fb.com/sscorpscom");
        }
    }
}