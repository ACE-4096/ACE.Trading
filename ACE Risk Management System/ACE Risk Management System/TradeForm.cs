using System;
using System.Collections.Generic;
using ACE_Risk_Management_System.UserControls;
using ACE_Risk_Management_System.DataTypes;

namespace ACE_Risk_Management_System
{
    public partial class TradeForm : Form
    {
        private TradeManager tm;

        private ExchangeApi api = new ExchangeApi();

        public TradeForm()
        {
            InitializeComponent();

            orderPanel.HorizontalScroll.Visible = false;
            orderPanel.HorizontalScroll.Enabled = false;
            orderPanel.VerticalScroll.Visible = true;
            orderPanel.VerticalScroll.Enabled = true;
            orderPanel.AutoScroll = true;

            tradePanel.HorizontalScroll.Enabled = false;
            tradePanel.VerticalScroll.Enabled = true;
            orderPanel.AutoScroll = true;
            tm = new TradeManager(orderPanel, tradePanel);
            tm.OnCalcsUpdated += Tm_OnCalcsUpdated;
            tm.OnEntryPriceUpdated += Tm_OnEntryPriceUpdated;
            // Load Trades
        }

        private void Tm_OnEntryPriceUpdated(decimal newPrice)
        {
            BeginInvoke((MethodInvoker)delegate { tradeEntry.Value = newPrice; });
        }

        private void Tm_OnCalcsUpdated(string lossText, string gainText, string qtyText)
        {

            BeginInvoke((MethodInvoker)delegate {
                this.lossTxt.Text = lossText;
                this.gainTxt.Text = gainText;
                this.qtyTxt.Text = qtyText; 
            });
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sf = new SettingsForm();
            sf.Show();
            sf.FormClosed += SettingsForm_FormClosed;
        }

        private void SettingsForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            //recalulcate();
        }

        private void addTp_Click(object sender, EventArgs e)
        {
            tm.createOrder(OrderType.TakeProfit, 0, 0);
        }

        private void TradeForm_Load(object sender, EventArgs e)
        {
            Api_UpdateBalance(api.getBalance());


            tm.createTrade("None");
            tm.createOrder(OrderType.StopLoss, 0, -1);
        }

        private void Api_UpdateBalance(decimal value)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                riskTxt.Text = $"{(Math.Round(value, 5) / 100) * Properties.Settings.Default.Risk}";
                balTxt.Text = $"${Math.Round(value, 5)}";
            });
        }

        private void RemoveTpBtn_Click(object sender, EventArgs e)
        {
            tm.RemoveTp();
        }

        private void tradeEntry_ValueChanged(object sender, EventArgs e)
        {
            var ti = tm.getTrade();
            ti.EntryPrice = tradeEntry.Value;
            tm.UpdateTrade(ti);

        }

        private void TickerTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tm.createAndShowTrade(TickerTxt.Text);
                TickerTxt.Text = "";
            }
        }
    }
}