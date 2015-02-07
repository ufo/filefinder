using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NppPluginNET;

namespace FileFinder
{
    class Main
    {
        #region " Fields "
        internal const string PluginName = "FileFinder";

        internal static string PluginDir;
        static string pluginConfigDir;
        static string iniFilePath;

        const string OPEN_FROM_DIRECTORY_GREEDY = "Open from directory (greedy)...";
        const string SEARCH_IN_DIRECTORY_EXPLICITLY = "Search in directory (explicitly)...";
        const string OPEN_FROM_FILE_HISTORY = "Open from file history...";
        const string OPEN_LAST_CLOSED_FILE = "Open last closed file";
        static Bitmap tbBmpOpenFromDirectoryGreedy = Properties.Resources.open_from_directory_greedy;
        static Bitmap tbBmpSearchInDirectoryExplicitly = Properties.Resources.search_in_directory_explicitly;
        static Bitmap tbBmpOpenFromFileHistory = Properties.Resources.open_from_file_history;
        static Bitmap tbBmpOpenLastClosedFile = Properties.Resources.open_last_closed_file;

        internal static int OpenFileDialogWidth;
        internal static int OpenFileDialogHeight;

        static bool showTbOpenFromDirectoryGreedy;
        static bool showTbSearchInDirectoryExplicitly;
        static bool showTbOpenFromFileHistory;
        static bool showTbOpenLastClosedFile;

        internal static bool CaseSensitiveSearch;

        const string PATH_EXT_DIR_SEARCH_EXCLUSIONS = ".dir-search-exclusions.txt";
        internal static List<string> DirSearchExclusions;
        static string[] DEFAULT_DIR_SEARCH_EXCLUSIONS = new string[]
        {
            ".git", ".hg", ".svn",
            "*.com", "*.dll", "*.exe", "*.lib",
            "*.obj", "*.pyc", "*.pyd", "*.pyo"
        };
        const string PATH_EXT_DIR_SEARCH_PATTERNS = ".dir-search-patterns.txt";
        internal static List<string> LastSearchPatterns;
        internal const int MAX_LAST_SEARCH_PATTERNS = 100;
        internal static bool ShowFilteredPaths = true;
        internal static bool BypassFSR = true;

        const string PATH_EXT_HISTORY_FILES = ".history-files.txt";
        internal static List<string> HistoryFiles;
        const string PATH_EXT_HISTORY_EXCLUSIONS = ".history-exclusions.txt";
        internal static List<string> HistoryExclusions;
        static string[] DEFAULT_HISTORY_EXCLUSIONS = new string[] { "%temp%" };
        internal static int MaxHistoryLength;
        internal static bool AutoValidateFilenames;

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
            PluginBase.SetCommand(0, OPEN_FROM_DIRECTORY_GREEDY, OpenFromDirectoryGreedy, new ShortcutKey(true, false, true, Keys.O));
            PluginBase.SetCommand(1, SEARCH_IN_DIRECTORY_EXPLICITLY, SearchInDirectoryExplicitly, new ShortcutKey(true, true, true, Keys.O));
            PluginBase.SetCommand(2, "", null);
            PluginBase.SetCommand(3, OPEN_FROM_FILE_HISTORY, OpenFromFileHistory, new ShortcutKey(true, false, true, Keys.H));
            PluginBase.SetCommand(4, OPEN_LAST_CLOSED_FILE, OpenLastClosedFile, new ShortcutKey(true, false, true, Keys.L));
            PluginBase.SetCommand(5, "", null);
            PluginBase.SetCommand(6, "Options", ShowOptions);
            PluginBase.SetCommand(7, "", null);
            PluginBase.SetCommand(8, "Help", ShowHelp);
            PluginBase.SetCommand(9, "About", ShowAbout);
        }
        static void SetToolBarIcon(int id, Bitmap bmp)
        {
            toolbarIcons tbIcons = new toolbarIcons();
            tbIcons.hToolbarBmp = bmp.GetHbitmap();
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ADDTOOLBARICON,
                PluginBase._funcItems.Items[id]._cmdID, pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
        }
        internal static void SetToolBarIcon()
        {
            if (showTbOpenFromDirectoryGreedy)
                SetToolBarIcon(0, tbBmpOpenFromDirectoryGreedy);
            if (showTbSearchInDirectoryExplicitly)
                SetToolBarIcon(1, tbBmpSearchInDirectoryExplicitly);
            if (showTbOpenFromFileHistory)
                SetToolBarIcon(3, tbBmpOpenFromFileHistory);
            if (showTbOpenLastClosedFile)
                SetToolBarIcon(4, tbBmpOpenLastClosedFile);
        }
        internal static void LoadSettings()
        {
            PluginDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            StringBuilder sbPluginDir = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR,
                Win32.MAX_PATH, sbPluginDir);
            pluginConfigDir = Path.Combine(sbPluginDir.ToString(), PluginName);
            if (!Directory.Exists(pluginConfigDir)) Directory.CreateDirectory(pluginConfigDir);

            iniFilePath = Path.Combine(pluginConfigDir, PluginName + ".ini");
            OpenFileDialogWidth = Win32.GetPrivateProfileInt("OpenFileDialog", "Width", 0, iniFilePath);
            if (OpenFileDialogWidth < 0) OpenFileDialogWidth = 0;
            OpenFileDialogHeight = Win32.GetPrivateProfileInt("OpenFileDialog", "Height", 0, iniFilePath);
            if (OpenFileDialogHeight < 0) OpenFileDialogHeight = 0;

            showTbOpenFromDirectoryGreedy = (Win32.GetPrivateProfileInt("Toolbar", "OpenFromDirectoryGreedy", 0, iniFilePath) == 1);
            showTbSearchInDirectoryExplicitly = (Win32.GetPrivateProfileInt("Toolbar", "SearchInDirectoryExplicitly", 1, iniFilePath) == 1);
            showTbOpenFromFileHistory = (Win32.GetPrivateProfileInt("Toolbar", "OpenFromFileHistory", 1, iniFilePath) == 1);
            showTbOpenLastClosedFile = (Win32.GetPrivateProfileInt("Toolbar", "OpenLastClosedFile", 0, iniFilePath) == 1);

            ShowFilteredPaths = (Win32.GetPrivateProfileInt("Options", "ShowFilteredPaths", 0, iniFilePath) == 1);
            BypassFSR = (Win32.GetPrivateProfileInt("Options", "BypassFSR", 0, iniFilePath) == 1);
            MaxHistoryLength = Win32.GetPrivateProfileInt("Options", "MaxHistoryLength", 500, iniFilePath);
            CaseSensitiveSearch = (Win32.GetPrivateProfileInt("Options", "CaseSensitiveSearch", 0, iniFilePath) == 1);
            AutoValidateFilenames = (Win32.GetPrivateProfileInt("Options", "AutoValidateFilenames", 0, iniFilePath) == 1);
            DisplayedFilePathFormat = (FilePathFormat)Win32.GetPrivateProfileInt("Options", "DisplayedFilePathFormat", 0, iniFilePath);

            string configFilePath = Path.Combine(pluginConfigDir, PluginName + PATH_EXT_HISTORY_FILES);
            HistoryFiles = File.Exists(configFilePath) ?
                new List<string>(File.ReadAllLines(configFilePath)) :
                new List<string>();
            configFilePath = Path.Combine(pluginConfigDir, PluginName + PATH_EXT_HISTORY_EXCLUSIONS);
            HistoryExclusions = File.Exists(configFilePath) ?
                new List<string>(File.ReadAllLines(configFilePath)) :
                new List<string>(DEFAULT_HISTORY_EXCLUSIONS);
            configFilePath = Path.Combine(pluginConfigDir, PluginName + PATH_EXT_DIR_SEARCH_EXCLUSIONS);
            DirSearchExclusions = File.Exists(configFilePath) ?
                new List<string>(File.ReadAllLines(configFilePath)) :
                new List<string>(DEFAULT_DIR_SEARCH_EXCLUSIONS);
            configFilePath = Path.Combine(pluginConfigDir, PluginName + PATH_EXT_DIR_SEARCH_PATTERNS);
            LastSearchPatterns = File.Exists(configFilePath) ?
                new List<string>(File.ReadAllLines(configFilePath)) :
                new List<string>();
        }
        internal static void SaveSettings()
        {
            Win32.WritePrivateProfileString("OpenFileDialog", "Width", OpenFileDialogWidth.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("OpenFileDialog", "Height", OpenFileDialogHeight.ToString(), iniFilePath);

            Win32.WritePrivateProfileString("Toolbar", "OpenFromDirectoryGreedy", showTbOpenFromDirectoryGreedy ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Toolbar", "SearchInDirectoryExplicitly", showTbSearchInDirectoryExplicitly ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Toolbar", "OpenFromFileHistory", showTbOpenFromFileHistory ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Toolbar", "OpenLastClosedFile", showTbOpenLastClosedFile ? "1" : "0", iniFilePath);

            Win32.WritePrivateProfileString("Options", "ShowFilteredPaths", ShowFilteredPaths ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Options", "BypassFSR", BypassFSR ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Options", "MaxHistoryLength", MaxHistoryLength.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "CaseSensitiveSearch", CaseSensitiveSearch ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Options", "AutoValidateFilenames", AutoValidateFilenames ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Options", "DisplayedFilePathFormat", ((int)DisplayedFilePathFormat).ToString(), iniFilePath);

            File.WriteAllLines(Path.Combine(pluginConfigDir, PluginName + PATH_EXT_HISTORY_FILES), HistoryFiles);
            File.WriteAllLines(Path.Combine(pluginConfigDir, PluginName + PATH_EXT_HISTORY_EXCLUSIONS), HistoryExclusions);
            File.WriteAllLines(Path.Combine(pluginConfigDir, PluginName + PATH_EXT_DIR_SEARCH_EXCLUSIONS), DirSearchExclusions);
            File.WriteAllLines(Path.Combine(pluginConfigDir, PluginName + PATH_EXT_DIR_SEARCH_PATTERNS), LastSearchPatterns);
        }
        #endregion

        #region " Menu functions "
        internal static void OpenFromDirectoryGreedy()
        {
            try
            {
                uint bufID = (uint)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTBUFFERID, 0, 0);
                string filePath = PluginBase.GetFilePathFromBufferID(bufID);
                string dirPath = "";
                if (File.Exists(filePath))
                {
                    dirPath = Path.GetDirectoryName(filePath);
                }
                else
                {
                    dirPath = Environment.CurrentDirectory;
                }
                API.OpenFromDirectoryGreedy("Recursive file search", dirPath, true);
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
            }
        }
        internal static void SearchInDirectoryExplicitly()
        {
            try
            {
                string rootDir = null;
                uint bufID = (uint)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTBUFFERID, 0, 0);
                string filePath = PluginBase.GetFilePathFromBufferID(bufID);
                if (File.Exists(filePath))
                {
                    rootDir = Path.GetDirectoryName(filePath);
                }
                API.SearchInDirectoryExplicitly("Recursive file search", rootDir, null, true, true);
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
            }
        }
        internal static void OpenFromFileHistory()
        {
            try
            {
                List<string> selectedFiles = API.OpenFromStringListGreedy("File history", HistoryFiles, true);
                foreach (string filePath in selectedFiles)
                {
                    HistoryFiles.Remove(filePath);
                }
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
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

                        if (Main.AutoValidateFilenames)
                        {
                            FileMaskMatcher fileMaskMatcher = new FileMaskMatcher(Main.HistoryExclusions);
                            if (fileMaskMatcher.IsMatch(filePath, FileMaskMatcher.MatchType.FilePath) ||
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
                Dbg.Msg(ex);
            }
        }
        internal static void ShowOptions()
        {
            try
            {
                frmOptions frmOptions = new frmOptions();

                frmOptions.btnOpenFromDirectoryGreedy.Checked = showTbOpenFromDirectoryGreedy;
                frmOptions.btnSearchInDirectoryExplicitly.Checked = showTbSearchInDirectoryExplicitly;
                frmOptions.btnOpenFromFileHistory.Checked = showTbOpenFromFileHistory;
                frmOptions.btnOpenLastClosedFile.Checked = showTbOpenLastClosedFile;
                frmOptions.cbxCaseSensitiveSearch.Checked = CaseSensitiveSearch;
                foreach (string frmt in Enum.GetNames(typeof(FilePathFormat)))
                {
                    frmOptions.cbxDisplayedFilePathFormat.Items.Add(frmt);
                }
                frmOptions.cbxDisplayedFilePathFormat.SelectedIndex = (int)DisplayedFilePathFormat;

                frmOptions.tbxDirSearchExclusions.Lines = DirSearchExclusions.ToArray();
                frmOptions.cbxShowFilteredPaths.Checked = ShowFilteredPaths;
                if (!FileSystemRedirection.IsUsed)
                {
                    frmOptions.cbxBypassFSR.Enabled = false;
                }
                else
                {
                    frmOptions.cbxBypassFSR.Checked = BypassFSR;
                }

                frmOptions.nudMaxHistoryLength.Value = MaxHistoryLength;
                frmOptions.cbxAutoValidateFilenames.Checked = AutoValidateFilenames;
                frmOptions.tbxHistoryExclusions.Lines = HistoryExclusions.ToArray();

                if (frmOptions.ShowDialog() == DialogResult.OK)
                {
                    showTbOpenFromDirectoryGreedy = frmOptions.btnOpenFromDirectoryGreedy.Checked;
                    showTbSearchInDirectoryExplicitly = frmOptions.btnSearchInDirectoryExplicitly.Checked;
                    showTbOpenFromFileHistory = frmOptions.btnOpenFromFileHistory.Checked;
                    showTbOpenLastClosedFile = frmOptions.btnOpenLastClosedFile.Checked;
                    CaseSensitiveSearch = frmOptions.cbxCaseSensitiveSearch.Checked;
                    DisplayedFilePathFormat = (FilePathFormat)frmOptions.cbxDisplayedFilePathFormat.SelectedIndex;

                    DirSearchExclusions = new List<string>(frmOptions.tbxDirSearchExclusions.Lines);
                    ShowFilteredPaths = frmOptions.cbxShowFilteredPaths.Checked;
                    BypassFSR = frmOptions.cbxBypassFSR.Checked;

                    MaxHistoryLength = (int)frmOptions.nudMaxHistoryLength.Value;
                    if (HistoryFiles.Count > MaxHistoryLength)
                    {
                        HistoryFiles.RemoveRange(MaxHistoryLength,
                            HistoryFiles.Count - MaxHistoryLength);
                    }
                    AutoValidateFilenames = frmOptions.cbxAutoValidateFilenames.Checked;
                    HistoryExclusions = new List<string>(frmOptions.tbxHistoryExclusions.Lines);
                }
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
            }
        }
        internal static void ShowHelp()
        {
            try
            {
                string filePath = Path.Combine(PluginDir, "doc", PluginName + ".README.txt");
                Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, filePath);
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
            }
        }
        internal static void ShowAbout()
        {
            try
            {
                new frmAbout().ShowDialog();
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
            }
        }
        #endregion
    }
}