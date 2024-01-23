using System;
using System.Collections.Generic;
using ACE_Risk_Management_System.UserControls;
using ACE_Risk_Management_System.DataTypes;

namespace ACE_Risk_Management_System
{
    public partial class TradeForm : Form
    {
        private int currentTrade = 0;

        private List<TradeInfo> trades = new List<TradeInfo>();

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

            // Load Trades
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
            createOrder(OrderType.TakeProfit, 0, 0);
        }

        private void TradeForm_Load(object sender, EventArgs e)
        {
            Api_UpdateBalance(api.getBalance());



            createTrade("None");
            createOrder(OrderType.StopLoss, 0, -1);
        }

        private void Api_UpdateBalance(decimal value)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                riskTxt.Text = $"{(Math.Round(value, 5) / 100) * Properties.Settings.Default.Risk}";
                balTxt.Text = $"${Math.Round(value, 5)}";
            });
        }

        private void createTrade(string Ticker)
        {
            var ti = new TradeInfo(Ticker);
            trades.Add(ti);
            tradePanel.Controls.Add(new TradeControl(ti));
        }

        private void createOrder(OrderType type, decimal triggerPrice, decimal quantity)
        {
            if (quantity == -1)
            {
                quantity = trades[currentTrade].TotalQty;
            }
            var oi = new OrderInfo(type, true, triggerPrice, quantity);
            oi.QuantityRatio = (100 / trades[currentTrade].TotalQty) * oi.Quantity;
            trades[currentTrade].Orders.Add(oi);
            reDrawOrders();
        }

        private void reDrawOrders()
        {
            orderPanel.Controls.Clear();
            var slOrders = trades[currentTrade].getOrders(OrderType.StopLoss);
            for (int i = 0; i < slOrders.Count; i++)
            {
                var ctl = new OrderControl($"Stop-Loss {i + 1}:", slOrders[i].TriggerPrice,
                    slOrders[i].QuantityRatio, 0);
                ctl.OnDataChanged += Ctl_OnDataChanged;
                ctl.LabelColour = Color.Red;
                orderPanel.Controls.Add(ctl);
                orderPanel.Controls[orderPanel.Controls.Count - 1].Location = new Point(3, 3 + (27 * (orderPanel.Controls.Count - 1)));
            }
            var tpOrders = trades[currentTrade].getOrders(OrderType.TakeProfit);
            for (int i = 0; i < tpOrders.Count; i++)
            {
                var ctl = new OrderControl($"Take-Profit {i + 1}:", tpOrders[i].TriggerPrice,
                    tpOrders[i].QuantityRatio, tpOrders[i].Quantity);
                ctl.OnDataChanged += Ctl_OnDataChanged;
                ctl.LabelColour = Color.Green;
                orderPanel.Controls.Add(ctl);
                orderPanel.Controls[orderPanel.Controls.Count - 1].Location = new Point(3, 3 + (27 * (orderPanel.Controls.Count - 1)));
            }
        }

        private void Ctl_OnDataChanged(OrderControl oc)
        {
            trades[currentTrade].Orders[orderPanel.Controls.IndexOf(oc)].TriggerPrice = oc.Limit;
            trades[currentTrade].Orders[orderPanel.Controls.IndexOf(oc)].QuantityRatio = oc.Percentage;
            oc.Value = RiskManagementCalculator.CalculateGain(trades[currentTrade]);
            trades[currentTrade].TotalQty = RiskManagementCalculator.CalculateTargetQuantity(trades[currentTrade]);

            var loss = RiskManagementCalculator.CalculateLoss(trades[currentTrade]);
            var gain = RiskManagementCalculator.CalculateGain(trades[currentTrade]);

            lossTxt.Text = $"${Math.Round(loss, 4)} ({Math.Round((100 / api.getBalance()) * loss, 4)}%)";
            gainTxt.Text = $"${Math.Round(gain, 4)} ({Math.Round((100 / api.getBalance()) * gain, 4)}%)";
            qtyTxt.Text = trades[currentTrade].TotalQty.ToString();
        }


        private void RemoveTpBtn_Click(object sender, EventArgs e)
        {
            if (trades[currentTrade].Orders.Count > 2)
            {
                trades[currentTrade].Orders.RemoveAt(trades[currentTrade].Orders.Count - 1);
                reDrawOrders();
            }
        }

        private void calcBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void tradeEntry_ValueChanged(object sender, EventArgs e)
        {
            trades[currentTrade].EntryPrice = tradeEntry.Value;
        }
    }
}