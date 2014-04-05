using System;
using System.Globalization;
using System.Windows.Forms;

namespace SS.Ynote.Classic.UI.Controls
{
    public class NumericTextBox : TextBox
    {
        #region Constants

        private bool _allowSpace;

        #endregion Constants

        #region KeyPress

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            var numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            var decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            var groupSeparator = numberFormatInfo.NumberGroupSeparator;
            var negativeSign = numberFormatInfo.NegativeSign;

            var keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
                     keyInput.Equals(negativeSign))
            {
            }
            else if (e.KeyChar == '\b')
            {
            }
            else if (_allowSpace && e.KeyChar == ' ')
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion KeyPress

        #region Properties

        public int IntValue
        {
            get { return Int32.Parse(Text); }
        }

        public decimal DecimalValue
        {
            get { return Decimal.Parse(Text); }
        }

        public bool AllowSpace
        {
            set { _allowSpace = value; }

            get { return _allowSpace; }
        }

        #endregion Properties
    }
}