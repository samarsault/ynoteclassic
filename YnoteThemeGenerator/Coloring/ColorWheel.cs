using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

#if USEEXTERNALCYOTEKLIBS
using Cyotek.Drawing;
#endif

namespace Cyotek.Windows.Forms
{
    // Cyotek Color Picker controls library
    // Copyright © 2013 Cyotek. All Rights Reserved.
    // http://cyotek.com/blog/tag/colorpicker

    // If you use this code in your applications, donations or attribution are welcome

    [DefaultProperty("Color")]
    [DefaultEvent("ColorChanged")]
    public class ColorWheel : Control, IColorEditor
    {
        #region Instance Fields

        private Brush _brush;

        private PointF _centerPoint;

        private Color _color;

        private int _colorStep;

        private HslColor _hslColor;

        private int _largeChange;

        private int _selectionSize;

        private int _smallChange;

        private int _updateCount;

        #endregion

        #region Constructors

        public ColorWheel()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.Selectable | ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
            Color = Color.Black;
            ColorStep = 4;
            SelectionSize = 10;
            SmallChange = 1;
            LargeChange = 5;
            SelectionGlyph = CreateSelectionGlyph();
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the Color property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ColorChanged;

        /// <summary>
        ///     Occurs when the ColorStep property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ColorStepChanged;

        /// <summary>
        ///     Occurs when the HslColor property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler HslColorChanged;

        /// <summary>
        ///     Occurs when the LargeChange property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler LargeChangeChanged;

        /// <summary>
        ///     Occurs when the SelectionSize property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler SelectionSizeChanged;

        /// <summary>
        ///     Occurs when the SmallChange property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler SmallChangeChanged;

        #endregion

        #region Overridden Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion

        #region Overridden Members

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_brush != null)
                    _brush.Dispose();

                if (SelectionGlyph != null)
                    SelectionGlyph.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            bool result;

            if ((keyData & Keys.Left) == Keys.Left || (keyData & Keys.Up) == Keys.Up ||
                (keyData & Keys.Down) == Keys.Down || (keyData & Keys.Right) == Keys.Right ||
                (keyData & Keys.PageUp) == Keys.PageUp || (keyData & Keys.PageDown) == Keys.PageDown)
                result = true;
            else
                result = base.IsInputKey(keyData);

            return result;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            HslColor color;
            double hue;
            int step;

            color = HslColor;
            hue = color.H;

            step = e.Shift ? LargeChange : SmallChange;

            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Up:
                    hue += step;
                    break;
                case Keys.Left:
                case Keys.Down:
                    hue -= step;
                    break;
                case Keys.PageUp:
                    hue += LargeChange;
                    break;
                case Keys.PageDown:
                    hue -= LargeChange;
                    break;
            }

            if (hue >= 360)
                hue = 0;
            if (hue < 0)
                hue = 359;

            if (hue != color.H)
            {
                color.H = hue;

                // As the Color and HslColor properties update each other, need to temporarily disable this and manually set both
                // otherwise the wheel "sticks" due to imprecise conversion from RGB to HSL
                LockUpdates = true;
                Color = color.ToRgbColor();
                HslColor = color;
                LockUpdates = false;

                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!Focused && TabStop)
                Focus();

            if (e.Button == MouseButtons.Left)
                SetColor(e.Location);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
                SetColor(e.Location);
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);

            RefreshWheel();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (AllowPainting)
            {
                base.OnPaintBackground(e);
                    // HACK: Easiest way of supporting things like BackgroundImage, BackgroundImageLayout etc

                if (_brush != null)
                    e.Graphics.FillPie(_brush, ClientRectangle, 0, 360);

                // smooth out the edge of the wheel
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(BackColor, 2))
                {
                    float radius;

                    radius = GetRadius(_centerPoint);
                    e.Graphics.DrawEllipse(pen,
                        new RectangleF(_centerPoint.X - radius, _centerPoint.Y - radius, radius*2, radius*2));
                }

                PaintCurrentColor(e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            RefreshWheel();
        }

        #endregion

        #region Properties

        [Category("Appearance")]
        [DefaultValue(4)]
        public virtual int ColorStep
        {
            get { return _colorStep; }
            set
            {
                if (value < 1 || value > 359)
                    throw new IndexOutOfRangeException("Value must be between 1 and 359");

                if (ColorStep != value)
                {
                    _colorStep = value;

                    OnColorStepChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(typeof (HslColor), "0, 0, 0")]
        [Browsable(false) /* disable editing until I write a proper type convertor */]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual HslColor HslColor
        {
            get { return _hslColor; }
            set
            {
                if (HslColor != value)
                {
                    _hslColor = value;

                    OnHslColorChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(5)]
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

        [Category("Appearance")]
        [DefaultValue(10)]
        public virtual int SelectionSize
        {
            get { return _selectionSize; }
            set
            {
                if (SelectionSize != value)
                {
                    _selectionSize = value;

                    OnSelectionSizeChanged(EventArgs.Empty);
                }
            }
        }

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
        ///     Gets a value indicating whether painting of the control is allowed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if painting of the control is allowed; otherwise, <c>false</c>.
        /// </value>
        protected virtual bool AllowPainting
        {
            get { return _updateCount == 0; }
        }

        protected Color[] Colors { get; set; }

        protected bool LockUpdates { get; set; }

        protected PointF[] Points { get; set; }

        protected Image SelectionGlyph { get; set; }

        [Category("Appearance")]
        [DefaultValue(typeof (Color), "Black")]
        public virtual Color Color
        {
            get { return _color; }
            set
            {
                if (Color != value)
                {
                    _color = value;

                    OnColorChanged(EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Members

        /// <summary>
        ///     Disables any redrawing of the image box
        /// </summary>
        public virtual void BeginUpdate()
        {
            _updateCount++;
        }

        /// <summary>
        ///     Enables the redrawing of the image box
        /// </summary>
        public virtual void EndUpdate()
        {
            if (_updateCount > 0)
                _updateCount--;

            if (AllowPainting)
                Invalidate();
        }

        protected virtual void CalculateWheel()
        {
            List<PointF> points;
            List<Color> colors;

            points = new List<PointF>();
            colors = new List<Color>();

            // Only define the points if the control is above a minimum size, otherwise if it's too small, you get an "out of memory" exceptions (of all things) when creating the brush
            if (ClientSize.Width > 16 && ClientSize.Height > 16)
            {
                double radius;
                int w;
                int h;

                w = ClientSize.Width;
                h = ClientSize.Height;

                _centerPoint = new PointF(w/2.0F, h/2.0F);
                radius = GetRadius(_centerPoint);

                for (double angle = 0; angle < 360; angle += ColorStep)
                {
                    double angleR;
                    PointF location;

                    angleR = angle*(Math.PI/180);
                    location = GetColorLocation(angleR, radius);

                    points.Add(location);
                    colors.Add(new HslColor(angle, 1, 0.5).ToRgbColor());
                }
            }

            Points = points.ToArray();
            Colors = colors.ToArray();
        }

        protected virtual Brush CreateGradientBrush()
        {
            Brush result;

            if (Points.Length != 0 && Points.Length == Colors.Length)
            {
                result = new PathGradientBrush(Points, WrapMode.Clamp)
                {
                    CenterPoint = _centerPoint,
                    CenterColor = Color.White,
                    SurroundColors = Colors
                };
            }
            else
                result = null;

            return result;
        }

        protected virtual Image CreateSelectionGlyph()
        {
            Image image;
            int halfSize;

            halfSize = SelectionSize/2;
            image = new Bitmap(SelectionSize + 1, SelectionSize + 1, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(image))
            {
                Point[] diamondOuter;

                diamondOuter = new[]
                {
                    new Point(halfSize, 0), new Point(SelectionSize, halfSize), new Point(halfSize, SelectionSize),
                    new Point(0, halfSize)
                };

                g.FillPolygon(SystemBrushes.Control, diamondOuter);
                g.DrawPolygon(SystemPens.ControlDark, diamondOuter);

                using (var pen = new Pen(Color.FromArgb(128, SystemColors.ControlDark)))
                {
                    g.DrawLine(pen, halfSize, 1, SelectionSize - 1, halfSize);
                    g.DrawLine(pen, halfSize, 2, SelectionSize - 2, halfSize);
                    g.DrawLine(pen, halfSize, SelectionSize - 1, SelectionSize - 2, halfSize + 1);
                    g.DrawLine(pen, halfSize, SelectionSize - 2, SelectionSize - 3, halfSize + 1);
                }

                using (var pen = new Pen(Color.FromArgb(196, SystemColors.ControlLightLight)))
                    g.DrawLine(pen, halfSize, SelectionSize - 1, 1, halfSize);

                g.DrawLine(SystemPens.ControlLightLight, 1, halfSize, halfSize, 1);
            }

            return image;
        }

        protected PointF GetColorLocation(Color color)
        {
            return GetColorLocation(new HslColor(color));
        }

        protected virtual PointF GetColorLocation(HslColor color)
        {
            double angleR = color.H*Math.PI/180;
            double radius = GetRadius(_centerPoint);
            radius *= color.S;

            return GetColorLocation(angleR, radius);
        }

        protected PointF GetColorLocation(double angleR, double radius)
        {
            double x;
            double y;

            x = Padding.Left + _centerPoint.X + Math.Cos(angleR)*radius;
            y = Padding.Top + _centerPoint.Y - Math.Sin(angleR)*radius;

            return new PointF((float) x, (float) y);
        }

        protected float GetRadius(PointF centerPoint)
        {
            return Math.Min(centerPoint.X, centerPoint.Y) -
                   (Math.Max(Padding.Horizontal, Padding.Vertical) + (SelectionSize/2));
        }

        /// <summary>
        ///     Raises the <see cref="ColorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorChanged(EventArgs e)
        {
            EventHandler handler;

            if (!LockUpdates)
                HslColor = new HslColor(Color);
            Refresh();

            handler = ColorChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ColorStepChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorStepChanged(EventArgs e)
        {
            EventHandler handler;

            RefreshWheel();

            handler = ColorStepChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="HslColorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnHslColorChanged(EventArgs e)
        {
            EventHandler handler;

            if (!LockUpdates)
                Color = HslColor.ToRgbColor();
            Invalidate();

            handler = HslColorChanged;

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
        ///     Raises the <see cref="SelectionSizeChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnSelectionSizeChanged(EventArgs e)
        {
            EventHandler handler;

            if (SelectionGlyph != null)
                SelectionGlyph.Dispose();

            SelectionGlyph = CreateSelectionGlyph();
            Invalidate();

            handler = SelectionSizeChanged;

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

        protected virtual void PaintCurrentColor(PaintEventArgs e)
        {
            if (!Color.IsEmpty)
            {
                PointF location;

                location = GetColorLocation(HslColor);

                if (!float.IsNaN(location.X) && !float.IsNaN(location.Y))
                {
                    int x;
                    int y;

                    x = (int) location.X - (SelectionSize/2);
                    y = (int) location.Y - (SelectionSize/2);

                    if (SelectionGlyph == null)
                        e.Graphics.DrawRectangle(Pens.Black, x, y, SelectionSize, SelectionSize);
                    else
                        e.Graphics.DrawImage(SelectionGlyph, x, y);

                    if (Focused)
                        ControlPaint.DrawFocusRectangle(e.Graphics,
                            new Rectangle(x - 1, y - 1, SelectionSize + 2, SelectionSize + 2));
                }
            }
        }

        protected virtual void SetColor(Point point)
        {
            double radius;
            double dx;
            double dy;
            double angle;
            double distance;
            double saturation;

            radius = GetRadius(_centerPoint);
            dx = Math.Abs(point.X - _centerPoint.X - Padding.Left);
            dy = Math.Abs(point.Y - _centerPoint.Y - Padding.Top);
            angle = Math.Atan(dy/dx)/Math.PI*180;
            distance = Math.Pow((Math.Pow(dx, 2) + (Math.Pow(dy, 2))), 0.5);
            saturation = distance/radius;

            if (distance < 6)
                saturation = 0; // snap to center

            if (point.X < _centerPoint.X)
                angle = 180 - angle;
            if (point.Y > _centerPoint.Y)
                angle = 360 - angle;

            LockUpdates = true;
            HslColor = new HslColor(angle, saturation, 0.5);
            Color = HslColor.ToRgbColor();
            LockUpdates = false;
        }

        private void RefreshWheel()
        {
            if (_brush != null)
                _brush.Dispose();

            CalculateWheel();
            _brush = CreateGradientBrush();
            Invalidate();
        }

        #endregion
    }
}