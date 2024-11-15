﻿using System;
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
using OpenAI_API;
using OpenAI_API.Models;
using static System.Formats.Asn1.AsnWriter;
using static ScottPlot.Generate;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;
using System.Drawing.Text;
using OpenAI_API.Files;
using ACE.Trading.Analytics.PredictedHistories;

namespace ACE.Trading.Data.Graphics
{
    public partial class PredictionView : Form
    {
        public PredictionView()
        {
            InitializeComponent();
        }
        List<Model> modelList = new List<Model>();
        List<FineTuneResult> fineTunes = new List<FineTuneResult>();
        List<OpenAI_API.Files.File> files = new List<OpenAI_API.Files.File>();
        string[] symbols;
        string filename;

        string[] intervals = new string[]
            {
                KlineInterval.ThreeDay.ToString(),
                KlineInterval.OneDay.ToString(),
                KlineInterval.TwelveHour.ToString(),
                KlineInterval.EightHour.ToString(),
                KlineInterval.OneHour.ToString(),
                KlineInterval.ThirtyMinutes.ToString(),
                KlineInterval.FifteenMinutes.ToString(),
                KlineInterval.OneMinute.ToString()
            };

        List<List<PredictedPriceHistory>> priceHistories = new List<List<PredictedPriceHistory>>();
        List<List<PredictedSlopeHistory>> slopeHistories = new List<List<PredictedSlopeHistory>>();
        BinanceHandler bh = new BinanceHandler();
        //PriceHistoryLogging logger = new PriceHistoryLogging();
        //System.Windows.Forms.Timer collectedDataPollingTimer;

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
        private async void loadModels()
        {

            // Loads fine tund models
            OpenAiIntegration ai = new OpenAiIntegration();
            fineTunes.Clear();
            FineTuneResultList results = await ai.getFineTuneList();
            fineTunes.AddRange(results.data);

            BeginInvoke((MethodInvoker)delegate
            {
                tunesCombo.Items.Clear();
                foreach (var tune in fineTunes)
                {
                    tunesCombo.Items.Add(tune.Id);
                }
            });
            // Loads Standard Models
            var result = ai.getModels();
            while (!result.IsCompleted) { Thread.Sleep(1000); }
            if (result.IsCompletedSuccessfully)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    modelList.AddRange(result.Result);
                });
            }
            else if (result.IsCanceled)
            {
                MessageBox.Show("Error retreiving fine tuned models.");
            }
            else if (result.IsFaulted)
            {
                MessageBox.Show("Error retreiving fine tuned models.");
                Debug.Print(result.Exception.ToString());
            }

            var newResult = await ai.getFineTuneList();
            BeginInvoke((MethodInvoker)delegate
            {
                foreach (var fineTuneId in newResult.data)
                {
                    if (fineTuneId.FineTunedModel != null)
                    {
                        fineTuneModelCombo.Items.Add(fineTuneId.FineTunedModel);
                    }
                }
                // Display all models
                modelIdCombo.Items.Clear();
                foreach (var model in modelList.ToArray())
                {
                    if (model.ModelID != null)
                    {
                        modelIdCombo.Items.Add(model.ModelID);
                    }
                }
            });
        }
        private void refreshGui()
        {
            symbols = DataCache.getAllSymbols();
            Analytics.Predictions.Load();

            loadFiles();

            // show time intervals

            timeIntervalCombo.Items.Clear();
            timeIntervalCombo.Items.AddRange(intervals);
            timeIntervalCombo2.Items.Clear();
            timeIntervalCombo2.Items.AddRange(intervals);

            // show time durations
            durationCombo.Items.Add(KlineInterval.OneMonth);
            durationCombo.Items.Add(KlineInterval.OneWeek);
            durationCombo.Items.Add(KlineInterval.OneDay);
            durationCombo.Items.Add(KlineInterval.OneHour);
            durationCombo.SelectedIndexChanged += DurationCombo_SelectedIndexChanged;

            // Show symbols
            symbolCombo.Items.Clear();
            symbolCombo.Items.AddRange(symbols);
            modelIdCombo.Items.Clear();
            new Thread(loadModels).Start();

        }

        private async void loadFiles()
        {
            filesListBox.Items.Clear();
            OpenAi.OpenAiIntegration ai = new OpenAiIntegration();
            files = await ai.getFiles();
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    filesListBox.Items.Add(file.Name + " | " + file.Bytes + 'B');
                }
            }
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

        #region Slope Generation
        private async Task<int> generateSlopes(string symbol, KlineInterval interval, System.DateTime startTime, System.DateTime finishTime, string filename = "")
        {
            if (filename == "")
            {
                SaveFileDialog sfd = new SaveFileDialog() { AddExtension = true, DefaultExt = ".jsonl" };
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("No filename specified.");
                    return -1;
                }
                filename = sfd.FileName.Contains(".jsonl") ? sfd.FileName : sfd.FileName + ".jsonl";
            }


            // Get binance data
            // loop through sice cap is 500 klines per request
            TimeSpan span = finishTime.Subtract(startTime);
            List<PricePoint> points = new List<PricePoint>();
            const int hoursPerCycle = 6; finishTime = startTime.AddHours(hoursPerCycle);
            int i;
            for (i = 0; i < span.TotalHours - hoursPerCycle; i += hoursPerCycle)
            {
                WebCallResult<IEnumerable<IBinanceKline>> result = await bh.getMarketData(symbolCombo.Text, interval, startTime, finishTime);

                points.AddRange(PricePoint.FromBinanceKline(result.Data));
                startTime = startTime.AddHours(hoursPerCycle);
                finishTime = finishTime.AddHours(hoursPerCycle);
            }

            // do remainder
            startTime = startTime.AddHours(hoursPerCycle);
            finishTime = finishTime.AddHours(span.TotalHours - i).AddMinutes(span.Minutes);
            var marketDataResult = await bh.getMarketData(symbolCombo.Text, interval, startTime, finishTime);
            points.AddRange(PricePoint.FromBinanceKline(marketDataResult.Data));

            // convert to slopes
            List<PricePointSlope> slopes = Convertions.FindAllV2(points.ToArray(), (int)tolleranceNum.Value);

            // display data
            genSlopeView(slopes);

            // convert to trainingData
            TrainingData td = BinanceToTraining.slopesToTraingingDataMinLanguage(slopes, (int)trainingPromptNum.Value, (int)trainingCompletionNum.Value);

            // save to tmp

            System.IO.File.WriteAllText(filename, td.ToString());

            return 0;

        }
        #endregion

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
        private void genSlopeView(List<PricePointSlope> promptSlopes, List<PricePointSlope> completionSlopes)
        {
            if (promptSlopes == null || completionSlopes == null)
            {
                MessageBox.Show("Predisction history data is invalid");
                return;
            }
            List<double> yVals1 = new List<double>();
            List<double> xVals1 = new List<double>();

            // Display the rest
            foreach (PricePointSlope slope in promptSlopes.ToArray())
            {
                yVals1.Add((double)slope.getOpenPrice);
                xVals1.Add((double)slope.OpenTimeUnix);
            }
            formsPlot1.Plot.AddScatter(xVals1.ToArray(), yVals1.ToArray(), color: Color.Blue);
            List<double> yVals2 = new List<double>();
            List<double> xVals2 = new List<double>();

            // Display the rest
            foreach (PricePointSlope slope in completionSlopes.ToArray())
            {
                yVals2.Add((double)slope.getOpenPrice);
                xVals2.Add((double)slope.OpenTimeUnix);
            }
            formsPlot1.Plot.AddScatter(xVals2.ToArray(), yVals2.ToArray(), color: Color.Orange);
            formsPlot1.Refresh();
        }
        #endregion

        // NOT YET IMPLEMENTED
        // Open fliud language slope file
        private string openFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Json Lines File (*.jsonl)|*.jsonl";

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


        #region GUI Events
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
        private void openTrainingFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = openFile();
            if (filename == "") return;
            string[] data = System.IO.File.ReadAllLines(filename);

            List<PricePointSlope> promptSlopes = new List<PricePointSlope>();
            List<PricePointSlope> completionSlopes = new List<PricePointSlope>();

            foreach (string line in data)
            {
                if (line.Length <= 12) continue;
                string prompt = line.Substring(12);
                int indexA = prompt.IndexOf('\"');
                if (indexA == -1) continue;
                prompt = prompt.Substring(0, indexA);

                int indexB = line.IndexOf("\", \"completion\": \"");
                if (indexB == -1) continue;
                string completion = line.Substring(indexB+ "\", \"completion\": \"".Length);
                int indexC = completion.IndexOf("\"}") - 1;
                if (indexC < completion.Length)
                    completion = completion.Substring(0, indexC);

                // if (line.Length < 12 + indexA + 18 && line.Length - 3 <= 12 + indexA + 18) continue;
                //string completion = line.Substring(12 + indexA + 18, line.Length - 3 -( 12 + indexA + 18));
                promptSlopes.AddRange(OpenAi.Formatting.MinLanguage.Decode(prompt));
                completionSlopes.AddRange(OpenAi.Formatting.MinLanguage.Decode(completion));
            }

            genSlopeView(promptSlopes, completionSlopes);

        }
        private void TrainingModelType_CheckedChanged(object sender, EventArgs e)
        {
            if (trainExistingRadioBtn.Checked)
            {
                modelSuffixTextBox.Enabled = false;
                fineTuneModelCombo.Enabled = true;
            }
            else
            {
                modelSuffixTextBox.Enabled = true;
                fineTuneModelCombo.Enabled = false;
            }
        }
        private async void genAndUploadBtn_Click(object sender, EventArgs e)
        {
            KlineInterval timeInterval = KlineInterval.OneMinute;
            switch (timeIntervalCombo2.Text)
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
            if (symbolCombo.SelectedIndex == -1)
            {
                MessageBox.Show("No Symbol selected.");
                return;
            }
            if (uploadFileName.Text.Length == 0)
            {
                MessageBox.Show("File name cannot be empty.");
                return;
            }
            else
            if (!uploadFileName.Text.Contains(".jsonl"))
            {
                MessageBox.Show("File name must have the extension '.jsonl'");
                return;
            }

            int x = await generateSlopes(symbolCombo.Text, timeInterval, startDateTime.Value, finishDateTime.Value, filename = uploadFileName.Text);

            if (!System.IO.File.Exists(uploadFileName.Text) || x != 0)
            {
                MessageBox.Show("File generation failed.");
                return;
            }

            // Upload
            OpenAiIntegration ai = new OpenAiIntegration();
            var uploadResult = await ai.uploadFile(uploadFileName.Text);

            FineTunes.FineTunes.AddFile(new FineTunes.FineTuneFile(uploadResult.Id, (int)trainingPromptNum.Value));

            refreshGui();
            MessageBox.Show("Task Completed,\n\nFile id: " + uploadResult.Id + "\nFile Name: " + uploadResult.Name);
        }
        private async void fineTuneBtn_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (filesListBox.SelectedIndex == -1 && filesListBox.SelectedIndex >= files.Count)
            {
                MessageBox.Show("No Files Selected.");
                return;
            }
            else
            {
                filename = files[filesListBox.SelectedIndex].Id;
            }
            if (trainNewRadioBtn.Checked && modelSuffixTextBox.Text == "")
            {
                MessageBox.Show("Model suffix entered. Use this to id yor different models.");
                return;
            }
            if (trainExistingRadioBtn.Checked && (fineTuneModelCombo.Text == "" || fineTuneModelCombo.SelectedIndex == -1))
            {
                MessageBox.Show("No model selected to train.");
                return;
            }

            if (symbolCombo.Text == "" || symbolCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid symbol selected.");
                return;
            }

            OpenAiIntegration ai = new OpenAiIntegration();
            HyperParams hypers = new HyperParams
            {
                BatchSize = (int)batchSizeNum.Value,
                PromptLossWeight = (double)promptWeightNum.Value,
                NumberOfEpochs = (int)epochCountNum.Value,
                LearningRateMultiplier = (double)learningRateNum.Value
            };

            var result = await ai.fineTune(filename, symbolCombo.Text, hypers);
            FineTunes.FineTunes.Add(new(result));

            MessageBox.Show($"Fine tune created. Current Status: {result.Status}");


        }
        private void durationCombo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
        private void createNew_Enter(object sender, EventArgs e)
        {

        }
        private void filesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filesListBox.SelectedIndex == -1)
            {
                fineTuneGroupBox.Enabled = deleteFileBtn.Enabled = button2.Enabled = false;
            }
            else
            {
                fineTuneGroupBox.Enabled = deleteFileBtn.Enabled = button2.Enabled = true;
            }
        }
        private async void deleteFileBtn_Click(object sender, EventArgs e)
        {
            if (filesListBox.SelectedIndex == -1 && filesListBox.SelectedIndex >= files.Count)
            {
                MessageBox.Show("No Files Selected.");
                return;
            }
            else
            {
                OpenAiIntegration ai = new OpenAiIntegration();
                var result = await ai.deleteFile(files[filesListBox.SelectedIndex].Id);

                MessageBox.Show(result == null || result.Deleted ? "File successfully deleted." : "Unable to delete file.");
                refreshGui();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            KlineInterval timeInterval = KlineInterval.OneMinute;
            switch (timeIntervalCombo2.Text)
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
            if (symbolCombo.SelectedIndex == -1)
            {
                MessageBox.Show("No Symbol selected.");
                return;
            }

            generateSlopes(symbolCombo.Text, timeInterval, startDateTime.Value, finishDateTime.Value, filename = uploadFileName.Text);

        }
        private async void uploadFileBtn_Click(object sender, EventArgs e)
        {
            // get filename
            OpenFileDialog ofd = new OpenFileDialog() { CheckFileExists = true, Multiselect = false, Filter = "Json Lines Files|*.jsonl" };
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Invalid file / File does not exist.");
                return;
            }

            //upload
            OpenAiIntegration ai = new OpenAiIntegration();
            var uploadResult = await ai.uploadFile(ofd.FileName);
            refreshGui();
            MessageBox.Show("Task Completed, File id: " + uploadResult.Id + "| File Name: " + uploadResult.Name);
        }

        // Help Info
        private void batchSizeHelpBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The batch size to use for training. The batch size is the number of training examples used to train a single forward and backward pass.\r\n\r\nBy default, the batch size will be dynamically configured to be ~0.2% of the number of examples in the training set, capped at 256 - in general, we've found that larger batch sizes tend to work better for larger datasets.");
        }
        private void epochCountHelpBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The number of epochs to train the model for. An epoch refers to one full cycle through the training dataset.");
        }
        private void weightLossHelpBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The weight to use for loss on the prompt tokens. This controls how much the model tries to learn to generate the prompt (as compared to the completion which always has a weight of 1.0), and can add a stabilizing effect to training when completions are short.\r\n\r\nIf prompts are extremely long (relative to completions), it may make sense to reduce this weight so as to avoid over-prioritizing learning the prompt.");
        }
        private void learningMultiplierHelpBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The learning rate multiplier to use for training. The fine-tuning learning rate is the original learning rate used for pretraining multiplied by this value.\r\n\r\nBy default, the learning rate multiplier is the 0.05, 0.1, or 0.2 depending on final batch_size (larger learning rates tend to perform better with larger batch sizes). We recommend experimenting with values in the range 0.02 to 0.2 to see what produces the best results.");
        }
        private async void tunesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tunesCombo.SelectedIndex == -1)
                return;
            if (tunesCombo.SelectedIndex >= fineTunes.Count)
                return;

            FineTuneResult tune = fineTunes[tunesCombo.SelectedIndex];
            OpenAiIntegration ai = new OpenAiIntegration();
            tune = await ai.getFineTuneUpdate(tune.Id);


            tunesStatusLabel.Text = tune.Status;
            tunesBaseModelLabel.Text = tune.model;
            tunesCreatedAtLabel.Text = DateTimeOffset.FromUnixTimeSeconds(tune.createdAt).DateTime.ToString();
            var tuneDetails = FineTunes.FineTunes.FindFineTuneById(tune.Id);
            if (tuneDetails != null)
            {
                tunesSlopeNumLabel.Text = tuneDetails.slopesPerPrompt.ToString();
            }
            // hypers
            tunesBatchSizeLabel.Text = tune.HyperParameters.BatchSize.ToString();
            tunesEpochCountLabel.Text = tune.HyperParameters.NumberOfEpochs.ToString();
            tunesWeightLossLabel.Text = tune.HyperParameters.PromptLossWeight.ToString();
            tunesLearningMultiplierLabel.Text = tune.HyperParameters.LearningRateMultiplier.ToString();

            tunesEventsList.Items.Clear();
            if (tune.Events != null && tune.Events.Count > 0)
            {
                foreach (var eventX in tune.Events)
                {
                    tunesEventsList.Items.Add($"{eventX.Message}\n");
                }
            }

        }
        #endregion

        private async void tunesEventsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tunesCombo.SelectedIndex == -1)
                return;
            if (tunesCombo.SelectedIndex >= fineTunes.Count)
                return;

            FineTuneResult tune = fineTunes[tunesCombo.SelectedIndex];
            OpenAiIntegration ai = new OpenAiIntegration();
            tune = await ai.getFineTuneUpdate(tune.Id);

            if (tunesEventsList.SelectedIndex == -1)
                return;
            if (tunesEventsList.SelectedIndex >= tune.Events.Count)
                return;
            var eventX = tune.Events[tunesEventsList.SelectedIndex];
            var time = DateTimeOffset.FromUnixTimeSeconds(tune.createdAt).DateTime.ToString();

            MessageBox.Show($"{eventX.Message}\nAt: {time}\nLevel: {eventX.level}");

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (filesListBox.SelectedIndex == -1 && filesListBox.SelectedIndex >= files.Count)
            {
                MessageBox.Show("No Files Selected.");
                return;
            }
            else
            {
                OpenAiIntegration ai = new OpenAiIntegration();
                var result = await ai.getFileData(files[filesListBox.SelectedIndex].Id);
                SaveFileDialog sfd = new SaveFileDialog() { Filter = "Any File Type|*.*" };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfd.FileName, result);
                }
            }
        }
    }
}
