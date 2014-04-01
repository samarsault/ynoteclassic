using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Packages
{
    public class YnotePackageUninstaller
    {
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

        static string Extractmanifest(string package)
        {
            var zip = ZipStorer.Open(package, FileAccess.Read);
            var dirs = zip.ReadCentralDir();
            foreach (var entry in dirs)
            {
                if (Path.GetFileName(entry.FilenameInZip) == "index.manifest")
                {
                    string path = Path.GetTempFileName() + "index.manifest";
                    zip.ExtractFile(entry, path);
                    zip.Close();
                    return path;
                }
            }
            return null;
        }
        static IEnumerable<string> GetFiles(string manifest)
        {
            var dic = YnotePackage.GenerateDictionary(manifest);
            // foreach (string item in dic.Values)
            //     files.Add(item.Replace("$ynotedir", Application.StartupPath));
            return dic.Values.Select(item => item.Replace("$ynotedir", Application.StartupPath)).ToList();
        }
    }
}
