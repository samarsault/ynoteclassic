using SS.Ynote.Classic.Core.Project;

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
    /// <summary>
    /// A Project Plugin
    /// </summary>
    public interface IProjectPlugin
    {
        /// <summary>
        /// Detects if the Plugin has to Open the Project
        /// </summary>
        /// <returns></returns>
        bool Detect();
        /// <summary>
        /// Open the Project
        /// </summary>
        /// <param name="project"></param>
        void OpenProject(YnoteProject project);
        /// <summary>
        /// Save the Project
        /// </summary>
        /// <param name="project"></param>
        /// <param name="fileName"></param>
        void SaveProject(YnoteProject project, string fileName);
    }
}