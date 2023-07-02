using Newtonsoft.Json;
using OpenAI_API.Models;
using ACE.Trading.Data;
using ACE.Trading.Data.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.Trading.Analytics.Slopes;
using Binance.Net.Objects.Models.Spot.SubAccountData;
using System.Data;
using CryptoExchange.Net.CommonObjects;

namespace ACE.Trading.Analytics
{
    public static class Predictions
    {
        private static Cache cache = new Cache();
        private class Cache
        {
            [JsonProperty("PredictionSlopeHistory")]
            internal List<PredictedSlopeHistory> slopeHistory = new List<PredictedSlopeHistory>();

            [JsonProperty("PredictionPriceHistory")]
            internal List<PredictedPriceHistory> priceHistory = new List<PredictedPriceHistory>();

            [JsonIgnore]
            internal const string DATACACHE_FILENAME = "C:\\Users\\Toby\\.ace\\ACE-PREDICTIONHISTORY.x3";
        }

        public static void addPricePrediction(string symbol, Model model, List<PricePoint> Input, List<PricePoint> Output)
        {
            long id = cache.priceHistory.Count;
            PredictedPriceHistory ph = new PredictedPriceHistory(id, symbol, model, Input, Output);
            cache.priceHistory.Add(ph);
            Save();
        }
        public static long addSlopePrediction(string symbol, Model model, List<PricePointSlope> Input, List<PricePointSlope> Output)
        {
            long id = cache.slopeHistory.Count;
            PredictedSlopeHistory ph = new PredictedSlopeHistory(id, symbol, model, Input, Output);
            cache.slopeHistory.Add(ph);
            Save();
            return id;
        }

        public static PredictedPriceHistory findPricePrediction(long id)
        {
            return cache.priceHistory.Find(ph => ph.getId == id);
        }
        public static PredictedSlopeHistory findSlopePrediction(long id)
        {
            return cache.slopeHistory.Find(ph => ph.getId == id);
        }

        public static bool findPricePredictions(string symbol, out List<PredictedPriceHistory> histories)
        {
            histories = cache.priceHistory.FindAll(ph => ph.getSymbol == symbol);
            return histories != null && histories?.Count > 0;
        }
        public static bool findSlopePredictions(string symbol, out List<PredictedSlopeHistory> histories)
        {
            histories = cache.slopeHistory.FindAll(ph => ph.getSymbol == symbol);
            return histories != null && histories?.Count > 0;
        }

        public static decimal ComputeAverageSlopeAccuracy()
        {
            int num = 0;
            decimal sum = 0.0m;
            foreach (var item in cache.slopeHistory)
            {
                if (!item.isComplete)
                    continue;

                num++;
                sum += item.computeAccuracy();

            }
            return sum / num;
        }
        public static decimal ComputeAveragePriceAccuracy()
        {
            int num = 0;
            decimal sum = 0.0m;
            foreach (var item in cache.slopeHistory)
            {
                if (!item.isComplete)
                    continue;

                num++;
                sum += item.computeAccuracy();

            }
            return sum / num;
        }

        public static void UpdatePredictions()
        {
            foreach(PredictedSlopeHistory p in cache.slopeHistory.ToArray())
            {
                if (!p.isComplete)
                {
                    cache.slopeHistory.Remove(p);
                    p.update();
                    cache.slopeHistory.Add(p);
                }
            }
            foreach (PredictedPriceHistory p in cache.priceHistory.ToArray())
            {
                if (!p.isComplete)
                {
                    cache.priceHistory.Remove(p);
                    p.update();
                    cache.priceHistory.Add(p);
                }
            }
        }

        public static void Load()
        {
            if (File.Exists(Cache.DATACACHE_FILENAME) && cache.priceHistory.Count == 0 && cache.slopeHistory.Count == 0)
            {
                // read
                string json = File.ReadAllText(Cache.DATACACHE_FILENAME);

                // decrypt - Not Yet Implemented


                // deserialize
                cache = JsonConvert.DeserializeObject<Cache>(json);
            }
            else
            {
                if (cache == null)
                {
                    cache = new Cache();
                }
            }
        }
        public static void Save()
        {
            // serialize
            string jsonString = JsonConvert.SerializeObject(cache);

            // encrypt - Not Yet Implemented


            // write
            File.WriteAllText(Cache.DATACACHE_FILENAME, jsonString);
        }
    }

    public class PredictedPriceHistory
    {
        #region Properties
        // internal prediction ID
        [JsonProperty("Id")]
        long Id { get; set; }

        [JsonProperty("Symbol")]
        string Symbol { get; set; }

        [JsonProperty("Model")]
        string Model { get; set; }

        // Input to base the prediction from
        [JsonProperty("PredictedInput")]
        List<PricePoint>? PredictionInput { get; set; }

        // predicted outcome
        [JsonProperty("PredictionOutput")]
        List<PricePoint>? PredictionOutput { get; set; }

        // Real outcome
        [JsonProperty("RealResult")]
        List<PricePoint>? RealResult { get; set; }

        // bool represents if all the required data is present to do calcs
        [JsonProperty("Complete")]
        bool Complete { get; set; }

        [JsonIgnore]
        public bool isComplete
        {
            get { return Complete; }
        }

        [JsonIgnore]
        public long getId { get { return Id; } }

        [JsonIgnore]
        public string getSymbol { get { return Symbol; } }
        #endregion

        internal void update()
        {
            RealResult = new List<PricePoint>();
            // Find bars from datacache that match predicted output timestamps
            foreach (var item in PredictionOutput)
            {
                List<PricePoint> p = DataCache.findAllByTime(Symbol, item.timeUtc);
                if (p != null && p?.Count > 0)
                {
                    decimal avg = 0.0m;
                    foreach (PricePoint price in p)
                    {
                        avg += price.deltaPrice;
                    }
                    avg /= p.Count;
                    PricePoint price1 = new PricePoint { deltaPrice = avg, timeUtc = item.timeUtc };
                    RealResult.Add(price1);
                }
            }
            TimeSpan x = PredictionOutput.First().timeUtc.Subtract(PredictionOutput.Last().timeUtc);
            if (x.TotalMinutes == 0)
            {
                Complete = true;
            }
        }

        public PredictedPriceHistory(long id, string symbol, Model model, List<PricePoint> input, List<PricePoint> output)
        {
            this.Id = id;
            this.Symbol = symbol;
            this.Model = model;
            if (input.Count > 0 && output.Count > 0)
            {
                this.PredictionInput = input;
                this.PredictionOutput = output; 
                
                update();
            }
            else
            {
                this.PredictionInput = null;
                this.PredictionOutput = null;
                Complete = false;
            }
        }


        /// <summary>
        /// Calculates the accuracy of the predicted result
        /// </summary>
        /// <returns>A percentage representing how accurate the predicted result is compared to the real result</returns>
        public decimal computeAccuracy()
        {
            decimal accuracy = 0.0m; // %
            if (PredictionInput?.Count > 0 && PredictionOutput?.Count > 0 && PredictionOutput?.Count == RealResult?.Count)
            {
                // sort to same order
                PredictionOutput?.Sort(DataHandling.sortTime_earliestFirst);
                RealResult?.Sort(DataHandling.sortTime_earliestFirst);

                List<decimal> accuracyPerPrice = new List<decimal>();
                for (int i = 0; i < PredictionOutput?.Count; i++)
                {
                    decimal x = (100.00m / RealResult[i].deltaPrice) * PredictionOutput[i].deltaPrice;
                    accuracyPerPrice.Add(x);
                }
            }
            return accuracy;
        }
    }
    public class PredictedSlopeHistory
    {
        #region Properties
        // internal prediction ID
        [JsonProperty("Id")]
        long Id { get; set; }

        [JsonProperty("Symbol")]
        string Symbol { get; set; }

        [JsonProperty("Model")]
        string Model { get; set; }

        // Input to base the prediction from
        [JsonProperty("PredictedInput")]
        List<PricePointSlope>? PredictionInput { get; set; }
        public List<PricePointSlope>? getPredictionInput
        {
            get
            {
                return PredictionInput;
            }
        }

        // predicted outcome
        [JsonProperty("PredictionOutput")]
        List<PricePointSlope>? PredictionOutput { get; set; }
        public List<PricePointSlope>? getPredictionOutput
        {
            get
            {
                return PredictionOutput;
            }
        }

        // Real outcome
        [JsonProperty("RealResult")]
        List<PricePointSlope>? RealResult { get; set; }
        public List<PricePointSlope>? getRealResult
        {
            get
            {
                return RealResult;
            }
        }

        // bool represents if all the required data is present to do calcs
        [JsonProperty("Complete")]
        bool Complete { get; set; }

        [JsonIgnore]
        public bool isComplete
        {
            get { return Complete; }
        }

        [JsonIgnore]
        public long getId { get { return Id; } }

        [JsonIgnore]
        public string getSymbol { get { return Symbol; } }
        #endregion
        internal void update()
        {
            if (PredictionOutput == null || PredictionInput == null)
                return;
            List<PricePoint> points = DataCache.getAllPointsBetween(Symbol, PredictionOutput.First().openTimeUtc, PredictionOutput.Last().closeTimeUtc);

            // split points into minutebased points
            DateTime startTime = PredictionOutput.First().openTimeUtc;
            DateTime nextTime = startTime.AddMinutes(1);
            List<PricePoint> outp = new List<PricePoint>();
            while (startTime < PredictionOutput.Last().closeTimeUtc)
            {
                List<PricePoint> p1 = points.FindAll(p => DataHandling.AllBetween(p, startTime, nextTime));
                PricePoint p = new PricePoint();

                // Gets high price and low price
                p.highPrice = p.lowPrice = 0.0m;
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


                p1.Sort(DataHandling.sortTime_latestFirst);
                if (p1.Count == 0) {
                    startTime = startTime.AddMinutes(1);
                    nextTime = startTime.AddMinutes(1);
                    continue;
                }
                p.closePrice = p1.First().closePrice;
                p.openPrice = p1.Last().openPrice;
                outp.Add(p);

                startTime = startTime.AddMinutes(1);
                nextTime = startTime.AddMinutes(1);
            }
            RealResult = Convertions.FindAll(outp.ToArray());
            TimeSpan x = PredictionOutput.First().openTimeUtc.Subtract(PredictionOutput.Last().closeTimeUtc);
            if (x.TotalMinutes == 0)
            {
                Complete = true;
            }
        }

        public PredictedSlopeHistory(long id, string symbol, Model model, List<PricePointSlope> input, List<PricePointSlope> output)
        {
            this.Id = id;
            this.Symbol = symbol;
            this.Model = model;
            if (input?.Count > 0 && output?.Count > 0)
            {
                this.PredictionInput = input;
                this.PredictionOutput = output;

                update();
            }
            else
            {
                this.PredictionInput = null;
                this.PredictionOutput = null;
                Complete = false;
            }
        }


        /// <summary>
        /// Calculates the accuracy of the predicted result
        /// </summary>
        /// <returns>A percentage representing how accurate the predicted result is compared to the real result</returns>
        public decimal computeAccuracy()
        {
            decimal accuracy = 0.0m; // %
            if (PredictionInput?.Count > 0 && PredictionOutput?.Count > 0 && PredictionOutput?.Count == RealResult?.Count)
            {
                // sort to same order
                PredictionOutput?.Sort(Convertions.sortTime_oldestFirst);
                RealResult?.Sort(Convertions.sortTime_oldestFirst);

                List<decimal> accuracyPerPrice = new List<decimal>();
                for (int i = 0; i < PredictionOutput?.Count; i++)
                {
                    decimal x = (100.00m / RealResult[i].getDeltaPrice) * PredictionOutput[i].getDeltaPrice;
                    accuracyPerPrice.Add(x);
                }
            }
            return accuracy;
        }
    }
}
