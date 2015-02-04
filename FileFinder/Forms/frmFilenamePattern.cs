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

            cbxPattern.Items.AddRange(Main.LastSearchPatterns.ToArray());
            if (cbxPattern.Items.Count > 0)
            {
                cbxPattern.SelectedIndex = 0;
            }
        }

        private void frmFilenamePattern_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                string searchPattern = cbxPattern.Text.Trim();
                if (!string.IsNullOrEmpty(searchPattern))
                {
                    Main.LastSearchPatterns.Remove(searchPattern);
                    while (Main.LastSearchPatterns.Count >= Main.MAX_LAST_SEARCH_PATTERNS)
                    {
                        Main.LastSearchPatterns.RemoveAt(Main.MAX_LAST_SEARCH_PATTERNS);
                    }
                    Main.LastSearchPatterns.Insert(0, searchPattern);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
