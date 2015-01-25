using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NppPluginNET;
using System.Collections.Generic;
using System.Reflection;

namespace NppFileHistory
{
    class Main
    {
        #region " Fields "
        internal const string PluginName = "NppFileHistory";

        const string OPENFROMFILEHISTORY = "Open from file history...";
        const string OPENLASTCLOSEDFILE = "Open last closed file";
        static List<string> TbbFunctions = new List<string>()
        {
            "None",
            OPENFROMFILEHISTORY,
            OPENLASTCLOSEDFILE
        };
        static int idTbbFunction;
        static Bitmap tbBmp = Properties.Resources.history;

        static string iniFilePath;
        static int windowWidth;
        static int windowHeight;

        static string historyFilePath;
        internal static List<string> RecentFiles = new List<string>() { };
        internal static int maxHistoryLength;

        internal static bool caseSensitiveSearch;

        internal enum FilePathFormat
        {
            FileNameFirst,
            FullPath
        }
        internal static FilePathFormat filePathFormat;
        #endregion

        #region " StartUp/CleanUp "
        internal static void CommandMenuInit()
        {
            PluginBase.SetCommand(0, OPENFROMFILEHISTORY, OpenFromFileHistory,
                new ShortcutKey(true, false, true, Keys.O));
            PluginBase.SetCommand(1, OPENLASTCLOSEDFILE, OpenLastClosedFile,
                new ShortcutKey(true, true, true, Keys.O));
            PluginBase.SetCommand(2, "", null);
            PluginBase.SetCommand(3, "Options", ShowOptions);
            PluginBase.SetCommand(4, "", null);
            PluginBase.SetCommand(5, "About", ShowAbout);
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
            StringBuilder sbPluginFolder = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR,
                Win32.MAX_PATH, sbPluginFolder);
            string pluginFolder = sbPluginFolder.ToString();
            if (!Directory.Exists(pluginFolder)) Directory.CreateDirectory(pluginFolder);

            iniFilePath = Path.Combine(pluginFolder, PluginName + ".ini");
            windowWidth = Win32.GetPrivateProfileInt("Window", "Width", 0, iniFilePath);
            if (windowWidth < 0) windowWidth = 0;
            windowHeight = Win32.GetPrivateProfileInt("Window", "Height", 0, iniFilePath);
            if (windowHeight < 0) windowHeight = 0;
            idTbbFunction = Win32.GetPrivateProfileInt("Options", "TbbFunction", 1/*OPENFROMFILEHISTORY*/, iniFilePath);
            maxHistoryLength = Win32.GetPrivateProfileInt("Options", "MaxHistoryLength", 150, iniFilePath);
            caseSensitiveSearch = (Win32.GetPrivateProfileInt("Options", "CaseSensitiveSearch", 0, iniFilePath) == 1);
            filePathFormat = (Win32.GetPrivateProfileInt("Options", "FilePathFormat", 0, iniFilePath) == 0) ?
                FilePathFormat.FileNameFirst : FilePathFormat.FullPath;

            historyFilePath = Path.Combine(pluginFolder, PluginName + ".txt");
            if (File.Exists(historyFilePath))
            {
                RecentFiles = new List<string>(File.ReadAllLines(historyFilePath));
            }
        }
        internal static void SaveSettings()
        {
            File.WriteAllLines(historyFilePath, RecentFiles);

            Win32.WritePrivateProfileString("Window", "Width", windowWidth.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Window", "Height", windowHeight.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "TbbFunction", idTbbFunction.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "MaxHistoryLength", maxHistoryLength.ToString(), iniFilePath);
            Win32.WritePrivateProfileString("Options", "CaseSensitiveSearch", caseSensitiveSearch ? "1" : "0", iniFilePath);
            Win32.WritePrivateProfileString("Options", "FilePathFormat", ((int)filePathFormat).ToString(), iniFilePath);
        }
        #endregion

        #region " Menu functions "
        internal static void OpenFromFileHistory()
        {
            try
            {
                frmOpenFile frmOpenFile = new frmOpenFile(RecentFiles);
                frmOpenFile.Width = windowWidth;
                frmOpenFile.Height = windowHeight;
                if (frmOpenFile.ShowDialog() == DialogResult.OK)
                {
                    string filePath = frmOpenFile.lblFullSelectedPath.Text;
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, filePath);
                        RecentFiles.Remove(filePath);
                    }
                }
                windowWidth = frmOpenFile.Width;
                windowHeight = frmOpenFile.Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }
        internal static void ShowOptions()
        {
            frmOptions frmOptions = new frmOptions();
            frmOptions.nudMaxHistoryLength.Value = maxHistoryLength;
            frmOptions.cbxTbbFunction.Items.AddRange(TbbFunctions.ToArray());
            frmOptions.cbxTbbFunction.SelectedIndex = idTbbFunction;
            frmOptions.cbxCaseSensitiveSearch.Checked = caseSensitiveSearch;
            frmOptions.cbxFilePathFormat.Items.Add(FilePathFormat.FileNameFirst.ToString());
            frmOptions.cbxFilePathFormat.Items.Add(FilePathFormat.FullPath.ToString());
            frmOptions.cbxFilePathFormat.SelectedIndex = (int)filePathFormat;
            if (frmOptions.ShowDialog() == DialogResult.OK)
            {
                maxHistoryLength = (int)frmOptions.nudMaxHistoryLength.Value;
                if (RecentFiles.Count > maxHistoryLength)
                {
                    RecentFiles.RemoveRange(maxHistoryLength,
                        RecentFiles.Count - maxHistoryLength);
                }
                idTbbFunction = frmOptions.cbxTbbFunction.SelectedIndex;
                caseSensitiveSearch = frmOptions.cbxCaseSensitiveSearch.Checked;
                filePathFormat = (FilePathFormat)frmOptions.cbxFilePathFormat.SelectedIndex;
            }
        }
        internal static void ShowAbout()
        {
            string txt = string.Format("Version: {0}\nAuthor: ufo",
                Assembly.GetExecutingAssembly().GetName().Version.ToString(2));
            MessageBox.Show(txt, PluginName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}