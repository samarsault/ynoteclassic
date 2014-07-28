using System;
using System.IO;
using SS.Ynote.Classic.Extensibility.Packages;

namespace pkmgr // Package Manager
{
   static class Program
    {
       static void Main(string[] args)
        {
           // set ynote directory
            string ynotedir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "Ynote Classic";
           // check if ynote directory exists
            if (!Directory.Exists(ynotedir))
                Directory.CreateDirectory(ynotedir);
           // print some info
            Console.WriteLine("Ynote Package Manager 1.0 Beta 10");
            Console.WriteLine("Copyright (C) 2014 Samarjeet Singh");

            if (args.Length == 0)
                Console.WriteLine("\r\nNo Inputs Specified");
            else
            {
                if (!args[0].EndsWith(".ynotepackage"))
                {
                    Console.WriteLine("Error : Not a valid Ynote Package File");
                    return;
                }
                Console.WriteLine("Installing package {0} ...", args[0]);
                Console.WriteLine(YnotePackageManager.InstallPackage(args[0])
                    ? "Package Successfully Installed!"
                    : "Error Installing Package");
            }
        }
    }
}