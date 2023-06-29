using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using ACE.Sandbox.BinanceConvertions;
using ACE.Trading.Analytics.Slopes;
using ACE.Trading.Data;

namespace ACE.Trading.Analytics.Slopes
{


    public class Encoding{
        public static string formatString = "Open Time (Unix): {0}, Open Price: {1}, Close Time (Unix): {2}, Close Price: {3}{4}";
        public static string formatSlopeString = "Open Time (Unix): {0}, Open Price: {1}, Slope Gradient: {2}, Duration: {3}, Price Change: {4}, Close Time (Unix): {5}, Close Price: {6}{7}";
        public static string lineSeperator = " ||| ";
    }

    public static class BinanceToTraining
    {

        public static TrainingData convert(string filename, int promptInputs, int completionOutputs)
        {
            TrainingData td = new TrainingData();
            string[] inLines = File.ReadAllLines(filename);
            for (int i = 0; i < (inLines.Length -(promptInputs + completionOutputs)); i += promptInputs + completionOutputs)
            {
                string prompt = "", completion = "";
                for (int j = 0; j < promptInputs; j++)
                {
                    prompt += Encoding.lineSeperator + formatBinanceLine(inLines[i + j]);
                }
                for (int k = 0; k < completionOutputs; k++)
                {
                    completion += formatBinanceLine(inLines[i + promptInputs + k]);
                }
                td.Add(prompt, completion);
            }
            return td;
        }

        public static TrainingData convertSlopes(string filename, int numOfSlopesPerPrompt, int numOfSlopesPerCompletion)
        {
            PricePoint[] points;
            List<PricePointSlope> slopes = new List<PricePointSlope>();
            if (BinanceConvertions.readToPricePointArray(filename, out points))
            {
                slopes = Convertions.FindAll(points);
            }
            if (slopes == null || slopes.Count == 0)
                return null;

            TrainingData td = new TrainingData();


            for (int i = 0; i < (slopes.Count - (numOfSlopesPerPrompt + numOfSlopesPerCompletion)); i += numOfSlopesPerPrompt + numOfSlopesPerCompletion)
            {
                string prompt = "", completion = "";
                for (int j = 0; j < numOfSlopesPerPrompt; j++)
                {
                    prompt += Encoding.lineSeperator + formatBinanceLine(slopes[i + j]);
                }
                for (int k = 0; k < numOfSlopesPerCompletion; k++)
                {
                    completion += formatBinanceLine(slopes[i + numOfSlopesPerPrompt + k]);
                }
                td.Add(prompt, completion);
            }
            return td;
        }

        public static void convert(string binanceDataFilename, string ou, int promptInputs, int completionOutputs)
        {

        }

        public static string convertToString(string filename, int promptInputs, int completionOutputs)
        {
            return convert(filename, promptInputs, completionOutputs).ToString();
        }

        private static string formatBinanceLine(string input){
            string[] inputs = input.Split(',');
            return string.Format(Encoding.formatString, inputs[0], inputs[1], inputs[4], inputs[5], Encoding.lineSeperator);
        }

        private static string formatBinanceLine(PricePointSlope input){
            return string.Format(Encoding.formatSlopeString, input.getOpenTimeUtc.ToUnixTime(), input.getOpenPrice, input.getGradient, input.numOfPricePoints, input.getDeltaPrice,input.getCloseTimeUtc.ToUnixTime(), input.getClosePrice, Encoding.lineSeperator);
        }
    }
}
