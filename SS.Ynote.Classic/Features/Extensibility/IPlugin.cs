/// <summary>
///     Plugin Interface
/// </summary>
public interface IPlugin
{
    /// <summary>
    ///     Name
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Version
    /// </summary>
    double Version { get; }

    /// <summary>
    ///     Author of Plugin
    /// </summary>
    string Description { get; }
}