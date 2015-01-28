namespace NppFileSearch
{
    partial class frmOpenFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbxSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lbxFiles = new System.Windows.Forms.ListBox();
            this.btnOpenSelected = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbxFullSelectedPath = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnCaseSensitiveSearch = new System.Windows.Forms.ToolStripButton();
            this.btnCheckFilesExist = new System.Windows.Forms.ToolStripButton();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblEmpty = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxSearch
            // 
            this.tbxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSearch.Location = new System.Drawing.Point(12, 34);
            this.tbxSearch.Name = "tbxSearch";
            this.tbxSearch.Size = new System.Drawing.Size(302, 20);
            this.tbxSearch.TabIndex = 0;
            this.tbxSearch.TextChanged += new System.EventHandler(this.tbxSearch_TextChanged);
            this.tbxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxSearch_KeyDown);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(9, 12);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(226, 13);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Enter search fragments (separated by spaces):";
            // 
            // lbxFiles
            // 
            this.lbxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxFiles.DisplayMember = "Text";
            this.lbxFiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbxFiles.FormattingEnabled = true;
            this.lbxFiles.Location = new System.Drawing.Point(12, 126);
            this.lbxFiles.Name = "lbxFiles";
            this.lbxFiles.Size = new System.Drawing.Size(514, 251);
            this.lbxFiles.TabIndex = 2;
            this.lbxFiles.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxFiles_DrawItem);
            this.lbxFiles.SelectedIndexChanged += new System.EventHandler(this.lbxFiles_SelectedIndexChanged);
            this.lbxFiles.SizeChanged += new System.EventHandler(this.lbxFiles_SizeChanged);
            this.lbxFiles.DoubleClick += new System.EventHandler(this.lbxFiles_DoubleClick);
            this.lbxFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbxFiles_KeyDown);
            // 
            // btnOpenSelected
            // 
            this.btnOpenSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSelected.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOpenSelected.Location = new System.Drawing.Point(328, 32);
            this.btnOpenSelected.Name = "btnOpenSelected";
            this.btnOpenSelected.Size = new System.Drawing.Size(93, 23);
            this.btnOpenSelected.TabIndex = 4;
            this.btnOpenSelected.Text = "Open selected";
            this.btnOpenSelected.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(434, 32);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbxFullSelectedPath
            // 
            this.tbxFullSelectedPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFullSelectedPath.Location = new System.Drawing.Point(12, 62);
            this.tbxFullSelectedPath.Multiline = true;
            this.tbxFullSelectedPath.Name = "tbxFullSelectedPath";
            this.tbxFullSelectedPath.ReadOnly = true;
            this.tbxFullSelectedPath.Size = new System.Drawing.Size(514, 56);
            this.tbxFullSelectedPath.TabIndex = 6;
            this.tbxFullSelectedPath.TabStop = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCaseSensitiveSearch,
            this.btnCheckFilesExist,
            this.lblProgress,
            this.pbProgress,
            this.lblEmpty,
            this.lblResult});
            this.statusStrip.Location = new System.Drawing.Point(0, 383);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(539, 22);
            this.statusStrip.TabIndex = 9;
            // 
            // btnCaseSensitiveSearch
            // 
            this.btnCaseSensitiveSearch.CheckOnClick = true;
            this.btnCaseSensitiveSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCaseSensitiveSearch.Image = global::NppFileSearch.Properties.Resources.case_sensitive;
            this.btnCaseSensitiveSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCaseSensitiveSearch.Name = "btnCaseSensitiveSearch";
            this.btnCaseSensitiveSearch.Size = new System.Drawing.Size(23, 20);
            this.btnCaseSensitiveSearch.Text = "Case sensitive search";
            this.btnCaseSensitiveSearch.Click += new System.EventHandler(this.btnCaseSensitiveSearch_Click);
            // 
            // btnCheckFilesExist
            // 
            this.btnCheckFilesExist.CheckOnClick = true;
            this.btnCheckFilesExist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCheckFilesExist.Image = global::NppFileSearch.Properties.Resources.file_check;
            this.btnCheckFilesExist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckFilesExist.Name = "btnCheckFilesExist";
            this.btnCheckFilesExist.Size = new System.Drawing.Size(23, 20);
            this.btnCheckFilesExist.Text = "Check if files exist";
            this.btnCheckFilesExist.Click += new System.EventHandler(this.btnCheckFilesExist_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(83, 17);
            this.lblProgress.Text = "Updating list...";
            this.lblProgress.Visible = false;
            // 
            // pbProgress
            // 
            this.pbProgress.Enabled = false;
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(227, 16);
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbProgress.Visible = false;
            // 
            // lblEmpty
            // 
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(410, 17);
            this.lblEmpty.Spring = true;
            // 
            // lblResult
            // 
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(68, 17);
            this.lblResult.Text = "Result: 0 / 0";
            // 
            // frmOpenFile
            // 
            this.AcceptButton = this.btnOpenSelected;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(539, 405);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tbxFullSelectedPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOpenSelected);
            this.Controls.Add(this.lbxFiles);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.tbxSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(555, 444);
            this.Name = "frmOpenFile";
            this.Opacity = 0.95D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select file to open";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOpenFile_FormClosing);
            this.Load += new System.EventHandler(this.frmOpenFile_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ListBox lbxFiles;
        private System.Windows.Forms.Button btnOpenSelected;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TextBox tbxFullSelectedPath;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.ToolStripProgressBar pbProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblEmpty;
        private System.Windows.Forms.ToolStripStatusLabel lblResult;
        private System.Windows.Forms.ToolStripButton btnCaseSensitiveSearch;
        private System.Windows.Forms.ToolStripButton btnCheckFilesExist;
    }
}