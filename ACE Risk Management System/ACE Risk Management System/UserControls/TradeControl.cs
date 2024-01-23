using ACE_Risk_Management_System.DataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACE_Risk_Management_System.UserControls
{
    public partial class TradeControl : UserControl
    {


        public TradeControl()
        {
            InitializeComponent();
            Ticker = "None";
            Status = "InActive";
            Quantity = 0;
            EstimatedLoss = 0;
            EstimatedGain = 0;
        }
        public TradeControl(TradeInfo tradeInfo)
        {
            InitializeComponent();
            setTrade(tradeInfo);
        }
        public void setTrade(TradeInfo tradeInfo)
        {
            Ticker = tradeInfo.Ticker;
            Status = tradeInfo.Status.ToString();
            Quantity = tradeInfo.TotalQty;
            EstimatedLoss = RiskManagementCalculator.CalculateLoss(tradeInfo);
            EstimatedGain = RiskManagementCalculator.CalculateGain(tradeInfo);
        }

        private void TradeControl_Load(object sender, EventArgs e)
        {

        }

        public string Ticker
        {
            get
            {
                return tickerLabel.Text;
            }
            set
            {
                tickerLabel.Text = value;
            }
        }
        public string Status
        {
            get
            {
                return statusLabel.Text;
            }
            set
            {
                statusLabel.Text = value;
                switch (value)
                {
                    case "Active":
                        statusLabel.ForeColor = Color.Green;
                        break;
                    case "Inactive":
                        statusLabel.ForeColor = Color.Blue;
                        break;
                    case "StoppedOut":
                    case "Cancelled":
                        statusLabel.ForeColor = Color.Red;
                        break;
                }

            }
        }
        public decimal Quantity
        {
            set
            {
                qtyLabel.Text = $"Quantity: {value}";
            }
        }
        public decimal EstimatedLoss
        {
            set
            {
                lossLabel.Text = $"Est. Loss: {value}";
            }
        }
        public decimal EstimatedGain
        {
            set
            {
                gainLabel.Text = $"Est. Gain: {value}";
            }
        }

    }
}
