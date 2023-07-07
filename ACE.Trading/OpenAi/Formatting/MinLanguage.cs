using ACE.Trading.Analytics.Slopes;
using ACE.Trading.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.OpenAi.Formatting
{
    internal class MinLanguage
    {
        public static class Encoding
        {
            public static string formatString = "Open Time (Unix): {0}, Open Price: {1}, Close Time (Unix): {2}, Close Price: {3}{4}";

            /// <summary>
            /// {0} - Slope Number
            /// {1} - Starting Point X
            /// {2} - Starting Point Y
            /// {3} - Ending Point X
            /// {4} - Ending Point Y
            /// {5} - Gradient
            /// {6} - Weight
            /// </summary>
            public static string slopeFormat = "{ 'starting_point': [{0}, {1}], \"ending_point\":  [{2}, {3}], 'gradient': {4}, 'weight': {5} }";
            public static string slopeSeperator = " ||| ";
        }
        /// <summary>
        /// Prompt Builder
        /// </summary>
        /// <param name="inputSlopes"></param>
        /// <returns></returns>
        public static string Encode(List<PricePointSlope>inputSlopes)
        {

            string output = "";
            foreach (var slope in inputSlopes){
                output += $"{{ 'starting_point': [{slope.OpenTimeUnix}, {slope.getOpenPrice}], 'ending_point':  [{slope.CloseTimeUnix}, {slope.getClosePrice}], 'gradient': {slope.getGradient}, 'weight': {slope.getVolume} }},";
            }
            if (output.Length > Encoding.slopeFormat.Length)
            {
                output = output.Substring(0, output.Length-1);
            }
            return output;
        }

        public static List<PricePointSlope> Decode(string input)
        {
            List<PricePointSlope> slopes = new List<PricePointSlope>();
            foreach (var slopeLine in input.Split(Encoding.slopeSeperator, StringSplitOptions.RemoveEmptyEntries))
            {
                // Start Time
                #region
                string line = slopeLine.Replace("{ \"starting_point\": [", "");
                int firstIndex = line.IndexOf(',');
                if (firstIndex == -1 || line.Length <= firstIndex)
                    continue;
                string unixStartTime = line.Substring(0, firstIndex);
                long startTime;
                if (!long.TryParse(unixStartTime, out startTime)) 
                    continue;
                #endregion
                // Start Price
                #region
                firstIndex += 1;
                int secondIndex = line.IndexOf(']');
                if (secondIndex == -1 || line.Length <= secondIndex - firstIndex) 
                    continue;
                string startPrice = line.Substring(firstIndex, secondIndex - firstIndex);
                line = line.Substring(secondIndex);
                decimal openPrice;
                if (!decimal.TryParse(startPrice, out openPrice)) 
                    continue;
                #endregion
                // End Time
                #region 
                firstIndex = line.IndexOf('[') + 1;
                if (firstIndex == -1 || line.Length <= firstIndex) 
                    continue;
                secondIndex = line.IndexOf(',');
                if (secondIndex == -1 || line.Length <= secondIndex - firstIndex) 
                    continue;
                string unixEndTime = line.Substring(firstIndex, secondIndex - firstIndex);
                line = line.Substring(secondIndex+1);
                long endTime;
                if (!long.TryParse(unixEndTime, out endTime)) 
                    continue;
                #endregion
                // End Price
                #region 
                firstIndex = line.IndexOf(']');
                if (firstIndex == -1 || line.Length <= firstIndex) 
                    continue;
                string endPrice = line.Substring(0, firstIndex);
                decimal closePrice;
                if (!decimal.TryParse(endPrice, out closePrice)) 
                    continue;
                #endregion
                // Gradient
                #region 
                line = line.Substring(firstIndex);
                line = line.Replace("], \"gradient\": ", "");
                firstIndex = line.IndexOf(',');
                if (firstIndex == -1 || line.Length <= firstIndex) 
                    continue;
                string gradientStr = line.Substring(0, firstIndex);
                decimal gradient;
                if (!decimal.TryParse(gradientStr, out gradient)) 
                    continue;
                #endregion
                // Weight / Volume
                #region 
                line = line.Substring(firstIndex + 1);
                firstIndex = line.IndexOf(':');
                if (firstIndex == -1 || line.Length <= firstIndex) 
                    continue;
                secondIndex = line.IndexOf('}');
                if (secondIndex == -1 || line.Length <= secondIndex - firstIndex) 
                    continue;
                string weight = line.Substring(firstIndex, secondIndex - firstIndex);
                decimal volume;
                if (!decimal.TryParse(weight, out volume)) 
                    continue;
                #endregion 

                // Convert to slope
                #region 

                PricePoint start = new PricePoint() { openPrice = openPrice, unixTimeUtc = startTime, volume = volume / 2 }; 
                PricePoint end = new PricePoint() { closePrice = closePrice, unixTimeUtc = endTime, volume = volume / 2 };
                PricePointSlope slope = new PricePointSlope(new List<PricePoint>(new[] { start, end }));
                slopes.Add(slope);
                #endregion 
            }
            return slopes;
        }
    }
}
