using System.Resources;

namespace WeifenLuo.WinFormsUI.Docking
{
    internal static class ResourceHelper
    {
        private static ResourceManager _resourceManager;

        private static ResourceManager ResourceManager
        {
            get {
                return _resourceManager ??
                       (_resourceManager =
                           new ResourceManager("WeifenLuo.WinFormsUI.Docking.Strings", typeof (ResourceHelper).Assembly));
            }
        }

        public static string GetString(string name)
        {
            return ResourceManager.GetString(name);
        }
    }
}