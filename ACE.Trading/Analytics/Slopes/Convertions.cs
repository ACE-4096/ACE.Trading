using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.Trading.Data;

namespace ACE.Trading.Analytics.Slopes
{
    public class Convertions
    {
        public static List<PricePointSlope> FindAll(PricePoint[] pricePoints)
        {

            if (pricePoints.Length == 0)
                return null;
            var tmp = pricePoints.ToList();
            tmp.Sort(sortTime_oldestFirst);
            pricePoints = tmp.ToArray();
            List<PricePointSlope> slopeList = new List<PricePointSlope>();
            PricePointSlope currentSlope = new PricePointSlope(new List<PricePoint>(new[] { pricePoints[0], pricePoints[1] }));

            //currentSlopePricePoints.Add(pricePoints[0]);
            for (int i = 2; i < pricePoints.Length; i++)
            {
                if (currentSlope.getGradient > 0) // ascending
                {
                    if (pricePoints[i].avgPrice > currentSlope.getLatestPoint.avgPrice ||
                        pricePoints[i].highPrice > currentSlope.getLatestPoint.highPrice)
                    {
                        currentSlope.AddPoint(pricePoints[i]);
                    }
                    // if proceeding price point follows pattern ignore the deviation
                    else if (pricePoints.Length > i + 1)
                    {
                        if (pricePoints[i + 1].avgPrice > currentSlope.getLatestPoint.avgPrice ||
                        pricePoints[i + 1].highPrice > currentSlope.getLatestPoint.highPrice)
                        {
                            currentSlope.AddPoint(pricePoints[i]);
                        }
                        else
                        {
                            slopeList.Add(currentSlope);
                            currentSlope = new PricePointSlope(pricePoints[i]);
                        }
                    }
                    else
                    {
                        slopeList.Add(currentSlope);
                        break;
                    }

                }
                else if (currentSlope.getGradient == 0)
                {
                    //neutral
                    currentSlope.AddPoint(pricePoints[i]);
                }
                else // decending 
                {
                    if (pricePoints[i].avgPrice < currentSlope.getLatestPoint.avgPrice ||
                        pricePoints[i].lowPrice < currentSlope.getLatestPoint.lowPrice)
                    {
                        currentSlope.AddPoint(pricePoints[i]);
                    }
                    // if proceeding price point follows pattern ignore the deviation
                    else if (pricePoints.Length > i + 1)
                    {
                        if (pricePoints[i + 1].avgPrice < currentSlope.getLatestPoint.avgPrice ||
                        pricePoints[i + 1].lowPrice < currentSlope.getLatestPoint.lowPrice)
                        {
                            currentSlope.AddPoint(pricePoints[i]);
                        }
                        else
                        {
                            slopeList.Add(currentSlope);
                            currentSlope = new PricePointSlope(pricePoints[i]);
                        }
                    }
                    else
                    {
                        slopeList.Add(currentSlope);
                        break;
                    }
                }
            }

            return slopeList;
        }

        public static List<PricePointSlope> FindAllV2(PricePoint[] pricePoints)
        {
            return FindAllV2(pricePoints, 3);
        }
        public static List<PricePointSlope> FindAllV2(PricePoint[] pricePoints, int tollerance)
        {

            if (pricePoints.Length == 0)
                return null;
            var tmp = pricePoints.ToList();
            tmp.Sort(sortTime_oldestFirst);
            pricePoints = tmp.ToArray();
            List<PricePointSlope> slopeList = new List<PricePointSlope>();
            PricePointSlope currentSlope = new PricePointSlope(new List<PricePoint>(new[] { pricePoints[0], pricePoints[1] }));

            //currentSlopePricePoints.Add(pricePoints[0]);
            for (int i = 2; i < pricePoints.Length; i++)
            {
                if (currentSlope.getGradient > 0) // ascending
                {
                    if (pricePoints[i].avgPrice > currentSlope.getLatestPoint.avgPrice ||
                        pricePoints[i].highPrice > currentSlope.getLatestPoint.highPrice)
                    {
                        currentSlope.AddPoint(pricePoints[i]);
                    }
                    // if proceeding price point follows pattern ignore the deviation
                    else if (pricePoints.Length > i + tollerance)
                    {
                        if (pricePoints[i + tollerance].avgPrice > currentSlope.getLatestPoint.avgPrice ||
                        pricePoints[i + tollerance].highPrice > currentSlope.getLatestPoint.highPrice)
                        {
                            currentSlope.AddPoint(pricePoints[i]);
                        }
                        else
                        {
                            slopeList.Add(currentSlope);
                            currentSlope = new PricePointSlope(pricePoints[i]);
                        }
                    }
                    else
                    {
                        slopeList.Add(currentSlope);
                        break;
                    }

                }
                else if (currentSlope.getGradient == 0)
                {
                    //neutral
                    currentSlope.AddPoint(pricePoints[i]);
                }
                else // decending 
                {
                    if (pricePoints[i].avgPrice < currentSlope.getLatestPoint.avgPrice ||
                        pricePoints[i].lowPrice < currentSlope.getLatestPoint.lowPrice)
                    {
                        currentSlope.AddPoint(pricePoints[i]);
                    }
                    // if proceeding price point follows pattern ignore the deviation
                    else if (pricePoints.Length > i + tollerance)
                    {
                        if (pricePoints[i + tollerance].avgPrice < currentSlope.getLatestPoint.avgPrice ||
                        pricePoints[i + tollerance].lowPrice < currentSlope.getLatestPoint.lowPrice)
                        {
                            currentSlope.AddPoint(pricePoints[i]);
                        }
                        else
                        {
                            slopeList.Add(currentSlope);
                            currentSlope = new PricePointSlope(pricePoints[i]);
                        }
                    }
                    else
                    {
                        slopeList.Add(currentSlope);
                        break;
                    }
                }
            }

            return slopeList;
        }
        public static int sortTime_oldestFirst(PricePoint x, PricePoint y)
        {
            if (x.timeUtc > y.timeUtc)
                return -1;
            if (x.timeUtc < y.timeUtc)
                return +1;
            return 0;
        }
        public static int sortTime_latestFirst(PricePoint x, PricePoint y)
        {
            if (x.timeUtc > y.timeUtc)
                return -1;
            if (x.timeUtc < y.timeUtc)
                return +1;
            return 0;
        }
        public static int sortTime_oldestFirst(PricePointSlope x, PricePointSlope y)
        {
            if (x.openTimeUtc > y.openTimeUtc)
                return -1;
            if (x.openTimeUtc < y.openTimeUtc)
                return +1;
            return 0;
        }
        public static int sortTime_latestFirst(PricePointSlope x, PricePointSlope y)
        {
            if (x.openTimeUtc > y.openTimeUtc)
                return -1;
            if (x.openTimeUtc < y.openTimeUtc)
                return +1;
            return 0;
        }
    }
}
