using System;
using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.UI
{
    public partial class InputWindow : UserControl
    {
        private readonly Style LabelStyle;
        /// <summary>
        /// The value of the input
        /// </summary>
        public string InputValue
        {
            get { return tbInput.Text; }
        }
        /// <summary>
        /// The label text
        /// </summary>
        /// <summary>
        /// Occurs when the user presses enter
        /// </summary>
        public event EventHandler InputEntered;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InputWindow()
        {
            InitializeComponent();
            tbInput.KeyDown += tbInput_KeyDown;
            LabelStyle=new TextStyle(Brushes.Blue,null,FontStyle.Bold);
            tbInput.WideCaret = true;
        }

        void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnInputEntered(EventArgs.Empty);
                Hide();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }
        }
        /// <summary>
        /// Add a caption
        /// </summary>
        /// <param name="text"></param>
        public void AddCaptionBlock(string text)
        {
            var splits = tbInput.Text.Split(':');
            if (splits[0] + ":" == text)
            {
                if (splits[1] == string.Empty) return; else { tbInput.GoEnd();
                    while (tbInput.Selection.CharBeforeStart != ':') tbInput.Selection.GoLeft(true);
                    return;
                }
            }
            tbInput.Text = text;
            tbInput.GoEnd();
            tbInput.Range.SetStyle(new ReadOnlyStyle(), @"\b.*:");tbInput.Range.SetStyle(LabelStyle, @"\b.*:");
        }
        public virtual void OnInputEntered(EventArgs e)
        {
            var handler = InputEntered;
            if(handler != null)
                handler(this,e);
        }

        public void Focus()
        {
            tbInput.Focus();
        }
    }
}
