namespace NppFileSearch
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
            this.lblTbbFunction = new System.Windows.Forms.Label();
            this.cbxTbbFunction = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblMaxHistoryLength = new System.Windows.Forms.Label();
            this.nudMaxHistoryLength = new System.Windows.Forms.NumericUpDown();
            this.cbxCaseSensitiveSearch = new System.Windows.Forms.CheckBox();
            this.cbxDisplayedFilePathFormat = new System.Windows.Forms.ComboBox();
            this.lblDisplayedFilePathFormat = new System.Windows.Forms.Label();
            this.tbxDirSearchExcludedFileExts = new System.Windows.Forms.TextBox();
            this.gbxDirectorySearch = new System.Windows.Forms.GroupBox();
            this.lblDirSearchExcludedFileExt = new System.Windows.Forms.Label();
            this.tbxDirSearchExcludedDirs = new System.Windows.Forms.TextBox();
            this.lblDirSearchExcludedDirs = new System.Windows.Forms.Label();
            this.gbxHistorySearch = new System.Windows.Forms.GroupBox();
            this.tbxHistoryExcludedDirs = new System.Windows.Forms.TextBox();
            this.lblHistoryExcludedDirs = new System.Windows.Forms.Label();
            this.cbxAutoCheckFilesExist = new System.Windows.Forms.CheckBox();
            this.gbxCommon = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHistoryLength)).BeginInit();
            this.gbxDirectorySearch.SuspendLayout();
            this.gbxHistorySearch.SuspendLayout();
            this.gbxCommon.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTbbFunction
            // 
            this.lblTbbFunction.AutoSize = true;
            this.lblTbbFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTbbFunction.Location = new System.Drawing.Point(7, 25);
            this.lblTbbFunction.Name = "lblTbbFunction";
            this.lblTbbFunction.Size = new System.Drawing.Size(196, 13);
            this.lblTbbFunction.TabIndex = 0;
            this.lblTbbFunction.Text = "Toolbar button function (restart required)";
            // 
            // cbxTbbFunction
            // 
            this.cbxTbbFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTbbFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTbbFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTbbFunction.FormattingEnabled = true;
            this.cbxTbbFunction.Location = new System.Drawing.Point(221, 22);
            this.cbxTbbFunction.Name = "cbxTbbFunction";
            this.cbxTbbFunction.Size = new System.Drawing.Size(178, 21);
            this.cbxTbbFunction.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(331, 605);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(233, 605);
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
            this.nudMaxHistoryLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaxHistoryLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMaxHistoryLength.Location = new System.Drawing.Point(221, 29);
            this.nudMaxHistoryLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxHistoryLength.Name = "nudMaxHistoryLength";
            this.nudMaxHistoryLength.Size = new System.Drawing.Size(178, 20);
            this.nudMaxHistoryLength.TabIndex = 9;
            // 
            // cbxCaseSensitiveSearch
            // 
            this.cbxCaseSensitiveSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxCaseSensitiveSearch.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxCaseSensitiveSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCaseSensitiveSearch.Location = new System.Drawing.Point(6, 53);
            this.cbxCaseSensitiveSearch.Name = "cbxCaseSensitiveSearch";
            this.cbxCaseSensitiveSearch.Size = new System.Drawing.Size(393, 24);
            this.cbxCaseSensitiveSearch.TabIndex = 10;
            this.cbxCaseSensitiveSearch.Text = "Case sensitive search filter";
            this.cbxCaseSensitiveSearch.UseVisualStyleBackColor = true;
            // 
            // cbxDisplayedFilePathFormat
            // 
            this.cbxDisplayedFilePathFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDisplayedFilePathFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDisplayedFilePathFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDisplayedFilePathFormat.FormattingEnabled = true;
            this.cbxDisplayedFilePathFormat.Location = new System.Drawing.Point(221, 87);
            this.cbxDisplayedFilePathFormat.Name = "cbxDisplayedFilePathFormat";
            this.cbxDisplayedFilePathFormat.Size = new System.Drawing.Size(178, 21);
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
            // tbxDirSearchExcludedFileExts
            // 
            this.tbxDirSearchExcludedFileExts.AcceptsReturn = true;
            this.tbxDirSearchExcludedFileExts.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxDirSearchExcludedFileExts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDirSearchExcludedFileExts.Location = new System.Drawing.Point(221, 44);
            this.tbxDirSearchExcludedFileExts.Multiline = true;
            this.tbxDirSearchExcludedFileExts.Name = "tbxDirSearchExcludedFileExts";
            this.tbxDirSearchExcludedFileExts.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxDirSearchExcludedFileExts.Size = new System.Drawing.Size(175, 115);
            this.tbxDirSearchExcludedFileExts.TabIndex = 14;
            this.tbxDirSearchExcludedFileExts.WordWrap = false;
            // 
            // gbxDirectorySearch
            // 
            this.gbxDirectorySearch.Controls.Add(this.lblDirSearchExcludedFileExt);
            this.gbxDirectorySearch.Controls.Add(this.tbxDirSearchExcludedFileExts);
            this.gbxDirectorySearch.Controls.Add(this.tbxDirSearchExcludedDirs);
            this.gbxDirectorySearch.Controls.Add(this.lblDirSearchExcludedDirs);
            this.gbxDirectorySearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDirectorySearch.Location = new System.Drawing.Point(12, 413);
            this.gbxDirectorySearch.Name = "gbxDirectorySearch";
            this.gbxDirectorySearch.Size = new System.Drawing.Size(408, 177);
            this.gbxDirectorySearch.TabIndex = 15;
            this.gbxDirectorySearch.TabStop = false;
            this.gbxDirectorySearch.Text = "Directory search";
            // 
            // lblDirSearchExcludedFileExt
            // 
            this.lblDirSearchExcludedFileExt.AutoSize = true;
            this.lblDirSearchExcludedFileExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirSearchExcludedFileExt.Location = new System.Drawing.Point(218, 25);
            this.lblDirSearchExcludedFileExt.Name = "lblDirSearchExcludedFileExt";
            this.lblDirSearchExcludedFileExt.Size = new System.Drawing.Size(120, 13);
            this.lblDirSearchExcludedFileExt.TabIndex = 15;
            this.lblDirSearchExcludedFileExt.Text = "Excluded file extensions";
            // 
            // tbxDirSearchExcludedDirs
            // 
            this.tbxDirSearchExcludedDirs.AcceptsReturn = true;
            this.tbxDirSearchExcludedDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxDirSearchExcludedDirs.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxDirSearchExcludedDirs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDirSearchExcludedDirs.Location = new System.Drawing.Point(10, 44);
            this.tbxDirSearchExcludedDirs.Multiline = true;
            this.tbxDirSearchExcludedDirs.Name = "tbxDirSearchExcludedDirs";
            this.tbxDirSearchExcludedDirs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxDirSearchExcludedDirs.Size = new System.Drawing.Size(178, 115);
            this.tbxDirSearchExcludedDirs.TabIndex = 14;
            this.tbxDirSearchExcludedDirs.WordWrap = false;
            // 
            // lblDirSearchExcludedDirs
            // 
            this.lblDirSearchExcludedDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDirSearchExcludedDirs.AutoSize = true;
            this.lblDirSearchExcludedDirs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirSearchExcludedDirs.Location = new System.Drawing.Point(7, 25);
            this.lblDirSearchExcludedDirs.Name = "lblDirSearchExcludedDirs";
            this.lblDirSearchExcludedDirs.Size = new System.Drawing.Size(102, 13);
            this.lblDirSearchExcludedDirs.TabIndex = 16;
            this.lblDirSearchExcludedDirs.Text = "Excluded directories";
            // 
            // gbxHistorySearch
            // 
            this.gbxHistorySearch.Controls.Add(this.tbxHistoryExcludedDirs);
            this.gbxHistorySearch.Controls.Add(this.lblHistoryExcludedDirs);
            this.gbxHistorySearch.Controls.Add(this.cbxAutoCheckFilesExist);
            this.gbxHistorySearch.Controls.Add(this.lblMaxHistoryLength);
            this.gbxHistorySearch.Controls.Add(this.nudMaxHistoryLength);
            this.gbxHistorySearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxHistorySearch.Location = new System.Drawing.Point(12, 151);
            this.gbxHistorySearch.Name = "gbxHistorySearch";
            this.gbxHistorySearch.Size = new System.Drawing.Size(408, 247);
            this.gbxHistorySearch.TabIndex = 17;
            this.gbxHistorySearch.TabStop = false;
            this.gbxHistorySearch.Text = "History search";
            // 
            // tbxHistoryExcludedDirs
            // 
            this.tbxHistoryExcludedDirs.AcceptsReturn = true;
            this.tbxHistoryExcludedDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxHistoryExcludedDirs.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxHistoryExcludedDirs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxHistoryExcludedDirs.Location = new System.Drawing.Point(9, 114);
            this.tbxHistoryExcludedDirs.Multiline = true;
            this.tbxHistoryExcludedDirs.Name = "tbxHistoryExcludedDirs";
            this.tbxHistoryExcludedDirs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxHistoryExcludedDirs.Size = new System.Drawing.Size(178, 115);
            this.tbxHistoryExcludedDirs.TabIndex = 14;
            this.tbxHistoryExcludedDirs.WordWrap = false;
            // 
            // lblHistoryExcludedDirs
            // 
            this.lblHistoryExcludedDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHistoryExcludedDirs.AutoSize = true;
            this.lblHistoryExcludedDirs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistoryExcludedDirs.Location = new System.Drawing.Point(7, 95);
            this.lblHistoryExcludedDirs.Name = "lblHistoryExcludedDirs";
            this.lblHistoryExcludedDirs.Size = new System.Drawing.Size(102, 13);
            this.lblHistoryExcludedDirs.TabIndex = 16;
            this.lblHistoryExcludedDirs.Text = "Excluded directories";
            // 
            // cbxAutoCheckFilesExist
            // 
            this.cbxAutoCheckFilesExist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxAutoCheckFilesExist.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxAutoCheckFilesExist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAutoCheckFilesExist.Location = new System.Drawing.Point(6, 59);
            this.cbxAutoCheckFilesExist.Name = "cbxAutoCheckFilesExist";
            this.cbxAutoCheckFilesExist.Size = new System.Drawing.Size(393, 24);
            this.cbxAutoCheckFilesExist.TabIndex = 11;
            this.cbxAutoCheckFilesExist.Text = "Auto check if files exist";
            this.cbxAutoCheckFilesExist.UseVisualStyleBackColor = true;
            // 
            // gbxCommon
            // 
            this.gbxCommon.Controls.Add(this.lblTbbFunction);
            this.gbxCommon.Controls.Add(this.cbxTbbFunction);
            this.gbxCommon.Controls.Add(this.cbxCaseSensitiveSearch);
            this.gbxCommon.Controls.Add(this.cbxDisplayedFilePathFormat);
            this.gbxCommon.Controls.Add(this.lblDisplayedFilePathFormat);
            this.gbxCommon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxCommon.Location = new System.Drawing.Point(12, 12);
            this.gbxCommon.Name = "gbxCommon";
            this.gbxCommon.Size = new System.Drawing.Size(408, 124);
            this.gbxCommon.TabIndex = 18;
            this.gbxCommon.TabStop = false;
            this.gbxCommon.Text = "Common";
            // 
            // frmOptions
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(433, 642);
            this.Controls.Add(this.gbxCommon);
            this.Controls.Add(this.gbxHistorySearch);
            this.Controls.Add(this.gbxDirectorySearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOptions";
            this.Opacity = 0.95D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NppFileSearch";
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

        private System.Windows.Forms.Label lblTbbFunction;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.ComboBox cbxTbbFunction;
        private System.Windows.Forms.Label lblMaxHistoryLength;
        public System.Windows.Forms.NumericUpDown nudMaxHistoryLength;
        public System.Windows.Forms.CheckBox cbxCaseSensitiveSearch;
        public System.Windows.Forms.ComboBox cbxDisplayedFilePathFormat;
        private System.Windows.Forms.Label lblDisplayedFilePathFormat;
        private System.Windows.Forms.GroupBox gbxDirectorySearch;
        public System.Windows.Forms.TextBox tbxDirSearchExcludedFileExts;
        public System.Windows.Forms.TextBox tbxDirSearchExcludedDirs;
        private System.Windows.Forms.GroupBox gbxHistorySearch;
        private System.Windows.Forms.Label lblDirSearchExcludedDirs;
        private System.Windows.Forms.Label lblDirSearchExcludedFileExt;
        public System.Windows.Forms.CheckBox cbxAutoCheckFilesExist;
        private System.Windows.Forms.GroupBox gbxCommon;
        public System.Windows.Forms.TextBox tbxHistoryExcludedDirs;
        private System.Windows.Forms.Label lblHistoryExcludedDirs;
    }
}