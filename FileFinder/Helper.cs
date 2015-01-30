using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace FileFinder
{
    class FileMaskMatcher
    {
        public enum MatchType
        {
            FileName,
            Directory,
            FullPath
        }

        List<Regex> strictMasks;
        List<Regex> greedyMasks;
        public FileMaskMatcher(List<string> fileMaskList)
        {
            strictMasks = new List<Regex>();
            greedyMasks = new List<Regex>();
            foreach (string mask in fileMaskList)
            {
                string convertedMask = Environment.ExpandEnvironmentVariables(mask);
                convertedMask = Regex.Escape(convertedMask).Replace("\\*", ".*").Replace("\\?", ".");
                strictMasks.Add(new Regex("^" + convertedMask + "$", RegexOptions.IgnoreCase));
                greedyMasks.Add(new Regex(convertedMask, RegexOptions.IgnoreCase));
            }
        }
        public bool IsMatch(string name, MatchType matchType)
        {
            if (matchType == MatchType.FileName)
            {
                foreach (Regex fileMask in strictMasks)
                {
                    if (fileMask.IsMatch(name))
                    {
                        return true;
                    }
                }
            }
            else if (matchType == MatchType.Directory)
            {
                foreach (Regex fileMask in greedyMasks)
                {
                    if (fileMask.IsMatch(name))
                    {
                        return true;
                    }
                }
            }
            else if (matchType == MatchType.FullPath)
            {
                if (IsMatch(Path.GetFileName(name), MatchType.FileName) ||
                    IsMatch(Path.GetDirectoryName(name), MatchType.Directory))
                {
                    return true;
                }
            }
            return false;
        }
    }

    class SHGetIcon
    {
        [DllImport("shell32", CharSet = CharSet.Auto)]
        extern static int SHGetFileInfo(string pszPath, int dwFileAttributes, out SHFILEINFO psfi, uint cbfileInfo, SHGFI uFlags);

        const int MAX_PATH = 260;
        const int MAX_TYPE = 80;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct SHFILEINFO
        {
            public SHFILEINFO(bool b)
            {
                hIcon = IntPtr.Zero;
                iIcon = 0;
                dwAttributes = 0;
                szDisplayName = "";
                szTypeName = "";
            }
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_TYPE)]
            public string szTypeName;
        }

        [Flags]
        enum SHGFI
        {
            Icon = 0x000000100,
            DisplayName = 0x000000200,
            TypeName = 0x000000400,
            Attributes = 0x000000800,
            IconLocation = 0x000001000,
            ExeType = 0x000002000,
            SysIconIndex = 0x000004000,
            LinkOverlay = 0x000008000,
            Selected = 0x000010000,
            Attr_Specified = 0x000020000,
            LargeIcon = 0x000000000,
            SmallIcon = 0x000000001,
            OpenIcon = 0x000000002,
            ShellIconSize = 0x000000004,
            PIDL = 0x000000008,
            UseFileAttributes = 0x000000010,
            AddOverlays = 0x000000020,
            OverlayIndex = 0x000000040,
        }

        public static Bitmap GetBmp(string strPath, bool bSmall)
        {
            return GetIcon(strPath, bSmall).ToBitmap();
        }

        public static Icon GetIcon(string strPath, bool bSmall)
        {
            try
            {
                SHFILEINFO info = new SHFILEINFO(true);
                int cbFileInfo = Marshal.SizeOf(info);
                SHGFI flags;
                if (bSmall)
                    flags = SHGFI.Icon | SHGFI.SmallIcon | SHGFI.UseFileAttributes;
                else
                    flags = SHGFI.Icon | SHGFI.LargeIcon | SHGFI.UseFileAttributes;

                SHGetFileInfo(strPath, 0, out info, (uint)cbFileInfo, flags);
                return Icon.FromHandle(info.hIcon);
            }
            catch
            {
                if (bSmall)
                    return Icon.FromHandle(new Bitmap(16, 16).GetHicon());
                else
                    return Icon.FromHandle(new Bitmap(32, 32).GetHicon());
            }
        }
    }
}
