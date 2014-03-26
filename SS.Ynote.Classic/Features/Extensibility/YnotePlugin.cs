

/// <summary>
/// IYnote Plugin
/// </summary>
public interface IYnotePlugin : IPlugin
{
    /// <summary>
    ///     Run Plugin()
    /// </summary>
    void Initialize(IYnote ynote);
}