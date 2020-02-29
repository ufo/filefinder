using NppPluginNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FileFinder
{
    class API
    {
        internal const int NPPM_FILEFINDER_OPEN_FROM_DIRECTORY_GREEDY = 0x0101;
        // NppmFileFinderInfo.szDirPath
        // NppmFileFinderInfo.bOpenFiles
        // NppmFileFinderInfo.arrSelectedFilePaths

        internal const int NPPM_FILEFINDER_OPEN_FROM_STRINGLIST_GREEDY = 0x0102;
        // NppmFileFinderInfo.arrFilePaths
        // NppmFileFinderInfo.bOpenFiles
        // NppmFileFinderInfo.arrSelectedFilePaths

        internal const int NPPM_FILEFINDER_SEARCH_IN_DIRECTORY_EXPLICITLY = 0x0103;
        // NppmFileFinderInfo.szDirPath
        // NppmFileFinderInfo.bShowFolderBrowser
        // NppmFileFinderInfo.szSearchPattern
        // NppmFileFinderInfo.bOpenFiles
        // NppmFileFinderInfo.arrSelectedFilePaths

        internal const int NPPM_FILEFINDER_OPEN_FROM_HISTORY = 0x0104;
        // NppmFileFinderInfo.bOpenFiles
        // NppmFileFinderInfo.arrSelectedFilePaths

        internal const int NPPM_FILEFINDER_OPEN_LAST_CLOSED_FILE = 0x0105;
        // NppmFileFinderInfo.bOpenFiles
        // NppmFileFinderInfo.arrSelectedFilePaths
        
        [StructLayout(LayoutKind.Sequential)]
        internal struct NppmFileFinderInfo
        {
            public IntPtr szDirPath;
            public IntPtr arrFilePaths;
            public bool bShowFolderBrowser;
            public IntPtr szSearchPattern;
            public bool bOpenFiles;
            public IntPtr arrSelectedFilePaths;
        }

        internal static void HandlePluginMessage(IntPtr lParam)
        {
            CommunicationInfo communicationInfo = (CommunicationInfo)Marshal.PtrToStructure(
                (IntPtr)lParam, typeof(CommunicationInfo));
            string srcModuleName = Marshal.PtrToStringAuto(communicationInfo.srcModuleName);
            NppmFileFinderInfo info = (NppmFileFinderInfo)Marshal.PtrToStructure(
                communicationInfo.info, typeof(NppmFileFinderInfo));
            List<string> selectedFiles = null;

            if (communicationInfo.internalMsg == NPPM_FILEFINDER_OPEN_FROM_DIRECTORY_GREEDY)
            {
                selectedFiles = OpenFromDirectoryGreedy(
                    srcModuleName,
                    Marshal.PtrToStringAuto(info.szDirPath),
                    info.bOpenFiles);
            }
            else if (communicationInfo.internalMsg == NPPM_FILEFINDER_OPEN_FROM_STRINGLIST_GREEDY)
            {
                selectedFiles = OpenFromStringListGreedy(
                    srcModuleName,
                    new ClikeStringArray(info.arrFilePaths).ManagedStringsUnicode,
                    info.bOpenFiles);
            }
            else if (communicationInfo.internalMsg == NPPM_FILEFINDER_SEARCH_IN_DIRECTORY_EXPLICITLY)
            {
                selectedFiles = SearchInDirectoryExplicitly(
                    srcModuleName,
                    Marshal.PtrToStringAuto(info.szDirPath),
                    Marshal.PtrToStringAuto(info.szSearchPattern),
                    info.bShowFolderBrowser,
                    info.bOpenFiles);
            }
            else if (communicationInfo.internalMsg == NPPM_FILEFINDER_OPEN_FROM_HISTORY)
            {
                selectedFiles = OpenFromFileHistory(info.bOpenFiles);
            }
            else if (communicationInfo.internalMsg == NPPM_FILEFINDER_OPEN_LAST_CLOSED_FILE)
            {
                selectedFiles = OpenLastClosedFile(info.bOpenFiles);
            }

            updateCommunicationInfo(ref communicationInfo, ref info, selectedFiles, info.bOpenFiles);
        }

        internal static List<string> OpenFromDirectoryGreedy(string frmTitlePrefix, string dirPath, bool openFiles)
        {
            frmOpenFile frmOpenFile = new frmOpenFile(frmTitlePrefix, dirPath, null);
            return showFrmOpenFile(frmOpenFile, openFiles);
        }
  
        internal static List<string> OpenFromStringListGreedy(string frmTitlePrefix, List<string> lstFiles, bool openFiles)
        {
            frmOpenFile frmOpenFile = new frmOpenFile(frmTitlePrefix, lstFiles);
            return showFrmOpenFile(frmOpenFile, openFiles);
        }
  
        internal static List<string> SearchInDirectoryExplicitly(string frmTitlePrefix, string rootDir, string searchPattern, bool showFolderBrowser, bool openFiles)
        {
            if (string.IsNullOrEmpty(rootDir) || showFolderBrowser)
            {
                string title = "Select the folder from where to start the recursive file search";
                if (!FileSystemRedirection.IsUsed || !Main.BypassFSR)
                {
                    FolderSelectDialog dlg = new FolderSelectDialog();
                    dlg.Title = title;
                    dlg.InitialDirectory = rootDir;
                    if (!dlg.ShowDialog(PluginBase.nppData._nppHandle))
                    {
                        return new List<string>();
                    }
                    rootDir = dlg.FileName;
                }
                else
                {
                    using (Process p = new Process())
                    {
                        p.StartInfo.FileName = Path.Combine(Path.Combine(Main.PluginDir, Main.PluginName), "FolderSelectDialog.exe");
                        p.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"",
                            title, rootDir, PluginBase.nppData._nppHandle);
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.Start();
                        rootDir = p.StandardOutput.ReadToEnd().Replace("\r\n", "");
                        p.WaitForExit();
                        if (p.ExitCode != 0)
                        {
                            rootDir = "";
                        }
                    }
                    if (string.IsNullOrEmpty(rootDir))
                    {
                        return new List<string>();
                    }
                }
            }
            if (string.IsNullOrEmpty(searchPattern))
            {
                frmFilenamePattern frmFilenamePattern = new frmFilenamePattern();
                if (frmFilenamePattern.ShowDialog() != DialogResult.OK)
                {
                    return new List<string>();
                }
                searchPattern = frmFilenamePattern.cbxPattern.Text.Trim();
            }
            frmOpenFile frmOpenFile = new frmOpenFile(frmTitlePrefix, rootDir, searchPattern);
            return showFrmOpenFile(frmOpenFile, openFiles);
        }
  
        static List<string> showFrmOpenFile(frmOpenFile frmOpenFile, bool openFiles)
        {
            List<string> selectedFiles = new List<string>();
            if (frmOpenFile.ShowDialog() == DialogResult.OK)
            {
                selectedFiles = frmOpenFile.SelectedFiles;
                if (openFiles)
                {
                    foreach (string filePath in selectedFiles)
                    {
                        Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, IntPtr.Zero, filePath);
                    }
                }
            }
            return selectedFiles;
        }

        internal static List<string> OpenFromFileHistory(bool openFiles)
        {
            List<string> selectedFiles = OpenFromStringListGreedy("File history", Main.HistoryFiles, openFiles);
            if (openFiles)
            {
                foreach (string filePath in selectedFiles)
                {
                    Main.HistoryFiles.Remove(filePath);
                }
            }
            return selectedFiles;
        }

        internal static List<string> OpenLastClosedFile(bool openFiles)
        {
            List<string> selectedFiles = new List<string>();
            if (Main.HistoryFiles.Count > 0)
            {
                string filePath = Main.HistoryFiles[0];
                if (!string.IsNullOrEmpty(filePath))
                {
                    Main.HistoryFiles.Remove(filePath);

                    if (Main.AutoValidateFilenames)
                    {
                        FileMaskMatcher fileMaskMatcher = new FileMaskMatcher(Main.HistoryExclusions);
                        if (fileMaskMatcher.IsMatch(filePath, FileMaskMatcher.MatchType.FilePath) ||
                            !File.Exists(filePath))
                        {
                            return OpenLastClosedFile(openFiles);
                        }
                    }

                    if (openFiles)
                    {
                        Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_DOOPEN, IntPtr.Zero, filePath);
                    }
                    else
                    {
                        selectedFiles.Add(filePath);
                    }
                }
            }
            return selectedFiles;
        }

        static void updateCommunicationInfo(ref CommunicationInfo communicationInfo, ref NppmFileFinderInfo info, List<string> selectedFiles, bool openFiles)
        {
            if (!openFiles)
            {
                ClikeStringArray arr = new ClikeStringArray(selectedFiles);
                arr.AutoDispose = false;
                info.arrSelectedFilePaths = arr.NativePointer;
                Marshal.StructureToPtr(info, communicationInfo.info, false);
            }
        }
    }
}
