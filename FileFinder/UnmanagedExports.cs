using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NppPlugin.DllExport;
using NppPluginNET;

namespace FileFinder
{
    class UnmanagedExports
    {
        [DllExport(CallingConvention=CallingConvention.Cdecl)]
        static bool isUnicode()
        {
            return true;
        }

        [DllExport(CallingConvention = CallingConvention.Cdecl)]
        static void setInfo(NppData notepadPlusData)
        {
            try
            {
                PluginBase.nppData = notepadPlusData;
                Main.CommandMenuInit();
                Main.LoadSettings();
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
            }
        }

        [DllExport(CallingConvention = CallingConvention.Cdecl)]
        static IntPtr getFuncsArray(ref int nbF)
        {
            nbF = PluginBase._funcItems.Items.Count;
            return PluginBase._funcItems.NativePointer;
        }

        [DllExport(CallingConvention = CallingConvention.Cdecl)]
        static uint messageProc(uint Message, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                if (Message == (uint)NppMsg.NPPM_MSGTOPLUGIN)
                {
                    CommunicationInfo communicationInfo = (CommunicationInfo)Marshal.PtrToStructure(
                        (IntPtr)lParam, typeof(CommunicationInfo));
                    string srcModuleName = Marshal.PtrToStringAuto(communicationInfo.srcModuleName);
                    if (communicationInfo.internalMsg == API.NPPM_FILEFINDER_OPEN_FROM_DIRECTORY_GREEDY)
                    {
                        API.NppmFileFinderInfo info = (API.NppmFileFinderInfo)Marshal.PtrToStructure(
                            communicationInfo.info, typeof(API.NppmFileFinderInfo));
                        string dirPath = Marshal.PtrToStringAuto(info.szDirPath);
                        bool openFiles = info.bOpenFiles;
                        List<string> selectedFiles = API.OpenFromDirectoryGreedy(srcModuleName, dirPath, openFiles);
                    }
                    else if (communicationInfo.internalMsg == API.NPPM_FILEFINDER_OPEN_FROM_STRINGLIST_GREEDY)
                    {
                        API.NppmFileFinderInfo info = (API.NppmFileFinderInfo)Marshal.PtrToStructure(
                           communicationInfo.info, typeof(API.NppmFileFinderInfo));
                        List<string> lstFiles = new ClikeStringArray(info.arrFilePaths).ManagedStringsUnicode;
                        bool openFiles = info.bOpenFiles;
                        List<string> selectedFiles = API.OpenFromStringListGreedy(srcModuleName, lstFiles, openFiles);
                    }
                    else if (communicationInfo.internalMsg == API.NPPM_FILEFINDER_SEARCH_IN_DIRECTORY_EXPLICITLY)
                    {
                        API.NppmFileFinderInfo info = (API.NppmFileFinderInfo)Marshal.PtrToStructure(
                          communicationInfo.info, typeof(API.NppmFileFinderInfo));
                        string dirPath = Marshal.PtrToStringAuto(info.szDirPath);
                        string searchPattern = Marshal.PtrToStringAuto(info.szSearchPattern);
                        bool showFolderBrowser = info.bShowFolderBrowser;
                        bool openFiles = info.bOpenFiles;
                        List<string> selectedFiles = API.SearchInDirectoryExplicitly(srcModuleName, dirPath,
                            searchPattern, showFolderBrowser, openFiles);
                    }
                    else if (communicationInfo.internalMsg == API.NPPM_FILEFINDER_OPEN_FROM_HISTORY)
                    {
                        Main.OpenFromFileHistory();
                    }
                    else if (communicationInfo.internalMsg == API.NPPM_FILEFINDER_OPEN_LAST_CLOSED_FILE)
                    {
                        Main.OpenLastClosedFile();
                    }

                }
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
            }
            return 1;
        }

        static IntPtr _ptrPluginName = IntPtr.Zero;
        [DllExport(CallingConvention = CallingConvention.Cdecl)]
        static IntPtr getName()
        {
            if (_ptrPluginName == IntPtr.Zero)
                _ptrPluginName = Marshal.StringToHGlobalUni(Main.PluginName);
            return _ptrPluginName;
        }

        [DllExport(CallingConvention = CallingConvention.Cdecl)]
        static void beNotified(IntPtr notifyCode)
        {
            try
            {
                SCNotification nc = (SCNotification)Marshal.PtrToStructure(notifyCode, typeof(SCNotification));
                if (nc.nmhdr.code == (uint)NppMsg.NPPN_TBMODIFICATION)
                {
                    PluginBase._funcItems.RefreshItems();
                    Main.SetToolBarIcon();
                }
                else if (nc.nmhdr.code == (uint)NppMsg.NPPN_SHUTDOWN)
                {
                    Main.SaveSettings();
                    Marshal.FreeHGlobal(_ptrPluginName);
                }
                else if (nc.nmhdr.code == (uint)NppMsg.NPPN_FILEOPENED)
                {
                    string filePath = PluginBase.GetFilePathFromBufferID(nc.nmhdr.idFrom);
                    if (File.Exists(filePath))
                    {
                        while (Main.HistoryFiles.Contains(filePath))
                        {
                            Main.HistoryFiles.Remove(filePath);
                        }
                    }
                }
                else if (nc.nmhdr.code == (uint)NppMsg.NPPN_FILEBEFORECLOSE)
                {
                    string filePath = PluginBase.GetFilePathFromBufferID(nc.nmhdr.idFrom);
                    if (File.Exists(filePath))
                    {
                        FileMaskMatcher fileMaskMatcher = new FileMaskMatcher(Main.HistoryExclusions);
                        if (!fileMaskMatcher.IsMatch(filePath, FileMaskMatcher.MatchType.FilePath))
                        {
                            if (!Main.HistoryFiles.Contains(filePath))
                            {
                                Main.HistoryFiles.Insert(0, filePath);
                                if (Main.HistoryFiles.Count > Main.MaxHistoryLength)
                                {
                                    Main.HistoryFiles.RemoveRange(Main.MaxHistoryLength,
                                        Main.HistoryFiles.Count - Main.MaxHistoryLength);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
            }
        }
    }
}
