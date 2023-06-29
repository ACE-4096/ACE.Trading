using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI_API.Models;
using OpenAI_API.Completions;
using OpenAI_API;
using Alpaca.Markets;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.IO;
using OpenAI_API.FineTune;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace ACE.Trading.CognitionEngine
{
    public class OpenAiIntegration
    {



        private OpenAIAPI api;

        #region Events
        public delegate void ReplyHandler(string reply);
        public event ReplyHandler onReply;

        public delegate void BarReplyHandler(replyStruct bar);
        public event BarReplyHandler onBarReply;
        #endregion

        public struct replyStruct
        {
            public DateTime timeUtc;
            public decimal low, high, open, close, volume;
        }

        public OpenAiIntegration()
        {            
            // authorise api
            APIAuthentication apiAuth = new APIAuthentication(apiKey);
            api = new OpenAIAPI(apiAuth);
        }

        public async Task<string> PredictFromFineTune(string input, string fineTuneModelId)
        {
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Model = new Model(fineTuneModelId);
            completionRequest.Prompt = input;
            completionRequest.NumChoicesPerPrompt = 1;
            completionRequest.TopP = 1;
            completionRequest.Temperature = 0;
            completionRequest.BestOf = 1;
            var result = await api.Completions.CreateCompletionAsync(completionRequest);

            // A List of completion results;
            // result.Result.Completions;
            string output = "";
            if (result.Completions.Count == 1)
            {
                output = result.Completions[0].Text;
            }
            output = "Too many results.";
            onReply?.Invoke(output);
            return output;
        }
        public string formatDataSets(string[] lines)
        {
            string output = seperator;
            foreach (string line in lines)
            {
                output += line + seperator;
            }
            return output;
        }

        public bool createFineTuneDataFiles(string datasetFileName, string validationFileName, string trainingFile, double ratio = 0.8)
        {
            bool success = false;
            Random randy = new Random();

            string text = System.IO.File.ReadAllText(datasetFileName);
            List<string> lines = new List<string>(text.Split(seperator, StringSplitOptions.RemoveEmptyEntries));
            int initialLineCount = lines.Count;
            List<string> valadationFileLines = new List<string>();

            int numToRemove = (int)((double)lines.Count * (1 - ratio));

            for (int i = 0; i < numToRemove; i++)
            {
                int lineNumToRemove = randy.Next(2, lines.Count);
                valadationFileLines.Add(lines[lineNumToRemove]);
                lines.RemoveAt(lineNumToRemove);
            }
            

            if (lines.Count + valadationFileLines.Count == initialLineCount && valadationFileLines.Count > 0)
            {
                System.IO.File.WriteAllText(validationFileName, formatDataSets(valadationFileLines.ToArray()));
                System.IO.File.WriteAllText(trainingFile, formatDataSets(lines.ToArray()));
                success = true;
            }
            return success;
        }

        #region FineTune Inegrations
        public async Task<FineTuneResult> fineTune(string filename, string symbolName)
        {
            // convert input to datafileFormat


            if (!createFineTuneDataFiles(filename, "validationDataset.csv", "trainingDataset.csv"))
            {
                Debug.WriteLine("ProcessingEngine.fineTune: ExitCode=-1");
                return null;
            }
            // Upload DataFiles 

            // upload trainig file
            var trainingFileUploadResult = await api.Files.UploadFileAsync("trainingDataset.csv");
            if (trainingFileUploadResult.Status != "uploaded")
            {
                Debug.WriteLine("ProcessingEngine.fineTune: ExitCode=-2");
                return null;
            }

            // upload validation file
            var validationFileUploadResult = await api.Files.UploadFileAsync("validationDataset.csv");
            if (validationFileUploadResult.Status != "uploaded")
            {
                Debug.WriteLine("ProcessingEngine.fineTune: ExitCode=-3");
                return null;
            }


            // request fine tune
            FineTuneRequest request = new FineTuneRequest();
            request.ValidationFile = validationFileUploadResult.Name;
            request.TrainingFile = trainingFileUploadResult.Name;
            request.NumberOfEpochs = 4;
            request.BatchSize = 4;
            request.Model = Model.DavinciText;
            request.Suffix = "ACE-4096-" + symbolName;

            // Modelname would be text-davinci-003::ACE-4096-{symbolName}
            var result = await api.FineTune.CreateFineTuneAsync(request);

            // check validity of result
            if (result == null) { Debug.WriteLine("ProcessingEngine.fineTune: ExitCode=-4"); return null; }

            Debug.WriteLine("result Status: " + result.Status);
            return result;
        }

        public async Task<FineTuneResult> getFineTuneUpdate(string finetuneID)
        {
            return await api.FineTune.RetrieveFineTune(finetuneID);
        }

        public async Task<FineTuneResultList> getFineTuneList()
        {
            return await api.FineTune.RetrieveFineTuneList();
        }
        #endregion

        #region Alpaca - Depreciated
        // Depreciated
        public async void PredictNextBar(List<IBar> bars)
        {
            string input = "Complete the next line in the sequence below: \nAverage|TimeUTC";//Open|Close|High|Low|Volume|";
            foreach (var bar in bars)
            {
                input += "\n " + ((bar.High + bar.Low) / 2) + "|" + bar.TimeUtc;// + "|" + bar.Open + "|" + bar.Close + "|" + bar.High + "|" + bar.Low + "|" + bar.Volume +c;
            }

            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Model = Model.AdaText;
            completionRequest.Prompt = input;
            completionRequest.NumChoicesPerPrompt = 1;
            completionRequest.TopP = 1;
            completionRequest.Temperature = 0;
            completionRequest.BestOf = 1;

            var result = await api.Completions.CreateCompletionAsync(completionRequest);
            // A List of completion results;
            // result.Result.Completions;
            Debug.WriteLine("result:" + result);
            if (result.Completions.Count == 1)
            {
                //reply = Double.Parse(result.Result.Completions[0].Text);
                string prediction = result.Completions[0].ToString();
                string[] predictions = prediction.Split('|');
                if (predictions.Length == 6)
                {
                    replyStruct reply = new replyStruct();
                    reply.open = Decimal.Parse(predictions[0]);
                    reply.close = Decimal.Parse(predictions[1]);
                    reply.high = Decimal.Parse(predictions[2]);
                    reply.low = Decimal.Parse(predictions[3]);
                    reply.volume = Decimal.Parse(predictions[4]);
                    reply.timeUtc = DateTime.Parse(predictions[5]);
                    onBarReply?.Invoke(reply);
                }
                if (predictions.Length > 2)
                {
                    replyStruct reply = new replyStruct();
                    //reply.timeUtc = DateTime.Parse(predictions[0]);
                    reply.open = Decimal.Parse(predictions[0]);
                    reply.close = Decimal.Parse(predictions[0]);
                    reply.high = Decimal.Parse(predictions[0]);
                    reply.low = Decimal.Parse(predictions[0]);
                    //reply.volume = Decimal.Parse(predictions[0]);
                    onBarReply?.Invoke(reply);
                }
                Debug.WriteLine("Prediction: " + prediction);
            }
            else
            {
                Debug.WriteLine("too many predictions");
            }



        }
        // Depreciated
        public async void Predict(string input)
        {

            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Model = Model.DavinciText;
            completionRequest.Prompt = input;
            completionRequest.NumChoicesPerPrompt = 1;
            completionRequest.TopP = 1;
            completionRequest.Temperature = 0;
            completionRequest.BestOf = 1;
            var result = await api.Completions.CreateCompletionAsync(completionRequest);

            // A List of completion results;
            // result.Result.Completions;
            string output = "";
            if (result.Completions.Count == 1)
            {
                output = result.Completions[0].Text;
            }
            output = "Too many results.";
            onReply?.Invoke(output);
            //return output;
        }
        #endregion
    }
}
