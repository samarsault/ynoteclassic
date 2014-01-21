
namespace SS.Ynote.Classic
{
    #region Using Directives
    using System.Windows.Forms;
    using System.Globalization;
    using System;
    #endregion

    public class NumericTextBox : TextBox
    {
        #region Constants
        private bool allowSpace = false;
        #endregion

        #region KeyPress

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
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
            else if (this.allowSpace && e.KeyChar == ' ')
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
            get
            {
                return Int32.Parse(this.Text);
            }
        }

        public decimal DecimalValue
        {
            get
            {
                return Decimal.Parse(this.Text);
            }
        }

        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }
        #endregion
    }
}
