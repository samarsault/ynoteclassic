//========================================
//
// Copyright (C) 2014 Samarjeet Singh
// The Ynote Classic Project
//
//========================================
namespace SS.Ynote.Classic
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    /// <summary>
    /// Displays File Properties
    /// </summary>
    internal static class NativeMethods
    {
        #region Win API
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern bool ShellExecuteEx(ref Shellexecuteinfo lpExecInfo);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct Shellexecuteinfo
        {
            public int cbSize;
            public uint fMask;
            private readonly IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            private readonly string lpParameters;
            [MarshalAs(UnmanagedType.LPTStr)]
            private readonly string lpDirectory;
            public int nShow;
            private readonly IntPtr hInstApp;
            private readonly IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPTStr)]
            private readonly string lpClass;
            private readonly IntPtr hkeyClass;
            private readonly uint dwHotKey;
            private readonly IntPtr hIcon;
            private readonly IntPtr hProcess;
        }

        private const int SwShow = 5;
        private const uint Seemaskinvokeidlist = 12;
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        internal static bool ShowFileProperties(string filename)
        {
            var info = new Shellexecuteinfo();
            info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = filename;
            info.nShow = SwShow;
            info.fMask = Seemaskinvokeidlist;
            return ShellExecuteEx(ref info);
        }
        #endregion
    }
}
