namespace FileFinder
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
            this.components = new System.ComponentModel.Container();
            this.tbxSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.LbxFiles = new System.Windows.Forms.ListBox();
            this.btnOpenSelected = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnCaseSensitiveSearch = new System.Windows.Forms.ToolStripButton();
            this.btnAutoValidateFilenames = new System.Windows.Forms.ToolStripButton();
            this.btnShowFilteredPaths = new System.Windows.Forms.ToolStripButton();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblEmpty = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnFolderUp = new System.Windows.Forms.Button();
            this.ttOpenFile = new System.Windows.Forms.ToolTip(this.components);
            this.rbxFullSelectedPath = new System.Windows.Forms.RichTextBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxSearch
            // 
            this.tbxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSearch.Location = new System.Drawing.Point(12, 34);
            this.tbxSearch.Name = "tbxSearch";
            this.tbxSearch.Size = new System.Drawing.Size(278, 20);
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
            // LbxFiles
            // 
            this.LbxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LbxFiles.DisplayMember = "Text";
            this.LbxFiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LbxFiles.FormattingEnabled = true;
            this.LbxFiles.ItemHeight = 17;
            this.LbxFiles.Location = new System.Drawing.Point(12, 126);
            this.LbxFiles.Name = "LbxFiles";
            this.LbxFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LbxFiles.Size = new System.Drawing.Size(514, 242);
            this.LbxFiles.TabIndex = 2;
            this.LbxFiles.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxFiles_DrawItem);
            this.LbxFiles.SelectedIndexChanged += new System.EventHandler(this.lbxFiles_SelectedIndexChanged);
            this.LbxFiles.DoubleClick += new System.EventHandler(this.lbxFiles_DoubleClick);
            this.LbxFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbxFiles_KeyDown);
            // 
            // btnOpenSelected
            // 
            this.btnOpenSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSelected.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOpenSelected.Location = new System.Drawing.Point(335, 32);
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
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCaseSensitiveSearch,
            this.btnAutoValidateFilenames,
            this.btnShowFilteredPaths,
            this.lblProgress,
            this.pbProgress,
            this.lblEmpty,
            this.lblResult});
            this.statusStrip.Location = new System.Drawing.Point(0, 383);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(539, 22);
            this.statusStrip.TabIndex = 9;
            // 
            // btnCaseSensitiveSearch
            // 
            this.btnCaseSensitiveSearch.CheckOnClick = true;
            this.btnCaseSensitiveSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCaseSensitiveSearch.Image = global::FileFinder.Properties.Resources.case_sensitive;
            this.btnCaseSensitiveSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCaseSensitiveSearch.Name = "btnCaseSensitiveSearch";
            this.btnCaseSensitiveSearch.Size = new System.Drawing.Size(23, 20);
            this.btnCaseSensitiveSearch.Text = "Case sensitive search";
            this.btnCaseSensitiveSearch.Click += new System.EventHandler(this.btnCaseSensitiveSearch_Click);
            // 
            // btnAutoValidateFilenames
            // 
            this.btnAutoValidateFilenames.CheckOnClick = true;
            this.btnAutoValidateFilenames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAutoValidateFilenames.Image = global::FileFinder.Properties.Resources.file_check;
            this.btnAutoValidateFilenames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutoValidateFilenames.Name = "btnAutoValidateFilenames";
            this.btnAutoValidateFilenames.Size = new System.Drawing.Size(23, 20);
            this.btnAutoValidateFilenames.Text = "Auto validate filenames";
            this.btnAutoValidateFilenames.Click += new System.EventHandler(this.btnAutoValidateFilename_Click);
            // 
            // btnShowFilteredPaths
            // 
            this.btnShowFilteredPaths.CheckOnClick = true;
            this.btnShowFilteredPaths.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowFilteredPaths.Image = global::FileFinder.Properties.Resources.show_filtered_paths;
            this.btnShowFilteredPaths.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowFilteredPaths.Name = "btnShowFilteredPaths";
            this.btnShowFilteredPaths.Size = new System.Drawing.Size(23, 20);
            this.btnShowFilteredPaths.Text = "Show filtered paths";
            this.btnShowFilteredPaths.Click += new System.EventHandler(this.btnShowFilteredPaths_Click);
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
            this.lblEmpty.Size = new System.Drawing.Size(44, 17);
            this.lblEmpty.Spring = true;
            // 
            // lblResult
            // 
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(68, 17);
            this.lblResult.Text = "Result: 0 / 0";
            // 
            // btnFolderUp
            // 
            this.btnFolderUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFolderUp.Image = global::FileFinder.Properties.Resources.folder_up;
            this.btnFolderUp.Location = new System.Drawing.Point(296, 32);
            this.btnFolderUp.Name = "btnFolderUp";
            this.btnFolderUp.Size = new System.Drawing.Size(24, 23);
            this.btnFolderUp.TabIndex = 10;
            this.ttOpenFile.SetToolTip(this.btnFolderUp, "Up to parent folder (Alt + Up Arrow)");
            this.btnFolderUp.UseVisualStyleBackColor = true;
            this.btnFolderUp.Click += new System.EventHandler(this.btnFolderUp_Click);
            // 
            // rbxFullSelectedPath
            // 
            this.rbxFullSelectedPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbxFullSelectedPath.DetectUrls = false;
            this.rbxFullSelectedPath.Location = new System.Drawing.Point(12, 62);
            this.rbxFullSelectedPath.Name = "rbxFullSelectedPath";
            this.rbxFullSelectedPath.ReadOnly = true;
            this.rbxFullSelectedPath.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rbxFullSelectedPath.Size = new System.Drawing.Size(514, 56);
            this.rbxFullSelectedPath.TabIndex = 11;
            this.rbxFullSelectedPath.Text = "";
            this.rbxFullSelectedPath.TextChanged += new System.EventHandler(this.rbxFullSelectedPath_TextChanged);
            // 
            // frmOpenFile
            // 
            this.AcceptButton = this.btnOpenSelected;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(539, 405);
            this.Controls.Add(this.rbxFullSelectedPath);
            this.Controls.Add(this.btnFolderUp);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOpenSelected);
            this.Controls.Add(this.LbxFiles);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.tbxSearch);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(555, 444);
            this.Name = "frmOpenFile";
            this.Opacity = 0.95D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select files to open";
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
        private System.Windows.Forms.Button btnOpenSelected;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.ToolStripProgressBar pbProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblEmpty;
        private System.Windows.Forms.ToolStripStatusLabel lblResult;
        private System.Windows.Forms.ToolStripButton btnCaseSensitiveSearch;
        private System.Windows.Forms.ToolStripButton btnAutoValidateFilenames;
        public System.Windows.Forms.ListBox LbxFiles;
        private System.Windows.Forms.Button btnFolderUp;
        private System.Windows.Forms.ToolTip ttOpenFile;
        public System.Windows.Forms.RichTextBox rbxFullSelectedPath;
        private System.Windows.Forms.ToolStripButton btnShowFilteredPaths;
    }
}