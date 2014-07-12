using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class BuildOutput : DockContent
    {
        public BuildOutput()
        {
            InitializeComponent();
        }

        public void AddOutput(string s)
        {
            this.tbout.Text = s;
        }

        public void GoToEnd()
        {
            this.tbout.SelectionStart = this.tbout.Text.Length;
            this.tbout.ScrollToCaret();
        }
    }
}
