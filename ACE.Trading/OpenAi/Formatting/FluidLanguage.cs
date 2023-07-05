using ACE.Trading.Analytics.Slopes;
using ACE.Trading.Data;
using Binance.Net.Objects.Models.Spot.Mining;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.OpenAi.Formatting
{

    public static class FluidLanguage
    {
        public static string formatString = "During the minute proceeding the unix time of {0}, The open price of was ${1}, Close Time (Unix): {2}, Close Price: {3}{4}";
        public static string formatSlopeString = "A trading slope had an open time of {0} Unix Time, The open price was ${1}. The gradient of the slope was {2}$/minute, The duration of the slope was {3} minutes. The total price change of the slope period was ${4}. The slope finished at the time of {5} Unix time, with a price of ${6}{7}";
        public static string lineSeperator = " ||| ";
        public static string formatBinanceLine(string input)
        {
            string[] inputs = input.Split(',');
            return string.Format(formatString, inputs[0], inputs[1], inputs[4], inputs[5], lineSeperator);
        }
        public static string formatBinanceLine(PricePointSlope input)
        {
            return string.Format(formatSlopeString, input.OpenTimeUnix, input.getOpenPrice, input.getGradient, input.numOfPricePoints, input.getDeltaPrice, input.CloseTimeUnix, input.getClosePrice, lineSeperator);
        }
        public static List<PricePointSlope> Decode(string input)
        {

            List<PricePointSlope> output = new List<PricePointSlope>();
            try
            {
                if (!input.Contains(lineSeperator))
                {
                    Debug.WriteLine("Invalid INPUT");
                    return output;
                }
                string[] completionSlopes = input.Split(lineSeperator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in completionSlopes)
                {
                    //OpenTime
                    int firstIndex = formatSlopeString.IndexOf("{0}");
                    int secondIndex = s.IndexOf("Unix Time") - 1;
                    if (s.Length <= secondIndex || secondIndex==firstIndex)
                        continue;
                    long openTime;
                    if (!long.TryParse(s.Substring(firstIndex, secondIndex - firstIndex), out openTime))
                    {
                        continue;
                    }

                    // openPrice
                    firstIndex = s.IndexOf('$') + 1;
                    secondIndex = s.IndexOf(". T") - 1;
                    if (s.Length <= firstIndex || s.Length <= secondIndex || firstIndex < 5)
                        continue;
                    string tmp = s.Substring(firstIndex, secondIndex - firstIndex);
                    decimal openPrice;
                    if (!decimal.TryParse(tmp, out openPrice)) continue;


                    // gradient
                    // ". The gradient of the slope was " = 32 chars
                    if (s.Length <= secondIndex + 32)
                        continue;

                    string newString = s.Substring(secondIndex + 33);
                    int thirdIndex = newString.IndexOf('$');
                    if (thirdIndex == -1 || newString.Length <= thirdIndex)
                        continue;
                    decimal gradient;
                    if (!decimal.TryParse(newString.Substring(0, thirdIndex), out gradient)) continue;

                    // duration
                    // ", The duration of the slope was " - 32
                    int fourthIndex = newString.IndexOf(',');
                    if (fourthIndex == -1 || newString.Length <= fourthIndex+32)
                        continue;
                    newString = newString.Substring(fourthIndex + 32);
                    int fifthIndex = newString.IndexOf('m') - 1;
                    if (fifthIndex == -1 || newString.Length <= fifthIndex)
                        continue;
                    int duration;
                    if (!int.TryParse(newString.Substring(0, fifthIndex), out duration)) continue;

                    // delta price
                    newString.Substring(fifthIndex);
                    int sixthIndex = newString.IndexOf('$') + 1;
                    if (sixthIndex == -1 || newString.Length <= sixthIndex)
                        continue;
                    newString = newString.Substring(sixthIndex);
                    int seventhIndex = newString.IndexOf(". T") - 1;
                    if (seventhIndex == -1 || newString.Length <= seventhIndex + 36)
                        continue;
                    decimal deltaPrice;
                    if (!decimal.TryParse(newString.Substring(0, seventhIndex), out deltaPrice)) continue;

                    //close time
                    newString = newString.Substring(seventhIndex + 36);
                    int eighthIndex = newString.IndexOf(" Unix time");
                    if (eighthIndex == -1 || newString.Length <= eighthIndex)
                        continue;
                    long closeTime;
                    if(!long.TryParse(newString.Substring(0, eighthIndex), out closeTime)) continue;

                    //close price
                    int ninethIndex = newString.IndexOf('$') + 1;
                    if (ninethIndex == -1 || newString.Length <= ninethIndex)
                        continue;
                    decimal closePrice;
                    if (!decimal.TryParse(newString.Substring(ninethIndex), out closePrice)) continue;


                    //Convert to price points
                    List<PricePoint> points = new List<PricePoint>();
                    DateTime startTime = DateTimeOffset.FromUnixTimeMilliseconds(openTime).DateTime.AddDays(1.575);                    //DateTime closeTime = DateTimeOffset.FromUnixTimeMilliseconds(closeTime).DateTime;
                    for (int k = 0; k < duration; k++)
                    {
                        decimal pointOpenPrice = openPrice + (gradient * k);
                        decimal pointClosePrice = pointOpenPrice + (gradient * (k+1));
                        decimal high = gradient > 0 ? pointClosePrice : pointOpenPrice;
                        decimal low = gradient < 0 ? pointClosePrice : pointOpenPrice;
                        points.Add(new PricePoint()
                        {
                            openPrice = pointOpenPrice,
                            closePrice = pointClosePrice,
                            deltaPrice = gradient,
                            highPrice = high,
                            lowPrice = low,
                            timeUtc = startTime.AddMinutes(k)
                        });
                    }
                    output.Add(new PricePointSlope(points));


                }
            }catch(Exception e)
            {
                Debug.Write(e.Message + " ||| " +e.StackTrace);
            }

            return output;
        }
        /*private static string[] decodeFormattedString (string input, string format)
        {
            int number = format.Count(c => c == '{'); string substring;
            for (int i = 0; i < number; i++)
            {
                int firstFormatIndex = format.IndexOf('{'); 
                int firstFormatIndex = format.IndexOf('{');
                substring = input.Substring(firstIndex, input.);
            }
            
        }*/
    }
}
