using ACE.Trading.Analytics;
using ACE.Trading.Data;
using ACE.Trading.Data.Collection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Optimisation
{
    /// <summary>
    /// Computes metrics for a given prediction
    /// </summary>
    public static class Computation
    {
        public async static PredictedSlopeHistory Evaluate(PredictedSlopeHistory input)
        {
            if (input == null)
                return null;

            if (input.getRealResult == null || input.getRealResult.Count == 0)
                return null;
            if (input.getPredictionOutput == null || input.getPredictionOutput.Count == 0)
                return null;

            DateTime start = input.getPredictionOutput.First().openTimeUtc, finish = input.getPredictionOutput.Last().closeTimeUtc;
            BinanceHandler bh = new BinanceHandler();
            var realData = await bh.getMarketData(input.getSymbol, Binance.Net.Enums.KlineInterval.OneMinute, start, finish);


            // creates missing points 
            var realPoints = PricePoint.FromBinanceKline(realData.Data);
            List<PricePoint> predictedPoints = new List<PricePoint>();
            foreach(var s in input.getPredictionOutput)
            {
                List<PricePoint> p1 = s.getSlopePoints;
                TimeSpan slopePeriod = p1.First().timeUtc.Subtract(p1.Last().timeUtc);
                for(int i = 0; i < slopePeriod.TotalMinutes; i++)
                {
                    var p2 = p1.Find(point => point.timeUtc == p1.First().timeUtc.AddMinutes(i));
                    if (p2 == null)
                    {
                        p2 = new PricePoint() { lastKnownPrice = s.getConstant + (s.getGradient * i), timeUtc = p1.First().timeUtc.AddMinutes(i) };
                        p1.Add(p2);
                    }
                }
            }

            // removed duplicates from real
            var mins = System.Math.Abs(start.Subtract(finish).TotalMinutes);
            if (realPoints.Count != mins)
            {
                for (int i = 0; i < mins; i++)
                {
                    var x = realPoints.FindAll(p => DataHandling.findByTime(p, start.AddMinutes(i)));
                    if (x == null)
                    {
                        continue; // BAD
                    }else if (x.Count > 1)
                    {
                        foreach (var b in x)
                        {
                            realPoints.Remove(b);
                        }
                        realPoints.Add(x[0]);
                    }
                }
            }


            // compares points to form a metric 
            Metrics metric = new Metrics();

            

            return input;
        }
    }
}
