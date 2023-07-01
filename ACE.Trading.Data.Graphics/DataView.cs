using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACE.Trading.Data.Graphics
{
    public partial class DataViewView : Form
    {
        public DataViewView()
        {
            InitializeComponent();
        }

        string[] symbols;

        private void DataView_Load(object sender, EventArgs e)
        {
            DataCache.Load();
            symbols = DataCache.getAllSymbols();

            var mainNode = treeView.Nodes.Add("Symbol List");
            foreach (string str in symbols)
            {
                var subNode = mainNode.Nodes.Add(str);

                var sd = DataCache.GetSymbolData(str);

                subNode.Nodes.Add($"Latest Price: ${sd.getLatestPrice}");
                subNode.Nodes.Add($"Data Entries: {sd.getPriceHistory.Count}");

            }
            mainNode.ExpandAll();

        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (symbols.Contains(e.Node.Text))
            {
                var sd = DataCache.GetSymbolData(e.Node.Text);
                
                genGraphFromSymbolData(sd);
            }
        }

        private void genGraphFromSymbolData(SymbolData sd)
        {
            List<double> prices = new List<double>();
            List<double> dateTimes = new List<double>();
            var x = 0;
            foreach(var price in sd.getPriceHistory.ToArray())
            {
                prices.Add((double)price.avgPrice);
                dateTimes.Add(x++);
            }

            formsPlot1.Plot.AddScatter(dateTimes.ToArray(),prices.ToArray());
            formsPlot1.Refresh();
        }
    }
}
