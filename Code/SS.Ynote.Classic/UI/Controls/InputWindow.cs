using System;
using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.UI
{
    public partial class InputWindow : UserControl
    {
        public delegate void GotInputEventHandler(object sender, InputEventArgs e);
        /// <summary>
        /// Occurs when user has got an input
        /// </summary>
        public event GotInputEventHandler GotInput;
        /// <summary>
        /// Read Only Style
        /// </summary>
        private readonly Style ReadOnlyStyle;
        /// <summary>
        /// Style of the label
        /// </summary>
        private readonly Style LabelStyle;
        /// <summary>
        /// The value of the input
        /// </summary>
        public string InputValue
        {
            get { return tbInput.Text; }
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InputWindow()
        {
            InitializeComponent();
            tbInput.KeyDown += tbInput_KeyDown;
            LabelStyle=new TextStyle(Brushes.Blue,null,FontStyle.Regular);
            ReadOnlyStyle = new ReadOnlyStyle();
            tbInput.WideCaret = true;
        }

        void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnInputEntered(new InputEventArgs(InputValue));
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
        public void InitInput(string text, GotInputEventHandler handler)
        {
            var splits = tbInput.Text.Split(':');
            if (splits[0] + ":" == text)
            {
                if (splits[1] == string.Empty) 
                    return;
                tbInput.GoEnd();
                while (tbInput.Selection.CharBeforeStart != ':') 
                    tbInput.Selection.GoLeft(true);
                return;
            }
            tbInput.Text = text;
            tbInput.GoEnd();
            tbInput.Range.ClearStyle();
            tbInput.Range.ClearStyle(ReadOnlyStyle,LabelStyle);
            tbInput.Range.SetStyle(ReadOnlyStyle, @"\b.*:");
            tbInput.Range.SetStyle(LabelStyle, @"\b.*:");
            this.GotInput = null;
            GotInput = handler;
        }
        public virtual void OnInputEntered(InputEventArgs e)
        {
            var handler = GotInput;
            if(handler != null)
                handler(this,e);
        }

        public void Focus()
        {
            tbInput.Focus();
        }
    }
    public class InputEventArgs : EventArgs
    {
        /// <summary>
        /// The vale of the Input
        /// </summary>
        public string InputValue;
        /// <summary>
        /// Gets a formatted input seperated by :
        /// </summary>
        /// <returns></returns>
        public string GetFormattedInput()
        {
            return InputValue.Split(':')[1];
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InputEventArgs(string val)
        {
            this.InputValue = val;
        }
    }
}
