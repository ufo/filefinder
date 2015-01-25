using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace NppFileSearch
{
    public partial class frmOpenFile : Form
    {
        string CallerName;
        List<string> Files;
        Dictionary<string, string> FormattedFiles;
        BackgroundWorker bw = null;

        public frmOpenFile(string callerName, List<string> files)
        {
            InitializeComponent();

            CallerName = callerName;
            Files = files;
            InitForm();
        }
        public frmOpenFile(string callerName, string folderPath)
        {
            InitializeComponent();

            CallerName = callerName;
            Files = new List<string>();
            InitForm();

            lblProgress.Visible = true;
            pbProgress.Visible = true;
            pbProgress.Enabled = true;

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync(folderPath);
        }

        void InitForm()
        {
            Text = string.Format("{0}: {1}", CallerName, Text);
            Width = Main.windowWidth;
            Height = Main.windowHeight;
        }

        void InitList()
        {
            FormattedFiles = new Dictionary<string, string>();
            lock (Files)
            {
                foreach (string file in Files)
                {
                    string f = string.Copy(file);
                    if (Main.filePathFormat == Main.FilePathFormat.FileNameFirst)
                    {
                        f = f.Replace("...", ",,,");
                        string dirName = Path.GetDirectoryName(f);
                        string fileName = Path.GetFileName(f);
                        f = string.Format("{0} ({1})", fileName, dirName);
                    }
                    TextRenderer.MeasureText(f, Font, new System.Drawing.Size(tbxFullSelectedPath.Width - 20, 0),
                        TextFormatFlags.ModifyString | TextFormatFlags.PathEllipsis);
                    FormattedFiles[file] = f;
                }
            }
        }

        void UpdateListBox()
        {
            string pattern = tbxSearch.Text.Trim();
            string oldSelection = tbxFullSelectedPath.Text;
            tbxFullSelectedPath.Text = "";

            lbxFiles.BeginUpdate();
            lbxFiles.Items.Clear();

            if (string.IsNullOrEmpty(pattern))
            {
                foreach (string file in FormattedFiles.Keys)
                {
                    ListViewItem lvi = new ListViewItem(FormattedFiles[file]);
                    lvi.Tag = file;
                    lbxFiles.Items.Add(lvi);
                    if (file == oldSelection)
                    {
                        lbxFiles.SelectedIndex = lbxFiles.Items.Count - 1;
                    }
                }
            }
            else
            {
                string[] patterns = pattern.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                StringComparison strCompMode = Main.caseSensitiveSearch ?
                    StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                foreach (string file in FormattedFiles.Keys)
                {
                    bool match = true;
                    foreach (string _pat in patterns)
                    {
                        if (!(file.IndexOf(_pat, strCompMode) >= 0))
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        ListViewItem lvi = new ListViewItem(FormattedFiles[file]);
                        lvi.Tag = file;
                        lbxFiles.Items.Add(lvi);
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

            lblResult.Text = string.Format("Result: {0} / {1}", lbxFiles.Items.Count, FormattedFiles.Count);
        }

        int FileCounter = 0;
        void GetFiles(string folderPath)
        {
            foreach (string dir in Directory.GetDirectories(folderPath))
            {
                if (bw.CancellationPending == true)
                {
                    break;
                }
                foreach (string file in Directory.GetFiles(dir))
                {
                    if (bw.CancellationPending == true)
                    {
                        break;
                    }
                    lock (Files)
                    {
                        Files.Add(file);
                    }
                    FileCounter++;
                    if (FileCounter == 100)
                    {
                        FileCounter = 0;
                        bw.ReportProgress(0);
                    }
                    if (FileCounter % 2 == 0)
                    {
                        System.Threading.Thread.Sleep(1);
                    }
                }
                GetFiles(dir);
            }
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string folderPath = (string)e.Argument;
                GetFiles(folderPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, CallerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Main.windowWidth = Width;
            Main.windowHeight = Height;
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private void lbxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem lvi = (ListViewItem)lbxFiles.SelectedItem;
            tbxFullSelectedPath.Text = (string)lvi.Tag;
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
    }
}
