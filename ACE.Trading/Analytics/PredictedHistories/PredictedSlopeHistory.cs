using ACE.Trading.Analytics.Slopes;
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


        [JsonProperty("Metrics")]
        Optimization.Metrics metrics { get; set; }
        [JsonIgnore]
        public Optimization.Metrics getMtetrics
        {
            get
            {
                return metrics;
            }
        }
        [JsonIgnore]
        public Optimization.Metrics setMtetrics
        {
            set
            {
                if (metrics == null && value.isLocked) metrics = value;
            }
        }

        [JsonIgnore]
        public bool isComplete
        {
            get { return metrics == null && metrics.isLocked; }
        }

        [JsonIgnore]
        public long getId { get { return Id; } }

        [JsonIgnore]
        public string getSymbol { get { return Symbol; } }
        #endregion
        internal async void update()
        {
            if (PredictionOutput == null || PredictionInput == null)
                return;
            //List<PricePoint> points = DataCache.getAllPointsBetween(Symbol, PredictionOutput.First().openTimeUtc, PredictionOutput.Last().closeTimeUtc);
            List<PricePoint> points = await PricePoint.FromBinanceApi(Symbol, PredictionOutput.First().openTimeUtc, PredictionOutput.Last().closeTimeUtc);
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
                if (p1.Count == 0)
                {
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
            }
        }

    }
}
