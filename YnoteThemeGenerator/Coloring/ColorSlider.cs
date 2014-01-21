using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
#if USEEXTERNALCYOTEKLIBS
using Cyotek.Win32;
#else
using NativeConstants = Cyotek.Windows.Forms.NativeMethods;

#endif

namespace Cyotek.Windows.Forms
{
    // Cyotek Color Picker controls library
    // Copyright © 2013 Cyotek. All Rights Reserved.
    // http://cyotek.com/blog/tag/colorpicker

    // If you use this code in your applications, donations or attribution are welcome

    /// <summary>
    ///     Represents a control for selecting a value from a scale
    /// </summary>
    [DefaultValue("Value")]
    [DefaultEvent("ValueChanged")]
    [ToolboxItem(false)]
    public class ColorSlider : Control
    {
        #region Instance Fields

        private Rectangle _barBounds;

        private Padding _barPadding;

        private ColorBarStyle _barStyle;

        private Color _color1;

        private Color _color2;

        private Color _color3;

        private ColorCollection _customColors;

        private int _largeChange;

        private float _maximum;

        private float _minimum;

        private Color _nubColor;

        private Size _nubSize;

        private ColorSliderNubStyle _nubStyle;

        private Orientation _orientation;

        private bool _showValueDivider;

        private int _smallChange;

        private float _value;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorSlider" /> class.
        /// </summary>
        public ColorSlider()
        {
            SetStyle(
                ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
            Orientation = Orientation.Horizontal;
            Color1 = Color.Black;
            Color2 = Color.FromArgb(127, 127, 127);
            Color3 = Color.White;
            Minimum = 0;
            Maximum = 100;
            NubStyle = ColorSliderNubStyle.BottomRight;
            NubSize = new Size(8, 8);
            NubColor = Color.Black;
            SmallChange = 1;
            LargeChange = 10;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the BarBounds property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler BarBoundsChanged;

        /// <summary>
        ///     Occurs when the BarPadding property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler BarPaddingChanged;

        /// <summary>
        ///     Occurs when the Style property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler BarStyleChanged;

        /// <summary>
        ///     Occurs when the Color1 property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler Color1Changed;

        /// <summary>
        ///     Occurs when the Color2 property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler Color2Changed;

        /// <summary>
        ///     Occurs when the Color3 property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler Color3Changed;

        /// <summary>
        ///     Occurs when the CustomColors property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler CustomColorsChanged;

        /// <summary>
        ///     Occurs when the LargeChange property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler LargeChangeChanged;

        /// <summary>
        ///     Occurs when the Maximum property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler MaximumChanged;

        /// <summary>
        ///     Occurs when the Minimum property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler MinimumChanged;

        /// <summary>
        ///     Occurs when the NubColor property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler NubColorChanged;

        /// <summary>
        ///     Occurs when the NubSize property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler NubSizeChanged;

        /// <summary>
        ///     Occurs when the NubStyle property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler NubStyleChanged;

        /// <summary>
        ///     Occurs when the Orientation property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler OrientationChanged;

        /// <summary>
        ///     Occurs when the ShowValueDivider property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ShowValueDividerChanged;

        /// <summary>
        ///     Occurs when the SliderStyle property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler SliderStyleChanged;

        /// <summary>
        ///     Occurs when the SmallChange property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler SmallChangeChanged;

        /// <summary>
        ///     Occurs when the Percent property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ValueChanged;

        #endregion

        #region Overridden Properties

        /// <summary>
        ///     Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        /// <returns>
        ///     The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the
        ///     value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.
        /// </returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        ///     Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        /// <returns>
        ///     The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the
        ///     <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.
        /// </returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        ///     Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        /// <returns>The text associated with this control.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion

        #region Overridden Members

        /// <summary>
        ///     Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls
        ///     and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && SelectionGlyph != null)
                SelectionGlyph.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        ///     Determines whether the specified key is a regular input key or a special key that requires preprocessing.
        /// </summary>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
        /// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
        protected override bool IsInputKey(Keys keyData)
        {
            bool result;

            if ((keyData & Keys.Left) == Keys.Left || (keyData & Keys.Up) == Keys.Up ||
                (keyData & Keys.Down) == Keys.Down || (keyData & Keys.Right) == Keys.Right ||
                (keyData & Keys.PageUp) == Keys.PageUp || (keyData & Keys.PageDown) == Keys.PageDown ||
                (keyData & Keys.Home) == Keys.Home || (keyData & Keys.End) == Keys.End)
                result = true;
            else
                result = base.IsInputKey(keyData);

            return result;
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            int step;
            float value;

            step = e.Shift ? LargeChange : SmallChange;
            value = Value;

            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Down:
                    value += step;
                    break;
                case Keys.Left:
                case Keys.Up:
                    value -= step;
                    break;
                case Keys.PageDown:
                    value += LargeChange;
                    break;
                case Keys.PageUp:
                    value -= LargeChange;
                    break;
                case Keys.Home:
                    value = Minimum;
                    break;
                case Keys.End:
                    value = Maximum;
                    break;
            }

            if (value < Minimum)
                value = Minimum;

            if (value > Maximum)
                value = Maximum;

            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (value != Value)
                // ReSharper restore CompareOfFloatsByEqualityOperator
            {
                Value = value;

                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            Invalidate();
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!Focused && TabStop)
                Focus();

            if (e.Button == MouseButtons.Left)
                PointToValue(e.Location);
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
                PointToValue(e.Location);
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);

            DefineBar();
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            PaintBar(e);
            PaintAdornments(e);
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            DefineBar();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the location and size of the color bar.
        /// </summary>
        /// <value>The location and size of the color bar.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Rectangle BarBounds
        {
            get { return _barBounds; }
            protected set
            {
                if (BarBounds != value)
                {
                    _barBounds = value;

                    OnBarBoundsChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the bar padding.
        /// </summary>
        /// <value>The bar padding.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Padding BarPadding
        {
            get { return _barPadding; }
            protected set
            {
                if (BarPadding != value)
                {
                    _barPadding = value;

                    OnBarPaddingChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the bar style.
        /// </summary>
        /// <value>The bar style.</value>
        [Category("Appearance")]
        [DefaultValue(typeof (ColorBarStyle), "TwoColor")]
        public virtual ColorBarStyle BarStyle
        {
            get { return _barStyle; }
            set
            {
                if (BarStyle != value)
                {
                    _barStyle = value;

                    OnBarStyleChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the first color of the bar.
        /// </summary>
        /// <value>The first color.</value>
        /// <remarks>
        ///     This property is ignored if the <see cref="BarStyle" /> property is set to Custom and a valid color set has
        ///     been specified
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(typeof (Color), "Black")]
        public virtual Color Color1
        {
            get { return _color1; }
            set
            {
                if (Color1 != value)
                {
                    _color1 = value;

                    OnColor1Changed(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the second color of the bar.
        /// </summary>
        /// <value>The second color.</value>
        /// <remarks>
        ///     This property is ignored if the <see cref="BarStyle" /> property is set to Custom and a valid color set has
        ///     been specified
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(typeof (Color), "127, 127, 127")]
        public virtual Color Color2
        {
            get { return _color2; }
            set
            {
                if (Color2 != value)
                {
                    _color2 = value;

                    OnColor2Changed(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the third color of the bar.
        /// </summary>
        /// <value>The third color.</value>
        /// <remarks>
        ///     This property is ignored if the <see cref="BarStyle" /> property is set to Custom and a valid color set has
        ///     been specified, or if the BarStyle is set to TwoColor.
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(typeof (Color), "White")]
        public virtual Color Color3
        {
            get { return _color3; }
            set
            {
                if (Color3 != value)
                {
                    _color3 = value;

                    OnColor3Changed(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color range used by the custom bar style.
        /// </summary>
        /// <value>The custom colors.</value>
        /// <remarks>This property is ignored if the <see cref="BarStyle" /> property is not set to Custom</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ColorCollection CustomColors
        {
            get { return _customColors; }
            set
            {
                if (CustomColors != value)
                {
                    _customColors = value;

                    OnCustomColorsChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value to be added to or subtracted from the <see cref="Value" /> property when the selection is
        ///     moved a large distance.
        /// </summary>
        /// <value>A numeric value. The default value is 10.</value>
        [Category("Behavior")]
        [DefaultValue(10)]
        public virtual int LargeChange
        {
            get { return _largeChange; }
            set
            {
                if (LargeChange != value)
                {
                    _largeChange = value;

                    OnLargeChangeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the upper limit of values of the selection range.
        /// </summary>
        /// <value>A numeric value. The default value is 100.</value>
        [Category("Behavior")]
        [DefaultValue(100F)]
        public virtual float Maximum
        {
            get { return _maximum; }
            set
            {
                // ReSharper disable CompareOfFloatsByEqualityOperator
                if (Maximum != value)
                    // ReSharper restore CompareOfFloatsByEqualityOperator
                {
                    _maximum = value;

                    OnMaximumChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the lower limit of values of the selection range.
        /// </summary>
        /// <value>A numeric value. The default value is 0.</value>
        [Category("Behavior")]
        [DefaultValue(0F)]
        public virtual float Minimum
        {
            get { return _minimum; }
            set
            {
                // ReSharper disable CompareOfFloatsByEqualityOperator
                if (Minimum != value)
                    // ReSharper restore CompareOfFloatsByEqualityOperator
                {
                    _minimum = value;

                    OnMinimumChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the selection nub.
        /// </summary>
        /// <value>The color of the nub.</value>
        [Category("Appearance")]
        [DefaultValue(typeof (Color), "Black")]
        public virtual Color NubColor
        {
            get { return _nubColor; }
            set
            {
                if (NubColor != value)
                {
                    _nubColor = value;

                    OnNubColorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the size of the selection nub.
        /// </summary>
        /// <value>The size of the nub.</value>
        [Category("Appearance")]
        [DefaultValue(typeof (Size), "8, 8")]
        public virtual Size NubSize
        {
            get { return _nubSize; }
            set
            {
                if (NubSize != value)
                {
                    _nubSize = value;

                    OnNubSizeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the selection nub style.
        /// </summary>
        /// <value>The nub style.</value>
        [Category("Appearance")]
        [DefaultValue(typeof (ColorSliderNubStyle), "BottomRight")]
        public virtual ColorSliderNubStyle NubStyle
        {
            get { return _nubStyle; }
            set
            {
                if (NubStyle != value)
                {
                    _nubStyle = value;

                    OnNubStyleChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the orientation of the color bar.
        /// </summary>
        /// <value>The orientation.</value>
        [Category("Appearance")]
        [DefaultValue(typeof (Orientation), "Horizontal")]
        public virtual Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                if (Orientation != value)
                {
                    _orientation = value;

                    OnOrientationChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether a divider is shown at the selection nub location.
        /// </summary>
        /// <value><c>true</c> if a value divider is to be shown; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(false)]
        public virtual bool ShowValueDivider
        {
            get { return _showValueDivider; }
            set
            {
                if (ShowValueDivider != value)
                {
                    _showValueDivider = value;

                    OnShowValueDividerChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the value to be added to or subtracted from the <see cref="Value" /> property when the selection is
        ///     moved a small distance.
        /// </summary>
        /// <value>A numeric value. The default value is 1.</value>
        [Category("Behavior")]
        [DefaultValue(1)]
        public virtual int SmallChange
        {
            get { return _smallChange; }
            set
            {
                if (SmallChange != value)
                {
                    _smallChange = value;

                    OnSmallChangeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets a numeric value that represents the current position of the selection numb on the color slider
        ///     control.
        /// </summary>
        /// <value>
        ///     A numeric value that is within the <see cref="Minimum" /> and <see cref="Maximum" /> range. The default value is
        ///     0.
        /// </value>
        [Category("Appearance")]
        [DefaultValue(0F)]
        public virtual float Value
        {
            get { return _value; }
            set
            {
                if (value < Minimum)
                    value = Minimum;
                if (value > Maximum)
                    value = Maximum;

                // ReSharper disable CompareOfFloatsByEqualityOperator
                if (Value != value)
                    // ReSharper restore CompareOfFloatsByEqualityOperator
                {
                    _value = value;

                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the selection glyph.
        /// </summary>
        /// <value>The selection glyph.</value>
        protected Image SelectionGlyph { get; set; }

        #endregion

        #region Members

        /// <summary>
        ///     Creates the selection nub glyph.
        /// </summary>
        /// <returns>Image.</returns>
        protected virtual Image CreateNubGlyph()
        {
            Image image;

            image = new Bitmap(NubSize.Width + 1, NubSize.Height + 1, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(image))
            {
                Point[] outer;
                Point firstCorner;
                Point lastCorner;
                Point tipCorner;

                if (NubStyle == ColorSliderNubStyle.BottomRight)
                {
                    lastCorner = new Point(NubSize.Width, NubSize.Height);

                    if (Orientation == Orientation.Horizontal)
                    {
                        firstCorner = new Point(0, NubSize.Height);
                        tipCorner = new Point(NubSize.Width/2, 0);
                    }
                    else
                    {
                        firstCorner = new Point(NubSize.Width, 0);
                        tipCorner = new Point(0, NubSize.Height/2);
                    }
                }
                else
                {
                    firstCorner = Point.Empty;

                    if (Orientation == Orientation.Horizontal)
                    {
                        lastCorner = new Point(NubSize.Width, 0);
                        tipCorner = new Point(NubSize.Width/2, NubSize.Height);
                    }
                    else
                    {
                        lastCorner = new Point(0, NubSize.Height);
                        tipCorner = new Point(NubSize.Width, NubSize.Height/2);
                    }
                }

                // draw the shape
                outer = new[]
                {
                    firstCorner, lastCorner, tipCorner
                };

                // TODO: Add 3D edging similar to the mousewheel's diamond

                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (Brush brush = new SolidBrush(NubColor))
                    g.FillPolygon(brush, outer);
            }

            return image;
        }

        /// <summary>
        ///     Defines the bar bounds and padding.
        /// </summary>
        protected virtual void DefineBar()
        {
            if (SelectionGlyph != null)
                SelectionGlyph.Dispose();

            BarPadding = GetBarPadding();
            BarBounds = GetBarBounds();
            SelectionGlyph = NubStyle != ColorSliderNubStyle.None ? CreateNubGlyph() : null;
        }

        /// <summary>
        ///     Gets the bar bounds.
        /// </summary>
        /// <returns>Rectangle.</returns>
        protected virtual Rectangle GetBarBounds()
        {
            Rectangle clientRectangle;
            Padding padding;

            clientRectangle = ClientRectangle;
            padding = BarPadding + Padding;

            return new Rectangle(clientRectangle.Left + padding.Left, clientRectangle.Top + padding.Top,
                clientRectangle.Width - padding.Horizontal, clientRectangle.Height - padding.Vertical);
        }

        /// <summary>
        ///     Gets the bar padding.
        /// </summary>
        /// <returns>Padding.</returns>
        protected virtual Padding GetBarPadding()
        {
            int left;
            int top;
            int right;
            int bottom;

            left = 0;
            top = 0;
            right = 0;
            bottom = 0;

            switch (NubStyle)
            {
                case ColorSliderNubStyle.BottomRight:
                    if (Orientation == Orientation.Horizontal)
                    {
                        bottom = NubSize.Height + 1;
                        left = (NubSize.Width/2) + 1;
                        right = left;
                    }
                    else
                    {
                        right = NubSize.Width + 1;
                        top = (NubSize.Height/2) + 1;
                        bottom = top;
                    }
                    break;
                case ColorSliderNubStyle.TopLeft:
                    if (Orientation == Orientation.Horizontal)
                    {
                        top = NubSize.Height + 1;
                        left = (NubSize.Width/2) + 1;
                        right = left;
                    }
                    else
                    {
                        left = NubSize.Width + 1;
                        top = (NubSize.Height/2) + 1;
                        bottom = top;
                    }
                    break;
            }

            return new Padding(left, top, right, bottom);
        }

        /// <summary>
        ///     Raises the <see cref="BarBoundsChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnBarBoundsChanged(EventArgs e)
        {
            EventHandler handler;

            handler = BarBoundsChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="BarPaddingChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnBarPaddingChanged(EventArgs e)
        {
            EventHandler handler;

            Invalidate();

            handler = BarPaddingChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="BarStyleChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnBarStyleChanged(EventArgs e)
        {
            EventHandler handler;

            Invalidate();

            handler = BarStyleChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Color1Changed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColor1Changed(EventArgs e)
        {
            EventHandler handler;

            Invalidate();

            handler = Color1Changed;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Color2Changed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColor2Changed(EventArgs e)
        {
            EventHandler handler;

            Invalidate();

            handler = Color2Changed;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Color3Changed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColor3Changed(EventArgs e)
        {
            EventHandler handler;

            Invalidate();

            handler = Color3Changed;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="CustomColorsChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnCustomColorsChanged(EventArgs e)
        {
            EventHandler handler;

            Invalidate();

            handler = CustomColorsChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="LargeChangeChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnLargeChangeChanged(EventArgs e)
        {
            EventHandler handler;

            handler = LargeChangeChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="MaximumChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnMaximumChanged(EventArgs e)
        {
            EventHandler handler;

            handler = MaximumChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="MinimumChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnMinimumChanged(EventArgs e)
        {
            EventHandler handler;

            Invalidate();

            handler = MinimumChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="NubColorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnNubColorChanged(EventArgs e)
        {
            EventHandler handler;

            Invalidate();

            handler = NubColorChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="NubSizeChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnNubSizeChanged(EventArgs e)
        {
            EventHandler handler;

            DefineBar();
            Invalidate();

            handler = NubSizeChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="NubStyleChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnNubStyleChanged(EventArgs e)
        {
            EventHandler handler;

            DefineBar();
            Invalidate();

            handler = NubStyleChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="OrientationChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnOrientationChanged(EventArgs e)
        {
            EventHandler handler;

            DefineBar();
            Invalidate();

            handler = OrientationChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ShowValueDividerChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnShowValueDividerChanged(EventArgs e)
        {
            EventHandler handler;

            Invalidate();

            handler = ShowValueDividerChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="SliderStyleChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnSliderStyleChanged(EventArgs e)
        {
            EventHandler handler;

            DefineBar();
            Invalidate();

            handler = SliderStyleChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="SmallChangeChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnSmallChangeChanged(EventArgs e)
        {
            EventHandler handler;

            handler = SmallChangeChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ValueChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler;

            Refresh();

            handler = ValueChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Paints control adornments.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        protected virtual void PaintAdornments(PaintEventArgs e)
        {
            Point point;

            point = ValueToPoint(Value);

            // divider
            if (ShowValueDivider)
            {
                Point start;
                Point end;
                IntPtr hdc;

                if (Orientation == Orientation.Horizontal)
                {
                    start = new Point(point.X, BarBounds.Top);
                    end = new Point(point.X, BarBounds.Bottom);
                }
                else
                {
                    start = new Point(BarBounds.Left, point.Y);
                    end = new Point(BarBounds.Right, point.Y);
                }

                // draw a XOR'd line using Win32 API as this functionality isn't part of .NET
                hdc = e.Graphics.GetHdc();
                NativeMethods.SetROP2(hdc, NativeConstants.R2_NOT);
                NativeMethods.MoveToEx(hdc, start.X, start.Y, IntPtr.Zero);
                NativeMethods.LineTo(hdc, end.X, end.Y);
                e.Graphics.ReleaseHdc(hdc);
            }

            // drag nub
            if (NubStyle != ColorSliderNubStyle.None && SelectionGlyph != null)
            {
                int x;
                int y;

                if (Orientation == Orientation.Horizontal)
                {
                    x = point.X - NubSize.Width/2;
                    if (NubStyle == ColorSliderNubStyle.BottomRight)
                        y = BarBounds.Bottom;
                    else
                        y = BarBounds.Top - NubSize.Height;
                }
                else
                {
                    y = point.Y - NubSize.Height/2;
                    if (NubStyle == ColorSliderNubStyle.BottomRight)
                        x = BarBounds.Right;
                    else
                        x = BarBounds.Left - NubSize.Width;
                }

                e.Graphics.DrawImage(SelectionGlyph, x, y);
            }

            // focus
            if (Focused)
                ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(BarBounds, -2, -2));
        }

        /// <summary>
        ///     Paints the bar.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        protected virtual void PaintBar(PaintEventArgs e)
        {
            float angle;

            angle = (Orientation == Orientation.Horizontal) ? 0 : 270;

            if (BarBounds.Height > 0 && BarBounds.Width > 0)
            {
                ColorBlend blend;

                // HACK: Inflating the brush rectangle by 1 seems to get rid of a odd issue where the last color is drawn on the first pixel

                blend = new ColorBlend();
                using (
                    var brush = new LinearGradientBrush(Rectangle.Inflate(BarBounds, 1, 1), Color.Empty, Color.Empty,
                        angle, false))
                {
                    switch (BarStyle)
                    {
                        case ColorBarStyle.TwoColor:
                            blend.Colors = new[]
                            {
                                Color1, Color2
                            };
                            blend.Positions = new[]
                            {
                                0F, 1F
                            };
                            break;
                        case ColorBarStyle.ThreeColor:
                            blend.Colors = new[]
                            {
                                Color1, Color2, Color3
                            };
                            blend.Positions = new[]
                            {
                                0, 0.5F, 1
                            };
                            break;
                        case ColorBarStyle.Custom:
                            if (CustomColors != null && CustomColors.Count > 0)
                            {
                                blend.Colors = CustomColors.ToArray();
                                blend.Positions =
                                    Enumerable.Range(0, CustomColors.Count)
                                        .Select(
                                            i =>
                                                i == 0
                                                    ? 0
                                                    : i == CustomColors.Count - 1
                                                        ? 1
                                                        : (float) (1.0D/CustomColors.Count)*i)
                                        .ToArray();
                            }
                            else
                            {
                                blend.Colors = new[]
                                {
                                    Color1, Color2
                                };
                                blend.Positions = new[]
                                {
                                    0F, 1F
                                };
                            }
                            break;
                    }

                    brush.InterpolationColors = blend;
                    e.Graphics.FillRectangle(brush, BarBounds);
                }
            }
        }

        /// <summary>
        ///     Computes the location of the specified client point into value coordinates.
        /// </summary>
        /// <param name="location">The client coordinate <see cref="Point" /> to convert.</param>
        protected virtual void PointToValue(Point location)
        {
            float value;

            location.X += ClientRectangle.X - BarBounds.X;
            location.Y += ClientRectangle.Y - BarBounds.Y;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    value = Minimum + (location.X/(float) BarBounds.Width*(Minimum + Maximum));
                    break;
                default:
                    value = Minimum + (location.Y/(float) BarBounds.Height*(Minimum + Maximum));
                    break;
            }

            if (value < Minimum)
                value = Minimum;

            if (value > Maximum)
                value = Maximum;

            Value = value;
        }

        /// <summary>
        ///     Computes the location of the value point into client coordinates.
        /// </summary>
        /// <param name="value">The value coordinate <see cref="Point" /> to convert.</param>
        /// <returns>A <see cref="Point" /> that represents the converted <see cref="Point" />, value, in client coordinates.</returns>
        protected virtual Point ValueToPoint(float value)
        {
            double x;
            double y;
            Padding padding;

            padding = BarPadding + Padding;
            x = 0;
            y = 0;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    x = (BarBounds.Width/Maximum)*value;
                    break;
                default:
                    y = (BarBounds.Height/Maximum)*value;
                    break;
            }

            return new Point((int) x + padding.Left, (int) y + padding.Top);
        }

        #endregion
    }
}