using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NppPlugin.DllExport;
using NppPluginNET;

namespace NppFileSearch
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
                MessageBox.Show(ex.Message, Main.PluginName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [DllExport(CallingConvention = CallingConvention.Cdecl)]
        static IntPtr getFuncsArray(ref int nbF)
        {
            nbF = PluginBase._funcItems.Items.Count;
            return PluginBase._funcItems.NativePointer;
        }

        const int NPEM_NPPFILESEARCH_HISTORY = 0x0101;
        const int NPEM_NPPFILESEARCH_STRINGLIST = 0x0102;
        const int NPEM_NPPFILESEARCH_DIRECTORY = 0x0103;
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
                    if (communicationInfo.internalMsg == NPEM_NPPFILESEARCH_HISTORY)
                    {
                        Main.OpenFromFileHistory();
                    }
                    else if (communicationInfo.internalMsg == NPEM_NPPFILESEARCH_STRINGLIST)
                    {
                        List<string> lstFiles = new ClikeStringArray(communicationInfo.info).ManagedStringsUnicode;
                        Main.OpenFromStringList(srcModuleName, lstFiles);
                    }
                    else if (communicationInfo.internalMsg == NPEM_NPPFILESEARCH_DIRECTORY)
                    {
                        string folderPath = Marshal.PtrToStringAuto(communicationInfo.info);
                        Main.OpenFromDirectory(srcModuleName, folderPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Main.PluginName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        Main.HistoryFiles.Remove(filePath);
                    }
                }
                else if (nc.nmhdr.code == (uint)NppMsg.NPPN_FILEBEFORECLOSE)
                {
                    string filePath = PluginBase.GetFilePathFromBufferID(nc.nmhdr.idFrom);
                    if (File.Exists(filePath))
                    {
                        string dir = Path.GetDirectoryName(filePath);
                        bool skipFile = false;
                        foreach (string excl in Main.HistoryExclusions)
                        {
                            string _excl = Environment.ExpandEnvironmentVariables(excl);
                            if (_excl.Contains(":") || _excl.StartsWith("\\"))
                            {
                                if (dir.ToLower().StartsWith(_excl))
                                    skipFile = true;
                            }
                            else if (_excl.Contains("\\"))
                            {
                                if (dir.ToLower().EndsWith(_excl))
                                    skipFile = true;
                            }
                            else
                            {
                                //string[] _excls = _excl.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                                //string startsWith = "";
                                //string endsWith = "";
                                if (_excl == Path.GetFileName(dir).ToLower())
                                    skipFile = true;
                            }
                            if (skipFile)
                                break;
                        }

                        if (!skipFile)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Main.PluginName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
