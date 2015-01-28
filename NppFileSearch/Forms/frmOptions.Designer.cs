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
            this.cbxFilePathFormat = new System.Windows.Forms.ComboBox();
            this.lblFilePathFormat = new System.Windows.Forms.Label();
            this.tbxIncludedFileExts = new System.Windows.Forms.TextBox();
            this.gbxDirectorySearch = new System.Windows.Forms.GroupBox();
            this.lblExcludedDirs = new System.Windows.Forms.Label();
            this.tbxExcludedDirs = new System.Windows.Forms.TextBox();
            this.lblIncludedFileExt = new System.Windows.Forms.Label();
            this.gbxHistorySearch = new System.Windows.Forms.GroupBox();
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
            this.btnCancel.Location = new System.Drawing.Point(331, 449);
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
            this.btnSave.Location = new System.Drawing.Point(233, 449);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblMaxHistoryLength
            // 
            this.lblMaxHistoryLength.AutoSize = true;
            this.lblMaxHistoryLength.Location = new System.Drawing.Point(6, 31);
            this.lblMaxHistoryLength.Name = "lblMaxHistoryLength";
            this.lblMaxHistoryLength.Size = new System.Drawing.Size(116, 13);
            this.lblMaxHistoryLength.TabIndex = 8;
            this.lblMaxHistoryLength.Text = "Maximum history length";
            // 
            // nudMaxHistoryLength
            // 
            this.nudMaxHistoryLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.cbxCaseSensitiveSearch.Location = new System.Drawing.Point(6, 53);
            this.cbxCaseSensitiveSearch.Name = "cbxCaseSensitiveSearch";
            this.cbxCaseSensitiveSearch.Size = new System.Drawing.Size(393, 24);
            this.cbxCaseSensitiveSearch.TabIndex = 10;
            this.cbxCaseSensitiveSearch.Text = "Case sensitive search filter";
            this.cbxCaseSensitiveSearch.UseVisualStyleBackColor = true;
            // 
            // cbxFilePathFormat
            // 
            this.cbxFilePathFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFilePathFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFilePathFormat.FormattingEnabled = true;
            this.cbxFilePathFormat.Location = new System.Drawing.Point(221, 87);
            this.cbxFilePathFormat.Name = "cbxFilePathFormat";
            this.cbxFilePathFormat.Size = new System.Drawing.Size(178, 21);
            this.cbxFilePathFormat.TabIndex = 12;
            // 
            // lblFilePathFormat
            // 
            this.lblFilePathFormat.AutoSize = true;
            this.lblFilePathFormat.Location = new System.Drawing.Point(7, 90);
            this.lblFilePathFormat.Name = "lblFilePathFormat";
            this.lblFilePathFormat.Size = new System.Drawing.Size(79, 13);
            this.lblFilePathFormat.TabIndex = 11;
            this.lblFilePathFormat.Text = "File path format";
            // 
            // tbxIncludedFileExts
            // 
            this.tbxIncludedFileExts.AcceptsReturn = true;
            this.tbxIncludedFileExts.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxIncludedFileExts.Location = new System.Drawing.Point(10, 44);
            this.tbxIncludedFileExts.Multiline = true;
            this.tbxIncludedFileExts.Name = "tbxIncludedFileExts";
            this.tbxIncludedFileExts.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxIncludedFileExts.Size = new System.Drawing.Size(175, 115);
            this.tbxIncludedFileExts.TabIndex = 14;
            this.tbxIncludedFileExts.WordWrap = false;
            // 
            // gbxDirectorySearch
            // 
            this.gbxDirectorySearch.Controls.Add(this.lblExcludedDirs);
            this.gbxDirectorySearch.Controls.Add(this.tbxExcludedDirs);
            this.gbxDirectorySearch.Controls.Add(this.lblIncludedFileExt);
            this.gbxDirectorySearch.Controls.Add(this.tbxIncludedFileExts);
            this.gbxDirectorySearch.Location = new System.Drawing.Point(12, 257);
            this.gbxDirectorySearch.Name = "gbxDirectorySearch";
            this.gbxDirectorySearch.Size = new System.Drawing.Size(408, 177);
            this.gbxDirectorySearch.TabIndex = 15;
            this.gbxDirectorySearch.TabStop = false;
            this.gbxDirectorySearch.Text = "Directory search:";
            // 
            // lblExcludedDirs
            // 
            this.lblExcludedDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExcludedDirs.AutoSize = true;
            this.lblExcludedDirs.Location = new System.Drawing.Point(218, 25);
            this.lblExcludedDirs.Name = "lblExcludedDirs";
            this.lblExcludedDirs.Size = new System.Drawing.Size(105, 13);
            this.lblExcludedDirs.TabIndex = 16;
            this.lblExcludedDirs.Text = "Excluded directories:";
            // 
            // tbxExcludedDirs
            // 
            this.tbxExcludedDirs.AcceptsReturn = true;
            this.tbxExcludedDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxExcludedDirs.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxExcludedDirs.Location = new System.Drawing.Point(221, 44);
            this.tbxExcludedDirs.Multiline = true;
            this.tbxExcludedDirs.Name = "tbxExcludedDirs";
            this.tbxExcludedDirs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxExcludedDirs.Size = new System.Drawing.Size(178, 115);
            this.tbxExcludedDirs.TabIndex = 14;
            this.tbxExcludedDirs.WordWrap = false;
            // 
            // lblIncludedFileExt
            // 
            this.lblIncludedFileExt.AutoSize = true;
            this.lblIncludedFileExt.Location = new System.Drawing.Point(6, 25);
            this.lblIncludedFileExt.Name = "lblIncludedFileExt";
            this.lblIncludedFileExt.Size = new System.Drawing.Size(120, 13);
            this.lblIncludedFileExt.TabIndex = 15;
            this.lblIncludedFileExt.Text = "Included file extensions:";
            // 
            // gbxHistorySearch
            // 
            this.gbxHistorySearch.Controls.Add(this.cbxAutoCheckFilesExist);
            this.gbxHistorySearch.Controls.Add(this.lblMaxHistoryLength);
            this.gbxHistorySearch.Controls.Add(this.nudMaxHistoryLength);
            this.gbxHistorySearch.Location = new System.Drawing.Point(12, 151);
            this.gbxHistorySearch.Name = "gbxHistorySearch";
            this.gbxHistorySearch.Size = new System.Drawing.Size(408, 91);
            this.gbxHistorySearch.TabIndex = 17;
            this.gbxHistorySearch.TabStop = false;
            this.gbxHistorySearch.Text = "History search:";
            // 
            // cbxAutoCheckFilesExist
            // 
            this.cbxAutoCheckFilesExist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxAutoCheckFilesExist.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.gbxCommon.Controls.Add(this.cbxFilePathFormat);
            this.gbxCommon.Controls.Add(this.lblFilePathFormat);
            this.gbxCommon.Location = new System.Drawing.Point(12, 12);
            this.gbxCommon.Name = "gbxCommon";
            this.gbxCommon.Size = new System.Drawing.Size(408, 124);
            this.gbxCommon.TabIndex = 18;
            this.gbxCommon.TabStop = false;
            this.gbxCommon.Text = "Common:";
            // 
            // frmOptions
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(433, 486);
            this.Controls.Add(this.gbxCommon);
            this.Controls.Add(this.gbxHistorySearch);
            this.Controls.Add(this.gbxDirectorySearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOptions";
            this.Opacity = 0.95D;
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
        public System.Windows.Forms.ComboBox cbxFilePathFormat;
        private System.Windows.Forms.Label lblFilePathFormat;
        private System.Windows.Forms.GroupBox gbxDirectorySearch;
        public System.Windows.Forms.TextBox tbxIncludedFileExts;
        public System.Windows.Forms.TextBox tbxExcludedDirs;
        private System.Windows.Forms.GroupBox gbxHistorySearch;
        private System.Windows.Forms.Label lblExcludedDirs;
        private System.Windows.Forms.Label lblIncludedFileExt;
        public System.Windows.Forms.CheckBox cbxAutoCheckFilesExist;
        private System.Windows.Forms.GroupBox gbxCommon;
    }
}