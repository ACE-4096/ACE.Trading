using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using OpenAI_API.Models;

namespace OpenAI_API.FineTune
{
    /// <summary>
    /// Text generation is the core function of the API. You give the API a prompt, and it generates a completion. The way you “program” the API to do a task is by simply describing the task in plain english or providing a few written examples. This simple approach works for a wide range of use cases, including summarization, translation, grammar correction, question answering, chatbots, composing emails, and much more (see the prompt library for inspiration).
    /// </summary>
    public class FineTuneEndpoint : EndpointBase, IFineTuneEndpoint
    {
        /// <summary>
        /// This allows you to set default parameters for every request, for example to set a default temperature or max tokens.  For every request, if you do not have a parameter set on the request but do have it set here as a default, the request will automatically pick up the default value.
        /// </summary>
        public FineTuneRequest DefaultFineTuneRequestArgs { get; set; } = new FineTuneRequest() { Model = Model.DavinciText };

        /// <summary>
        /// The name of the endpoint, which is the final path segment in the API URL.  For example, "completions".
        /// </summary>
        protected override string Endpoint { get { return "completions"; } }

        /// <summary>
        /// Constructor of the api endpoint.  Rather than instantiating this yourself, access it through an instance of <see cref="OpenAIAPI"/> as <see cref="OpenAIAPI.FineTune"/>.
        /// </summary>
        /// <param name="api"></param>
        internal FineTuneEndpoint(OpenAIAPI api) : base(api) { }

        public async Task<FineTuneResult> CreateFineTuneAsync(FineTuneRequest request)
        {
            return await HttpPost<FineTuneResult>(postData: request);
        }

        public Task<FineTuneResult> CreateFineTuneAsync(string TrainingFile, string ValidationFile = null, Model model = null, int? NumberOfEpochs = null, int? BatchSize = null, double? LearningRateMultiplier = null, double? PromptLoseWeight = null, bool ComputeClassificationMetrics = false, int? ClassNumberOfClasses = null, string ClassPositiveClass = null, int[] ClassBetas = null, string Suffix = null)
        {
            FineTuneRequest request = new FineTuneRequest(TrainingFile, ValidationFile, model, NumberOfEpochs, BatchSize, LearningRateMultiplier, PromptLoseWeight, ComputeClassificationMetrics, ClassNumberOfClasses, ClassPositiveClass, ClassBetas, Suffix);
            return CreateFineTuneAsync(request);
        }

        public Task<FineTuneResult> CreateFineTuneAsync(string TrainingFile)
        {
            FineTuneRequest request = new FineTuneRequest(TrainingFile);
            return CreateFineTuneAsync(request);
        }


        public Task<string> DeleteFineTuneModel(Model model)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveFineTune(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<string> CancelFineTune(string Id)
        {
            throw new NotImplementedException();
        }
    }
}