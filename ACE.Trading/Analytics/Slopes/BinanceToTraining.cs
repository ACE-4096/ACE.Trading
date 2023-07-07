using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using ACE.Sandbox.BinanceConvertions;
using ACE.Trading.Analytics.Slopes;
using ACE.Trading.Data;
using ACE.Trading.OpenAi.Formatting;
using static System.Formats.Asn1.AsnWriter;

namespace ACE.Trading.Analytics.Slopes
{

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
                    prompt += FluidLanguage.lineSeperator + FluidLanguage.formatBinanceLine(inLines[i + j]);
                }
                for (int k = 0; k < completionOutputs; k++)
                {
                    completion += FluidLanguage.formatBinanceLine(inLines[i + promptInputs + k]);
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
                slopes = Convertions.FindAllV2(points);
            }
            if (slopes == null || slopes.Count == 0)
                return null;

            return slopesToTraingingDataFliudLanguage(slopes, numOfSlopesPerPrompt, numOfSlopesPerCompletion);
        }
        public static TrainingData slopesToTraingingDataFliudLanguage(List<PricePointSlope> slopes, int numOfSlopesPerPrompt, int numOfSlopesPerCompletion)
        {
            slopes.Sort(Convertions.sortTime_oldestFirst);
            TrainingData td = new TrainingData();


            for (int i = 0; i < (slopes.Count - (numOfSlopesPerPrompt + numOfSlopesPerCompletion)); i += numOfSlopesPerPrompt + numOfSlopesPerCompletion)
            {
                string prompt = FluidLanguage.lineSeperator, completion = "";
                for (int j = 0; j < numOfSlopesPerPrompt; j++)
                {
                    prompt += FluidLanguage.formatBinanceLine(slopes[i + j]);
                }
                for (int k = 0; k < numOfSlopesPerCompletion; k++)
                {
                    completion += FluidLanguage.formatBinanceLine(slopes[i + numOfSlopesPerPrompt + k]);
                }
                td.Add(prompt, completion);
            }
            return td;
        }
        public static TrainingData slopesToTraingingDataMinLanguage(List<PricePointSlope> slopes, int numOfSlopesPerPrompt, int numOfSlopesPerCompletion)
        {
            slopes.Sort(Convertions.sortTime_oldestFirst);
            TrainingData td = new TrainingData();


            for (int i = 0; i < (slopes.Count - (numOfSlopesPerPrompt + numOfSlopesPerCompletion)); i += numOfSlopesPerPrompt + numOfSlopesPerCompletion)
            {
                string prompt = MinLanguage.Encoding.slopeSeperator, completion = "";
                for (int j = 0; j < numOfSlopesPerPrompt; j++)
                {
                    prompt += FluidLanguage.formatBinanceLine(slopes[i + j]);
                }
                for (int k = 0; k < numOfSlopesPerCompletion; k++)
                {
                    completion += FluidLanguage.formatBinanceLine(slopes[i + numOfSlopesPerPrompt + k]);
                }
                td.Add(prompt, completion);
            }
            return td;
        }


        public static string convertToString(string filename, int promptInputs, int completionOutputs)
        {
            return convert(filename, promptInputs, completionOutputs).ToString();
        }
    }
}
