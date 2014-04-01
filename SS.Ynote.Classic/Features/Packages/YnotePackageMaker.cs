using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SS.Ynote.Classic.Features.Packages
{
    public static class YnotePackageMaker
    {
        private static string GenerateManifest(IDictionary<string, string> dic)
        {
            var builder = new StringBuilder();
            foreach (var key in dic.Keys)
            {
                string temp;
                dic.TryGetValue(key, out temp);
                builder.AppendFormat("{0}:{1}", Path.GetFileName(key), temp + "\\" + Path.GetFileName(key));
                builder.AppendLine();
            }
            return builder.ToString();
        }

        public static bool MakePackage(string outfile, IDictionary<string, string> dictionary)
        {
            try
            {
                var manifest = GenerateManifest(dictionary);
                string path = Path.GetTempFileName() + ".manifest";
                File.WriteAllText(path, manifest);
                using (var package = ZipStorer.Create(outfile, ""))
                {
                    foreach (var key in dictionary.Keys)
                        package.AddFile(ZipStorer.Compression.Store, key,Path.GetFileName(key) , "");
                    package.AddFile(ZipStorer.Compression.Store, path, "index.manifest", "");
                    return true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Package Creation Not Succesful\r\nError : " + ex.Message);
                return false;
            }
        }
    }
}