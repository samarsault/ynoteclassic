using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Features.Syntax
{
    public class SyntaxDesc
    {
        private Language _lang;
        private SyntaxBase _synbase;

        /// <summary>
        ///     if IsBase = false Value of Language
        /// </summary>
        public Language Language
        {
            get { return _lang; }
            set
            {
                SyntaxBase = null;
                _lang = value;
            }
        }

        /// <summary>
        ///     Syntax Base
        /// </summary>
        public SyntaxBase SyntaxBase
        {
            get { return _synbase; }
            set
            {
                _lang = Language.Text;
                _synbase = value;
            }
        }
    }
}