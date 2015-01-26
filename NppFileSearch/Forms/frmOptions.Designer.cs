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
            this.tbxIncludedFileExt = new System.Windows.Forms.TextBox();
            this.gbxIncludedFileExt = new System.Windows.Forms.GroupBox();
            this.gbxExcludedDirs = new System.Windows.Forms.GroupBox();
            this.tbxExcludedDirs = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHistoryLength)).BeginInit();
            this.gbxIncludedFileExt.SuspendLayout();
            this.gbxExcludedDirs.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTbbFunction
            // 
            this.lblTbbFunction.AutoSize = true;
            this.lblTbbFunction.Location = new System.Drawing.Point(12, 53);
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
            this.cbxTbbFunction.Location = new System.Drawing.Point(226, 50);
            this.cbxTbbFunction.Name = "cbxTbbFunction";
            this.cbxTbbFunction.Size = new System.Drawing.Size(187, 21);
            this.cbxTbbFunction.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(324, 347);
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
            this.btnSave.Location = new System.Drawing.Point(226, 347);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblMaxHistoryLength
            // 
            this.lblMaxHistoryLength.AutoSize = true;
            this.lblMaxHistoryLength.Location = new System.Drawing.Point(12, 18);
            this.lblMaxHistoryLength.Name = "lblMaxHistoryLength";
            this.lblMaxHistoryLength.Size = new System.Drawing.Size(116, 13);
            this.lblMaxHistoryLength.TabIndex = 8;
            this.lblMaxHistoryLength.Text = "Maximum history length";
            // 
            // nudMaxHistoryLength
            // 
            this.nudMaxHistoryLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaxHistoryLength.Location = new System.Drawing.Point(226, 16);
            this.nudMaxHistoryLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxHistoryLength.Name = "nudMaxHistoryLength";
            this.nudMaxHistoryLength.Size = new System.Drawing.Size(187, 20);
            this.nudMaxHistoryLength.TabIndex = 9;
            // 
            // cbxCaseSensitiveSearch
            // 
            this.cbxCaseSensitiveSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxCaseSensitiveSearch.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxCaseSensitiveSearch.Location = new System.Drawing.Point(11, 84);
            this.cbxCaseSensitiveSearch.Name = "cbxCaseSensitiveSearch";
            this.cbxCaseSensitiveSearch.Size = new System.Drawing.Size(403, 24);
            this.cbxCaseSensitiveSearch.TabIndex = 10;
            this.cbxCaseSensitiveSearch.Text = "Case sensitive search filter";
            this.cbxCaseSensitiveSearch.UseVisualStyleBackColor = true;
            // 
            // cbxFilePathFormat
            // 
            this.cbxFilePathFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFilePathFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFilePathFormat.FormattingEnabled = true;
            this.cbxFilePathFormat.Location = new System.Drawing.Point(226, 118);
            this.cbxFilePathFormat.Name = "cbxFilePathFormat";
            this.cbxFilePathFormat.Size = new System.Drawing.Size(187, 21);
            this.cbxFilePathFormat.TabIndex = 12;
            // 
            // lblFilePathFormat
            // 
            this.lblFilePathFormat.AutoSize = true;
            this.lblFilePathFormat.Location = new System.Drawing.Point(12, 121);
            this.lblFilePathFormat.Name = "lblFilePathFormat";
            this.lblFilePathFormat.Size = new System.Drawing.Size(79, 13);
            this.lblFilePathFormat.TabIndex = 11;
            this.lblFilePathFormat.Text = "File path format";
            // 
            // tbxIncludedFileExt
            // 
            this.tbxIncludedFileExt.AcceptsReturn = true;
            this.tbxIncludedFileExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxIncludedFileExt.Location = new System.Drawing.Point(6, 19);
            this.tbxIncludedFileExt.Multiline = true;
            this.tbxIncludedFileExt.Name = "tbxIncludedFileExt";
            this.tbxIncludedFileExt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxIncludedFileExt.Size = new System.Drawing.Size(175, 115);
            this.tbxIncludedFileExt.TabIndex = 14;
            this.tbxIncludedFileExt.WordWrap = false;
            // 
            // gbxIncludedFileExt
            // 
            this.gbxIncludedFileExt.Controls.Add(this.tbxIncludedFileExt);
            this.gbxIncludedFileExt.Location = new System.Drawing.Point(15, 165);
            this.gbxIncludedFileExt.Name = "gbxIncludedFileExt";
            this.gbxIncludedFileExt.Size = new System.Drawing.Size(187, 140);
            this.gbxIncludedFileExt.TabIndex = 15;
            this.gbxIncludedFileExt.TabStop = false;
            this.gbxIncludedFileExt.Text = "Included file extensions:";
            // 
            // gbxExcludedDirs
            // 
            this.gbxExcludedDirs.Controls.Add(this.tbxExcludedDirs);
            this.gbxExcludedDirs.Location = new System.Drawing.Point(226, 165);
            this.gbxExcludedDirs.Name = "gbxExcludedDirs";
            this.gbxExcludedDirs.Size = new System.Drawing.Size(187, 140);
            this.gbxExcludedDirs.TabIndex = 16;
            this.gbxExcludedDirs.TabStop = false;
            this.gbxExcludedDirs.Text = "Excluded directories:";
            // 
            // tbxExcludedDirs
            // 
            this.tbxExcludedDirs.AcceptsReturn = true;
            this.tbxExcludedDirs.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxExcludedDirs.Location = new System.Drawing.Point(6, 19);
            this.tbxExcludedDirs.Multiline = true;
            this.tbxExcludedDirs.Name = "tbxExcludedDirs";
            this.tbxExcludedDirs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxExcludedDirs.Size = new System.Drawing.Size(175, 115);
            this.tbxExcludedDirs.TabIndex = 14;
            this.tbxExcludedDirs.WordWrap = false;
            // 
            // frmOptions
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(434, 384);
            this.Controls.Add(this.gbxExcludedDirs);
            this.Controls.Add(this.gbxIncludedFileExt);
            this.Controls.Add(this.cbxFilePathFormat);
            this.Controls.Add(this.lblFilePathFormat);
            this.Controls.Add(this.cbxCaseSensitiveSearch);
            this.Controls.Add(this.nudMaxHistoryLength);
            this.Controls.Add(this.lblMaxHistoryLength);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbxTbbFunction);
            this.Controls.Add(this.lblTbbFunction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOptions";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NppFileSearch";
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHistoryLength)).EndInit();
            this.gbxIncludedFileExt.ResumeLayout(false);
            this.gbxIncludedFileExt.PerformLayout();
            this.gbxExcludedDirs.ResumeLayout(false);
            this.gbxExcludedDirs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.GroupBox gbxIncludedFileExt;
        private System.Windows.Forms.GroupBox gbxExcludedDirs;
        public System.Windows.Forms.TextBox tbxIncludedFileExt;
        public System.Windows.Forms.TextBox tbxExcludedDirs;
    }
}