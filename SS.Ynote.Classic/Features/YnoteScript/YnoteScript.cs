#region

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CSScriptLibrary;

#endregion

namespace SS.Ynote.Classic
{
    public static class YnoteScript
    {
        private static void Run(IYnote ynote, string code)
        {
            var helper =
                new AsmHelper(CSScript.LoadMethod(code, Assembly.GetExecutingAssembly().FullName,
                    Application.StartupPath + @"\FastColoredTextBox.dll",
                    Application.StartupPath + @"\WeifenLuo.WinFormsUI.Docking"));
            helper.Invoke("*.Run", ynote);
        }

        static string[] GetReferences()
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
                var helper = new AsmHelper(CSScript.LoadMethod(File.ReadAllText(ysfile),Assembly.GetExecutingAssembly().FullName, Application.StartupPath + @"\FastColoredTextBox.dll", Application.StartupPath + @"\WeifenLuo.WinFormsUI.Docking"));
                helper.Invoke("*.Run", ynote);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an Error running the script : \r\n" + ex.Message);
            }
        }
    }
}