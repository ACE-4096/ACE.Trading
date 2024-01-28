using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACE_Risk_Management_System
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Risk = risk.Value;
            Properties.Settings.Default.Leverage = leverage.Value;
            Properties.Settings.Default.Fees = fees.Value;
            Properties.Settings.Default.Save();
            this.Close();
            this.Dispose();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            risk.Value = Properties.Settings.Default.Risk;
            leverage.Value = Properties.Settings.Default.Leverage;
            fees.Value = Properties.Settings.Default.Fees;
        }
    }
}
