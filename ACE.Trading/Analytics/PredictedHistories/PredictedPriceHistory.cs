using ACE.Trading.Data;
using Newtonsoft.Json;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Analytics.PredictedHistories
{
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

        internal async void update()
        {
            RealResult = new List<PricePoint>();
            // Find bars from datacache that match predicted output timestamps
            foreach (var item in PredictionOutput)
            {
                List<PricePoint> p = await PricePoint.FromBinanceApi(Symbol, item.timeUtc, item.timeUtc);
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
}
