using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI_API.Models;
using OpenAI_API.Completions;
using OpenAI_API;
//using Alpaca.Markets;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.IO;
using OpenAI_API.FineTune;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Newtonsoft.Json;
using ACE.Trading.Data;
using static ACE.Trading.Data.DataHandling;
using OpenAI_API.Files;
using Microsoft.VisualBasic;

namespace ACE.Trading.OpenAi
{
    public class OpenAiIntegration
    {

        internal const string apiKey = "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";

        private static OpenAIAPI api;

        #region Events
        // Depreciated
        //public delegate void ReplyHandler(string reply);
        //public event ReplyHandler onReply;


        // public delegate void BarReplyHandler(replyStruct bar);
        // public event BarReplyHandler onBarReply;
        #endregion

        public static async List<Model> getModels()
        {
            return await api.Models.GetModelsAsync();
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
            completionRequest.MaxTokens = 1000;
            var result = await api.Completions.CreateCompletionAsync(completionRequest);

            // A List of completion results;
            // result.Result.Completions;
            string output = "Too many results.";
            if (result.Completions.Count == 1)
            {
                output = result.Completions[0].Text;
            }
            //onReply?.Invoke(output);
            
            return output;
        }
        public bool createFineTuneDataFiles(string datasetFileName, string validationFileName, string trainingFile, double ratio = 0.8)
        {
            bool success = false;
            Random randy = new Random();

            string text = System.IO.File.ReadAllText(datasetFileName);
            List<string> lines = new List<string>(text.Split(SimpleEncoding.seperator, StringSplitOptions.RemoveEmptyEntries));
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
                System.IO.File.WriteAllText(validationFileName, SimpleEncoding.formatDataSets(valadationFileLines.ToArray()));
                System.IO.File.WriteAllText(trainingFile, SimpleEncoding.formatDataSets(lines.ToArray()));
                success = true;
            }
            return success;
        }

        #region FineTune Inegrations
        public async Task<FineTuneResult> fineTuneAndUpload(string filename, string symbolName)
        {
            // convert input to datafileFormat


            // upload trainig file
            var trainingFileUploadResult = await api.Files.UploadFileAsync(filename);
            if (trainingFileUploadResult.Status != "uploaded")
            {
                Debug.WriteLine("ProcessingEngine.fineTune: ExitCode=-2");
                return null;
            }

            return await fineTune(trainingFileUploadResult.Id, symbolName);
        }
        public async Task<FineTuneResult> fineTune(string trainingFileId, string symbolName)
        {

            // request fine tune
            FineTuneRequest request = new FineTuneRequest();
            request.TrainingFile = trainingFileId;
            request.NumberOfEpochs = 4;
            request.BatchSize = 4;
            request.ModelId = "ada";
            request.Suffix = "ACE-4096-" + symbolName;

            // Modelname would be text-davinci-003::ACE-4096-{symbolName}
            var result = await api.FineTune.CreateFineTuneAsync(request);

            // check validity of result
            if (result == null) { Debug.WriteLine("ProcessingEngine.fineTune: ExitCode=-4"); return null; }

            Debug.WriteLine("result Status: " + result.Status);
            return result;
        }
        public async Task<FineTuneResult> fineTune(string trainingFileId, string symbolName, HyperParams hypers)
        {

            // request fine tune
            FineTuneRequest request = new FineTuneRequest();
            request.TrainingFile = trainingFileId;
            request.NumberOfEpochs = hypers.NumberOfEpochs;
            request.BatchSize = hypers.BatchSize;
            request.PromptLossWeight = hypers.PromptLossWeight;
            request.LearningRateMultiplier = hypers.LearningRateMultiplier;
            request.ModelId = "ada";
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

        #region Files
        public async Task<List<OpenAI_API.Files.File>> getFiles()
        {
            return await api.Files.GetFilesAsync();
        }

        public async Task<string> getFileData(string fileId)
        {
            return await api.Files.GetFileContentAsStringAsync(fileId);
        }

        public async Task<OpenAI_API.Files.File> uploadFile(string filename)
        {
            if (!System.IO.File.Exists(filename))
            {
                return null;
            }
            // upload training file
            var trainingFileUploadResult = await api.Files.UploadFileAsync(filename);
            if (trainingFileUploadResult.Status != "uploaded")
            {
                Debug.WriteLine("ProcessingEngine.fineTune: ExitCode=-2");
                return null;
            }
            return trainingFileUploadResult;
        }

        public async Task<OpenAI_API.Files.File> deleteFile(string fileId)
        {
            // delete training file
            var trainingFileDeletionResult = await api.Files.DeleteFileAsync(fileId);
            if (trainingFileDeletionResult.Status != "uploaded")
            {
                Debug.WriteLine("ProcessingEngine.fineTune: ExitCode=-2");
                return null;
            }
            return trainingFileDeletionResult;
        }
        #endregion

        #region Alpaca - Depreciated
        /* Depreciated
         
        public struct replyStruct
        {
            public DateTime timeUtc;
            public decimal low, high, open, close, volume;
        }
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
        }*/
        #endregion
    }
}
