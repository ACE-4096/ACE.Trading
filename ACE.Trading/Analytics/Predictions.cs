using Newtonsoft.Json;
using OpenAI_API.Models;
using ACE.Trading.Data;
using ACE.Trading.Data.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Analytics
{
    internal class Predictions
    {
        private static Cache cache = new Cache();
        private class Cache
        {
            [JsonProperty("PredictionHistory")]
            internal List<PredictedHistory> data = new List<PredictedHistory>();

            [JsonIgnore]
            internal const string DATACACHE_FILENAME = "C:\\Users\\Toby\\.ace\\ACE-PREDICTIONHISTORY.x3";

        }
        public static void addPrediction(string symbol, Model model, List<Price> Input, List<Price> Output)
        {
            long id = cache.data.Count;
            PredictedHistory ph = new PredictedHistory(id, symbol, model, Input, Output);
            cache.data.Add(ph);
            Save();
        }
        
        public static PredictedHistory findPrediction(long id)
        {
            return cache.data.Find(ph => ph.getId == id);
        }

        public static bool findPredictions(string symbol, out List<PredictedHistory> histories)
        {
            histories = cache.data.FindAll(ph => ph.getSymbol == symbol);
            return histories != null && histories?.Count > 0;
        }

        public static decimal ComputeAverageAccuracy()
        {
            int num = 0;
            decimal sum = 0.0m;
            foreach (var item in cache.data)
            {
                if (!item.isComplete)
                    continue;

                num++;
                sum += item.computeAccuracy();

            }
            return sum / num;
        }

        public static void Load()
        {
            if (File.Exists(Cache.DATACACHE_FILENAME) && cache.data.Count == 0)
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

    internal class PredictedHistory
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
        List<Price>? PredictionInput { get; set; }

        // predicted outcome
        [JsonProperty("PredictionOutput")]
        List<Price>? PredictionOutput { get; set; }

        // Real outcome
        [JsonProperty("RealResult")]
        List<Price>? RealResult { get; set; }

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

        public PredictedHistory(long id, string symbol, Model model, List<Price> input, List<Price> output)
        {
            this.Id = id;
            this.Symbol = symbol;
            this.Model = model;
            if (input.Count > 0 && output.Count > 0)
            {
                this.PredictionInput = input;
                this.PredictionOutput = output; 
                
                RealResult = new List<Price>();
                // Find bars from datacache that match predicted output timestamps
                foreach (var item in output)
                {
                    List<Price> p = DataCache.findAllByTime(symbol, item.timeUtc);
                    if (p != null && p?.Count > 0)
                    {
                        decimal avg = 0.0m;
                        foreach(Price price in p)
                        {
                            avg += price.getDeltaPrice;
                        }
                        avg /= p.Count;
                        Price price1 = new Price { _deltaPrice = avg, timeUtc = item.timeUtc };
                        RealResult.Add(price1);
                    }
                }
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
                    decimal x = (100.00m / RealResult[i].getDeltaPrice) * PredictionOutput[i].getDeltaPrice;
                    accuracyPerPrice.Add(x);
                }
            }
            return accuracy;
        }
    }
}
