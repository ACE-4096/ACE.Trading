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
using ScottPlot;
using static ScottPlot.Generate;

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
        System.Windows.Forms.Timer collectedDataPollingTimer;
        TreeNode PredictionsNode, DataNode;

        #region Loading / Saving
        private void PredictionView_Load(object sender, EventArgs e)
        {
            DataCache.Load();
            logger.startLogging();
            refreshGui();
            collectedDataPollingTimer = new System.Windows.Forms.Timer();
            collectedDataPollingTimer.Interval = 1000;
            collectedDataPollingTimer.Tick += CollectedDataPollingTimer_Tick;
            collectedDataPollingTimer.Enabled = true;
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
                    modelIdCombo.Items.Clear();
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
        private void refreshGui()
        {
            treeView.Nodes.Clear();
            symbols = DataCache.getAllSymbols();
            symbolCombo.Items.Clear();
            symbolCombo.Items.AddRange(symbols);
            Analytics.Predictions.Load();

            DataNode = treeView.Nodes.Add("Collected Data");
            PredictionsNode = treeView.Nodes.Add("Predictions");


            // Collected Data
            foreach (string str in symbols)
            {
                var subNode = DataNode.Nodes.Add(str);

                var sd = DataCache.GetSymbolData(str);

                subNode.Nodes.Add($"Latest Price: ${sd.getLatestPrice}");
                subNode.Nodes.Add($"Data Entries: {sd.getPriceHistory.Count}");

            }

            DataNode.ExpandAll();

            // Predicted Data

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
            var MainSlopeNode = PredictionsNode.Nodes.Add("Slopes");
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
            var MainPriceNode = PredictionsNode.Nodes.Add("PricePoints");
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
            PredictionsNode.ExpandAll();

            modelIdCombo.Items.Clear();
            new Thread(loadFineTunedModels).Start();

        }
        private void PredictionView_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.stopLogging();
            DataCache.Save();
        }
        #endregion

        private void CollectedDataPollingTimer_Tick(object? sender, EventArgs e)
        {
            long dataCount = 0;
            foreach (var symbol in symbols)
            {
                var sd = DataCache.GetSymbolData(symbol);
                dataCount += sd.getPriceHistory.Count;
            }
            this.Text = $"Prediction View | Collected Data: {dataCount}";
        }

        #region Graphics
        private void genGraphFromSymbolData(SymbolData sd)
        {
            List<double> prices = new List<double>();
            List<double> dateTimes = new List<double>();
            var x = 0;

            foreach (var price in sd.getPriceHistory.ToArray())
            {
                prices.Add((double)price.avgPrice);
                dateTimes.Add(price.unixTimeUtc);
            }
            prices.Reverse();
            dateTimes.Reverse();

            formsPlot1.Plot.AddScatter(dateTimes.ToArray(), prices.ToArray());
            formsPlot1.Refresh();
        }
        private void genGraphFromPrediction(PredictedSlopeHistory sd)
        {
            if (sd.getPredictionOutput == null || sd.getPredictionInput == null)
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
                    realDateTimes.Add(((DateTimeOffset)p.timeUtc).ToUnixTimeMilliseconds());
                }
            }
            // Real Result
            if (sd.getRealResult != null)
            {
                foreach (PricePointSlope slope in sd.getRealResult)
                {
                    foreach (PricePoint p in slope.getSlopePoints)
                    {
                        realPrices.Add((double)p.avgPrice);
                        realDateTimes.Add(((DateTimeOffset)p.timeUtc).ToUnixTimeMilliseconds());
                    }
                }
            }
            // Fake Result
            foreach (PricePointSlope slope in sd.getPredictionOutput)
            {
                foreach (PricePoint p in slope.getSlopePoints)
                {
                    predictedPrices.Add((double)p.avgPrice);
                    predictedDateTimes.Add(((DateTimeOffset)p.timeUtc).ToUnixTimeMilliseconds());
                }
            }

            realPrices.Reverse();
            realDateTimes.Reverse();
            predictedPrices.Reverse();
            predictedDateTimes.Reverse();
            formsPlot1.Plot.AddScatter(realDateTimes.ToArray(), realPrices.ToArray());
            formsPlot1.Plot.AddScatter(predictedDateTimes.ToArray(), predictedPrices.ToArray());
            formsPlot1.Refresh();
        }
        #endregion

        // NOT YET IMPLEMENTED
        // Open fliud language slope file
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

        #region Predictions
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
                    refreshGui();
                });
            }
            else
            {
                MessageBox.Show(predicitonTask.Exception.ToString());
            }

        }
        #endregion


        #region Click events
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (DataNode.Nodes.Contains(e.Node))
            {
                var sd = DataCache.GetSymbolData(e.Node.Text);

                genGraphFromSymbolData(sd);
            }
            else if (PredictionsNode.Nodes.Contains(e.Node))
            {

            }
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
        private void clearGraphBtn_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Clear(); 
            formsPlot1.Reset(); 
            formsPlot1.Refresh();
        }
        private void refreshDataBtn_Click(object sender, EventArgs e)
        {
            refreshGui();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.stopLogging();
            DataCache.Save();
            logger.startLogging();
        }
        private void fluidLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filename = openFile();
            switch (Path.GetExtension(filename))
            {

            }
        }
        #endregion
    }
}
