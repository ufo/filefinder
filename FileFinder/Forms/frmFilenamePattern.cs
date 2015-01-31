using System.Drawing;
using System.Windows.Forms;

namespace FileFinder
{
    public partial class frmFilenamePattern : Form
    {
        public frmFilenamePattern()
        {
            InitializeComponent();

            Icon = Properties.Resources.filefinder;

            if (Main.LastSearchPatterns.Count > 0)
            {
                cbxPattern.Text = Main.LastSearchPatterns[0];
            }
        }

        private void frmFilenamePattern_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                string searchPattern = cbxPattern.Text.Trim();
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
