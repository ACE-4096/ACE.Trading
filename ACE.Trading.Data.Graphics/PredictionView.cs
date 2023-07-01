using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACE.Trading.Analytics;
using ACE.Trading.Analytics.Slopes;
using ACE.Trading.Data.Collection;
using ACE.Trading.OpenAi;
using OpenAI_API.FineTune;

namespace ACE.Trading.Data.Graphics
{
    public partial class PredictionView : Form
    {


        public PredictionView()
        {
            InitializeComponent();
        }

        string[] symbols;
        string filename;

        List<List<PredictedPriceHistory>> priceHistories = new List<List<PredictedPriceHistory>>();
        List<List<PredictedSlopeHistory>> slopeHistories = new List<List<PredictedSlopeHistory>>();
        PriceHistoryLogging logger = new PriceHistoryLogging();
        private void PredictionView_Load(object sender, EventArgs e)
        {
            DataCache.Load();
            logger.startLogging();
            symbols = DataCache.getAllSymbols();
            symbolCombo.Items.Clear();
            symbolCombo.Items.AddRange(symbols);

            Analytics.Predictions.Load();

            // get slopes/prices filtered into lists per symbol
            foreach (string str in symbols)
            {
                List<PredictedPriceHistory> priceHistory;
                if (Analytics.Predictions.findPricePredictions(str, out priceHistory))
                {
                    priceHistories.Add(priceHistory);
                }
                List<PredictedSlopeHistory> slopeHistory;
                if (Analytics.Predictions.findSlopePredictions(str, out slopeHistory))
                {
                    slopeHistories.Add(slopeHistory);
                }
            }

            // Draw Slope Prediction Nodes
            var MainSlopeNode = treeView.Nodes.Add("Slope Predictions");
            foreach (string str in symbols)
            {
                var subNode = MainSlopeNode.Nodes.Add(str);
                var symbolSlopes = slopeHistories.Find(lst => lst.First().getSymbol == str);
                if (symbolSlopes == null || symbolSlopes.Count == 0)
                    continue;
                foreach (var slope in symbolSlopes)
                {
                    var slopeNode = subNode.Nodes.Add(slope.getId.ToString());
                    slopeNode.Nodes.Add($"Accuracy: {slope.computeAccuracy()}");
                }
            }

            // Draw Price Prediction Nodes
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

            modelIdCombo.Items.Clear();
            new Thread(loadFineTunedModels).Start();

        }

        private async void loadFineTunedModels()
        {
            OpenAiIntegration ai = new OpenAiIntegration();
            Task<FineTuneResultList> results = ai.getFineTuneList();

            while (!results.IsCompleted) { Thread.Sleep(1000); }
            if (results.IsCompletedSuccessfully)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    foreach (var model in results.Result.data)
                    {
                        if (model.FineTunedModel != null)
                        {
                            modelIdCombo.Items.Add(model.FineTunedModel);
                        }
                    }
                });
            }
            else if (results.IsCanceled)
            {
                MessageBox.Show("Error retreiving fine tuned models.");
            }
            else if (results.IsFaulted)
            {
                MessageBox.Show("Error retreiving fine tuned models.");
                Debug.Print(results.Exception.ToString());
            }
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

            foreach (var price in sd.getPriceHistory.ToArray())
            {
                prices.Add((double)price.avgPrice);
                dateTimes.Add(x++);
            }

            formsPlot1.Plot.AddScatter(dateTimes.ToArray(), prices.ToArray());
            formsPlot1.Refresh();
        }
        private void genGraphFromPrediction(PredictedSlopeHistory sd)
        {
            if (sd.getRealResult == null || sd.getPredictionOutput == null || sd.getPredictionInput == null)
            {
                MessageBox.Show("Predisction history data is invalid");
                return;
            }

            List<double> realPrices = new List<double>();
            List<double> realDateTimes = new List<double>();

            List<double> predictedPrices = new List<double>();
            List<double> predictedDateTimes = new List<double>();
            var x = 0;
            // Input
            foreach (PricePointSlope slope in sd.getPredictionInput)
            {
                foreach (PricePoint p in slope.getSlopePoints)
                {
                    realPrices.Add((double)p.avgPrice);
                    realDateTimes.Add(p.timeUtc.ToUnixTime());
                }
            }
            // Real Result
            foreach (PricePointSlope slope in sd.getRealResult)
            {
                foreach (PricePoint p in slope.getSlopePoints)
                {
                    realPrices.Add((double)p.avgPrice);
                    realDateTimes.Add(p.timeUtc.ToUnixTime());
                }
            }

            // Fake Result
            foreach (PricePointSlope slope in sd.getPredictionOutput)
            {
                foreach (PricePoint p in slope.getSlopePoints)
                {
                    predictedPrices.Add((double)p.avgPrice);
                    predictedDateTimes.Add(p.timeUtc.ToUnixTime());
                }
            }

            formsPlot1.Plot.AddScatter(realDateTimes.ToArray(), realPrices.ToArray());

            formsPlot1.Plot.AddScatter(predictedDateTimes.ToArray(), predictedPrices.ToArray());
            formsPlot1.Refresh();
        }

        // Open sliud language slope file
        private void fluidLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filename = openFile();
            switch (Path.GetExtension(filename))
            {

            }
        }

        private string openFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = " (*.*)|*.*";

            ofd.Multiselect = false;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return "";
        }

        private void predictBtn_Click(object sender, EventArgs e)
        {
            if (modelIdCombo.SelectedIndex == -1)
            {
                MessageBox.Show("No model selected for predictions.");
                return;
            }
            if (symbolCombo.SelectedIndex == -1)
            {
                MessageBox.Show("No symbol selected for predictions.");
                return;
            }
            if (promptNum.Value <= 0 || promptNum.Value > 12)
            {
                MessageBox.Show($"Cannot predict with {promptNum.Value} prompts.");
                return;
            }
            var parameters = new PredictionParams() { modelId = modelIdCombo.Text, symbol = symbolCombo.Text, promptNum = (int)promptNum.Value };
            new Thread(predict).Start(parameters);
        }
        class PredictionParams
        {
            public string modelId;
            public string symbol;
            public int promptNum;
        }
        private async void predict(object? input)
        {
            if (input == null)
                return;
            var predictionParams = (PredictionParams)input;
            OpenAi.Predictions p = new OpenAi.Predictions();
            Task<long> predicitonTask = p.predictSlopes(predictionParams.symbol, new OpenAI_API.Models.Model(predictionParams.modelId), predictionParams.promptNum);
            while (!predicitonTask.IsCompleted) Thread.Sleep(100);
            if (predicitonTask.IsCompletedSuccessfully)
            {
                if (predicitonTask.Result < 0)
                {
                    MessageBox.Show($"Invalid prediction id: {predicitonTask.Result}");
                    return;
                }
                PredictedSlopeHistory hist = Analytics.Predictions.findSlopePrediction(predicitonTask.Result);
                BeginInvoke((MethodInvoker)delegate
                {
                    genGraphFromPrediction(hist);
                });
            }
            else
            {
                MessageBox.Show(predicitonTask.Exception.ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PredictionView_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.stopLogging();
            DataCache.Save();
        }
    }
}
