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

            /// <summary>
            /// {0} - Slope Number
            /// {1} - Starting Point X
            /// {2} - Starting Point Y
            /// {3} - Ending Point X
            /// {4} - Ending Point Y
            /// {5} - Gradient
            /// {6} - Weight
            /// </summary>
            public static string slopeFormat = "\"slope\": { \"starting_point\": [{1},{2}], \"ending_point\":  [{3},{4}], \"gradient\": {5}, \"weight\": {6}, \"pattern\": \"{7}\", \"phase\": \"{8}\" }";
            public static string lineSeperator = " ||| ";
        }
        public static string formatBinanceLine(string input)
        {
            string[] inputs = input.Split(',');
            return string.Format(Encoding.formatString, inputs[0], inputs[1], inputs[4], inputs[5], Encoding.lineSeperator);
        }

        /*public static string toBinance
        {
            
        }*/
    }
}
