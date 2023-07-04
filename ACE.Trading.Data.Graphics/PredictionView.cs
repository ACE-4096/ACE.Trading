using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACE.Trading.Analytics;
using ACE.Trading.Analytics.Slopes;
using ACE.Trading.Data.Collection;
using ACE.Trading.OpenAi;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using OpenAI_API.FineTune;
using OpenAI_API.Moderation;
using ScottPlot;
using static System.Formats.Asn1.AsnWriter;
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
        //PriceHistoryLogging logger = new PriceHistoryLogging();
        BinanceHandler bh = new BinanceHandler();
        System.Windows.Forms.Timer collectedDataPollingTimer;
        TreeNode PredictionsNode, DataNode;

        #region Loading / Saving
        private void PredictionView_Load(object sender, EventArgs e)
        {
            DataCache.Load();
            //logger.startLogging();
            refreshGui();
            //collectedDataPollingTimer = new System.Windows.Forms.Timer();
            //collectedDataPollingTimer.Interval = 1000;
            //collectedDataPollingTimer.Tick += CollectedDataPollingTimer_Tick;
            //collectedDataPollingTimer.Enabled = true;
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
            Analytics.Predictions.Load();

            // show time intervals
            timeIntervalCombo.Items.Clear();
            timeIntervalCombo.Items.Add(KlineInterval.ThreeDay);
            timeIntervalCombo.Items.Add(KlineInterval.OneDay);
            timeIntervalCombo.Items.Add(KlineInterval.TwelveHour);
            timeIntervalCombo.Items.Add(KlineInterval.EightHour);
            timeIntervalCombo.Items.Add(KlineInterval.OneHour);
            timeIntervalCombo.Items.Add(KlineInterval.ThirtyMinutes);
            timeIntervalCombo.Items.Add(KlineInterval.FifteenMinutes);
            timeIntervalCombo.Items.Add(KlineInterval.OneMinute);

            // show time durations
            durationCombo.Items.Add(KlineInterval.OneMonth);
            durationCombo.Items.Add(KlineInterval.OneWeek);
            durationCombo.Items.Add(KlineInterval.OneDay);
            durationCombo.Items.Add(KlineInterval.OneHour);
            durationCombo.SelectedIndexChanged += DurationCombo_SelectedIndexChanged;

            // Show symbols
            symbolCombo.Items.Clear();
            symbolCombo.Items.AddRange(symbols);

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
                /*List<PredictedPriceHistory> priceHistory;
                if (Analytics.Predictions.findPricePredictions(str, out priceHistory))
                {
                    priceHistories.Add(priceHistory);
                }*/

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

        private async void DurationCombo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // get duration
            System.DateTime startTime = System.DateTime.UtcNow, endTime = System.DateTime.UtcNow;
            switch (durationCombo.Text)
            {
                case "OneMonth":
                    startTime = startTime.AddMonths(-1);
                    break;
                case "OneWeek":
                    startTime = startTime.AddDays(-7);
                    break;
                case "OneDay":
                    startTime = startTime.AddDays(-1);
                    break;
                case "OneHour":
                    startTime = startTime.AddHours(-1);
                    break;
            }


            // get time interval
            KlineInterval timeInterval = KlineInterval.OneMinute;
            switch (timeIntervalCombo.Text)
            {
                case "OneDay":
                    timeInterval = KlineInterval.OneDay;
                    break;
                case "TwelveHour":
                    timeInterval = KlineInterval.TwelveHour;
                    break;
                case "EightHour":
                    timeInterval = KlineInterval.EightHour;
                    break;
                case "OneHour":
                    timeInterval = KlineInterval.OneHour;
                    break;
                case "ThirtyMinutes":
                    timeInterval = KlineInterval.ThirtyMinutes;
                    break;
                case "FifteenMinutes":
                    timeInterval = KlineInterval.FifteenMinutes;
                    break;
                case "OneMinute":
                    timeInterval = KlineInterval.OneMinute;
                    break;
            }

            if (symbols.Contains(symbolCombo.Text) && startTime != endTime)
            {
                var result = await bh.getMarketData(symbolCombo.Text, timeInterval, startTime, endTime);
                if (result.Success)
                {
                    genMarketData(result.Data);
                }
                /*while (!result.IsCompleted)Thread.Sleep(50) ;
                if (result.IsCompletedSuccessfully)
                {
                    
                }*/

            }
        }
        private void genMarketData(IEnumerable<IBinanceKline> klines)
        {
            formsPlot1.Plot.AddCandlesticks(PricePoint.FromBinanceKline(klines).ToArray());
            formsPlot1.Refresh();
        }

        private void PredictionView_FormClosing(object sender, FormClosingEventArgs e)
        {
            //logger.stopLogging();
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
            if (sd.getPredictionInput == null)
            {
                MessageBox.Show("Predisction history data is invalid");
                return;
            }
            if (sd.getPredictionOutput != null && sd.getPredictionOutput.Count > 0)
            {
                List<double> yVals1 = new List<double>();
                List<double> xVals1 = new List<double>();
                foreach (var slopeX in sd.getPredictionOutput)
                {
                    yVals1.Add((double)slopeX.getOpenPrice);
                    xVals1.Add((double)slopeX.OpenTimeUnix);
                }
                formsPlot1.Plot.AddScatter(xVals1.ToArray(), yVals1.ToArray(), color: Color.Green);
                this.Text += " | Output: " + sd.getPredictionOutput.Last().openTimeUtc;
            }
            List<double> yVals2 = new List<double>();
            List<double> xVals2 = new List<double>();

            // Display the rest
            foreach (PricePointSlope slope in sd.getPredictionInput.ToArray())
            {
                yVals2.Add((double)slope.getOpenPrice);
                xVals2.Add((double)slope.OpenTimeUnix);
            }
            this.Text += " | Input: " + sd.getPredictionInput.First().closeTimeUtc;
            formsPlot1.Plot.AddScatter(xVals2.ToArray(), yVals2.ToArray(), color: Color.Blue);
            formsPlot1.Refresh();

            /*
            List<PricePoint> predictedPoints = new List<PricePoint>();
            List<PricePoint> points = new List<PricePoint>();
            // Input
            foreach (PricePointSlope slope in sd.getPredictionInput)
            {
                points.AddRange(slope.getSlopePoints);
            }

            // Real Result
            if (sd.getRealResult != null)
            {
                foreach (PricePointSlope slope in sd.getRealResult)
                {
                    realPrices.Add((double)slope.getOpenPrice);
                    realDateTimes.Add(slope.OpenTimeUnix);
                }
            }

            // Fake Result
            foreach (PricePointSlope slope in sd.getPredictionOutput)
            {
                predictedPoints.AddRange(slope.getSlopePoints);
            }
            formsPlot1.Plot.AddCandlesticks(points.ToArray());
            formsPlot1.Plot.AddCandlesticks(predictedPoints.ToArray());
            formsPlot1.Refresh();*/
        }
        private void genSlopeView(List<PricePointSlope> slopes, int selectedSlope = -1)
        {
            if (slopes == null)
            {
                MessageBox.Show("Predisction history data is invalid");
                return;
            }
            if (selectedSlope >= 0 && selectedSlope < slopes.Count)
            {
                var slopeX = slopes[selectedSlope];
                slopes.Remove(slopeX);
                double[] yVals1 = new double[] { (double)slopeX.getOpenPrice, (double)slopeX.getClosePrice };
                double[] xVals1 = new double[] { (double)slopeX.OpenTimeUnix, (double)slopeX.CloseTimeUnix };

                formsPlot1.Plot.AddScatter(xVals1, yVals1, color: Color.Green);
            }
            List<double> yVals2 = new List<double>();
            List<double> xVals2 = new List<double>();

            // Display the rest
            foreach (PricePointSlope slope in slopes.ToArray())
            {
                yVals2.Add((double)slope.getOpenPrice);
                xVals2.Add((double)slope.OpenTimeUnix);
            }
            formsPlot1.Plot.AddScatter(xVals2.ToArray(), yVals2.ToArray(), color: Color.Blue);
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
            //logger.stopLogging();
            //DataCache.Save();
            //logger.startLogging();
        }
        private void fluidLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filename = openFile();
            switch (Path.GetExtension(filename))
            {

            }
        }
        private void viewFileDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void openTrainingFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = openFile();
            if (filename == "") return;
            string[] data = File.ReadAllLines(filename);

            List<PricePointSlope> trainingSlopes = new List<PricePointSlope>();

            foreach (string line in data)
            {
                if (line.Length <= 12) continue;
                string prompt = line.Substring(12);
                int indexA = prompt.IndexOf('\"');
                if (indexA == -1) continue;
                prompt = prompt.Substring(0, indexA);

                int indexB = line.IndexOf("\", \"completion\": \"");
                if (indexB == -1) continue;
                string completion = line.Substring(indexB);
                int indexC = completion.IndexOf("\"}") - 1;
                if (indexC < completion.Length)
                    completion = completion.Substring(0, indexC);

                // if (line.Length < 12 + indexA + 18 && line.Length - 3 <= 12 + indexA + 18) continue;
                //string completion = line.Substring(12 + indexA + 18, line.Length - 3 -( 12 + indexA + 18));
                trainingSlopes.AddRange(OpenAi.Formatting.FluidLanguage.Decode(prompt));
                trainingSlopes.AddRange(OpenAi.Formatting.FluidLanguage.Decode(completion));
            }

            genSlopeView(trainingSlopes);

        }
    }
}
