using CryptoExchange.Net.CommonObjects;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using ACE.Trading.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ACE.Trading.Data.DataHandling;
using ACE.Trading.Analytics.Slopes;
using ACE.Trading.OpenAi.Formatting;
using ACE.Trading.OpenAi;
using Newtonsoft.Json;

namespace ACE.Trading.OpenAi
{
    public class Predictions
    {

        #region Predictions

        public Predictions()
        {
            Analytics.Predictions.Load();
        }
        public async Task<List<PricePoint>> predict(string symbol, Model model)
        {
            // retrieve symbol data
            SymbolData sd = DataCache.GetSymbolData(symbol);
            if (sd == null)
            {
                Debug.WriteLine("ACE.Trading.Predictions.predict: sd == null");
                return null;
            }

            // retrieves logged price history
            List<PricePoint> lastHour = sd.getPriceHistory.FindAll(ph => ph.timeUtc >= DateTime.UtcNow.AddHours(-1));

            // check validity of price history
            if (lastHour == null && lastHour?.Count == 0)
            {
                Debug.WriteLine("ACE.Trading.Predictions.predict: (lastHour == null && lastHour?.Count == 0)");
                return null;
            }

            // translate to 2d array
            List<string[]> strings = new List<string[]>();
            foreach (PricePoint price in lastHour)
            {
                //strings.Add(new string[] { price.timeUtc.ToString(), price.averagePrice.ToString() });    // --> Uses the average price
                strings.Add(new string[] { price.timeUtc.ToString(), price.deltaPrice.ToString() });          // --> Uses the price change from the previous bar
            }


            // Predict
            OpenAi.OpenAiIntegration pe = new OpenAi.OpenAiIntegration();
            var encodedStrs = OpenAi.SimpleEncoding.Encode(strings.ToArray());
            string prediction = await pe.PredictFromFineTune(encodedStrs, model);
            string[][] predictedStrs = OpenAi.SimpleEncoding.Decode(prediction);

            // translate to local Price object
            List<PricePoint> PredictedPrices = new List<PricePoint>();
            foreach (string[] str in predictedStrs)
            {
                // Check vars
                if (str.Length != 2)
                {
                    Debug.WriteLine("ACE.Trading.Predictions.predict: (str.Length != 2)");
                    continue;
                }
                if (str[0].Length <= 0 && str[1].Length <= 0)
                {
                    Debug.WriteLine("ACE.Trading.Predictions.predict: (str[0].Length <= 0 && str[1].Length <= 0)");
                    continue;
                }


                PricePoint price = new PricePoint { timeUtc = DateTime.Parse(str[0]), deltaPrice = decimal.Parse(str[1]) };
                PredictedPrices.Add(price);
            }

            // Log prediction data
            Analytics.Predictions.addPricePrediction(symbol, model, lastHour, PredictedPrices);

            return PredictedPrices;
        }
        /// <summary>
        /// Predicts the next series of slopes based on slope inputs
        /// </summary>
        /// <param name="symbol">Which symbol the prediction is for</param>
        /// <param name="model">which model to use for the prediction</param>
        /// <param name="numOfPromptSlopes">number of slopes to use for the prompt</param>
        /// <returns>The prediction history id of the prediction once it has completed successfully</returns>
        public async Task<long> predictSlopes(string symbol, Model model, int numOfPromptSlopes)
        {
            // retrieve symbol data
            SymbolData sd = DataCache.GetSymbolData(symbol);
            if (sd == null)
            {
                Debug.WriteLine("ACE.Trading.Predictions.predict: sd == null");
                return -2;
            }

            // retrieves logged price history
            List<PricePoint> points = sd.getPriceHistory.FindAll(ph => ph.timeUtc >= DateTime.UtcNow.AddHours(-1));

            // split points into minutebased points
            DateTime startTime = DateTime.UtcNow.AddHours(-1);
            DateTime nextTime = startTime.AddMinutes(1);
            List<PricePoint> outp = new List<PricePoint>();
            while (startTime < DateTime.UtcNow)
            {
                List<PricePoint> p1 = points.FindAll(p => DataHandling.AllBetween(p, startTime, nextTime));
                PricePoint p = new PricePoint();

                // Gets high price and low price
                p.highPrice = p.lowPrice = 0.0m;
                if (p1 == null || p1.Count == 0) 
                {
                    startTime = startTime.AddMinutes(1);
                    nextTime = startTime.AddMinutes(1);
                    continue;
                }
                foreach (var p2 in p1)
                {
                    if (p2.highPrice != 0.0m)
                    {
                        if (p2.highPrice > p.highPrice) p.highPrice = p2.highPrice;
                    }
                    else if (p2.lastKnownPrice != 0.0m)
                    {
                        if (p2.lastKnownPrice > p.highPrice) p.highPrice = p2.lastKnownPrice;
                    }
                    if (p2.lowPrice != 0.0m)
                    {
                        if (p2.lowPrice < p.lowPrice) p.lowPrice = p2.lowPrice;
                    }
                    else if (p2.lastKnownPrice != 0.0m)
                    {
                        if (p2.lastKnownPrice < p.lowPrice) p.lowPrice = p2.lastKnownPrice;
                    }
                }
                p.timeUtc = startTime;

                p1.Sort(DataHandling.sortTime_latestFirst);
                p.closePrice = p1.First().closePrice;
                p.openPrice = p1.Last().openPrice;
                outp.Add(p);
                startTime = startTime.AddMinutes(1);
                nextTime = startTime.AddMinutes(1);
            }
            List < PricePointSlope > inputSlopes = Convertions.FindAll(outp.ToArray());



            // check validity of price history
            if (inputSlopes == null && inputSlopes?.Count == 0)
            {
                Debug.WriteLine("ACE.Trading.Predictions.predictSlopes: (inputSlopes == null && inputSlopes?.Count == 0)");
                return -1;
            }
            inputSlopes.Sort(Analytics.Slopes.Convertions.sortTime_latestFirst);

            if (inputSlopes.Count == 0)
            {
                Debug.WriteLine("Input Slopes List is empty. No Data");
                return -3;
            }else if (inputSlopes.Count - numOfPromptSlopes < 0)
            {
                Debug.WriteLine("Not enough slopes to create the prompt");
                return -4;
            }

            string prompt = FluidLanguage.lineSeperator;
            List<PricePointSlope> limitedInputSlopes = new List<PricePointSlope>();
            for (int j = inputSlopes.Count-numOfPromptSlopes; j < inputSlopes.Count; j++)
            {
                prompt += FluidLanguage.formatBinanceLine(inputSlopes[j]);
                limitedInputSlopes.Add(inputSlopes[j]);
            }
            string prediction = await new OpenAiIntegration().PredictFromFineTune(prompt, model.ModelID);

            List<PricePointSlope> outputSlopes = OpenAi.Formatting.FluidLanguage.Decode(prediction);

            // Log prediction data
            long id = Analytics.Predictions.addSlopePrediction(symbol, model, limitedInputSlopes, outputSlopes);

            return id;
        }

        public bool genTrainingDataFromDataCache(string inputFilename, string outputFilename, string symbol, OpenAi.SimpleEncoding.priceInterval interval, OpenAi.SimpleEncoding.priceType type, int hours, out string output)
        {

            output = OpenAi.SimpleEncoding.seperator;
            DateTime end = DateTime.UtcNow;
            DateTime start = end.AddHours(-hours);

            var sd = DataCache.GetSymbolData(symbol);
            List<PricePoint> list = sd.getPriceHistory.FindAll(p => p.timeUtc >= start && p.timeUtc < end);

            if (list.Count == 0) return false;

            int intervalInMinutes = 0;
            switch (interval)
            {
                case OpenAi.SimpleEncoding.priceInterval.OneMinute:
                    intervalInMinutes = 1; break;
                case OpenAi.SimpleEncoding.priceInterval.FiveMinute:
                    intervalInMinutes = 5; break;
                case OpenAi.SimpleEncoding.priceInterval.FifteenMinutes:
                    intervalInMinutes = 15; break;
                case OpenAi.SimpleEncoding.priceInterval.OneHour:
                    intervalInMinutes = 60; break;
            }
            if (intervalInMinutes == 0) return false;


            list.Sort(sortTime_earliestFirst);
            int numOfPricePoints = (hours * 60) / intervalInMinutes;
            for (int i = 0; i < numOfPricePoints; i++)
            {
                var pricePoints = list.FindAll(p => p.timeUtc >= start && p.timeUtc < start.AddMinutes(intervalInMinutes));

                if (pricePoints.Count == 0) return false;
                decimal priceAvg = 0.0m;

                foreach (var pricePoint in pricePoints)
                {
                    priceAvg += type == OpenAi.SimpleEncoding.priceType.DeltaPrice ? pricePoint.deltaPrice : pricePoint.avgPrice;
                }

                priceAvg /= pricePoints.Count;
                output += string.Format("Time: {0} | Price: {1}{2}", start.ToString(), priceAvg, OpenAi.SimpleEncoding.seperator);
                start = start.AddMinutes(intervalInMinutes);
            }

            return true;
        }
        #endregion


    }
}
