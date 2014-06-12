namespace SS.Ynote.Classic
{
    /// <summary>
    ///     A Ynote Command which when executed
    ///     is stored in a YnoteCommand
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Command Key
        /// </summary>
        string Key { get; }

        /// <summary>
        ///     Possible Commands
        /// </summary>
        string[] Commands { get; }

        /// <summary>
        ///     Processes Command
        /// </summary>
        /// <param name="val"></param>
        void ProcessCommand(string val, IYnote ynote);
    }
}