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
using Newtonsoft.Json.Linq;
using ACE.Trading.Analytics.PredictedHistories;

namespace ACE.Trading.Analytics
{
    public enum AccuracyLevel
    {
        VeryHigh = 90,
        High = 80,
        Medium = 65,
        Marginal = 50,
        Low = 40,
        VeryLow = 30
    }
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
            internal const string DATACACHE_FILENAME = "ACE-PREDICTIONHISTORY.x3";
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

        // Evaluates the last 10 histories and computes an accuracy based on price and fluency comparisions
        public static AccuracyLevel getAccuracyLevel(string symbol)
        {
            List<PredictedSlopeHistory> histories;
            if (findSlopePredictions(symbol, out histories))
            {
                histories.Sort(Convertions.sortTime_latestFirst);
                List<PredictedSlopeHistory> histToEval = new List<PredictedSlopeHistory>();
                int max = 10;
                if (histories.Count > max)
                {
                    for(int i = 0; i < max && max < histories.Count; i++)
                    {
                        if (histories[i].isComplete)
                        {
                            histToEval.Add(histories[i]);
                        }
                        else
                        {
                            max++;
                        }
                    }
                }
                else
                {
                    histToEval = histories;
                }

                double accuracy = 0.0;

                foreach(var hist in histToEval)
                {
                    var mets = hist.getMtetrics;
                    accuracy += (mets.fluency > 80.0) ? mets.accuracy : mets.accuracy / 2;
                }
                accuracy /= histToEval.Count;
                if (accuracy > (int)AccuracyLevel.VeryHigh)
                {
                    return AccuracyLevel.VeryHigh;
                }
                else if (accuracy > (int)AccuracyLevel.High)
                {
                    return AccuracyLevel.High;
                }
                else if (accuracy > (int)AccuracyLevel.Medium)
                {
                    return AccuracyLevel.Medium;
                }
                else if (accuracy > (int)AccuracyLevel.Marginal)
                {
                    return AccuracyLevel.Marginal;
                }
                else if (accuracy > (int)AccuracyLevel.Low)
                {
                    return AccuracyLevel.Low;
                }
                else
                    return AccuracyLevel.VeryLow;
            }
            else return AccuracyLevel.VeryLow;
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
            else if (File.Exists("C:\\Users\\bellramt\\ace\\ACE-PREDICTIONHISTORY.x3"))
            {
                string json = File.ReadAllText("C:\\Users\\bellramt\\ace\\ACE-PREDICTIONHISTORY.x3");
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
            Thread.Sleep(50);
            if (Directory.Exists(Path.GetDirectoryName(Cache.DATACACHE_FILENAME)))
            {
                File.WriteAllText(Cache.DATACACHE_FILENAME, jsonString);
            }
            else
            {
                File.WriteAllText("C:\\Users\\bellramt\\ace\\ACE-PREDICTIONHISTORY.x3", jsonString);
            }
        }
    }
}
