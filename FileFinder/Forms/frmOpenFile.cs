using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FileFinder
{
    public partial class frmOpenFile : Form
    {
        #region " Fields "
        string callerName;

        MatchItemList allMatchItemsInterThreaded;
        MatchItemList allMatchItemsThreadSafe;
        MatchItemList listBoxShadowItems = new MatchItemList();
        internal List<string> SelectedFiles;

        static ImageList iconCache = null;

        string lcaDirPath; // least common ancestor
        FileMaskMatcher fileMaskMatcher;

        DirectorySearch directorySearch = null;
        
        BackgroundWorker bw = null;
        bool formShown = false;
        int updateCounter;
        bool updatingListBox = false;

        Dictionary<Keys, string> EventKeys2SendKeys = new Dictionary<Keys, string>()
        {
            {Keys.Up, "{UP}"},
            {Keys.Down, "{DOWN}"},
            {Keys.PageUp, "{PGUP}"},
            {Keys.PageDown, "{PGDN}"}
        };
        bool KeySendToLbx = false;
        #endregion

        #region " StartUp/CleanUp "
        public frmOpenFile(string name, string dirPath, string searchPattern)
        {
            InitializeComponent();

            callerName = name;
            allMatchItemsInterThreaded = new MatchItemList();
            fileMaskMatcher = new FileMaskMatcher(Main.DirSearchExclusions);
            InitForm();
            btnAutoValidateFilenames.Visible = false;
            StartBackgroundWorker(new DirectorySearch(dirPath, searchPattern));
        }
        public frmOpenFile(string name, List<string> files)
        {
            InitializeComponent();

            callerName = name;
            allMatchItemsInterThreaded = new MatchItemList();
            foreach (string file in files)
            {
                allMatchItemsInterThreaded.Add(
                    new MatchItem(MatchItem.MatchItemStatus.Matched, file, null));
            }
            fileMaskMatcher = new FileMaskMatcher(Main.HistoryExclusions);
            InitForm();
            btnFolderUp.Visible = false;
            btnShowFilteredPaths.Visible = false;
            StartBackgroundWorker(null);
        }
        void InitForm()
        {
            Icon = Properties.Resources.filefinder;
            Text = string.Format("{0}: {1}", callerName, Text);
            Width = Main.OpenFileDialogWidth;
            Height = Main.OpenFileDialogHeight;
            btnCaseSensitiveSearch.Checked = Main.CaseSensitiveSearch;
            btnAutoValidateFilenames.Checked = Main.AutoValidateFilenames;
            btnShowFilteredPaths.Checked = Main.ShowFilteredPaths;
            if (iconCache == null)
            {
                iconCache = new ImageList();
                iconCache.ImageSize = new Size(16, 16);
                iconCache.TransparentColor = Color.Black;
            }
        }
        private void frmOpenFile_Load(object sender, EventArgs e)
        {
            InitList();
            UpdateListBox();
            formShown = true;
        }
        private void frmOpenFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bw != null)
            {
                bw.CancelAsync();
                while (bw.IsBusy)
                    Application.DoEvents();
                bw.Dispose();
            }

            SelectedFiles = new List<string>();
            foreach (int index in LbxFiles.SelectedIndices)
            {
                string filePath = listBoxShadowItems[index].FullPath;
                if (!filePath.EndsWith(@"\"))
                {
                    SelectedFiles.Add(filePath);
                }
            }

            Main.OpenFileDialogWidth = Width;
            Main.OpenFileDialogHeight = Height;
        }
        #endregion

        #region " File list functions "
        void InitList()
        {
            allMatchItemsThreadSafe = new MatchItemList();
            lock (allMatchItemsInterThreaded)
            {
                using (SuspendDrawingUpdate sdu = new SuspendDrawingUpdate(LbxFiles))
                {
                    if ((Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePathFileNameFirst) ||
                        (Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePath))
                    {
                        lcaDirPath = null;
                        foreach (MatchItem mi in allMatchItemsInterThreaded)
                        {
                            if ((mi.Status == MatchItem.MatchItemStatus.Matched) || Main.ShowFilteredPaths)
                            {
                                string dirPath = Path.GetDirectoryName(mi.FullPath).ToLower();
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
                    }
                    else
                    {
                        lcaDirPath = "";
                    }
                    foreach (MatchItem mi in allMatchItemsInterThreaded)
                    {
                        if ((mi.Status == MatchItem.MatchItemStatus.Matched) || Main.ShowFilteredPaths)
                        {
                            allMatchItemsThreadSafe.Add(
                                new MatchItem(mi.Status, mi.FullPath, mi.FullPath.Substring(lcaDirPath.Length)));
                        }
                    }
                }
            }
        }
        void UpdateListBox()
        {
            string pattern = tbxSearch.Text.Trim();
            string oldSelection = rbxFullSelectedPath.Text;
            if (directorySearch != null)
            {
                rbxFullSelectedPath.Text = directorySearch.Directory;
            }
            else
            {
                rbxFullSelectedPath.Text = "";
            }

            updatingListBox = true;
            LbxFiles.BeginUpdate();
            LbxFiles.Items.Clear();
            listBoxShadowItems.Clear();

            using (SuspendDrawingUpdate sdu = new SuspendDrawingUpdate(LbxFiles))
            {
                if (string.IsNullOrEmpty(pattern))
                {
                    foreach (MatchItem mi in allMatchItemsThreadSafe)
                    {
                        LbxFiles.Items.Add("");
                        listBoxShadowItems.Add(
                            new MatchItem(mi.Status, mi.FullPath, mi.FormattedPath));
                        if (mi.FullPath == oldSelection)
                        {
                            LbxFiles.SelectedIndex = LbxFiles.Items.Count - 1;
                        }
                    }
                }
                else
                {
                    string[] patterns = pattern.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    StringComparison strCompMode = Main.CaseSensitiveSearch ?
                        StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                    foreach (MatchItem mi in allMatchItemsThreadSafe)
                    {
                        bool match = true;
                        foreach (string _pat in patterns)
                        {
                            if (!(mi.FormattedPath.IndexOf(_pat, strCompMode) >= 0))
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {
                            LbxFiles.Items.Add("");
                            listBoxShadowItems.Add(
                                new MatchItem(mi.Status, mi.FullPath, mi.FormattedPath));
                            if (mi.FullPath == oldSelection)
                            {
                                LbxFiles.SelectedIndex = LbxFiles.Items.Count - 1;
                            }
                        }
                    }
                }

                if ((LbxFiles.SelectedItem == null) && (LbxFiles.Items.Count > 0))
                {
                    LbxFiles.SelectedIndex = 0;
                }
            }

            LbxFiles.EndUpdate();
            updatingListBox = false;

            lblResult.Text = string.Format("Result: {0} / {1}", LbxFiles.Items.Count, allMatchItemsThreadSafe.Count);
        }
        string TrimFormattedPath(string fullPath)
        {
            string trimmedFormattedPath = string.Copy(fullPath).Substring(lcaDirPath.Length);
            if ((Main.DisplayedFilePathFormat == Main.FilePathFormat.FullPathFileNameFirst) ||
                (Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePathFileNameFirst))
            {
                string dirName = Path.GetDirectoryName(trimmedFormattedPath);
                if (((dirName.Length == 1) && (dirName[0] == '\\')) ||
                    ((dirName.Length > 1) && (trimmedFormattedPath[0] == '\\') && (trimmedFormattedPath[1] != '\\')))
                {
                    dirName = dirName.Substring(1);
                }
                if (!string.IsNullOrEmpty(dirName))
                {
                    dirName = string.Format(" ({0})", dirName);
                }
                string fileName = Path.GetFileName(trimmedFormattedPath);
                trimmedFormattedPath = fileName + dirName;
            }
            else if (Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePath)
            {
                if (((trimmedFormattedPath.Length == 1) && (trimmedFormattedPath[0] == '\\')) ||
                    ((trimmedFormattedPath.Length > 1) && (trimmedFormattedPath[0] == '\\') && (trimmedFormattedPath[1] != '\\')))
                {
                    trimmedFormattedPath = trimmedFormattedPath.Substring(1);
                }
            }
            TextRenderer.MeasureText(trimmedFormattedPath, LbxFiles.Font,
                new System.Drawing.Size(LbxFiles.ClientSize.Width - iconCache.ImageSize.Width - 1, 0),
                TextFormatFlags.ModifyString | TextFormatFlags.PathEllipsis);
            return trimmedFormattedPath;
        }
        void GetFiles(string dirPath)
        {
            if (fileMaskMatcher.IsMatch(dirPath, FileMaskMatcher.MatchType.Directory))
            {
                lock (allMatchItemsInterThreaded)
                {
                    allMatchItemsInterThreaded.Add(
                            new MatchItem(MatchItem.MatchItemStatus.Excluded, dirPath + @"\", null));
                }
                return;
            }

            string[] dirFiles = new string[] { };
            try
            {
                if (string.IsNullOrEmpty(directorySearch.SearchPattern) || (directorySearch.RegexPattern != null))
                {
                    dirFiles = Directory.GetFiles(dirPath);
                }
                else
                {
                    dirFiles = Directory.GetFiles(dirPath, directorySearch.SearchPattern);
                }
            }
            catch (UnauthorizedAccessException)
            {
                lock (allMatchItemsInterThreaded)
                {
                    allMatchItemsInterThreaded.Add(
                          new MatchItem(MatchItem.MatchItemStatus.Denied, dirPath + @"\", null));
                }
                return;
            }
            foreach (string file in dirFiles)
            {
                if (bw.CancellationPending == true)
                {
                    return;
                }

                bool skipFile = false;
                if ((directorySearch.RegexPattern != null) &&
                    !directorySearch.RegexPattern.IsMatch(file, FileMaskMatcher.MatchType.RegEx))
                {
                    lock (allMatchItemsInterThreaded)
                    {
                        allMatchItemsInterThreaded.Add(
                                   new MatchItem(MatchItem.MatchItemStatus.Excluded, file, null));
                    }
                    skipFile = true;
                }
                if (!skipFile)
                {
                    if (!fileMaskMatcher.IsMatch(file, FileMaskMatcher.MatchType.FilePath))
                    {
                        lock (allMatchItemsInterThreaded)
                        {
                            allMatchItemsInterThreaded.Add(
                                new MatchItem(MatchItem.MatchItemStatus.Matched, file, null));
                        }
                    }
                    else
                    {
                        lock (allMatchItemsInterThreaded)
                        {
                            allMatchItemsInterThreaded.Add(
                                     new MatchItem(MatchItem.MatchItemStatus.Excluded, file, null));
                        }
                    }
                }

                if (!UpdateProgressBar(100))
                {
                    return;
                }
            }

            string[] dirs = new string[] { };
            try
            {
                dirs = Directory.GetDirectories(dirPath);
            }
            catch (UnauthorizedAccessException)
            {
                lock (allMatchItemsInterThreaded)
                {
                    allMatchItemsInterThreaded.Add(
                        new MatchItem(MatchItem.MatchItemStatus.Denied, dirPath + @"\", null));
                }
            }
            foreach (string dir in dirs)
            {
                if (bw.CancellationPending == true)
                {
                    return;
                }

                GetFiles(dir);

                if (!UpdateProgressBar(100))
                {
                    return;
                }
            }
        }
        void ValidateFilenames()
        {
            if (btnAutoValidateFilenames.Checked)
            {
                MatchItem[] _matchItems;
                lock (allMatchItemsInterThreaded)
                {
                    _matchItems = new MatchItem[allMatchItemsInterThreaded.Count];
                    allMatchItemsInterThreaded.CopyTo(_matchItems);
                }
                for (int i = _matchItems.Length - 1; i >= 0; i--)
                {
                    if (bw.CancellationPending == true)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(10);
                    if (fileMaskMatcher.IsMatch(_matchItems[i].FullPath, FileMaskMatcher.MatchType.FilePath) ||
                        !File.Exists(_matchItems[i].FullPath))
                    {
                        lock (allMatchItemsInterThreaded)
                        {
                            allMatchItemsInterThreaded.RemoveAt(i);
                        }
                    }

                    if (!UpdateProgressBar(10))
                    {
                        return;
                    }
                }
            }
        }
        bool UpdateProgressBar(int counterLimit)
        {
            updateCounter++;
            if (updateCounter == counterLimit)
            {
                updateCounter = 0;
                bw.ReportProgress(0);
            }
            while (updatingListBox)
            {
                if (bw.CancellationPending == true)
                {
                    return false;
                }
                System.Threading.Thread.Sleep(200);
            }
            return true;
        }
        #endregion

        #region " Background worker "
        void StartBackgroundWorker(object bwParam)
        {
            lblProgress.Visible = true;
            pbProgress.Visible = true;
            pbProgress.Enabled = true;

            updateCounter = 0;

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
                while (!formShown)
                {
                    System.Threading.Thread.Sleep(200);
                }
                if (e.Argument is DirectorySearch)
                {
                    directorySearch = (DirectorySearch)e.Argument;
                    GetFiles(directorySearch.Directory);
                }
                else
                {
                    ValidateFilenames();
                }
            }
            catch (Exception ex)
            {
                Dbg.Msg(ex);
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
            if (LbxFiles.SelectedIndex >= 0)
            {
                rbxFullSelectedPath.Text = listBoxShadowItems[LbxFiles.SelectedIndex].FullPath;
            }
            else
            {
                if (directorySearch != null)
                {
                    rbxFullSelectedPath.Text = directorySearch.Directory;
                }
                else
                {
                    rbxFullSelectedPath.Text = "";
                }
            }
            rbxFullSelectedPath.Enabled = (LbxFiles.SelectedIndices.Count == 1);
            LbxFiles.Invalidate();
        }
        private void lbxFiles_DoubleClick(object sender, EventArgs e)
        {
            if (LbxFiles.SelectedItem != null)
            {
                btnOpenSelected.PerformClick();
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
            try
            {
                if (e.Index >= 0)
                {
                    e.DrawBackground();
                    e.DrawFocusRectangle();

                    MatchItem mi = listBoxShadowItems[e.Index];

                    if (!string.IsNullOrEmpty(Path.GetFileName(mi.FullPath)))
                    {
                        string fileExt = Path.GetExtension(mi.FullPath).ToLower();
                        if (string.IsNullOrEmpty(fileExt))
                            fileExt = "-";
                        int imgIndex = iconCache.Images.IndexOfKey(fileExt);
                        if (imgIndex < 0)
                        {
                            Bitmap bmp = SHGetIcon.GetBmp(mi.FullPath, true);
                            iconCache.Images.Add(fileExt, bmp);
                            imgIndex = iconCache.Images.IndexOfKey(fileExt);
                        }
                        iconCache.Draw(e.Graphics, new Point(e.Bounds.Location.X + 1, e.Bounds.Location.Y), imgIndex);
                    }

                    Color foreColor = ForeColor;
                    if (mi.Status == MatchItem.MatchItemStatus.Excluded)
                    {
                        foreColor = Color.DarkCyan;
                    }
                    if (mi.Status == MatchItem.MatchItemStatus.Denied)
                    {
                        foreColor = Color.Red;
                    }

                    int fontSwitchIndex;
                    Color firstColor;
                    Color secondColor;
                    Color dimmedForeColor = Color.FromArgb(
                                127 - ((127 - foreColor.R) / 2),
                                127 - ((127 - foreColor.G) / 2),
                                127 - ((127 - foreColor.B) / 2));
                    Color dimmedHighlightText = Color.FromArgb(
                                127 - ((127 - SystemColors.HighlightText.R) / 2),
                                127 - ((127 - SystemColors.HighlightText.G) / 2),
                                127 - ((127 - SystemColors.HighlightText.B) / 2));

                    string trimmedFormattedPath = TrimFormattedPath(mi.FullPath);

                    if ((Main.DisplayedFilePathFormat == Main.FilePathFormat.FullPath) ||
                        (Main.DisplayedFilePathFormat == Main.FilePathFormat.RelativePath))
                    {
                        fontSwitchIndex = trimmedFormattedPath.LastIndexOf('\\') + 1;
                        if (LbxFiles.SelectedIndices.Contains(e.Index))
                        {
                            firstColor = dimmedHighlightText;
                            secondColor = SystemColors.HighlightText;
                        }
                        else
                        {
                            firstColor = dimmedForeColor;
                            secondColor = foreColor;
                        }
                    }
                    else
                    {
                        fontSwitchIndex = trimmedFormattedPath.IndexOf(" (");
                        if (LbxFiles.SelectedIndices.Contains(e.Index))
                        {
                            firstColor = SystemColors.HighlightText;
                            secondColor = dimmedHighlightText;
                        }
                        else
                        {
                            firstColor = foreColor;
                            secondColor = dimmedForeColor;
                        }
                    }
                    if (fontSwitchIndex < 0)
                        fontSwitchIndex = trimmedFormattedPath.Length;

                    TextRenderer.DrawText(e.Graphics, trimmedFormattedPath.Substring(0, fontSwitchIndex),
                        LbxFiles.Font, new Point(e.Bounds.Location.X + 17, e.Bounds.Location.Y), firstColor);
                    if (trimmedFormattedPath.Length > fontSwitchIndex)
                    {
                        SizeF sf = TextRenderer.MeasureText(trimmedFormattedPath.Substring(0, fontSwitchIndex), LbxFiles.Font);
                        TextRenderer.DrawText(e.Graphics, trimmedFormattedPath.Substring(fontSwitchIndex), LbxFiles.Font,
                            new Point((int)sf.Width + 17, e.Bounds.Y), secondColor);
                    }
                }
            }
            catch (Exception ex)
            {
                LbxFiles.DrawItem -= lbxFiles_DrawItem;
                Dbg.Msg(ex);
            }
        }
        #endregion

        #region " Other events "
        private void tbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnFolderUp.Visible && (e.Modifiers == Keys.Alt) && (e.KeyCode == Keys.Up))
            {
                btnFolderUp.PerformClick();
                e.Handled = true;
            }
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Back))
            {
                tbxSearch.Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (EventKeys2SendKeys.ContainsKey(e.KeyCode))
            {
                KeySendToLbx = true;
                LbxFiles.Focus();
                SendKeys.Send(EventKeys2SendKeys[e.KeyCode]);
                e.Handled = true;
            }
        }
        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateListBox();
        }
        void colorPartOfPath(int start, int length, Color bkColor, Color fgColor)
        {
            rbxFullSelectedPath.SelectionStart = start;
            rbxFullSelectedPath.SelectionLength = length;
            rbxFullSelectedPath.SelectionBackColor = bkColor;
            rbxFullSelectedPath.SelectionColor = fgColor;
        }
        private void rbxFullSelectedPath_TextChanged(object sender, EventArgs e)
        {
            if ((directorySearch != null) &&
                !string.IsNullOrEmpty(directorySearch.Directory) &&
                !string.IsNullOrEmpty(rbxFullSelectedPath.Text))
            {
                string filePath = rbxFullSelectedPath.Text;
                bool highlight = (LbxFiles.SelectedIndices.Count == 1);
                colorPartOfPath(0, filePath.Length,
                    rbxFullSelectedPath.BackColor,
                    highlight ? rbxFullSelectedPath.ForeColor : Color.FromKnownColor(KnownColor.ControlDarkDark));
                if (highlight)
                {
                    int fileNameIndex = directorySearch.Directory.TrimEnd(new char[] { '\\' }).Length + 1;
                    string[] dirNames = filePath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    int dirNameIndex = 0;
                    foreach (string dirName in dirNames)
                    {
                        dirNameIndex = filePath.IndexOf(dirName, dirNameIndex);
                        if ((dirNameIndex < 0) || (dirNameIndex >= fileNameIndex))
                        {
                            break;
                        }
                        colorPartOfPath(dirNameIndex, dirName.Length,
                            Color.FromKnownColor(KnownColor.Highlight),
                            Color.FromKnownColor(KnownColor.HighlightText));
                        //colorPartOfPath(dirNameIndex + dirName.Length, 1,
                        //    rbxFullSelectedPath.BackColor,
                        //    rbxFullSelectedPath.BackColor);
                        dirNameIndex++;
                    }
                }
            }
        }
        private void btnCaseSensitiveSearch_Click(object sender, EventArgs e)
        {
            Main.CaseSensitiveSearch = btnCaseSensitiveSearch.Checked;
            UpdateListBox();
        }
        private void btnAutoValidateFilename_Click(object sender, EventArgs e)
        {
            Main.AutoValidateFilenames = btnAutoValidateFilenames.Checked;
            StartBackgroundWorker(null);
        }
        private void btnShowFilteredPaths_Click(object sender, EventArgs e)
        {
            Main.ShowFilteredPaths = btnShowFilteredPaths.Checked;
            InitList();
            UpdateListBox();
        }
        private void btnFolderUp_Click(object sender, EventArgs e)
        {
            if (directorySearch != null)
            {
                string parentDir = Path.GetDirectoryName(directorySearch.Directory);
                if (!string.IsNullOrEmpty(parentDir) && (parentDir != directorySearch.Directory))
                {
                    if (bw != null)
                    {
                        bw.CancelAsync();
                        while (bw.IsBusy)
                            Application.DoEvents();
                        bw.Dispose();
                    }
                    directorySearch.Directory = parentDir;
                    allMatchItemsInterThreaded = new MatchItemList();
                    StartBackgroundWorker(directorySearch);
                }
            }
        }
        #endregion
    }
}
