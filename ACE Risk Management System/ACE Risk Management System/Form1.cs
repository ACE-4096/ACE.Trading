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
            decimal orderSize = accSize.Value * (riskLimit.Value / 100);
            sizeToRisk.Text = "$" + orderSize.ToString();

            // loss
            var lossPercentage = Math.Round(((100 / tradeEntry.Value) * Math.Abs(tradeEntry.Value - tradeStop.Value)) - fees.Value, 4);
            var lossInDollars = lossPercentage * (orderSize / 100);

            // gain
            var gainPecentage = Math.Round(((100 / tradeEntry.Value) * (Math.Abs(tradeEntry.Value - tradeStop.Value) * tradeRR.Value)) - fees.Value, 4);
            var gainInDollars = gainPecentage * (orderSize / 100);




            potLoss.Text = lossPercentage + "% - ( $" + lossInDollars + " )";
            potGain.Text = gainPecentage + "% - ( $" + gainInDollars + " )";

            qty.Text = Math.Round(orderSize / tradeEntry.Value, 4).ToString();
        }
    }
}