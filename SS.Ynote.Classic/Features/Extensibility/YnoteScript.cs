using CSScriptLibrary;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Extensibility
{
    public static class YnoteScript
    {
        private static string[] GetReferences()
        {
            return new[]
            {
                Assembly.GetExecutingAssembly().FullName,
                Application.StartupPath + @"\FastColoredTextBox.dll",
                Application.StartupPath + @"\WeifenLuo.WinFormsUI.Docking"
            };
        }

        public static void RunScript(IYnote ynote, string ysfile)
        {
            try
            {
                // var Run =
                //     new AsmHelper(CSScript.LoadMethod(File.ReadAllText(ysfile),));
                // Run.Invoke("*.Run", ynote);
                CSScript.CacheEnabled = true;
                CSScript.GlobalSettings.TargetFramework = "v3.5";
                var helper =
                    new AsmHelper(CSScript.LoadMethod(File.ReadAllText(ysfile), GetReferences()));
                helper.Invoke("*.Run", ynote);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an Error running the script : \r\n" + ex.Message);
            }
        }
    }
}