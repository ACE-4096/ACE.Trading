using ACE.Trading.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.OpenAi
{
    public static class Encoding
    {
        #region Constant Formatting Strings
        //public const string formatString = "The unix time this data was collected is {0}. {1} Trades were made, Which resulted in a price change of $ {2}";
        //public const string formatString = "TimeInUTC:  {0} | {1}, Which resulted in a price change of $ {3}";
        public const string formatString = "Time: {0} | Price: {1}";
        
        public const string seperator = "||";
        #endregion
        public enum priceInterval
        {
            OneMinute,
            FiveMinute,
            FifteenMinutes,
            OneHour
        }
        public enum priceType
        {
            DeltaPrice,
            AvgPrice
        }


        public static string Encode(string[][] strings)
        {
            string output = seperator;
            foreach (string[] s in strings)
            {
                if (s.Length != 2) continue;
                output += String.Format("Time: {0} | Price: {1}{2}", s[0], s[1], seperator);
            }
            return output;
        }
        public static string[][] Decode(string input)
        {
            string[] strs = input.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            if (strs == null)
            {
                Debug.WriteLine("Invalid input to: decodePrediction, NO DATA");
                return null;
            }
            List<string[]> output = new List<string[]>();
            foreach (string str in strs)
            {
                var trimmedStr = str.Trim();
                int firstIndex = trimmedStr.IndexOf(' ');

                if (firstIndex == -1)
                    continue;

                var sub1 = trimmedStr.Substring(firstIndex);

                int secondIndex = sub1.IndexOf('|');
                if (secondIndex <= 0)
                    continue;

                // TIME String
                var timeStr = sub1.Substring(0, secondIndex - 1);

                var sub2 = timeStr.Substring(secondIndex + 2);

                int thirdIndex = sub2.IndexOf(' ');
                if (thirdIndex == -1)
                    continue;

                var priceStr = sub2.Substring(thirdIndex + 1);
                output.Add(new string[] { timeStr, priceStr });
            }
            return output.ToArray();
        }

        public static string formatDataSets(string[] lines)
        {
            string output = seperator;
            foreach (string line in lines)
            {
                output += line + seperator;
            }
            return output;
        }

        public static bool translateBinanceDataToDeltaData(string inputFilename, string outputFilename)
        {
            bool success = false;

            // if vals are null or empty dont procced
            if (inputFilename == null || outputFilename == null)
                return success;
            if (inputFilename == "" || outputFilename == "")
                return success;

            string[] lines = File.ReadAllLines(inputFilename, System.Text.Encoding.UTF8);
            List<string> output = new List<string>();
            //string outp = seperator;
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                if (data.Length > 0)
                {
                    decimal openPrice = decimal.Parse(data[1]);
                    decimal closePrice = decimal.Parse(data[4]);
                    decimal deltaPrice = openPrice - closePrice;

                    string formatted = string.Format(formatString, data[0].Trim(), data[8].Trim(), deltaPrice.ToString().Trim());

                    output.Add(formatted);
                }
            }
            // outp.Substring(0, outp.Length - seperator.Length);
            // write data to file
            System.IO.File.WriteAllText(outputFilename, formatDataSets(output.ToArray()));
            return success;
        }

        /// <summary>
        /// Extracts the data from the datacache and presents it as a single prompt
        /// </summary>
        /// <param name="symbol">Which symbol to collect data for</param>
        /// <param name="interval">the time between price points</param>
        /// <param name="type">which price type to use</param>
        /// <param name="hours">how many hours woth of data to use</param>
        /// <param name="output">The output prompt string</param>
        /// <returns></returns>
        public static bool ExtractPromptFromDataCache(string symbol, priceInterval interval, priceType type, int hours, out string output)
        {

            output = Encoding.seperator;
            var sd = DataCache.GetSymbolData(symbol);


            if (sd == null)
                return false;




            sd.getPriceHistory.Sort(DataHandling.sortTime_latestFirst);
            var sortedList = sd.getPriceHistory;

            DateTime end = DateTime.UtcNow;
            DateTime start = end.AddHours(-hours);

            // Converts inputs enum(0-3) to minutes (1,5,15,60)

            int intervalInMinutes = 0;
            switch (interval)
            {
                case priceInterval.OneMinute:
                    intervalInMinutes = 1; break;
                case priceInterval.FiveMinute:
                    intervalInMinutes = 5; break;
                case priceInterval.FifteenMinutes:
                    intervalInMinutes = 15; break;
                case priceInterval.OneHour:
                    intervalInMinutes = 60; break;
            }
            if (intervalInMinutes == 0) return false;

            // gets data from timeframe
            List<PricePoint> list = sortedList.FindAll(p => p.timeUtc >= start && p.timeUtc < end);
            // sorts to oldest first
            list.Sort(DataHandling.sortTime_latestFirst);
            if (list.Count == 0) return false;


            int numOfPricePoints = (hours * 60) / intervalInMinutes;
            for (int i = 0; i < numOfPricePoints; i++)
            {
                PricePoint[] pricePoints = list.FindAll(p => ((p.timeUtc.CompareTo(start) > 0) && p.timeUtc.CompareTo(start.AddMinutes(intervalInMinutes)) < 0)).ToArray();

                if (pricePoints.Length == 0) continue;
                decimal priceAvg = 0.0m;

                foreach (var pricePoint in pricePoints)
                {
                    priceAvg += type == priceType.DeltaPrice ? pricePoint.deltaPrice : pricePoint.avgPrice;
                }

                priceAvg /= pricePoints.Length;
                output += string.Format(Encoding.formatString, start.ToString(), priceAvg, Encoding.seperator);
                start = start.AddMinutes(intervalInMinutes);
            }

            return true;
        }
    }
}