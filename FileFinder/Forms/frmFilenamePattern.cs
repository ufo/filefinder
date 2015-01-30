using System.Windows.Forms;

namespace FileFinder
{
    public partial class frmFilenamePattern : Form
    {
        public frmFilenamePattern()
        {
            InitializeComponent();

            if (Main.LastSearchPatterns.Count > 0)
            {
                tbxPattern.Text = Main.LastSearchPatterns[0];
            }
        }

        private void frmFilenamePattern_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                string searchPattern = tbxPattern.Text.Trim();
                if (!string.IsNullOrEmpty(searchPattern))
                {
                    if (!Main.LastSearchPatterns.Contains(searchPattern))
                    {
                        Main.LastSearchPatterns.Insert(0, searchPattern);
                        if (Main.LastSearchPatterns.Count > Main.MAX_LAST_SEARCH_PATTERNS)
                        {
                            Main.LastSearchPatterns.RemoveAt(Main.MAX_LAST_SEARCH_PATTERNS);
                        }
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
