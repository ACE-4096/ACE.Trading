using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACE.Trading.Analytics;
using ACE.Trading.Analytics.Slopes;

namespace ACE.Trading.Data.Graphics
{
    public partial class PredictionView : Form
    {
        public PredictionView()
        {
            InitializeComponent();
        }

        string[] symbols;

        List<List<PredictedPriceHistory>> priceHistories = new List<List<PredictedPriceHistory>>();
        List<List<PredictedSlopeHistory>> slopeHistories = new List<List<PredictedSlopeHistory>>();
        private void PredictionView_Load(object sender, EventArgs e)
        {
            DataCache.Load();
            symbols = DataCache.getAllSymbols();
            Analytics.Predictions.Load();

            foreach (string str in symbols)
            {
                List<PredictedPriceHistory> priceHistory;
                if (Analytics.Predictions.findPricePredictions(str, out priceHistory))
                {
                    priceHistories.Add(priceHistory);
                }
                List<PredictedSlopeHistory> slopeHistory;
                if(Analytics.Predictions.findSlopePredictions(str, out slopeHistory))
                {
                    slopeHistories.Add(slopeHistory);
                }
            }

            var MainSlopeNode = treeView.Nodes.Add("Slope Predictions");
            foreach (string str in symbols)
            {
                var subNode = MainSlopeNode.Nodes.Add(str);
                var symbolSlopes = slopeHistories.Find(lst => lst.First().getSymbol == str);
                if (symbolSlopes == null || symbolSlopes.Count == 0)
                    continue;
                foreach(var slope in symbolSlopes)
                {
                    var slopeNode = subNode.Nodes.Add(slope.getId.ToString());
                    slopeNode.Nodes.Add($"Accuracy: {slope.computeAccuracy()}");
                }
            }
            var MainPriceNode = treeView.Nodes.Add("Price Predictions");
            foreach (string str in symbols)
            {
                var subNode = MainPriceNode.Nodes.Add(str);
                var symbolprices = slopeHistories.Find(lst => lst.First().getSymbol == str);
                if (symbolprices == null || symbolprices.Count == 0)
                    continue;
                foreach (var price in symbolprices)
                {
                    var priceNode = subNode.Nodes.Add(price.getId.ToString());
                    priceNode.Nodes.Add($"Accuracy: {price.computeAccuracy()}");
                }
            }
            MainSlopeNode.ExpandAll();
            MainPriceNode.ExpandAll();

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
