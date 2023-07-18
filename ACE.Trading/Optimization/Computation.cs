using ACE.Trading.Analytics;
using ACE.Trading.Data;
using ACE.Trading.Data.Collection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.Trading.Analytics.PredictedHistories;

namespace ACE.Trading.Optimization
{
    /// <summary>
    /// Computes metrics for a given prediction
    /// </summary>
    public static class Computation
    {
        public async static Task<PredictedSlopeHistory> Evaluate(PredictedSlopeHistory input)
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

            // removed duplicates from prediction
            if (predictedPoints.Count != mins)
            {
                for (int i = 0; i < mins; i++)
                {
                    var x = predictedPoints.FindAll(p => DataHandling.findByTime(p, start.AddMinutes(i)));
                    if (x == null)
                    {
                        continue; // BAD
                    }
                    else if (x.Count > 1)
                    {
                        foreach (var b in x)
                        {
                            predictedPoints.Remove(b);
                        }
                        predictedPoints.Add(x[0]);
                    }
                }
            }


            // compares points to form a metric 
            Metrics metric = new Metrics();

            for (int i = 0; i < predictedPoints.Count; i++)
            {
                var comparedPoint = realPoints.Find(p => DataHandling.findByTime(p, predictedPoints[i].timeUtc));
                if (comparedPoint == null)
                {
                    return input;
                }
                
                if (i != 0) {
                    // Gradient Deviance
                    var prevComparedPoint = realPoints.Find(p => DataHandling.findByTime(p, predictedPoints[i-1].timeUtc));
                    if (prevComparedPoint == null)
                    {
                        return input;
                    }

                    decimal realGradient = comparedPoint.avgPrice - prevComparedPoint.avgPrice;
                    decimal fakeGradient = predictedPoints[i - 1].avgPrice - predictedPoints[i].avgPrice;
                    metric.totalGradientDeviance += (double)Math.Abs(realGradient - fakeGradient);


                    // Fluency
                    // Checks previous price point is cronologically sequentual
                    if (DataHandling.findByTime(predictedPoints[i - 1], predictedPoints[i].timeUtc.AddMinutes(-1)))
                    {
                        metric.fluency += (100.0 / predictedPoints.Count);
                    }
                }
                
                // Accuracy
                metric.accuracy += (double)((100.00m / comparedPoint.avgPrice) * predictedPoints[i].avgPrice);
                
                // Price Deviance
                metric.totalPriceDeviance += (double)Math.Abs(comparedPoint.avgPrice - predictedPoints[i].avgPrice);

            }
            metric.accuracy /= predictedPoints.Count;

            // Avg Price deviance
            metric.priceDeviance = metric.totalPriceDeviance / predictedPoints.Count;

            //  Avg Gradient deviance
            metric.gradientDeviance = metric.totalGradientDeviance / predictedPoints.Count;


            metric.Lock();
            input.setMtetrics = metric;

            return input;
        }
    }
}
