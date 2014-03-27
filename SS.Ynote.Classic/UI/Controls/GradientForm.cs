#region

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.UI
{
    public class GradientForm : Form
    {
        #region Private Variables

        private Color _color1 = Color.Gainsboro;
        private Color _color2 = Color.White;
        private float _colorAngle = 30f;

        #region Gui

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container components = null;

        #endregion

        #endregion

        #region Public Properties

        protected Color Color1
        {
            get { return _color1; }
            set
            {
                _color1 = value;
                Invalidate(); // Tell the Form to repaint itself
            }
        }

        public Color Color2
        {
            get { return _color2; }
            set
            {
                _color2 = value;
                Invalidate(); // Tell the Form to repaint itself
            }
        }

        public float ColorAngle
        {
            get { return _colorAngle; }
            set
            {
                _colorAngle = value;
                Invalidate(); // Tell the Form to repaint itself
            }
        }

        #endregion

        #region Constructors, Destructors

        protected GradientForm()
        {
            InitializeComponent();

            SetStyles();
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //
            // BaseFormGradient
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(432, 198);
            this.Name = "BaseFormGradient";
            this.Text = "Form1";
        }

        #endregion

        #region Private Methods

        private void SetStyles()
        {
            // Makes sure the form repaints when it was resized
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        #endregion

        #region Overriden Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Creating the rectangle for the gradient
            var rect = new Rectangle(0, 0, Width, Height);

            // Creating the lineargradient
            var lgbrush
                = new LinearGradientBrush(rect, _color1, _color2, _colorAngle);

            // Draw the gradient onto the form
            e.Graphics.FillRectangle(lgbrush, rect);

            // Disposing of the resources held by the brush
            lgbrush.Dispose();
        }

        #endregion
    }
}