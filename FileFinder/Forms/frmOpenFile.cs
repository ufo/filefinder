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
        
        List<string> allFiles;
        Dictionary<string, Dictionary<string, string>> formattedFiles;
        List<ListViewItem> listBoxItems = new List<ListViewItem>();
        internal List<string> SelectedFiles;

        static ImageList iconCache = null;

        string lcaDirPath; // least common ancestor
        FileMaskMatcher fileMaskMatcher;

        class DirectorySearch
        {
            public DirectorySearch(string directory, string searchPattern)
            {
                Directory = directory;
                SearchPattern = searchPattern;
            }
            public string Directory;
            public string SearchPattern;
        }
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
            allFiles = new List<string>();
            fileMaskMatcher = new FileMaskMatcher(Main.DirSearchExclusions);
            InitForm();
            btnAutoInvalidateFilenames.Visible = false;
            StartBackgroundWorker(new DirectorySearch(dirPath, searchPattern));
        }
        public frmOpenFile(string name, List<string> files)
        {
            InitializeComponent();

            callerName = name;
            allFiles = files;
            fileMaskMatcher = new FileMaskMatcher(Main.HistoryExclusions);
            InitForm();
            btnFolderUp.Visible = false;
            StartBackgroundWorker(null);
        }
        void InitForm()
        {
            Text = string.Format("{0}: {1}", callerName, Text);
            Width = Main.OpenFileDialogWidth;
            Height = Main.OpenFileDialogHeight;
            btnCaseSensitiveSearch.Checked = Main.CaseSensitiveSearch;
            btnAutoInvalidateFilenames.Checked = Main.AutoInvalidateFilenames;
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
                ListViewItem lvi = listBoxItems[index];
                SelectedFiles.Add((string)lvi.Tag);
            }

            Main.OpenFileDialogWidth = Width;
            Main.OpenFileDialogHeight = Height;
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
                    TextRenderer.MeasureText(f, LbxFiles.Font,
                        new System.Drawing.Size(LbxFiles.ClientSize.Width - iconCache.ImageSize.Width - 1, 0),
                        TextFormatFlags.ModifyString | TextFormatFlags.PathEllipsis);
                    formattedFiles[file]["TrimmedDisplay"] = f;
                }
            }
        }
        void UpdateListBox()
        {
            string pattern = tbxSearch.Text.Trim();
            string oldSelection = rbxFullSelectedPath.Text;
            rbxFullSelectedPath.Text = "";

            updatingListBox = true;
            LbxFiles.BeginUpdate();
            LbxFiles.Items.Clear();
            listBoxItems.Clear();

            if (string.IsNullOrEmpty(pattern))
            {
                foreach (string file in formattedFiles.Keys)
                {
                    ListViewItem lvi = new ListViewItem(formattedFiles[file]["TrimmedDisplay"]);
                    lvi.Tag = file;
                    LbxFiles.Items.Add("");
                    listBoxItems.Add(lvi);
                    if (file == oldSelection)
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
                        LbxFiles.Items.Add("");
                        listBoxItems.Add(lvi);
                        if (file == oldSelection)
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

            LbxFiles.EndUpdate();
            updatingListBox = false;

            lblResult.Text = string.Format("Result: {0} / {1}", LbxFiles.Items.Count, formattedFiles.Count);
        }
        void GetFiles(string dirPath, string searchPattern)
        {
            string[] dirFiles = new string[] { };
            try
            {
                if (string.IsNullOrEmpty(searchPattern))
                {
                    dirFiles = Directory.GetFiles(dirPath);
                }
                else
                {
                    dirFiles = Directory.GetFiles(dirPath, searchPattern);
                }
            }
            catch (UnauthorizedAccessException) { }
            foreach (string file in dirFiles)
            {
                if (bw.CancellationPending == true)
                {
                    return;
                }

                if (!fileMaskMatcher.IsMatch(Path.GetFileName(file), FileMaskMatcher.MatchType.FileName))
                {
                    lock (allFiles)
                    {
                        allFiles.Add(file);
                    }
                }

                if (!updateProgressBar(100))
                {
                    return;
                }
            }

            string[] dirs = new string[] { };
            try
            {
                dirs = Directory.GetDirectories(dirPath);
            }
            catch (UnauthorizedAccessException) { }
            foreach (string dir in dirs)
            {
                if (bw.CancellationPending == true)
                {
                    return;
                }

                if (!fileMaskMatcher.IsMatch(dir, FileMaskMatcher.MatchType.Directory))
                {
                    GetFiles(dir, searchPattern);
                }

                if (!updateProgressBar(100))
                {
                    return;
                }
            }
        }
        void InvalidateFilename()
        {
            if (btnAutoInvalidateFilenames.Checked)
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
                    if (fileMaskMatcher.IsMatch(file, FileMaskMatcher.MatchType.FullPath) ||
                        !File.Exists(file))
                    {
                        lock (allFiles)
                        {
                            allFiles.Remove(file);
                        }
                    }

                    if (!updateProgressBar(10))
                    {
                        return;
                    }
                }
            }
        }
        bool updateProgressBar(int counterLimit)
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
                    GetFiles(directorySearch.Directory, directorySearch.SearchPattern);
                }
                else
                {
                    InvalidateFilename();
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
            ListViewItem lvi = listBoxItems[LbxFiles.SelectedIndex];
            rbxFullSelectedPath.Text = (string)lvi.Tag;
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
        private void lbxFiles_SizeChanged(object sender, EventArgs e)
        {
            if (formShown)
            {
                InitList();
                UpdateListBox();
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

                string filePath = (string)lvi.Tag;
                string fileExt = Path.GetExtension(filePath).ToLower();
                if (string.IsNullOrEmpty(fileExt))
                    fileExt = "-";
                int imgIndex = iconCache.Images.IndexOfKey(fileExt);
                if (imgIndex < 0)
                {
                    Bitmap bmp = SHGetIcon.GetBmp(filePath, true);
                    iconCache.Images.Add(fileExt, bmp);
                    imgIndex = iconCache.Images.IndexOfKey(fileExt);
                }
                iconCache.Draw(e.Graphics, new Point(e.Bounds.Location.X + 1, e.Bounds.Location.Y), imgIndex);

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
                    if (LbxFiles.SelectedIndices.Contains(e.Index))
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
                    if (LbxFiles.SelectedIndices.Contains(e.Index))
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
                    LbxFiles.Font, new Point(e.Bounds.Location.X + 17, e.Bounds.Location.Y), firstColor);
                if (txt.Length > fontSwitchIndex)
                {
                    SizeF sf = TextRenderer.MeasureText(txt.Substring(0, fontSwitchIndex), LbxFiles.Font);
                    TextRenderer.DrawText(e.Graphics, txt.Substring(fontSwitchIndex), LbxFiles.Font,
                        new Point((int)sf.Width + 17, e.Bounds.Y), secondColor);
                }
            }
        }
        #endregion

        #region " Other events "
        private void tbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnFolderUp.Visible && (e.Modifiers == Keys.Alt) && (e.KeyCode == Keys.Up))
            {
                btnFolderUp.PerformClick();
            }
            else if (EventKeys2SendKeys.ContainsKey(e.KeyCode))
            {
                KeySendToLbx = true;
                LbxFiles.Focus();
                SendKeys.Send(EventKeys2SendKeys[e.KeyCode]);
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
        private void btnAutoInvalidateFilename_Click(object sender, EventArgs e)
        {
            Main.AutoInvalidateFilenames = btnAutoInvalidateFilenames.Checked;
            StartBackgroundWorker(null);
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
                    allFiles = new List<string>();
                    StartBackgroundWorker(directorySearch);
                }
            }
        }
        #endregion
    }
}
