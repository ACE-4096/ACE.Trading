using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.OpenAi.TrainingData
{
    public class Convertions
    {
        // Converts a single delta prompt
        public static DataFile ExtrapolateFromSinglePrompt(string input)
        {
            DataFile trainingDataFile = new DataFile();
            string[] lines = input.Split(Encoding.seperator);

            Random randy = new Random();

            for (int i = 0; i < lines.Length - 12; i += 12)
            {
                string prompt = Encoding.seperator, completion = "";
                for (int j = i; j < i + 9; j++)
                {
                    prompt += lines[j] + Encoding.seperator;
                }
                for (int k = i + 9; k < i + 12; k++)
                {
                    completion += lines[k] + Encoding.seperator;
                }
                trainingDataFile.Add(prompt, completion);
            }

            return trainingDataFile;
        }
        public static DataFile convertFromBinanceDataFile(string filename, int promptInputs, int completionOutputs)
        {
            return convertFromBinanceData(File.ReadAllText(filename), promptInputs, completionOutputs);
        }
        public static DataFile convertFromBinanceData(string input, int promptInputs, int completionOutputs)
        {
            DataFile td = new DataFile();
            string[] inLines = input.Split('\n');
            for (int i = 0; i < (inLines.Length - (promptInputs + completionOutputs)); i += promptInputs + completionOutputs)
            {
                string prompt = "", completion = "";
                for (int j = 0; j < promptInputs; j++)
                {
                    prompt += Encoding.seperator + formatBinanceLine(inLines[i + j]);
                }
                for (int k = 0; k < completionOutputs; k++)
                {
                    completion += formatBinanceLine(inLines[i + promptInputs + k]);
                }
                td.Add(prompt, completion);
            }
            return td;
        }
        public static void convertFromBinanceData(string binanceDataFilename, string outputFilename, int promptInputs, int completionOutputs)
        {
            File.WriteAllText(outputFilename, convertFromBinanceData(binanceDataFilename, promptInputs, completionOutputs).ToString());
        }
        public static string convertToString(string filename, int promptInputs, int completionOutputs)
        {
            return convertFromBinanceData(filename, promptInputs, completionOutputs).ToString();
        }
        private static string formatBinanceLine(string input)
        {
            string[] inputs = input.Split(',');
            return string.Format(Encoding.formatString, inputs[0], getAvg(decimal.Parse(inputs[1]), decimal.Parse(inputs[4])));
        }
        private static decimal getAvg(decimal d1, decimal d2)
        {
            return (d1 + d2) / 2;
        }
    }
}
