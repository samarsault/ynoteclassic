//=================================================
//
// Ynote Classic
// Copyright (C) 2014 Samarjeet Singh (singh.samarjeet.27@gmail.com)
//
//==================================================

using System;
using System.Windows.Forms;

namespace SS.Ynote.Classic
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(args.Length != 0 ? new MainForm(args[0]) : new MainForm(null));
        }
    }
}