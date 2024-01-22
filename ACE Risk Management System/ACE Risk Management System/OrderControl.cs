using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace ACE_Risk_Management_System
{
    public partial class OrderControl : UserControl
    {
        public int index = -1;
        public OrderControl()
        {
            InitializeComponent();
            label.Text = "TP: ";
            limitPrice.Value = 0;
            percentage.Value = 0;
        }

        public OrderControl(string text, decimal limit, decimal qtyPercentage, decimal valueChange)
        {
            InitializeComponent();
            label.Text = text;
            limitPrice.Value = limit;
            percentage.Value = qtyPercentage;
            ValueLabel.Text = $"{(valueChange > 0 ? "Gain: $" : "Loss: $")}{valueChange}";
            ValueLabel.ForeColor = valueChange > 0 ? Color.Green : Color.Red;
        }
        public Color LabelColour
        {
            get
            {
                return label.ForeColor;
            }
            set { label.ForeColor = value; }
        }
        override public string Text
        {
            get
            {
                return label.Text;
            }
            set { label.Text = value; }
        }
        public decimal Percentage
        {
            get
            {
                return percentage.Value;
            }
            set { percentage.Value = value; }
        }
        public decimal Limit
        {
            get
            {
                return limitPrice.Value;
            }
            set { limitPrice.Value = value; }
        }

        private void OrderControl_Load(object sender, EventArgs e)
        {
            Width = Parent.Size.Width;
            ForeColor = Parent.ForeColor;
            BackColor = Parent.BackColor;
        }

        private void percentage_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
