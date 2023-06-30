using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ACE.Trading.Data;

namespace ACE.Trading.Analytics.Slopes
{
    public class BinanceConvertions
    {
        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp).DateTime; ;
        }

        public static bool readToPricePointArray(string filename, out PricePoint[] points)
        {
            points = new PricePoint[] { };
            if (!File.Exists(filename))
                return false;

            List<PricePoint> output = new List<PricePoint>();
            string[] inputLines = File.ReadAllLines(filename);
            foreach (string line in inputLines)
            {
                string[] inputs = line.Split(',');
                if (inputs.Length < 7)
                    continue;
                PricePoint p = new PricePoint()
                {
                    timeUtc = UnixTimeStampToDateTime(long.Parse(inputs[0])),
                    openPrice = decimal.Parse(inputs[1]),
                    highPrice = decimal.Parse(inputs[2]),
                    lowPrice = decimal.Parse(inputs[3]),
                    closePrice = decimal.Parse(inputs[4])
                };
                p.deltaPrice = output.Count > 0 ? p.avgPrice - output.Last().avgPrice : 0;
                output.Add(p);

            } 
            points = output.ToArray();
            return output.Count > 0;
        }

    }

    public static class DateTimeExtensions
    {
        // Convert datetime to UNIX time
        public static long ToUnixTime(this DateTime dateTime)
        {
            var timeSpan = (dateTime - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
    }
}
