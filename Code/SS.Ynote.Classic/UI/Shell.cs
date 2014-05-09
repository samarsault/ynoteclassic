using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.Features.RunScript
{
    public partial class Shell : DockContent
    {
        #region WinAPI

        [DllImport("user32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        

        #endregion
        public Shell(string proc, string args)
        {
            InitializeComponent();
            shellControl.StartProcess(proc, args);
            shellControl.InternalRichTextBox.TextChanged += (sender, eventArgs) => CreateBlockCaret();
        }

        void CreateBlockCaret()
        {
            var size = Convert.ToInt32(shellControl.InternalRichTextBox.Font.Size) + 2;
            CreateCaret(shellControl.InternalRichTextBox.Handle, IntPtr.Zero, 10, size);
            ShowCaret(shellControl.InternalRichTextBox.Handle);
        }
        protected override void OnResize(EventArgs e)
        {
            CreateBlockCaret();
            base.OnResize(e);
        }
        protected override void OnShown(EventArgs e)
        {
            CreateBlockCaret();
            base.OnShown(e);
        }
        private void consoleControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}