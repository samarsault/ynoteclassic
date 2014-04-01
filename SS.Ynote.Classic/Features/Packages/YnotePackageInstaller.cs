using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Packages
{
    /// <summary>
    /// Ynote Package Loader
    /// </summary>
    public static class YnotePackageInstaller
    {
        public static bool InstallPackage(string pack)
        {
            try
            {
                var data = GetPluginData(pack);
                if (data != null)
                {
                    var zip = ZipStorer.Open(pack, FileAccess.Read);
                    var dirs = zip.ReadCentralDir();
                    foreach (var entry in dirs)
                    {
                        foreach (
                            var key in
                                data.Keys.Where(key => Path.GetFileName(entry.FilenameInZip) == Path.GetFileName(key)))
                        {
                            string temp;
                            data.TryGetValue(key, out temp);
                            if (temp != null)
                                zip.ExtractFile(entry, temp);
                        }
                    }
                    File.Copy(pack,
                        string.Format("{0}\\User\\Packages\\{1}", Application.StartupPath, Path.GetFileName(pack)));
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return false;
        }
        private static IDictionary<string, string> GetPluginData(string package)
        {
            var zip = ZipStorer.Open(package, FileAccess.Read);
            var dirs = zip.ReadCentralDir();
            foreach (var entry in dirs)
            {
                if (Path.GetFileName(entry.FilenameInZip) == "index.manifest")
                {
                    string path = Path.GetTempFileName() + "index.manifest";
                    zip.ExtractFile(entry, path);
                    return YnotePackage.GenerateDictionary(path);
                }
            }
            return null;
        }
    }
}