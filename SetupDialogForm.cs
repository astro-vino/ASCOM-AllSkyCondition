using System;
using System.Windows.Forms;

namespace ASCOM.AllSkyCondition
{
    public partial class SetupDialogForm : Form
    {
        private readonly SafetyMonitor _driver;

        public SetupDialogForm(SafetyMonitor driver)
        {
            InitializeComponent();
            _driver = driver;
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            _driver.ImagePath = txtImagePath.Text;
            _driver.CycleTime = (int)numCycle.Value;
            _driver.CloudyTolerance = (int)numCloudy.Value;
            _driver.CoveredTolerance = (int)numCovered.Value;
            _driver.DebugLog = chkDebugLog.Checked;
            _driver.SaveProfile();
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e)
        {
            // This is a placeholder for a link to the ASCOM Initiative website
            try
            {
                System.Diagnostics.Process.Start("https://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            chkConnected.Enabled = false;
            chkConnected.Checked = true; // This driver is always connected

            txtImagePath.Text = _driver.ImagePath;
            numCycle.Value = _driver.CycleTime;
            numCloudy.Value = _driver.CloudyTolerance;
            numCovered.Value = _driver.CoveredTolerance;
            chkDebugLog.Checked = _driver.DebugLog;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                fbd.CheckFileExists = true;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = fbd.FileName;
                }
            }
        }
    }
}
