#region

using System;
using System.Globalization;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic
{

    #region Using Directives

    #endregion

    public class NumericTextBox : TextBox
    {
        #region Constants

        private bool allowSpace;

        #endregion

        #region KeyPress

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

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
            else if (allowSpace && e.KeyChar == ' ')
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion

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
            set { allowSpace = value; }

            get { return allowSpace; }
        }

        #endregion
    }
}