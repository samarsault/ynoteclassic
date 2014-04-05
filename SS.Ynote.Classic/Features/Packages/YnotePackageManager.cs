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
    public static class YnotePackageManager
    {
        /// <summary>
        /// Install a Package
        /// </summary>
        /// <param name="pack"></param>
        /// <returns></returns>
        public static bool InstallPackage(string pack)
        {
            try
            {
                var data = GetPackageData(pack);
                if (data != null)
                {
                    using (var zip = ZipStorer.Open(pack, FileAccess.Read))
                    {
                        var dirs = zip.ReadCentralDir();
                        foreach (var entry in dirs)
                        {
                            foreach (
                                var key in
                                    data.Keys.Where(
                                        key => Path.GetFileName(entry.FilenameInZip) == Path.GetFileName(key)))
                            {
                                string temp;
                                data.TryGetValue(key, out temp);
                                if (temp != null)
                                    zip.ExtractFile(entry, temp);
                            }
                        }
                    }
                    if (!Directory.Exists(SettingsBase.SettingsDir + "Packages"))
                        Directory.CreateDirectory(SettingsBase.SettingsDir + "Packages");
                    File.Copy(pack,
                        string.Format("{0}\\Packages\\{1}", SettingsBase.SettingsDir, Path.GetFileName(pack)), true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return false;
        }
        /// <summary>
        /// Uninstall a Package
        /// </summary>
        /// <param name="packageFile"></param>
        public static void UninstallPackage(string packageFile)
        {
            try
            {
                var result = MessageBox.Show(string.Format("Are you Sure you want to uninstall : {0} ? ",
                    Path.GetFileNameWithoutExtension(packageFile)), "Ynote Classic", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (var file in GetFiles(Extractmanifest(packageFile)))
                        File.Delete(file);
                    File.Delete(packageFile);
                    var r2 = MessageBox.Show("Package Successfully Uninstalled. Restart now to make changes", "Ynote Classic",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r2 == DialogResult.Yes)
                        Application.Restart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error removing the package \r\nMessage : " + ex.Message, "Ynote Classic",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>
        /// Extract Manifest from package
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        static string Extractmanifest(string package)
        {
            var zip = ZipStorer.Open(package, FileAccess.Read);
            var dirs = zip.ReadCentralDir();
            foreach (var entry in dirs)
            {
                if (Path.GetFileName(entry.FilenameInZip) == "index.manifest")
                {
                    var path = Path.GetTempFileName() + "index.manifest";
                    zip.ExtractFile(entry, path);
                    zip.Close();
                    return path;
                }
            }
            return null;
        }
        /// <summary>
        /// Get All Files from Manifest
        /// </summary>
        /// <param name="manifest"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetFiles(string manifest)
        {
            var dic = YnotePackage.GenerateDictionary(manifest);
            // foreach (string item in dic.Values)
            //     files.Add(item.Replace("$ynotedir", Application.StartupPath));
            return
                dic.Values.Select(
                    item =>
                        item.Replace("$ynotedata", SettingsBase.SettingsDir)
                            .Replace("$ynotedir", Application.StartupPath)).ToArray();
        }
        /// <summary>
        /// Gets Package Data
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private static IDictionary<string, string> GetPackageData(string package)
        {
            var zip = ZipStorer.Open(package, FileAccess.Read);
            var dirs = zip.ReadCentralDir();
            foreach (var entry in dirs)
            {
                if (Path.GetFileName(entry.FilenameInZip) == "index.manifest")
                {
                    var path = Path.GetTempFileName() + "index.manifest";
                    zip.ExtractFile(entry, path);
                    return YnotePackage.GenerateDictionary(path);
                }
            }
            return null;
        }
    }
}