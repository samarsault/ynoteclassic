using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SUP.Host;
using SUP.Tester;

namespace SUP.Console
{
    internal static class Program
    {
        private static ConsoleAppUpdate uphost;
        
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }
            uphost = new ConsoleAppUpdate();
            uphost.UpdateFile = args[1];
            uphost.Files = AppFile.GetAppFiles(args[0]);
            uphost.CurrentVersion = Convert.ToDouble(args[2]);
            print("SUP Updater Console");
            print("Copyright (C) 2014 Samarjeet Singh");
            print(string.Empty);
            print("Checking for Updates.....");
            var checker = new Updater(uphost);
            if (checker.IsUpdateAvailable())
            {
                var result = MessageBox.Show("Updates are available ? Would you like to download it ?");
                if (result == DialogResult.OK)
                {
                    Update update = checker.GetUpdate();
                    update.UpdateDownload += update_UpdateDownload;
                    update.DownloadUpdate();
                }
            }
            else
            {
                MessageBox.Show("No Updates Are Available!");
            }
        }
        
        private static void ShowHelp()
        {
            print("Error : No Arguments Specified");
            print("Usage : sup.exe <appfiles.xml> <updatefile.xml> <currentversion>");
        }
        
        private static void update_UpdateDownload(UpdateDownloadEventArgs e)
        {
            Application.Run(new UpdateWindow(e.Update));
        }
        
        private static void print(string text)
        {
            System.Console.WriteLine(text);
        }
    }
    
    internal class ConsoleAppUpdate : IUpdateHost
    {
        private IEnumerable<AppFile> _files;
        
        public string Name
        {
            get { return "SUP.Console"; }
        }
        
        public string UpdateFile { get; set; }
        
        public string LatestFilesUpdate
        {
            get { throw new NotImplementedException(); }
        }
        
        public string RootDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
        
        public IEnumerable<AppFile> Files {get; set;}
        
        
        public double CurrentVersion
        {
            get;
            set;
        }
    }
}