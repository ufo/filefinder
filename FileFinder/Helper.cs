using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FileFinder
{
    class FileMaskMatcher
    {
        public enum MatchType
        {
            FilePath,
            Directory,
            RegEx
        }

        List<Regex> fileNameMasks;
        List<Regex> dirNameMasks;
        List<Regex> regexMasks;
        public FileMaskMatcher(List<string> fileMaskList)
        {
            fileNameMasks = new List<Regex>();
            dirNameMasks = new List<Regex>();
            regexMasks = new List<Regex>();
            foreach (string _mask in fileMaskList)
            {
                string mask = _mask;
                if (mask != null)
                {
                    mask = mask.Trim();
                }
                if (string.IsNullOrEmpty(mask))
                {
                    continue;
                }
                if (!mask.StartsWith(":"))
                {
                    string expandedMask = Environment.ExpandEnvironmentVariables(mask);

                    string convertedMask = Regex.Escape(expandedMask).Replace("\\*", ".*").Replace("\\?", ".");
                    fileNameMasks.Add(new Regex("^" + convertedMask + "$", RegexOptions.IgnoreCase));

                    if (!expandedMask.Contains(":") && !expandedMask.StartsWith(@"\"))
                    {
                        expandedMask = @"\" + expandedMask;
                    }
                    if (!expandedMask.EndsWith(@"\"))
                    {
                        expandedMask += @"\";
                    }
                    convertedMask = Regex.Escape(expandedMask).Replace("\\*", ".*").Replace("\\?", ".");
                    dirNameMasks.Add(new Regex(convertedMask, RegexOptions.IgnoreCase));
                }
                else
                {
                    regexMasks.Add(new Regex(mask.Substring(1)));
                }
            }
        }
        public bool IsMatch(string name, MatchType matchType)
        {
            if (matchType == MatchType.Directory)
            {
                if (IsMatch(name, MatchType.RegEx))
                {
                    return true;
                }

                string dir = name;
                if (!dir.EndsWith(@"\"))
                {
                    dir += @"\";
                }
                foreach (Regex dirNameMask in dirNameMasks)
                {
                    if (dirNameMask.IsMatch(dir))
                    {
                        return true;
                    }
                }
            }
            else if (matchType == MatchType.FilePath)
            {
                if (IsMatch(name, MatchType.RegEx))
                {
                    return true;
                }

                string fname = Path.GetFileName(name);
                foreach (Regex fileNameMask in fileNameMasks)
                {
                    if (fileNameMask.IsMatch(fname))
                    {
                        return true;
                    }
                }

                string dir = Path.GetDirectoryName(name) + @"\";
                foreach (Regex dirNameMask in dirNameMasks)
                {
                    if (dirNameMask.IsMatch(dir))
                    {
                        return true;
                    }
                }
            }
            else if (matchType == MatchType.RegEx)
            {
                foreach (Regex regexMask in regexMasks)
                {
                    if (regexMask.IsMatch(name))
                    {
                        return true;
                    }
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

    public class FolderSelectDialog
    {
        // taken from:
        // http://www.lyquidity.com/devblog/?p=136
        OpenFileDialog ofd = null;
        public FolderSelectDialog()
        {
            ofd = new System.Windows.Forms.OpenFileDialog();

            ofd.Filter = "Folders|\n";
            ofd.AddExtension = false;
            ofd.CheckFileExists = false;
            ofd.DereferenceLinks = true;
            ofd.Multiselect = false;
        }
        public string InitialDirectory
        {
            get { return ofd.InitialDirectory; }
            set { ofd.InitialDirectory = value == null || value.Length == 0 ? Environment.CurrentDirectory : value; }
        }
        public string Title
        {
            get { return ofd.Title; }
            set { ofd.Title = value == null ? "Select a folder" : value; }
        }
        public string FileName
        {
            get { return ofd.FileName; }
        }
        public bool ShowDialog()
        {
            return ShowDialog(IntPtr.Zero);
        }
        public bool ShowDialog(IntPtr hWndOwner)
        {
            bool flag = false;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                Reflector r = new Reflector("System.Windows.Forms");

                uint num = 0;
                Type typeIFileDialog = r.GetType("FileDialogNative.IFileDialog");
                object dialog = r.Call(ofd, "CreateVistaDialog");
                r.Call(ofd, "OnBeforeVistaDialog", dialog);

                uint options = (uint)r.CallAs(typeof(System.Windows.Forms.FileDialog), ofd, "GetOptions");
                options |= (uint)r.GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS");
                r.CallAs(typeIFileDialog, dialog, "SetOptions", options);

                object pfde = r.New("FileDialog.VistaDialogEvents", ofd);
                object[] parameters = new object[] { pfde, num };
                r.CallAs2(typeIFileDialog, dialog, "Advise", parameters);
                num = (uint)parameters[1];
                try
                {
                    int num2 = (int)r.CallAs(typeIFileDialog, dialog, "Show", hWndOwner);
                    flag = 0 == num2;
                }
                finally
                {
                    r.CallAs(typeIFileDialog, dialog, "Unadvise", num);
                    GC.KeepAlive(pfde);
                }
            }
            else
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = this.Title;
                fbd.SelectedPath = this.InitialDirectory;
                fbd.ShowNewFolderButton = false;
                if (fbd.ShowDialog(new WindowWrapper(hWndOwner)) != DialogResult.OK) return false;
                ofd.FileName = fbd.SelectedPath;
                flag = true;
            }

            return flag;
        }
    }

    public class WindowWrapper : IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }
        public IntPtr Handle
        {
            get { return _hwnd; }
        }
        private IntPtr _hwnd;
    }

    public class Reflector
    {
        string m_ns;
        Assembly m_asmb;
        public Reflector(string ns) : this(ns, ns) { }
        public Reflector(string an, string ns)
        {
            m_ns = ns;
            m_asmb = null;
            foreach (AssemblyName aN in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                if (aN.FullName.StartsWith(an))
                {
                    m_asmb = Assembly.Load(aN);
                    break;
                }
            }
        }
        public Type GetType(string typeName)
        {
            Type type = null;
            string[] names = typeName.Split('.');

            if (names.Length > 0)
                type = m_asmb.GetType(m_ns + "." + names[0]);

            for (int i = 1; i < names.Length; ++i)
            {
                type = type.GetNestedType(names[i], BindingFlags.NonPublic);
            }
            return type;
        }
        public object New(string name, params object[] parameters)
        {
            Type type = GetType(name);

            ConstructorInfo[] ctorInfos = type.GetConstructors();
            foreach (ConstructorInfo ci in ctorInfos)
            {
                try
                {
                    return ci.Invoke(parameters);
                }
                catch { }
            }

            return null;
        }
        public object Call(object obj, string func, params object[] parameters)
        {
            return Call2(obj, func, parameters);
        }
        public object Call2(object obj, string func, object[] parameters)
        {
            return CallAs2(obj.GetType(), obj, func, parameters);
        }
        public object CallAs(Type type, object obj, string func, params object[] parameters)
        {
            return CallAs2(type, obj, func, parameters);
        }
        public object CallAs2(Type type, object obj, string func, object[] parameters)
        {
            MethodInfo methInfo = type.GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return methInfo.Invoke(obj, parameters);
        }
        public object Get(object obj, string prop)
        {
            return GetAs(obj.GetType(), obj, prop);
        }
        public object GetAs(Type type, object obj, string prop)
        {
            PropertyInfo propInfo = type.GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return propInfo.GetValue(obj, null);
        }
        public object GetEnum(string typeName, string name)
        {
            Type type = GetType(typeName);
            FieldInfo fieldInfo = type.GetField(name);
            return fieldInfo.GetValue(null);
        }
    }

}
