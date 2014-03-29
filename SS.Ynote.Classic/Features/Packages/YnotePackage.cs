namespace SS.Ynote.Classic.Features.Packages
{
    #region Using Directives

    using Nini.Config;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    #endregion Using Directives

    #region Package Maker

    public class YnotePackageMaker
    {
        private static string GenerateManifest(string pluginfile, string[] references)
        {
#if DEBUG
            foreach (var reference in references)
                Debug.WriteLine("Reference : " + reference);
#endif
#if DEBUG
            Debug.WriteLine(string.Format("[Plugin]\r\nPluginFile={0}\r\nReferences={1}", Path.GetFileName(pluginfile),
                string.Join(";", references).Trim()));
#endif
            string refs = "";
            refs = references.Aggregate(refs, (current, reference) => current + (reference + ";"));
            return string.Format("[Plugin]\r\nPluginFile = {0}\r\nReferences={1}", Path.GetFileName(pluginfile),
                refs.Trim());
        }

        public bool MakePackage(string outfile, string pluginfile, string[] references)
        {
            // try
            // {
            var manifest = GenerateManifest(pluginfile, references);
            using (var package = ZipStorer.Create(outfile, ""))
            {
                package.AddFile(ZipStorer.Compression.Store, pluginfile, Path.GetFileName(pluginfile), "");
#if DEBUG
                Debug.WriteLine("Plugin File : " + pluginfile + " Path :" + Path.GetFileName(pluginfile));
#endif
                foreach (var reference in references)
                {
#if DEBUG
                    Debug.WriteLine("Reference Add : " + reference + " Added To :" + Path.GetFileName(reference));
#endif
                    package.AddFile(ZipStorer.Compression.Store, reference, Path.GetFileName(reference), "");
                }
                var path = Path.GetTempFileName() + ".manifest";
                File.WriteAllText(path, manifest);
                package.AddFile(ZipStorer.Compression.Store, path, "plugin.manifest", "");
                return true;
            }
            //}
            //catch (Exception ex)
            //{
            //
            //    MessageBox.Show("Package Creation Not Succesful\r\nError : " + ex.Message);
            //    return false;
            //}
        }
    }

    #endregion Package Maker

    #region Package Loader

    /// <summary>
    ///     Ynote Extension Loader
    /// </summary>
    public class YnotePackageLoader
    {
        public bool InstallPackage(string pack)
        {
            var data = GetPluginData(pack);
            if (data != null)
            {
                var zip = ZipStorer.Open(pack, FileAccess.Read);
                var dirs = zip.ReadCentralDir();
                var referenceList = data.References;
                foreach (var entry in dirs)
                {
                    if (entry.FilenameInZip == data.PluginFile)
                        zip.ExtractFile(entry, Application.StartupPath + @"\Plugins\" + data.PluginFile);
                    var list = referenceList as string[] ?? referenceList;
                    var enumerable = referenceList as string[] ?? list.ToArray();
                    var entry1 = entry;
                    foreach (var reference in enumerable.Where(reference => entry1.FilenameInZip == reference))
                        zip.ExtractFile(entry, Application.StartupPath + @"\" + reference);
                }
                return true;
            }
            return false;
        }

        private static YnotePluginData GetPluginData(string package)
        {
            var zip = ZipStorer.Open(package, FileAccess.Read);
            IEnumerable<ZipStorer.ZipFileEntry> dirs = zip.ReadCentralDir();
            foreach (var entry in dirs)
            {
                if (Path.GetFileName(entry.FilenameInZip) == "plugin.manifest")
                {
                    string path = Path.GetTempFileName() + "plugin.manifest";
                    zip.ExtractFile(entry, path);
                    var data = ReadManifest(path);
                    return data;
                }
            }
            return null;
        }

        private static YnotePluginData ReadManifest(string file)
        {
            IConfigSource source = new IniConfigSource(file);
            var dat = new YnotePluginData
            {
                PluginFile = source.Configs["Plugin"].Get("PluginFile"),
                References = source.Configs["Plugin"].Get("References").Split(';'),
                ManifestFile = file
            };
            return dat;
        }
    }

    #endregion Package Loader
}