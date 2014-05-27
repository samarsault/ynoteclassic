using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace SS.Ynote.Classic
{
    /// <summary>
    ///     P/Invoke for Showing the File Properties
    /// </summary>
    internal static class NativeMethods
    {
        private const int SwShow = 5;
        private const uint Seemaskinvokeidlist = 12;

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern bool ShellExecuteEx(ref Shellexecuteinfo lpExecInfo);

        [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        internal static bool ShowFileProperties(string filename)
        {
            var info = new Shellexecuteinfo();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = filename;
            info.nShow = SwShow;
            info.fMask = Seemaskinvokeidlist;
            return ShellExecuteEx(ref info);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct Shellexecuteinfo
        {
            public int cbSize;
            public uint fMask;
            private readonly IntPtr hwnd;

            [MarshalAs(UnmanagedType.LPTStr)] public string lpVerb;

            [MarshalAs(UnmanagedType.LPTStr)] public string lpFile;

            [MarshalAs(UnmanagedType.LPTStr)] private readonly string lpParameters;

            [MarshalAs(UnmanagedType.LPTStr)] private readonly string lpDirectory;

            public int nShow;
            private readonly IntPtr hInstApp;
            private readonly IntPtr lpIDList;

            [MarshalAs(UnmanagedType.LPTStr)] private readonly string lpClass;

            private readonly IntPtr hkeyClass;
            private readonly uint dwHotKey;
            private readonly IntPtr hIcon;
            private readonly IntPtr hProcess;
        }
    }
}