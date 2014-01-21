using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
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

    /// <summary>
    ///     Represents a control that allows the editing of a color in a variety of ways.
    /// </summary>
    [DefaultProperty("Color")]
    [DefaultEvent("ColorChanged")]
    public partial class ColorEditor : UserControl, IColorEditor
    {
        #region Instance Fields

        private HslColor _hslColor;

        private Orientation _orientation;

        /// <summary>
        ///     He
        /// </summary>
        public string Hex
        {
            get { return hexTextBox.Text; }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorEditor" /> class.
        /// </summary>
        public ColorEditor()
        {
            InitializeComponent();

            Color = Color.Black;
            Orientation = Orientation.Vertical;
            Size = new Size(200, 260);

            AddColorProperties<SystemColors>();
            AddColorProperties<Color>();

            SetDropDownWidth();
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the Color property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ColorChanged;

        /// <summary>
        ///     Occurs when the Orientation property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler OrientationChanged;

        #endregion

        #region Overridden Members

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);

            ResizeComponents();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            SetDropDownWidth();
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ResizeComponents();
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);

            ResizeComponents();
        }

        /// <summary>
        ///     Raises the <see cref="E:Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ResizeComponents();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the component color as a HSL structure.
        /// </summary>
        /// <value>The component color.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual HslColor HslColor
        {
            get { return _hslColor; }
            set
            {
                if (HslColor != value)
                {
                    _hslColor = value;

                    OnColorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the orientation of the editor.
        /// </summary>
        /// <value>The orientation.</value>
        [Category("Appearance")]
        [DefaultValue(typeof (Orientation), "Vertical")]
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
        ///     Gets or sets a value indicating whether input changes should be processed.
        /// </summary>
        /// <value><c>true</c> if input changes should be processed; otherwise, <c>false</c>.</value>
        protected bool LockUpdates { get; set; }

        /// <summary>
        ///     Gets or sets the component color.
        /// </summary>
        /// <value>The component color.</value>
        [Category("Appearance")]
        [DefaultValue(typeof (Color), "0, 0, 0")]
        public virtual Color Color
        {
            get { return HslColor.ToRgbColor(); }
            set { HslColor = new HslColor(value); }
        }

        #endregion

        #region Members

        /// <summary>
        ///     Raises the <see cref="ColorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorChanged(EventArgs e)
        {
            EventHandler handler;

            UpdateFields(false);

            handler = ColorChanged;

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

            ResizeComponents();

            handler = OrientationChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Resizes the editing components.
        /// </summary>
        protected virtual void ResizeComponents()
        {
            try
            {
                int group1HeaderLeft;
                int group1BarLeft;
                int group1EditLeft;
                int group2HeaderLeft;
                int group2BarLeft;
                int group2EditLeft;
                int barWidth;
                int editWidth;
                int top;
                int innerMargin;
                int columnWidth;
                int rowHeight;
                int labelOffset;
                int colorBarOffset;
                int editOffset;

                top = Padding.Top;
                innerMargin = 3;
                editWidth = TextRenderer.MeasureText(new string('W', 6), Font).Width;
                rowHeight = Math.Max(Math.Max(rLabel.Height, rColorBar.Height), rNumericUpDown.Height);
                labelOffset = (rowHeight - rLabel.Height)/2;
                colorBarOffset = (rowHeight - rColorBar.Height)/2;
                editOffset = (rowHeight - rNumericUpDown.Height)/2;

                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        columnWidth = (ClientSize.Width - (Padding.Horizontal + innerMargin))/2;
                        break;
                    default:
                        columnWidth = ClientSize.Width - Padding.Horizontal;
                        break;
                }

                group1HeaderLeft = Padding.Left;
                group1EditLeft = columnWidth - editWidth;
                group1BarLeft = group1HeaderLeft + aLabel.Width + innerMargin;

                if (Orientation == Orientation.Horizontal)
                {
                    group2HeaderLeft = Padding.Left + columnWidth + innerMargin;
                    group2EditLeft = ClientSize.Width - (Padding.Right + editWidth);
                    group2BarLeft = group2HeaderLeft + aLabel.Width + innerMargin;
                }
                else
                {
                    group2HeaderLeft = group1HeaderLeft;
                    group2EditLeft = group1EditLeft;
                    group2BarLeft = group1BarLeft;
                }

                barWidth = group1EditLeft - (group1BarLeft + innerMargin);

                // RGB header
                rgbHeaderLabel.SetBounds(group1HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                top += rowHeight + innerMargin;

                // R row
                rLabel.SetBounds(group1HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                rColorBar.SetBounds(group1BarLeft, top + colorBarOffset, barWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                rNumericUpDown.SetBounds(group1EditLeft + editOffset, top, editWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                top += rowHeight + innerMargin;

                // G row
                gLabel.SetBounds(group1HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                gColorBar.SetBounds(group1BarLeft, top + colorBarOffset, barWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                gNumericUpDown.SetBounds(group1EditLeft + editOffset, top, editWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                top += rowHeight + innerMargin;

                // B row
                bLabel.SetBounds(group1HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                bColorBar.SetBounds(group1BarLeft, top + colorBarOffset, barWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                bNumericUpDown.SetBounds(group1EditLeft + editOffset, top, editWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                top += rowHeight + innerMargin;

                // Hex row
                hexLabel.SetBounds(group1HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                hexTextBox.SetBounds(group1EditLeft, top + colorBarOffset, editWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                top += rowHeight + innerMargin;

                // reset top
                if (Orientation == Orientation.Horizontal)
                    top = Padding.Top;

                // HSL header
                hslLabel.SetBounds(group2HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                top += rowHeight + innerMargin;

                // H row
                hLabel.SetBounds(group2HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                hColorBar.SetBounds(group2BarLeft, top + colorBarOffset, barWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                hNumericUpDown.SetBounds(group2EditLeft, top + editOffset, editWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                top += rowHeight + innerMargin;

                // S row
                sLabel.SetBounds(group2HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                sColorBar.SetBounds(group2BarLeft, top + colorBarOffset, barWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                sNumericUpDown.SetBounds(group2EditLeft, top + editOffset, editWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                top += rowHeight + innerMargin;

                // L row
                lLabel.SetBounds(group2HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                lColorBar.SetBounds(group2BarLeft, top + colorBarOffset, barWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                lNumericUpDown.SetBounds(group2EditLeft, top + editOffset, editWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                top += rowHeight + innerMargin;

                // A row
                aLabel.SetBounds(group2HeaderLeft, top + labelOffset, 0, 0, BoundsSpecified.Location);
                aColorBar.SetBounds(group2BarLeft, top + colorBarOffset, barWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
                aNumericUpDown.SetBounds(group2EditLeft, top + editOffset, editWidth, 0,
                    BoundsSpecified.Location | BoundsSpecified.Width);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
                // ignore errors
            }
        }

        /// <summary>
        ///     Updates the editing field values.
        /// </summary>
        /// <param name="userAction">if set to <c>true</c> the update is due to user interaction.</param>
        protected virtual void UpdateFields(bool userAction)
        {
            if (!LockUpdates)
            {
                try
                {
                    LockUpdates = true;

                    // RGB
                    if (!(userAction && rNumericUpDown.Focused))
                        rNumericUpDown.Value = Color.R;
                    if (!(userAction && gNumericUpDown.Focused))
                        gNumericUpDown.Value = Color.G;
                    if (!(userAction && bNumericUpDown.Focused))
                        bNumericUpDown.Value = Color.B;
                    rColorBar.Value = Color.R;
                    rColorBar.Color = Color;
                    gColorBar.Value = Color.G;
                    gColorBar.Color = Color;
                    bColorBar.Value = Color.B;
                    bColorBar.Color = Color;

                    // HTML
                    if (!(userAction && hexTextBox.Focused))
                        hexTextBox.Text = string.Format("{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B);

                    // HSL
                    if (!(userAction && hNumericUpDown.Focused))
                        hNumericUpDown.Value = (int) HslColor.H;
                    if (!(userAction && sNumericUpDown.Focused))
                        sNumericUpDown.Value = (int) (HslColor.S*100);
                    if (!(userAction && lNumericUpDown.Focused))
                        lNumericUpDown.Value = (int) (HslColor.L*100);
                    hColorBar.Value = (int) HslColor.H;
                    sColorBar.Color = Color;
                    sColorBar.Value = (int) (HslColor.S*100);
                    lColorBar.Color = Color;
                    lColorBar.Value = (int) (HslColor.L*100);

                    // Alpha
                    if (!(userAction && aNumericUpDown.Focused))
                        aNumericUpDown.Value = Color.A;
                    aColorBar.Color = Color;
                    aColorBar.Value = Color.A;
                }
                finally
                {
                    LockUpdates = false;
                }
            }
        }

        private void AddColorProperties<T>()
        {
            Type type;

            type = typeof (T);

            foreach (
                PropertyInfo property in
                    type.GetProperties(BindingFlags.Public | BindingFlags.Static)
                        .Where(property => property.PropertyType == typeof (Color)))
            {
                Color color;

                color = (Color) property.GetValue(type, null);
                if (!color.IsEmpty)
                    hexTextBox.Items.Add(color.Name);
            }
        }

        private string AddSpaces(string text)
        {
            string result;

            //http://stackoverflow.com/a/272929/148962

            if (!string.IsNullOrEmpty(text))
            {
                StringBuilder newText;

                newText = new StringBuilder(text.Length*2);
                newText.Append(text[0]);
                for (int i = 1; i < text.Length; i++)
                {
                    if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                        newText.Append(' ');
                    newText.Append(text[i]);
                }

                result = newText.ToString();
            }
            else
                result = null;

            return result;
        }

        private void SetDropDownWidth()
        {
            if (hexTextBox.Items.Count != 0)
                hexTextBox.DropDownWidth = (hexTextBox.ItemHeight*2) +
                                           hexTextBox.Items.Cast<string>()
                                               .Max(s => TextRenderer.MeasureText(s, Font).Width);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Change handler for editing components.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ValueChangedHandler(object sender, EventArgs e)
        {
            if (!LockUpdates)
            {
                bool useHsl;
                bool useRgb;

                useHsl = false;
                useRgb = false;

                LockUpdates = true;

                if (sender == hexTextBox)
                {
                    string text;
                    int namedIndex;

                    text = hexTextBox.Text;
                    if (text.StartsWith("#"))
                        text = text.Substring(1);

                    namedIndex = hexTextBox.FindStringExact(text);

                    if (namedIndex != -1 || text.Length == 6 || text.Length == 8)
                    {
                        try
                        {
                            Color color;

                            color = namedIndex != -1 ? Color.FromName(text) : ColorTranslator.FromHtml("#" + text);
                            aNumericUpDown.Value = color.A;
                            rNumericUpDown.Value = color.R;
                            bNumericUpDown.Value = color.B;
                            gNumericUpDown.Value = color.G;

                            useRgb = true;
                        }
                            // ReSharper disable EmptyGeneralCatchClause
                        catch
                        {
                        }
                        // ReSharper restore EmptyGeneralCatchClause
                    }
                }
                else if (sender == aColorBar || sender == rColorBar || sender == gColorBar || sender == bColorBar)
                {
                    aNumericUpDown.Value = (int) aColorBar.Value;
                    rNumericUpDown.Value = (int) rColorBar.Value;
                    gNumericUpDown.Value = (int) gColorBar.Value;
                    bNumericUpDown.Value = (int) bColorBar.Value;

                    useRgb = true;
                }
                else if (sender == aNumericUpDown || sender == rNumericUpDown || sender == gNumericUpDown ||
                         sender == bNumericUpDown)
                    useRgb = true;
                else if (sender == hColorBar || sender == lColorBar || sender == sColorBar)
                {
                    hNumericUpDown.Value = (int) hColorBar.Value;
                    sNumericUpDown.Value = (int) sColorBar.Value;
                    lNumericUpDown.Value = (int) lColorBar.Value;

                    useHsl = true;
                }
                else if (sender == hNumericUpDown || sender == sNumericUpDown || sender == lNumericUpDown)
                    useHsl = true;

                if (useRgb)
                {
                    Color color;

                    color = Color.FromArgb((int) aNumericUpDown.Value, (int) rNumericUpDown.Value,
                        (int) gNumericUpDown.Value, (int) bNumericUpDown.Value);
                    HslColor = new HslColor(color);
                }
                else if (useHsl)
                {
                    HslColor color;

                    color = new HslColor((int) aNumericUpDown.Value, (double) hNumericUpDown.Value,
                        (double) sNumericUpDown.Value/100, (double) lNumericUpDown.Value/100);
                    HslColor = color;
                }

                LockUpdates = false;
                UpdateFields(true);
            }
        }

        private void hexTextBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // TODO: Really, this should be another control - ColorComboBox or ColorListBox etc.

            if (e.Index != -1)
            {
                Rectangle colorBox;
                string name;
                Color color;

                e.DrawBackground();

                name = (string) hexTextBox.Items[e.Index];
                color = Color.FromName(name);
                colorBox = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Height - 3, e.Bounds.Height - 3);

                using (Brush brush = new SolidBrush(color))
                    e.Graphics.FillRectangle(brush, colorBox);
                e.Graphics.DrawRectangle(SystemPens.ControlText, colorBox);

                TextRenderer.DrawText(e.Graphics, AddSpaces(name), Font, new Point(colorBox.Right + 3, colorBox.Top),
                    e.ForeColor);

                //if (color == Color.Transparent && (e.State & DrawItemState.Selected) != DrawItemState.Selected)
                //  e.Graphics.DrawLine(SystemPens.ControlText, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);

                e.DrawFocusRectangle();
            }
        }

        private void hexTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hexTextBox.SelectedIndex != -1)
                Color = Color.FromName((string) hexTextBox.SelectedItem);
        }

        #endregion
    }
}