using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace SS.Ynote.Classic
{
    /// <summary>
    ///     Displays File Properties
    /// </summary>
    internal static class NativeMethods
    {
        #region File Properties

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

        #endregion File Properties

        /* #region Monospaced Font list

        public const Int32 LF_FACESIZE = 32;
        public const Int32 LF_FULLFACESIZE = 64;
        public const Int32 DEFAULT_CHARSET = 1;
        public const Int32 FIXED_PITCH = 1;
        public const Int32 TRUETYPE_FONTTYPE = 0x0004;

        public delegate Int32 FONTENUMPROC(
            ref ENUMLOGFONT lpelf, ref NEWTEXTMETRIC lpntm, UInt32 FontType, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LOGFONT
        {
             public int lfHeight;
             public int lfWidth;
             public int lfEscapement;
             public int lfOrientation;
             public int lfWeight;
             public byte lfItalic;
             public byte lfUnderline;
             public byte lfStrikeOut;
             public byte lfCharSet;
             public byte lfOutPrecision;
             public byte lfClipPrecision;
             public byte lfQuality;
             public byte lfPitchAndFamily;
             [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
             public string lfFaceName;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TEXTMETRIC
        {
            public Int32 tmHeight;
            public Int32 tmAscent;
            public Int32 tmDescent;
            public Int32 tmInternalLeading;
            public Int32 tmExternalLeading;
            public Int32 tmAveCharWidth;
            public Int32 tmMaxCharWidth;
            public Int32 tmWeight;
            public Int32 tmOverhang;
            public Int32 tmDigitizedAspectX;
            public Int32 tmDigitizedAspectY;
            public Int32 tmFirstChar;
            public Int32 tmLastChar;
            public Int32 tmDefaultChar;
            public Int32 tmBreakChar;
            public Int32 tmItalic;
            public Int32 tmUnderlined;
            public Int32 tmStruckOut;
            public Int32 tmPitchAndFamily;
            public Int32 tmCharSet;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ENUMLOGFONT
        {
            public LOGFONT elfLogFont;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string elfFullName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string elfStyle;

            public override string
        * ()
            {
                return elfFullName;
            }
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NEWTEXTMETRIC
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
            public UInt32 ntmFlags;
            public UInt32 ntmSizeEM;
            public UInt32 ntmCellHeight;
            public UInt32 ntmAvgWidth;
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 EnumFontFamiliesEx(IntPtr hdc, ref LOGFONT lpLogfont,
            FONTENUMPROC lbEnumFontFamExProc, IntPtr lParam, UInt32 dwFlags);

        #endregion */
    }
}