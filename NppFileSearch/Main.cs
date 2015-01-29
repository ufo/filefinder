using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NppPluginNET;

namespace NppFileSearch
{
    class Main
    {
        #region " Fields "
        internal const string PluginName = "NppFileSearch";

        const string OPEN_FROM_CURRENT_DIRECTORY_GREEDY = "Open from directory (greedy)...";
        const string OPEN_FROM_CURRENT_DIRECTORY_EXPLICIT = "Open from directory (explicit)...";
        const string OPEN_FROM_FILE_HISTORY = "Open from file history...";
        const string OPEN_LAST_CLOSED_FILE = "Open last closed file";
        enum ToolbarButtonFunctions
        {
            OpenFromDirectoryGreedy = 0,
            OpenFromDirectoryExplicit = 1,
            // separator = 2,
            OpenFromFileHistory = 3,
            OpenLastClosedFile = 4
        }
        static int tbbFunction;
        static Bitmap tbBmp = Properties.Resources.search;

        static string pluginFolder;
        static string pluginConfigFolder;

        static string iniFilePath;
        internal static int WindowWidth;
        internal static int WindowHeight;
        
        internal static bool CaseSensitiveSearch;

        const string PATH_EXT_DIR_SEARCH_EXCLUSIONS = ".dir-search-exclusions.txt";
        internal static List<string> DirSearchExclusions;
        static string[] DEFAULT_DIR_SEARCH_EXCLUSIONS = new string[]
        {
            ".git", ".hg", ".svn",
            "*.com", "*.dll", "*.exe", "*.lib",
            "*.obj", "*.pyc", "*.pyd", "*.pyo"
        };

        const string PATH_EXT_HISTORY_FILES = ".history-files.txt";
        internal static List<string> HistoryFiles;
        const string PATH_EXT_HISTORY_EXCLUSIONS = ".history-exclusions.txt";
        internal static List<string> HistoryExclusions;
        static string[] DEFAULT_HISTORY_EXCLUSIONS = new string[] { "%temp%" };
        internal static int MaxHistoryLength;
        internal static bool AutoInvalidateFilename;

        internal enum FilePathFormat
        {
            FullPathFileNameFirst,
            FullPath,
            RelativePathFileNameFirst,
            RelativePath
        }
        internal static FilePathFormat DisplayedFilePathFormat;
        #endregion

        #region " StartUp/CleanUp "
        internal static void CommandMenuInit()
        {
            PluginBase.SetCommand((int)ToolbarButtonFunctions.OpenFromDirectoryGreedy,
                OPEN_FROM_CURRENT_DIRECTORY_GREEDY, OpenFromCurrentDirectoryGreedy,
                new ShortcutKey(true, false, true, Keys.O));
            PluginBase.SetCommand((int)ToolbarButtonFunctions.OpenFromDirectoryExplicit,
                OPEN_FROM_CURRENT_DIRECTORY_EXPLICIT, OpenFromCurrentDirectoryExplicit,
                new ShortcutKey(true, true, true, Keys.O));
            PluginBase.SetCommand(2, "", null);
            PluginBase.SetCommand((int)ToolbarButtonFunctions.OpenFromFileHistory,
                OPEN_FROM_FILE_HISTORY, OpenFromFileHistory,
                new ShortcutKey(true, false, true, Keys.H));
            PluginBase.SetCommand((int)ToolbarButtonFunctions.OpenLastClosedFile,
                OPEN_LAST_CLOSED_FILE, OpenLastClosedFile,
                new ShortcutKey(true, false, true, Keys.L));
            PluginBase.SetCommand(5, "", null);
            PluginBase.SetCommand(6, "Options", ShowOptions);
            PluginBase.SetCommand(7, "", null);
            PluginBase.SetCommand(8, "Help", ShowHelp);
            PluginBase.SetCommand(9, "About", ShowAbout);
        }
        internal static void SetToolBarIcon()
        {
            if (tbbFunction >= 0)
            {
                toolbarIcons tbIcons = new toolbarIcons();
                tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
                IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
                Marshal.StructureToPtr(tbIcons, pTbIcons, false);
                Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ADDTOOLBARICON,
                    PluginBase._funcItems.Items[tbbFunction]._cmdID, pTbIcons);
                Marshal.FreeHGlobal(pTbIcons);
            }
        }
        internal static void LoadSettings()
        {
            pluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            StringBuilder sbPluginFolder = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR,
                Win32.MAX_PATH, sbPluginFolder);
            pluginConfigFolder = Path.Combine(sbPluginFolder.ToString(), PluginName);
            if (!Directory.Exists(pluginConfigFolder)) Directory.CreateDirectory(pluginConfigFolder);

            iniFilePath = Path.Combine(pluginConfigFolder, PluginName + ".ini");
            WindowWidth = Win32.GetPrivateProfileInt("Window", "Width", 0, iniFilePath);
            if (WindowWidth < 0) WindowWidth = 0;
            WindowHeight = Win32.GetPrivateProfileInt("Window", "Height", 0, iniFilePath);
            if (WindowHeight < 0) WindowHeight = 0;
            tbbFunction = Win32.GetPrivateProfileInt("Options", "TbbFunction",
                (int)ToolbarButtonFunctions.OpenFromFileHistory, iniFilePath);
            MaxHistoryLength = Win32.GetPrivateProfileInt("Options", "MaxHistoryLength", 500, iniFilePath);
            CaseSensitiveSearch = (Win32.GetPrivateProfileInt("Options", "CaseSensitiveSearch", 0, iniFilePath) == 1);
            AutoInvalidateFilename = (Win32.GetPrivateProfileInt("Options", "AutoInvalidateFilename", 0, iniFilePath) == 1);
            DisplayedFilePathFormat = (FilePathFormat)Win32.GetPrivateProfileInt("Options", "DisplayedFilePathFormat", 0, iniFilePath);

            string configFilePath = Path.Combine(pluginConfigFolder, PluginName + PATH_EXT_HISTORY_FILES);
            HistoryFiles = File.Exists(configFilePath) ?
                new List<string>(File.ReadAllLines(configFilePath)) :
                new List<string>();
            configFilePath = Path.Combine(pluginConfigFolder, PluginName + PATH_EXT_HISTORY_EXCLUSIONS);
            HistoryExclusions = File.Exists(configFilePath) ?
                new List<string>(File.ReadAllLines(configFilePath)) :
                new List<string>(DEFAULT_HISTORY_EXCLUSIONS);
            configFilePath = Path.Combine(pluginConfigFolder, PluginName + PATH_EXT_DIR_SEARCH_EXCLUSIONS);
            DirSearchExclusions = File.Exists(configFilePath) ?
                new List<string>(File.ReadAllLines(configFilePath)) :
                new List<string>(DEFAULT_DIR_SEARCH_EXCLUSIONS);
        }
        internal static void SaveSettings()
        {
            Win32.WritePrivateProfileString("Window", "Width", WindowWidth.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Window", "Height", WindowHeight.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "TbbFunction", tbbFunction.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "MaxHistoryLength", MaxHistoryLength.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "CaseSensitiveSearch", CaseSensitiveSearch ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Options", "AutoInvalidateFilename", AutoInvalidateFilename ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Options", "DisplayedFilePathFormat", ((int)DisplayedFilePathFormat).ToString(), iniFilePath);

            File.WriteAllLines(Path.Combine(pluginConfigFolder, PluginName + PATH_EXT_HISTORY_FILES), HistoryFiles);
            File.WriteAllLines(Path.Combine(pluginConfigFolder, PluginName + PATH_EXT_HISTORY_EXCLUSIONS), HistoryExclusions);
            File.WriteAllLines(Path.Combine(pluginConfigFolder, PluginName + PATH_EXT_DIR_SEARCH_EXCLUSIONS), DirSearchExclusions);
        }
        #endregion

        #region " Menu functions "
        internal static void OpenFromCurrentDirectoryGreedy()
        {
            try
            {
                uint bufID = (uint)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTBUFFERID, 0, 0);
                string filePath = PluginBase.GetFilePathFromBufferID(bufID);
                string folderPath = "";
                if (File.Exists(filePath))
                {
                    folderPath = Path.GetDirectoryName(filePath);
                }
                else
                {
                    folderPath = Environment.CurrentDirectory;
                }
                frmOpenFile frmOpenFile = new frmOpenFile("Current folder structure", folderPath);
                if (frmOpenFile.ShowDialog() == DialogResult.OK)
                {
                    filePath = frmOpenFile.tbxFullSelectedPath.Text;
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, PluginName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void OpenFromCurrentDirectoryExplicit()
        {
        }
        internal static void OpenFromFileHistory()
        {
            try
            {
                string filePath = OpenFromStringList("File history", HistoryFiles);
                if (!string.IsNullOrEmpty(filePath))
                {
                    HistoryFiles.Remove(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, PluginName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void OpenLastClosedFile()
        {
            try
            {
                if (HistoryFiles.Count > 0)
                {
                    string filePath = HistoryFiles[0];
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        HistoryFiles.Remove(filePath);

                        if (Main.AutoInvalidateFilename)
                        {
                            FileMaskMatcher fileMaskMatcher = new FileMaskMatcher(Main.HistoryExclusions);
                            if (fileMaskMatcher.IsMatch(filePath, FileMaskMatcher.MatchType.FullPath) ||
                                !File.Exists(filePath))
                            {
                                OpenLastClosedFile();
                                return;
                            }
                        }

                        Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, PluginName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void ShowOptions()
        {
            frmOptions frmOptions = new frmOptions();

            frmOptions.cbxTbbFunction.Items.Add("");
            foreach (string func in Enum.GetNames(typeof(ToolbarButtonFunctions)))
            {
                frmOptions.cbxTbbFunction.Items.Add(func);
            }
            frmOptions.cbxTbbFunction.SelectedIndex = tbbFunction;
            frmOptions.cbxCaseSensitiveSearch.Checked = CaseSensitiveSearch;
            foreach (string frmt in Enum.GetNames(typeof(FilePathFormat)))
            {
                frmOptions.cbxDisplayedFilePathFormat.Items.Add(frmt);
            }
            frmOptions.cbxDisplayedFilePathFormat.SelectedIndex = (int)DisplayedFilePathFormat;

            frmOptions.nudMaxHistoryLength.Value = MaxHistoryLength;
            frmOptions.cbxAutoInvalidateFilename.Checked = AutoInvalidateFilename;
            frmOptions.tbxHistoryExclusions.Lines = HistoryExclusions.ToArray();

            frmOptions.tbxDirSearchExclusions.Lines = DirSearchExclusions.ToArray();

            if (frmOptions.ShowDialog() == DialogResult.OK)
            {
                tbbFunction = (frmOptions.cbxTbbFunction.SelectedIndex > 0) ?
                    (int)Enum.Parse(typeof(ToolbarButtonFunctions), frmOptions.cbxTbbFunction.Text) :
                    -1;
                CaseSensitiveSearch = frmOptions.cbxCaseSensitiveSearch.Checked;
                DisplayedFilePathFormat = (FilePathFormat)frmOptions.cbxDisplayedFilePathFormat.SelectedIndex;

                MaxHistoryLength = (int)frmOptions.nudMaxHistoryLength.Value;
                if (HistoryFiles.Count > MaxHistoryLength)
                {
                    HistoryFiles.RemoveRange(MaxHistoryLength,
                        HistoryFiles.Count - MaxHistoryLength);
                }
                AutoInvalidateFilename = frmOptions.cbxAutoInvalidateFilename.Checked;
                HistoryExclusions = new List<string>(frmOptions.tbxHistoryExclusions.Lines);

                DirSearchExclusions = new List<string>(frmOptions.tbxDirSearchExclusions.Lines);
            }
        }
        internal static void ShowHelp()
        {
            string filePath = Path.Combine(pluginFolder, "doc", PluginName + ".README.txt");
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, filePath);
        }
        internal static void ShowAbout()
        {
            string txt = string.Format("Version: {0}\nAuthor: ufo",
                Assembly.GetExecutingAssembly().GetName().Version.ToString(2));
            MessageBox.Show(txt, PluginName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region " API functions "
        internal static void OpenFromDirectory(string frmTitlePrefix, string folderPath)
        {
            frmOpenFile frmOpenFile = new frmOpenFile(frmTitlePrefix, folderPath);
            if (frmOpenFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = frmOpenFile.tbxFullSelectedPath.Text;
                if (!string.IsNullOrEmpty(filePath))
                {
                    Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, filePath);
                }
            }
        }
        internal static string OpenFromStringList(string frmTitlePrefix, List<string> lstFiles)
        {
            frmOpenFile frmOpenFile = new frmOpenFile(frmTitlePrefix, HistoryFiles);
            if (frmOpenFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = frmOpenFile.tbxFullSelectedPath.Text;
                if (!string.IsNullOrEmpty(filePath))
                {
                    Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, filePath);
                    return filePath;
                }
            }
            return null;
        }
        #endregion
    }
}