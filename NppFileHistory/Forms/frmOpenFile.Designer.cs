namespace NppFileHistory
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
            this.lblFullSelectedPath = new System.Windows.Forms.Label();
            this.btnOpenSelected = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxSearch
            // 
            this.tbxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSearch.Location = new System.Drawing.Point(12, 34);
            this.tbxSearch.Name = "tbxSearch";
            this.tbxSearch.Size = new System.Drawing.Size(286, 20);
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
            this.lbxFiles.FormattingEnabled = true;
            this.lbxFiles.Location = new System.Drawing.Point(12, 126);
            this.lbxFiles.Name = "lbxFiles";
            this.lbxFiles.Size = new System.Drawing.Size(514, 264);
            this.lbxFiles.TabIndex = 2;
            this.lbxFiles.SelectedIndexChanged += new System.EventHandler(this.lbxFiles_SelectedIndexChanged);
            this.lbxFiles.SizeChanged += new System.EventHandler(this.lbxFiles_SizeChanged);
            this.lbxFiles.DoubleClick += new System.EventHandler(this.lbxFiles_DoubleClick);
            this.lbxFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbxFiles_KeyDown);
            // 
            // lblFullSelectedPath
            // 
            this.lblFullSelectedPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFullSelectedPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFullSelectedPath.Enabled = false;
            this.lblFullSelectedPath.Location = new System.Drawing.Point(12, 62);
            this.lblFullSelectedPath.Name = "lblFullSelectedPath";
            this.lblFullSelectedPath.Size = new System.Drawing.Size(514, 56);
            this.lblFullSelectedPath.TabIndex = 3;
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
            // frmOpenFile
            // 
            this.AcceptButton = this.btnOpenSelected;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(539, 405);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOpenSelected);
            this.Controls.Add(this.lblFullSelectedPath);
            this.Controls.Add(this.lbxFiles);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.tbxSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(555, 444);
            this.Name = "frmOpenFile";
            this.Opacity = 0.9D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select recent file to open";
            this.Load += new System.EventHandler(this.frmOpenFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ListBox lbxFiles;
        private System.Windows.Forms.Button btnOpenSelected;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Label lblFullSelectedPath;
    }
}