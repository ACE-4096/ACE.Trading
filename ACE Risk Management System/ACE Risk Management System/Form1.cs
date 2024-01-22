using System;
using System.Collections.Generic;

namespace ACE_Risk_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal orderSize = 20;//accSize.Value * (Properties.Settings.Default.Risk / 100);
            sizeToRisk.Text = "$" + orderSize.ToString();

            // loss
            var lossPercentage = Math.Round(((100 / tradeEntry.Value) * Math.Abs(tradeEntry.Value - getOrder(0).Limit)) - Properties.Settings.Default.Fees, 4);
            var lossInDollars = lossPercentage * (orderSize / 100);

            // gain
            var gainPecentage = Math.Round(((100 / tradeEntry.Value) * (Math.Abs(tradeEntry.Value - getOrder(0).Limit) * tradeRR.Value)) - Properties.Settings.Default.Fees, 4);
            var gainInDollars = gainPecentage * (orderSize / 100);

            // Size to risk is the account size multiplied by a max loss percentage, default to 2
            decimal qtyAtSL = orderSize / Math.Abs(tradeEntry.Value - getOrder(0).Limit);



            potLoss.Text = lossPercentage + "% - ( $" + lossInDollars + " )";
            potGain.Text = gainPecentage + "% - ( $" + gainInDollars + " )";

            qty.Text = Math.Round(orderSize / tradeEntry.Value, 4).ToString();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sf = new SettingsForm();
            sf.FormClosed += SettingsForm_FormClosed;
        }

        private void SettingsForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            addToList(new OrderControl("Stop-Loss:", 0, 0, 0));
            ((OrderControl)orderPanel.Controls[orderPanel.Controls.Count - 1]).LabelColour = Color.Red;
        }

        // order control list
        private void addToList(OrderControl control)
        {
            control.index = orderPanel.Controls.Count;
            control.Location = new Point(3, 3 + (control.index * control.Height));
            orderPanel.Controls.Add(control);
        }
        private List<OrderControl> getStopOrders()
        {
            List<OrderControl> output = new List<OrderControl>();
            foreach (OrderControl control in orderPanel.Controls)
            {
                if (control.Text.Contains("Stop-Loss"))
                {
                    output.Add(control);
                }
            }
            return output;
        }
        private List<OrderControl> getTakeOrders()
        {
            List<OrderControl> output = new List<OrderControl>();
            foreach (OrderControl control in orderPanel.Controls)
            {
                if (control.Text.Contains("Take-Profit"))
                {
                    output.Add(control);
                }
            }
            return output;
        }
        private OrderControl getOrder(int index)
        {
            return (OrderControl)orderPanel.Controls[index];
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addTp_Click(object sender, EventArgs e)
        {

            addToList(new OrderControl($"Take-Profit {orderPanel.Controls.Count}:", 0, 0, 0));
        }
    }
}