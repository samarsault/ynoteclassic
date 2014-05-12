namespace Nini.Config
{
    /// <include file='IniConfig.xml' path='//Class[@name="IniConfig"]/docs/*' />
    public class IniConfig : ConfigBase
    {
        #region Private variables

        private readonly IniConfigSource parent;

        #endregion Private variables

        #region Constructors

        /// <include file='IniConfig.xml' path='//Constructor[@name="Constructor"]/docs/*' />
        public IniConfig(string name, IConfigSource source)
            : base(name, source)
        {
            parent = (IniConfigSource)source;
        }

        #endregion Constructors

        #region Public properties

        #endregion Public properties

        #region Public methods

        /// <include file='IniConfig.xml' path='//Method[@name="Get"]/docs/*' />
        public override string Get(string key)
        {
            if (!parent.CaseSensitive)
            {
                key = CaseInsensitiveKeyName(key);
            }

            return base.Get(key);
        }

        /// <include file='IniConfig.xml' path='//Method[@name="Set"]/docs/*' />
        public override void Set(string key, object value)
        {
            if (!parent.CaseSensitive)
            {
                key = CaseInsensitiveKeyName(key);
            }

            base.Set(key, value);
        }

        /// <include file='IniConfig.xml' path='//Method[@name="Remove"]/docs/*' />
        public override void Remove(string key)
        {
            if (!parent.CaseSensitive)
            {
                key = CaseInsensitiveKeyName(key);
            }

            base.Remove(key);
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        ///     Returns the key name if the case insensitivity is turned on.
        /// </summary>
        private string CaseInsensitiveKeyName(string key)
        {
            string result = null;

            var lowerKey = key.ToLower();
            foreach (string currentKey in keys.Keys)
            {
                if (currentKey.ToLower() == lowerKey)
                {
                    result = currentKey;
                    break;
                }
            }

            return (result == null) ? key : result;
        }

        #endregion Private methods
    }
}