using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NppPluginNET;

namespace NppFileSearch
{
    public partial class frmOpenFile : Form
    {
        #region " Fields "
        string callerName;
        
        List<string> allFiles;
        Dictionary<string, Dictionary<string, string>> formattedFiles;
        List<ListViewItem> listBoxItems = new List<ListViewItem>();

        string lcaDirPath; // least common ancestor
        
        BackgroundWorker bw = null;
        int fileCounter;
        bool updatingListBox = false;

        Dictionary<Keys, string> EventKeys2SendKeys = new Dictionary<Keys, string>()
        {
            {Keys.Up, "{UP}"},
            {Keys.Down, "{DOWN}"},
            {Keys.PageUp, "{PGUP}"},
            {Keys.PageDown, "{PGDN}"},
            {Keys.Home, "{HOME}"},
            {Keys.End, "{END}"},
        };
        bool KeySendToLbx = false;
        #endregion

        #region " StartUp/CleanUp "
        public frmOpenFile(string name, List<string> files)
        {
            InitializeComponent();

            callerName = name;
            allFiles = files;
            InitForm();

            StartBackgroundWorker(null);
        }
        public frmOpenFile(string name, string folderPath)
        {
            InitializeComponent();

            callerName = name;
            allFiles = new List<string>();
            InitForm();

            btnCheckFilesExist.Visible = false;

            StartBackgroundWorker(folderPath);
        }
        void InitForm()
        {
            Text = string.Format("{0}: {1}", callerName, Text);
            Width = Main.WindowWidth;
            Height = Main.WindowHeight;
            btnCaseSensitiveSearch.Checked = Main.CaseSensitiveSearch;
            btnCheckFilesExist.Checked = Main.AutoCheckFilesExist;
        }
        private void frmOpenFile_Load(object sender, EventArgs e)
        {
            InitList();
            UpdateListBox();
        }
        private void frmOpenFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((bw != null) && bw.IsBusy)
            {
                bw.CancelAsync();
            }
            Main.WindowWidth = Width;
            Main.WindowHeight = Height;
        }
        #endregion

        #region " File list functions "
        void InitList()
        {
            formattedFiles = new Dictionary<string, Dictionary<string, string>>();
            lock (allFiles)
            {
                if ((Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePathFileNameFirst) ||
                    (Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePath))
                {
                    lcaDirPath = null;
                    foreach (string file in allFiles)
                    {
                        string dirPath = Path.GetDirectoryName(file).ToLower();
                        if (lcaDirPath == null)
                        {
                            lcaDirPath = dirPath;
                        }
                        else
                        {
                            for (int i = 0; i < lcaDirPath.Length; i++)
                            {
                                if (lcaDirPath[i] != dirPath[i])
                                {
                                    if (i == 0)
                                    {
                                        lcaDirPath = "";
                                    }
                                    else
                                    {
                                        lcaDirPath = Path.GetDirectoryName(lcaDirPath.Substring(0, i + 1));
                                        if (lcaDirPath == null)
                                        {
                                            lcaDirPath = "";
                                        }
                                    }
                                    break;
                                }
                                else if (i == (dirPath.Length - 1))
                                {
                                    lcaDirPath = dirPath;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lcaDirPath = "";
                }
                foreach (string file in allFiles)
                {
                    formattedFiles[file] = new Dictionary<string, string>();
                    formattedFiles[file]["FullDisplay"] = file.Substring(lcaDirPath.Length);
                    string f = string.Copy(file).Substring(lcaDirPath.Length);
                    if ((Main.DisplayedFilePathFormat == Main.FilePathFormat.FullPathFileNameFirst) ||
                        (Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePathFileNameFirst))
                    {
                        string dirName = Path.GetDirectoryName(f);
                        if (((dirName.Length == 1) && (dirName[0] == '\\')) ||
                            ((dirName.Length > 1) && (f[0] == '\\') && (f[1] != '\\')))
                        {
                            dirName = dirName.Substring(1);
                        }
                        if (!string.IsNullOrEmpty(dirName))
                        {
                            dirName = string.Format(" ({0})", dirName);
                        }
                        string fileName = Path.GetFileName(f);
                        f = fileName + dirName;
                    }
                    else if (Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePath)
                    {
                        if (((f.Length == 1) && (f[0] == '\\')) ||
                            ((f.Length > 1) && (f[0] == '\\') && (f[1] != '\\')))
                        {
                            f = f.Substring(1);
                        }
                    }
                    TextRenderer.MeasureText(f, lbxFiles.Font, new System.Drawing.Size(lbxFiles.ClientSize.Width, 0),
                        TextFormatFlags.ModifyString | TextFormatFlags.PathEllipsis);
                    formattedFiles[file]["TrimmedDisplay"] = f;
                }
            }
        }
        void UpdateListBox()
        {
            string pattern = tbxSearch.Text.Trim();
            string oldSelection = tbxFullSelectedPath.Text;
            tbxFullSelectedPath.Text = "";

            updatingListBox = true;
            lbxFiles.BeginUpdate();
            lbxFiles.Items.Clear();
            listBoxItems.Clear();

            if (string.IsNullOrEmpty(pattern))
            {
                foreach (string file in formattedFiles.Keys)
                {
                    ListViewItem lvi = new ListViewItem(formattedFiles[file]["TrimmedDisplay"]);
                    lvi.Tag = file;
                    lbxFiles.Items.Add("");
                    listBoxItems.Add(lvi);
                    if (file == oldSelection)
                    {
                        lbxFiles.SelectedIndex = lbxFiles.Items.Count - 1;
                    }
                }
            }
            else
            {
                string[] patterns = pattern.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                StringComparison strCompMode = Main.CaseSensitiveSearch ?
                    StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                foreach (string file in formattedFiles.Keys)
                {
                    bool match = true;
                    foreach (string _pat in patterns)
                    {
                        if (!(formattedFiles[file]["FullDisplay"].IndexOf(_pat, strCompMode) >= 0))
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        ListViewItem lvi = new ListViewItem(formattedFiles[file]["TrimmedDisplay"]);
                        lvi.Tag = file;
                        lbxFiles.Items.Add("");
                        listBoxItems.Add(lvi);
                        if (file == oldSelection)
                        {
                            lbxFiles.SelectedIndex = lbxFiles.Items.Count - 1;
                        }
                    }
                }
            }

            if ((lbxFiles.SelectedItem == null) && (lbxFiles.Items.Count > 0))
            {
                lbxFiles.SelectedIndex = 0;
            }

            lbxFiles.EndUpdate();
            updatingListBox = false;

            lblResult.Text = string.Format("Result: {0} / {1}", lbxFiles.Items.Count, formattedFiles.Count);
        }
        void GetFiles(string folderPath)
        {
            foreach (string file in Directory.GetFiles(folderPath))
            {
                if (bw.CancellationPending == true)
                {
                    break;
                }

                if (Main.DirSearchExcludedFileExts.Count > 0)
                {
                    bool skipFile = false;
                    foreach (string _excl in Main.DirSearchExcludedFileExts)
                    {
                        if (Win32.FitsFileMask(Path.GetFileName(file), _excl))
                        {
                            skipFile = true;
                            break;
                        }
                    }
                    if (!skipFile)
                    {
                        lock (allFiles)
                        {
                            allFiles.Add(file);
                        }
                    }
                }

                fileCounter++;
                if (fileCounter == 100)
                {
                    fileCounter = 0;
                    bw.ReportProgress(0);
                }
                while (updatingListBox)
                {
                    System.Threading.Thread.Sleep(200);
                }
            }

            foreach (string dir in Directory.GetDirectories(folderPath))
            {
                if (bw.CancellationPending == true)
                {
                    break;
                }

                bool skipDir = false;
                foreach (string excl in Main.DirSearchExcludedDirs)
                {
                    string _excl = Environment.ExpandEnvironmentVariables(excl).ToLower();
                    if (_excl.Contains("\\"))
                    {
                        if (dir.ToLower().EndsWith(_excl))
                        {
                            skipDir = true;
                            break;
                        }
                    }
                    else
                    {
                        if (_excl == Path.GetFileName(dir).ToLower())
                        {
                            skipDir = true;
                            break;
                        }
                    }
                }

                if (!skipDir)
                {
                    GetFiles(dir);
                }
            }
        }
        void CheckFilesExist()
        {
            if (btnCheckFilesExist.Checked)
            {
                string[] _files;
                lock (allFiles)
                {
                    _files = allFiles.ToArray();
                }
                foreach (string file in _files)
                {
                    if (bw.CancellationPending == true)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(10);
                    if (!File.Exists(file))
                    {
                        lock (allFiles)
                        {
                            allFiles.Remove(file);
                        }
                    }

                    fileCounter++;
                    if (fileCounter == 10)
                    {
                        fileCounter = 0;
                        bw.ReportProgress(0);
                    }
                    while (updatingListBox)
                    {
                        System.Threading.Thread.Sleep(200);
                    }
                }
            }
        }
        #endregion

        #region " Background worker "
        void StartBackgroundWorker(object bwParam)
        {
            lblProgress.Visible = true;
            pbProgress.Visible = true;
            pbProgress.Enabled = true;

            fileCounter = 0;

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync(bwParam);
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument is string)
                {
                    GetFiles((string)e.Argument);
                }
                else
                {
                    CheckFilesExist();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, callerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InitList();
            UpdateListBox();
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblProgress.Visible = false;
            pbProgress.Visible = false;
            pbProgress.Enabled = false;
            InitList();
            UpdateListBox();
        }
        #endregion

        #region " ListBox events "
        private void lbxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem lvi = listBoxItems[lbxFiles.SelectedIndex];
            tbxFullSelectedPath.Text = (string)lvi.Tag;
            lbxFiles.Invalidate();
        }
        private void lbxFiles_DoubleClick(object sender, EventArgs e)
        {
            if (lbxFiles.SelectedItem != null)
            {
                btnOpenSelected.PerformClick();
            }
        }
        private void lbxFiles_SizeChanged(object sender, EventArgs e)
        {
            InitList();
            UpdateListBox();
        }
        private void tbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (EventKeys2SendKeys.ContainsKey(e.KeyCode))
            {
                KeySendToLbx = true;
                lbxFiles.Focus();
                SendKeys.Send(EventKeys2SendKeys[e.KeyCode]);
            }
        }
        private void lbxFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeySendToLbx)
            {
                KeySendToLbx = false;
                tbxSearch.Focus();
            }
        }
        private void lbxFiles_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                ListViewItem lvi = listBoxItems[e.Index] as ListViewItem;
                String txt = lvi.Text;

                int fontSwitchIndex;
                Color firstColor;
                Color secondColor;
                Color dimmedForeColor = Color.FromArgb(
                            127 - ((127 - ForeColor.R) / 2),
                            127 - ((127 - ForeColor.G) / 2),
                            127 - ((127 - ForeColor.B) / 2));
                Color dimmedHighlightText = Color.FromArgb(
                            127 - ((127 - SystemColors.HighlightText.R) / 2),
                            127 - ((127 - SystemColors.HighlightText.G) / 2),
                            127 - ((127 - SystemColors.HighlightText.B) / 2));

                if ((Main.DisplayedFilePathFormat == Main.FilePathFormat.FullPath) ||
                    (Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePath))
                {
                    fontSwitchIndex = txt.LastIndexOf('\\') + 1;
                    if (e.Index == lbxFiles.SelectedIndex)
                    {
                        firstColor = dimmedHighlightText;
                        secondColor = SystemColors.HighlightText;
                    }
                    else
                    {
                        firstColor = dimmedForeColor;
                        secondColor = ForeColor;
                    }
                }
                else
                {
                    fontSwitchIndex = txt.IndexOf(" (");
                    if (e.Index == lbxFiles.SelectedIndex)
                    {
                        firstColor = SystemColors.HighlightText;
                        secondColor = dimmedHighlightText;
                    }
                    else
                    {
                        firstColor = ForeColor;
                        secondColor = dimmedForeColor;
                    }
                }
                if (fontSwitchIndex < 0)
                    fontSwitchIndex = txt.Length;

                TextRenderer.DrawText(e.Graphics, txt.Substring(0, fontSwitchIndex),
                    lbxFiles.Font, e.Bounds.Location, firstColor);
                if (txt.Length > fontSwitchIndex)
                {
                    SizeF sf = TextRenderer.MeasureText(txt.Substring(0, fontSwitchIndex), lbxFiles.Font);
                    TextRenderer.DrawText(e.Graphics, txt.Substring(fontSwitchIndex), lbxFiles.Font,
                        new Point((int)sf.Width, e.Bounds.Y), secondColor);
                }
            }
        }
        #endregion

        #region " Other events "
        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateListBox();
        }
        private void btnCaseSensitiveSearch_Click(object sender, EventArgs e)
        {
            Main.CaseSensitiveSearch = btnCaseSensitiveSearch.Checked;
            UpdateListBox();
        }
        private void btnCheckFilesExist_Click(object sender, EventArgs e)
        {
            Main.AutoCheckFilesExist = btnCheckFilesExist.Checked;
            StartBackgroundWorker(null);
        }
        #endregion
    }
}
