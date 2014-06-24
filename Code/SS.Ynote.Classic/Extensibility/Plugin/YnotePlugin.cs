namespace SS.Ynote.Classic.Extensibility
{
    /// <summary>
    ///     IYnote Plugin
    /// </summary>
    public interface IYnotePlugin
    {
        /// <summary>
        ///     Main method of Plugin
        /// </summary>
        void Main(IYnote ynote);
    }

}