using System;
using System.ComponentModel;
using System.Drawing;

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
    ///     Represents a control that binds multiple editors together as a single composite unit.
    /// </summary>
    public class ColorEditorManager : Component, IColorEditor
    {
        #region Instance Fields

        private ColorEditor _colorEditor;

        private ColorGrid _grid;

        private HslColor _hslColor;

        private LightnessColorSlider _lightnessColorSlider;

        private ScreenColorPicker _screenColorPicker;

        private ColorWheel _wheel;

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the Color property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ColorChanged;

        /// <summary>
        ///     Occurs when the ColorEditor property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ColorEditorChanged;

        /// <summary>
        ///     Occurs when the Grid property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ColorGridChanged;

        /// <summary>
        ///     Occurs when the Wheel property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ColorWheelChanged;

        /// <summary>
        ///     Occurs when the LightnessColorSlider property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler LightnessColorSliderChanged;

        /// <summary>
        ///     Occurs when the ScreenColorPicker property value changes
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler ScreenColorPickerChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the linked <see cref="ColorEditor" />.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof (ColorEditor), null)]
        public virtual ColorEditor ColorEditor
        {
            get { return _colorEditor; }
            set
            {
                if (ColorEditor != value)
                {
                    _colorEditor = value;

                    OnColorEditorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the linked <see cref="ColorGrid" />.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof (ColorGrid), null)]
        public virtual ColorGrid ColorGrid
        {
            get { return _grid; }
            set
            {
                if (ColorGrid != value)
                {
                    _grid = value;

                    OnColorGridChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the linked <see cref="ColorWheel" />.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof (ColorWheel), null)]
        public virtual ColorWheel ColorWheel
        {
            get { return _wheel; }
            set
            {
                if (ColorWheel != value)
                {
                    _wheel = value;

                    OnColorWheelChanged(EventArgs.Empty);
                }
            }
        }

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
        ///     Gets or sets the linked <see cref="LightnessColorSlider" />.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof (LightnessColorSlider), null)]
        public virtual LightnessColorSlider LightnessColorSlider
        {
            get { return _lightnessColorSlider; }
            set
            {
                if (LightnessColorSlider != value)
                {
                    _lightnessColorSlider = value;

                    OnLightnessColorSliderChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the linked <see cref="ScreenColorPicker" />.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof (ScreenColorPicker), null)]
        public virtual ScreenColorPicker ScreenColorPicker
        {
            get { return _screenColorPicker; }
            set
            {
                if (ScreenColorPicker != value)
                {
                    _screenColorPicker = value;

                    OnScreenColorPickerChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether updating of linked components is disabled.
        /// </summary>
        /// <value><c>true</c> if updated of linked components is disabled; otherwise, <c>false</c>.</value>
        protected bool LockUpdates { get; set; }

        /// <summary>
        ///     Gets or sets the component color.
        /// </summary>
        /// <value>The component color.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Color Color
        {
            get { return HslColor.ToRgbColor(); }
            set { HslColor = new HslColor(value); }
        }

        #endregion

        #region Members

        /// <summary>
        ///     Binds events for the specified editor.
        /// </summary>
        /// <param name="control">The <see cref="IColorEditor" /> to bind to.</param>
        protected virtual void BindEvents(IColorEditor control)
        {
            control.ColorChanged += ColorChangedHandler;
        }

        /// <summary>
        ///     Raises the <see cref="ColorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorChanged(EventArgs e)
        {
            EventHandler handler;

            Synchronize(this);

            handler = ColorChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ColorEditorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorEditorChanged(EventArgs e)
        {
            EventHandler handler;

            if (ColorEditor != null)
                BindEvents(ColorEditor);

            handler = ColorEditorChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ColorGridChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorGridChanged(EventArgs e)
        {
            EventHandler handler;

            if (ColorGrid != null)
                BindEvents(ColorGrid);

            handler = ColorGridChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ColorWheelChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorWheelChanged(EventArgs e)
        {
            EventHandler handler;

            if (ColorWheel != null)
                BindEvents(ColorWheel);

            handler = ColorWheelChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="LightnessColorSliderChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnLightnessColorSliderChanged(EventArgs e)
        {
            EventHandler handler;

            if (LightnessColorSlider != null)
                BindEvents(LightnessColorSlider);

            handler = LightnessColorSliderChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ScreenColorPickerChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnScreenColorPickerChanged(EventArgs e)
        {
            EventHandler handler;

            if (ScreenColorPicker != null)
                BindEvents(ScreenColorPicker);

            handler = ScreenColorPickerChanged;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Sets the color of the given editor.
        /// </summary>
        /// <param name="control">The <see cref="IColorEditor" /> to update.</param>
        /// <param name="sender">The <see cref="IColorEditor" /> triggering the update.</param>
        protected virtual void SetColor(IColorEditor control, IColorEditor sender)
        {
            if (control != null && control != sender)
                control.Color = sender.Color;
        }

        /// <summary>
        ///     Synchronizes linked components with the specified <see cref="IColorEditor" />.
        /// </summary>
        /// <param name="sender">The <see cref="IColorEditor" /> triggering the update.</param>
        protected virtual void Synchronize(IColorEditor sender)
        {
            if (!LockUpdates)
            {
                try
                {
                    LockUpdates = true;
                    SetColor(ColorGrid, sender);
                    SetColor(ColorWheel, sender);
                    SetColor(ScreenColorPicker, sender);
                    SetColor(ColorEditor, sender);
                    SetColor(LightnessColorSlider, sender);
                }
                finally
                {
                    LockUpdates = false;
                }
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handler for linked controls.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ColorChangedHandler(object sender, EventArgs e)
        {
            if (!LockUpdates)
            {
                IColorEditor source;

                source = (IColorEditor) sender;

                LockUpdates = true;
                Color = source.Color;
                LockUpdates = false;
                Synchronize(source);
            }
        }

        #endregion
    }
}