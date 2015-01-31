using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace FileFinder
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();

            Icon = Properties.Resources.filefinder;

            lblVersion.Text = string.Format("Version: {0}",
                Assembly.GetExecutingAssembly().GetName().Version.ToString(2));
        }

        private void lblIssues_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://bitbucket.org/uph0/filefinder/issues");
        }
    }
}
