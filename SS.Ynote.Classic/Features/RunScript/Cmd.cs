#region

using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

#endregion

namespace SS.Ynote.Classic.Features.RunScript
{
    public partial class Cmd : DockContent
    {
        public Cmd(string proc, string args)
        {
            InitializeComponent();
            consoleControl1.StartProcess(proc, args);
        }

        private void consoleControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}