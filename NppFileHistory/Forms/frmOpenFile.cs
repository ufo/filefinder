using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NppFileHistory
{
    public partial class frmOpenFile : Form
    {
        List<string> Files;
        Dictionary<string, string> FormattedFiles;
        public frmOpenFile(List<string> files)
        {
            InitializeComponent();
            Files = files;
            InitList();
        }

        void InitList()
        {
            FormattedFiles = new Dictionary<string, string>();
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
                TextRenderer.MeasureText(f, Font, new System.Drawing.Size(lblFullSelectedPath.Width - 20, 0),
                    TextFormatFlags.ModifyString | TextFormatFlags.PathEllipsis);
                FormattedFiles[file] = f;
            }
        }

        void UpdateListBox()
        {
            string pattern = tbxSearch.Text.Trim();
            lblFullSelectedPath.Text = "";

            lbxFiles.BeginUpdate();
            lbxFiles.Items.Clear();

            if (string.IsNullOrEmpty(pattern))
            {
                foreach (string file in Files)
                {
                    ListViewItem lvi = new ListViewItem(FormattedFiles[file]);
                    lvi.Tag = file;
                    lbxFiles.Items.Add(lvi);
                }
            }
            else
            {
                string[] patterns = pattern.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                StringComparison strCompMode = Main.caseSensitiveSearch ?
                    StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                foreach (string file in Files)
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
                    }
                }
            }

            if ((lbxFiles.SelectedItem == null) && (lbxFiles.Items.Count > 0))
            {
                lbxFiles.SelectedIndex = 0;
            }

            lbxFiles.EndUpdate();
        }

        private void frmOpenFile_Load(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private void lbxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem lvi = (ListViewItem)lbxFiles.SelectedItem;
            lblFullSelectedPath.Text = (string)lvi.Tag;
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
            {Keys.PageDown, "{PGDN}"}
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
