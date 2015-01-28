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

        const string OPENFROMFILEHISTORY = "Open from file history...";
        const string OPENFROMCURRENTFOLDERSTRUCTURE = "Open from current folder structure...";
        const string OPENLASTCLOSEDFILE = "Open last closed file";
        static List<string> TbbFunctions = new List<string>()
        {
            "None",
            OPENFROMFILEHISTORY,
            OPENFROMCURRENTFOLDERSTRUCTURE,
            OPENLASTCLOSEDFILE
        };
        static int idTbbFunction;
        static Bitmap tbBmp = Properties.Resources.search;

        static string pluginFolder;
        static string pluginConfigFolder;

        static string iniFilePath;
        internal static int windowWidth;
        internal static int windowHeight;
        internal static int maxHistoryLength;
        internal static bool autoCheckFilesExist;
        internal static bool caseSensitiveSearch;
        internal static List<string> includedFileExts;
        internal static List<string> excludedDirs;

        static string historyFilePath;
        internal static List<string> RecentFiles = new List<string>() { };

        internal enum FilePathFormat
        {
            FullPathFileNameFirst,
            FullPath,
            RelativePathFileNameFirst,
            RelativePath
        }
        internal static FilePathFormat filePathFormat;
        #endregion

        #region " StartUp/CleanUp "
        internal static void CommandMenuInit()
        {
            PluginBase.SetCommand(0, OPENFROMFILEHISTORY, OpenFromFileHistory,
                new ShortcutKey(true, false, true, Keys.H));
            PluginBase.SetCommand(1, OPENFROMCURRENTFOLDERSTRUCTURE, OpenFromCurrentFolderStructure,
                new ShortcutKey(true, false, true, Keys.O));
            PluginBase.SetCommand(2, OPENLASTCLOSEDFILE, OpenLastClosedFile,
                new ShortcutKey(true, false, true, Keys.L));
            PluginBase.SetCommand(3, "", null);
            PluginBase.SetCommand(4, "Options", ShowOptions);
            PluginBase.SetCommand(5, "", null);
            PluginBase.SetCommand(6, "Help", ShowHelp);
            PluginBase.SetCommand(7, "About", ShowAbout);
        }
        internal static void SetToolBarIcon()
        {
            if (idTbbFunction > 0)
            {
                toolbarIcons tbIcons = new toolbarIcons();
                tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
                IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
                Marshal.StructureToPtr(tbIcons, pTbIcons, false);
                Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ADDTOOLBARICON,
                    PluginBase._funcItems.Items[idTbbFunction - 1]._cmdID, pTbIcons);
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
            windowWidth = Win32.GetPrivateProfileInt("Window", "Width", 0, iniFilePath);
            if (windowWidth < 0) windowWidth = 0;
            windowHeight = Win32.GetPrivateProfileInt("Window", "Height", 0, iniFilePath);
            if (windowHeight < 0) windowHeight = 0;
            idTbbFunction = Win32.GetPrivateProfileInt("Options", "TbbFunction", 1/*OPENFROMFILEHISTORY*/, iniFilePath);
            maxHistoryLength = Win32.GetPrivateProfileInt("Options", "MaxHistoryLength", 500, iniFilePath);
            caseSensitiveSearch = (Win32.GetPrivateProfileInt("Options", "CaseSensitiveSearch", 0, iniFilePath) == 1);
            autoCheckFilesExist = (Win32.GetPrivateProfileInt("Options", "AutoCheckFilesExist", 0, iniFilePath) == 1);
            filePathFormat = (FilePathFormat)Win32.GetPrivateProfileInt("Options", "FilePathFormat", 0, iniFilePath);

            string filterFilePath = Path.Combine(pluginConfigFolder, PluginName + ".included-file-exts.txt");
            includedFileExts = File.Exists(filterFilePath) ?
                new List<string>(File.ReadAllLines(filterFilePath)) :
                new List<string>() { };
            filterFilePath = Path.Combine(pluginConfigFolder, PluginName + ".excluded-dirs.txt");
            excludedDirs = File.Exists(filterFilePath) ?
                new List<string>(File.ReadAllLines(filterFilePath)) :
                new List<string>() { };

            historyFilePath = Path.Combine(pluginConfigFolder, PluginName + ".history.txt");
            if (File.Exists(historyFilePath))
            {
                RecentFiles = new List<string>(File.ReadAllLines(historyFilePath));
            }
        }
        internal static void SaveSettings()
        {
            Win32.WritePrivateProfileString("Window", "Width", windowWidth.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Window", "Height", windowHeight.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "TbbFunction", idTbbFunction.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "MaxHistoryLength", maxHistoryLength.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "CaseSensitiveSearch", caseSensitiveSearch ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Options", "AutoCheckFilesExist", autoCheckFilesExist ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Options", "FilePathFormat", ((int)filePathFormat).ToString(), iniFilePath);

            File.WriteAllLines(Path.Combine(pluginConfigFolder, PluginName + ".included-file-exts.txt"), includedFileExts);
            File.WriteAllLines(Path.Combine(pluginConfigFolder, PluginName + ".excluded-dirs.txt"), excludedDirs);

            File.WriteAllLines(historyFilePath, RecentFiles);
        }
        #endregion

        #region " Menu functions "
        internal static void OpenFromFileHistory()
        {
            try
            {
                string filePath = OpenFromStringList("File history", RecentFiles);
                if (!string.IsNullOrEmpty(filePath))
                {
                    RecentFiles.Remove(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, PluginName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void OpenFromCurrentFolderStructure()
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
        internal static void OpenLastClosedFile()
        {
            try
            {
                if (RecentFiles.Count > 0)
                {
                    string filePath = RecentFiles[0];
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, filePath);
                        RecentFiles.Remove(filePath);
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

            frmOptions.cbxTbbFunction.Items.AddRange(TbbFunctions.ToArray());
            frmOptions.cbxTbbFunction.SelectedIndex = idTbbFunction;
            frmOptions.cbxCaseSensitiveSearch.Checked = caseSensitiveSearch;
            foreach (string frmt in Enum.GetNames(typeof(FilePathFormat)))
            {
                frmOptions.cbxFilePathFormat.Items.Add(frmt);
            }
            frmOptions.cbxFilePathFormat.SelectedIndex = (int)filePathFormat;

            frmOptions.nudMaxHistoryLength.Value = maxHistoryLength;
            frmOptions.cbxAutoCheckFilesExist.Checked = autoCheckFilesExist;

            frmOptions.tbxIncludedFileExts.Lines = includedFileExts.ToArray();
            frmOptions.tbxExcludedDirs.Lines = excludedDirs.ToArray();

            if (frmOptions.ShowDialog() == DialogResult.OK)
            {
                idTbbFunction = frmOptions.cbxTbbFunction.SelectedIndex;
                caseSensitiveSearch = frmOptions.cbxCaseSensitiveSearch.Checked;
                filePathFormat = (FilePathFormat)frmOptions.cbxFilePathFormat.SelectedIndex;

                maxHistoryLength = (int)frmOptions.nudMaxHistoryLength.Value;
                if (RecentFiles.Count > maxHistoryLength)
                {
                    RecentFiles.RemoveRange(maxHistoryLength,
                        RecentFiles.Count - maxHistoryLength);
                }
                autoCheckFilesExist = frmOptions.cbxAutoCheckFilesExist.Checked;

                includedFileExts = new List<string>(frmOptions.tbxIncludedFileExts.Lines);
                excludedDirs = new List<string>(frmOptions.tbxExcludedDirs.Lines);
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
        internal static string OpenFromStringList(string frmTitlePrefix, List<string> lstFiles)
        {
            frmOpenFile frmOpenFile = new frmOpenFile(frmTitlePrefix, RecentFiles);
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
        #endregion
    }
}