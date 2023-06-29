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

namespace ACE.Trading
{
    public class Predictions
    {

        #region Predictions

        public Predictions()
        {
            Analytics.Predictions.Load();
        }
        public async Task<List<Price>> predict(string symbol, Model model)
        {
            // retrieve symbol data
            SymbolData sd = DataCache.GetSymbolData(symbol);
            if (sd == null)
            {
                Debug.WriteLine("ACE.Trading.Predictions.predict: sd == null");
                return null;
            }

            // retrieves logged price history
            List<Price> lastHour = sd.priceHistory.FindAll(ph => ph.timeUtc >= DateTime.UtcNow.AddHours(-1));

            // check validity of price history
            if (lastHour == null && lastHour?.Count == 0)
            {
                Debug.WriteLine("ACE.Trading.Predictions.predict: (lastHour == null && lastHour?.Count == 0)");
                return null;
            }

            // translate to 2d array
            List<string[]> strings = new List<string[]>();
            foreach (Price price in lastHour)
            {
                //strings.Add(new string[] { price.timeUtc.ToString(), price.averagePrice.ToString() });    // --> Uses the average price
                strings.Add(new string[] { price.timeUtc.ToString(), price.getDeltaPrice.ToString() });          // --> Uses the price change from the previous bar
            }


            // Predict
            OpenAi.OpenAiIntegration pe = new OpenAi.OpenAiIntegration();
            var encodedStrs = OpenAi.Encoding.Encode(strings.ToArray());
            string prediction = await pe.PredictFromFineTune(encodedStrs, model);
            string[][] predictedStrs = OpenAi.Encoding.Decode(prediction);

            // translate to local Price object
            List<Price> PredictedPrices = new List<Price>();
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


                Price price = new Price { timeUtc = DateTime.Parse(str[0]), _deltaPrice = decimal.Parse(str[1]) };
                PredictedPrices.Add(price);
            }

            // Log prediction data
            Analytics.Predictions.addPrediction(symbol, model, lastHour, PredictedPrices);

            return PredictedPrices;
        }

        public bool genTrainingDataFromDataCache(string inputFilename, string outputFilename, string symbol, OpenAi.Encoding.priceInterval interval, OpenAi.Encoding.priceType type, int hours, out string output)
        {

            output = OpenAi.Encoding.seperator;
            DateTime end = DateTime.UtcNow;
            DateTime start = end.AddHours(-hours);

            var sd = DataCache.GetSymbolData(symbol);
            var list = sd.priceHistory.FindAll(p => p.timeUtc >= start && p.timeUtc < end);

            if (list.Count == 0) return false;

            int intervalInMinutes = 0;
            switch (interval)
            {
                case OpenAi.Encoding.priceInterval.OneMinute:
                    intervalInMinutes = 1; break;
                case OpenAi.Encoding.priceInterval.FiveMinute:
                    intervalInMinutes = 5; break;
                case OpenAi.Encoding.priceInterval.FifteenMinutes:
                    intervalInMinutes = 15; break;
                case OpenAi.Encoding.priceInterval.OneHour:
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
                    priceAvg += type == OpenAi.Encoding.priceType.DeltaPrice ? pricePoint.getDeltaPrice : pricePoint.averagePrice;
                }

                priceAvg /= pricePoints.Count;
                output += string.Format("Time: {0} | Price: {1}{2}", start.ToString(), priceAvg, OpenAi.Encoding.seperator);
                start = start.AddMinutes(intervalInMinutes);
            }

            return true;
        }
        #endregion


    }
}
