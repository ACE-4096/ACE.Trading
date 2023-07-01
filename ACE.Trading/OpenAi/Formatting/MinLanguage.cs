using ACE.Trading.Analytics.Slopes;
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
            public static string formatSlopeString = "Open Time (Unix): {0}, Open Price: {1}, Slope Gradient: {2}, Duration: {3}, Price Change: {4}, Close Time (Unix): {5}, Close Price: {6}{7}";
            public static string lineSeperator = " ||| ";
        }
        public static string formatBinanceLine(string input)
        {
            string[] inputs = input.Split(',');
            return string.Format(Encoding.formatString, inputs[0], inputs[1], inputs[4], inputs[5], Encoding.lineSeperator);
        }

        public static string formatBinanceLine(PricePointSlope input)
        {
            return string.Format(Encoding.formatSlopeString, input.getOpenTimeUtc.ToUnixTime(), input.getOpenPrice, input.getGradient, input.numOfPricePoints, input.getDeltaPrice, input.getCloseTimeUtc.ToUnixTime(), input.getClosePrice, Encoding.lineSeperator);
        }
    }
}
