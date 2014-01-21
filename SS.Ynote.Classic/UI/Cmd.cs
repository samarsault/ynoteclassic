using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Cmd : DockContent
    {
        public Cmd()
        {
            InitializeComponent();
            consoleControl1.StartProcess("cmd.exe", string.Empty);
        }

        public Cmd(string batch)
        {
            InitializeComponent();
            consoleControl1.StartProcess("cmd.exe", "/K " + batch);
        }
    }
}
