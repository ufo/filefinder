namespace FileFinder
{
    partial class frmOptions
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
            this.lblToolbarButtons = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblMaxHistoryLength = new System.Windows.Forms.Label();
            this.nudMaxHistoryLength = new System.Windows.Forms.NumericUpDown();
            this.cbxCaseSensitiveSearch = new System.Windows.Forms.CheckBox();
            this.cbxDisplayedFilePathFormat = new System.Windows.Forms.ComboBox();
            this.lblDisplayedFilePathFormat = new System.Windows.Forms.Label();
            this.gbxDirectorySearch = new System.Windows.Forms.GroupBox();
            this.tbxDirSearchExclusions = new System.Windows.Forms.TextBox();
            this.lblDirSearchExclusions = new System.Windows.Forms.Label();
            this.gbxHistorySearch = new System.Windows.Forms.GroupBox();
            this.tbxHistoryExclusions = new System.Windows.Forms.TextBox();
            this.lblHistoryExclusions = new System.Windows.Forms.Label();
            this.cbxAutoValidateFilenames = new System.Windows.Forms.CheckBox();
            this.gbxCommon = new System.Windows.Forms.GroupBox();
            this.btnOpenLastClosedFile = new System.Windows.Forms.CheckBox();
            this.btnOpenFromFileHistory = new System.Windows.Forms.CheckBox();
            this.btnSearchInDirectoryExplicitly = new System.Windows.Forms.CheckBox();
            this.btnOpenFromDirectoryGreedy = new System.Windows.Forms.CheckBox();
            this.ttOptions = new System.Windows.Forms.ToolTip(this.components);
            this.cbxShowFilteredPaths = new System.Windows.Forms.CheckBox();
            this.cbxBypassFSR = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHistoryLength)).BeginInit();
            this.gbxDirectorySearch.SuspendLayout();
            this.gbxHistorySearch.SuspendLayout();
            this.gbxCommon.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblToolbarButtons
            // 
            this.lblToolbarButtons.AutoSize = true;
            this.lblToolbarButtons.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolbarButtons.Location = new System.Drawing.Point(7, 25);
            this.lblToolbarButtons.Name = "lblToolbarButtons";
            this.lblToolbarButtons.Size = new System.Drawing.Size(163, 13);
            this.lblToolbarButtons.TabIndex = 0;
            this.lblToolbarButtons.Text = "Tool bar buttons (restart required)";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(578, 348);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(474, 348);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblMaxHistoryLength
            // 
            this.lblMaxHistoryLength.AutoSize = true;
            this.lblMaxHistoryLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxHistoryLength.Location = new System.Drawing.Point(6, 31);
            this.lblMaxHistoryLength.Name = "lblMaxHistoryLength";
            this.lblMaxHistoryLength.Size = new System.Drawing.Size(116, 13);
            this.lblMaxHistoryLength.TabIndex = 8;
            this.lblMaxHistoryLength.Text = "Maximum history length";
            // 
            // nudMaxHistoryLength
            // 
            this.nudMaxHistoryLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMaxHistoryLength.Location = new System.Drawing.Point(180, 29);
            this.nudMaxHistoryLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxHistoryLength.Name = "nudMaxHistoryLength";
            this.nudMaxHistoryLength.Size = new System.Drawing.Size(127, 20);
            this.nudMaxHistoryLength.TabIndex = 9;
            // 
            // cbxCaseSensitiveSearch
            // 
            this.cbxCaseSensitiveSearch.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxCaseSensitiveSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCaseSensitiveSearch.Location = new System.Drawing.Point(6, 53);
            this.cbxCaseSensitiveSearch.Name = "cbxCaseSensitiveSearch";
            this.cbxCaseSensitiveSearch.Size = new System.Drawing.Size(301, 24);
            this.cbxCaseSensitiveSearch.TabIndex = 10;
            this.cbxCaseSensitiveSearch.Text = "Case sensitive search filter";
            this.cbxCaseSensitiveSearch.UseVisualStyleBackColor = true;
            // 
            // cbxDisplayedFilePathFormat
            // 
            this.cbxDisplayedFilePathFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDisplayedFilePathFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDisplayedFilePathFormat.FormattingEnabled = true;
            this.cbxDisplayedFilePathFormat.Location = new System.Drawing.Point(180, 87);
            this.cbxDisplayedFilePathFormat.Name = "cbxDisplayedFilePathFormat";
            this.cbxDisplayedFilePathFormat.Size = new System.Drawing.Size(127, 21);
            this.cbxDisplayedFilePathFormat.TabIndex = 12;
            // 
            // lblDisplayedFilePathFormat
            // 
            this.lblDisplayedFilePathFormat.AutoSize = true;
            this.lblDisplayedFilePathFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayedFilePathFormat.Location = new System.Drawing.Point(7, 90);
            this.lblDisplayedFilePathFormat.Name = "lblDisplayedFilePathFormat";
            this.lblDisplayedFilePathFormat.Size = new System.Drawing.Size(125, 13);
            this.lblDisplayedFilePathFormat.TabIndex = 11;
            this.lblDisplayedFilePathFormat.Text = "Displayed file path format";
            // 
            // gbxDirectorySearch
            // 
            this.gbxDirectorySearch.Controls.Add(this.cbxBypassFSR);
            this.gbxDirectorySearch.Controls.Add(this.cbxShowFilteredPaths);
            this.gbxDirectorySearch.Controls.Add(this.tbxDirSearchExclusions);
            this.gbxDirectorySearch.Controls.Add(this.lblDirSearchExclusions);
            this.gbxDirectorySearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDirectorySearch.Location = new System.Drawing.Point(12, 150);
            this.gbxDirectorySearch.Name = "gbxDirectorySearch";
            this.gbxDirectorySearch.Size = new System.Drawing.Size(320, 221);
            this.gbxDirectorySearch.TabIndex = 15;
            this.gbxDirectorySearch.TabStop = false;
            this.gbxDirectorySearch.Text = "Directory search";
            // 
            // tbxDirSearchExclusions
            // 
            this.tbxDirSearchExclusions.AcceptsReturn = true;
            this.tbxDirSearchExclusions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDirSearchExclusions.Location = new System.Drawing.Point(180, 28);
            this.tbxDirSearchExclusions.Multiline = true;
            this.tbxDirSearchExclusions.Name = "tbxDirSearchExclusions";
            this.tbxDirSearchExclusions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxDirSearchExclusions.Size = new System.Drawing.Size(127, 115);
            this.tbxDirSearchExclusions.TabIndex = 14;
            this.tbxDirSearchExclusions.WordWrap = false;
            // 
            // lblDirSearchExclusions
            // 
            this.lblDirSearchExclusions.AutoSize = true;
            this.lblDirSearchExclusions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirSearchExclusions.Location = new System.Drawing.Point(6, 31);
            this.lblDirSearchExclusions.Name = "lblDirSearchExclusions";
            this.lblDirSearchExclusions.Size = new System.Drawing.Size(160, 13);
            this.lblDirSearchExclusions.TabIndex = 16;
            this.lblDirSearchExclusions.Text = "Excluded directories / file names";
            // 
            // gbxHistorySearch
            // 
            this.gbxHistorySearch.Controls.Add(this.tbxHistoryExclusions);
            this.gbxHistorySearch.Controls.Add(this.lblHistoryExclusions);
            this.gbxHistorySearch.Controls.Add(this.cbxAutoValidateFilenames);
            this.gbxHistorySearch.Controls.Add(this.lblMaxHistoryLength);
            this.gbxHistorySearch.Controls.Add(this.nudMaxHistoryLength);
            this.gbxHistorySearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxHistorySearch.Location = new System.Drawing.Point(347, 12);
            this.gbxHistorySearch.Name = "gbxHistorySearch";
            this.gbxHistorySearch.Size = new System.Drawing.Size(320, 224);
            this.gbxHistorySearch.TabIndex = 17;
            this.gbxHistorySearch.TabStop = false;
            this.gbxHistorySearch.Text = "History search";
            // 
            // tbxHistoryExclusions
            // 
            this.tbxHistoryExclusions.AcceptsReturn = true;
            this.tbxHistoryExclusions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxHistoryExclusions.Location = new System.Drawing.Point(180, 92);
            this.tbxHistoryExclusions.Multiline = true;
            this.tbxHistoryExclusions.Name = "tbxHistoryExclusions";
            this.tbxHistoryExclusions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxHistoryExclusions.Size = new System.Drawing.Size(127, 115);
            this.tbxHistoryExclusions.TabIndex = 14;
            this.tbxHistoryExclusions.WordWrap = false;
            // 
            // lblHistoryExclusions
            // 
            this.lblHistoryExclusions.AutoSize = true;
            this.lblHistoryExclusions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistoryExclusions.Location = new System.Drawing.Point(7, 95);
            this.lblHistoryExclusions.Name = "lblHistoryExclusions";
            this.lblHistoryExclusions.Size = new System.Drawing.Size(160, 13);
            this.lblHistoryExclusions.TabIndex = 16;
            this.lblHistoryExclusions.Text = "Excluded directories / file names";
            // 
            // cbxAutoValidateFilenames
            // 
            this.cbxAutoValidateFilenames.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxAutoValidateFilenames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAutoValidateFilenames.Location = new System.Drawing.Point(6, 59);
            this.cbxAutoValidateFilenames.Name = "cbxAutoValidateFilenames";
            this.cbxAutoValidateFilenames.Size = new System.Drawing.Size(301, 24);
            this.cbxAutoValidateFilenames.TabIndex = 11;
            this.cbxAutoValidateFilenames.Text = "Auto validate filenames";
            this.cbxAutoValidateFilenames.UseVisualStyleBackColor = true;
            // 
            // gbxCommon
            // 
            this.gbxCommon.Controls.Add(this.btnOpenLastClosedFile);
            this.gbxCommon.Controls.Add(this.btnOpenFromFileHistory);
            this.gbxCommon.Controls.Add(this.btnSearchInDirectoryExplicitly);
            this.gbxCommon.Controls.Add(this.btnOpenFromDirectoryGreedy);
            this.gbxCommon.Controls.Add(this.lblToolbarButtons);
            this.gbxCommon.Controls.Add(this.cbxCaseSensitiveSearch);
            this.gbxCommon.Controls.Add(this.cbxDisplayedFilePathFormat);
            this.gbxCommon.Controls.Add(this.lblDisplayedFilePathFormat);
            this.gbxCommon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxCommon.Location = new System.Drawing.Point(12, 12);
            this.gbxCommon.Name = "gbxCommon";
            this.gbxCommon.Size = new System.Drawing.Size(320, 124);
            this.gbxCommon.TabIndex = 18;
            this.gbxCommon.TabStop = false;
            this.gbxCommon.Text = "Common";
            // 
            // btnOpenLastClosedFile
            // 
            this.btnOpenLastClosedFile.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnOpenLastClosedFile.AutoSize = true;
            this.btnOpenLastClosedFile.Image = global::FileFinder.Properties.Resources.open_last_closed_file;
            this.btnOpenLastClosedFile.Location = new System.Drawing.Point(285, 20);
            this.btnOpenLastClosedFile.Name = "btnOpenLastClosedFile";
            this.btnOpenLastClosedFile.Size = new System.Drawing.Size(22, 22);
            this.btnOpenLastClosedFile.TabIndex = 17;
            this.ttOptions.SetToolTip(this.btnOpenLastClosedFile, "Open last closed file");
            this.btnOpenLastClosedFile.UseVisualStyleBackColor = true;
            // 
            // btnOpenFromFileHistory
            // 
            this.btnOpenFromFileHistory.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnOpenFromFileHistory.AutoSize = true;
            this.btnOpenFromFileHistory.Image = global::FileFinder.Properties.Resources.open_from_file_history;
            this.btnOpenFromFileHistory.Location = new System.Drawing.Point(250, 20);
            this.btnOpenFromFileHistory.Name = "btnOpenFromFileHistory";
            this.btnOpenFromFileHistory.Size = new System.Drawing.Size(22, 22);
            this.btnOpenFromFileHistory.TabIndex = 16;
            this.ttOptions.SetToolTip(this.btnOpenFromFileHistory, "Open from file history...");
            this.btnOpenFromFileHistory.UseVisualStyleBackColor = true;
            // 
            // btnSearchInDirectoryExplicitly
            // 
            this.btnSearchInDirectoryExplicitly.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnSearchInDirectoryExplicitly.AutoSize = true;
            this.btnSearchInDirectoryExplicitly.Image = global::FileFinder.Properties.Resources.search_in_directory_explicitly;
            this.btnSearchInDirectoryExplicitly.Location = new System.Drawing.Point(215, 20);
            this.btnSearchInDirectoryExplicitly.Name = "btnSearchInDirectoryExplicitly";
            this.btnSearchInDirectoryExplicitly.Size = new System.Drawing.Size(22, 22);
            this.btnSearchInDirectoryExplicitly.TabIndex = 15;
            this.ttOptions.SetToolTip(this.btnSearchInDirectoryExplicitly, "Search in directory (explicitly)...");
            this.btnSearchInDirectoryExplicitly.UseVisualStyleBackColor = true;
            // 
            // btnOpenFromDirectoryGreedy
            // 
            this.btnOpenFromDirectoryGreedy.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnOpenFromDirectoryGreedy.AutoSize = true;
            this.btnOpenFromDirectoryGreedy.Image = global::FileFinder.Properties.Resources.open_from_directory_greedy;
            this.btnOpenFromDirectoryGreedy.Location = new System.Drawing.Point(180, 20);
            this.btnOpenFromDirectoryGreedy.Name = "btnOpenFromDirectoryGreedy";
            this.btnOpenFromDirectoryGreedy.Size = new System.Drawing.Size(22, 22);
            this.btnOpenFromDirectoryGreedy.TabIndex = 14;
            this.ttOptions.SetToolTip(this.btnOpenFromDirectoryGreedy, "Open from directory (greedy)...");
            this.btnOpenFromDirectoryGreedy.UseVisualStyleBackColor = true;
            // 
            // cbxShowFilteredPaths
            // 
            this.cbxShowFilteredPaths.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxShowFilteredPaths.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxShowFilteredPaths.Location = new System.Drawing.Point(6, 154);
            this.cbxShowFilteredPaths.Name = "cbxShowFilteredPaths";
            this.cbxShowFilteredPaths.Size = new System.Drawing.Size(301, 24);
            this.cbxShowFilteredPaths.TabIndex = 17;
            this.cbxShowFilteredPaths.Text = "Show filtered paths";
            this.cbxShowFilteredPaths.UseVisualStyleBackColor = true;
            // 
            // cbxBypassFSR
            // 
            this.cbxBypassFSR.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxBypassFSR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxBypassFSR.Location = new System.Drawing.Point(6, 184);
            this.cbxBypassFSR.Name = "cbxBypassFSR";
            this.cbxBypassFSR.Size = new System.Drawing.Size(301, 24);
            this.cbxBypassFSR.TabIndex = 18;
            this.cbxBypassFSR.Text = "Bypass file system redirection";
            this.cbxBypassFSR.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(681, 384);
            this.Controls.Add(this.gbxCommon);
            this.Controls.Add(this.gbxHistorySearch);
            this.Controls.Add(this.gbxDirectorySearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.Opacity = 0.95D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FileFinder";
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHistoryLength)).EndInit();
            this.gbxDirectorySearch.ResumeLayout(false);
            this.gbxDirectorySearch.PerformLayout();
            this.gbxHistorySearch.ResumeLayout(false);
            this.gbxHistorySearch.PerformLayout();
            this.gbxCommon.ResumeLayout(false);
            this.gbxCommon.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblToolbarButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblMaxHistoryLength;
        public System.Windows.Forms.NumericUpDown nudMaxHistoryLength;
        public System.Windows.Forms.CheckBox cbxCaseSensitiveSearch;
        public System.Windows.Forms.ComboBox cbxDisplayedFilePathFormat;
        private System.Windows.Forms.Label lblDisplayedFilePathFormat;
        private System.Windows.Forms.GroupBox gbxDirectorySearch;
        public System.Windows.Forms.TextBox tbxDirSearchExclusions;
        private System.Windows.Forms.GroupBox gbxHistorySearch;
        private System.Windows.Forms.Label lblDirSearchExclusions;
        public System.Windows.Forms.CheckBox cbxAutoValidateFilenames;
        private System.Windows.Forms.GroupBox gbxCommon;
        public System.Windows.Forms.TextBox tbxHistoryExclusions;
        private System.Windows.Forms.Label lblHistoryExclusions;
        public System.Windows.Forms.CheckBox btnOpenFromDirectoryGreedy;
        public System.Windows.Forms.CheckBox btnOpenLastClosedFile;
        public System.Windows.Forms.CheckBox btnOpenFromFileHistory;
        public System.Windows.Forms.CheckBox btnSearchInDirectoryExplicitly;
        private System.Windows.Forms.ToolTip ttOptions;
        public System.Windows.Forms.CheckBox cbxBypassFSR;
        public System.Windows.Forms.CheckBox cbxShowFilteredPaths;
    }
}