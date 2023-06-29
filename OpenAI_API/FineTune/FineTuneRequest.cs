using Newtonsoft.Json;
using OpenAI_API.Models;
using System.Dynamic;
using System.Linq;

namespace OpenAI_API.FineTune
{

    public class FineTuneRequests
    {
        [JsonProperty("object")]
        public string ObjectType { get; set; }

        [JsonProperty("data")]
        public IReadOnlyList<FineTuneRequest>  data{ get; set; }
    }

    /// <summary>
    /// Represents a request to the Completions API.  Mostly matches the parameters in <see href="https://beta.openai.com/api-ref#create-completion-post">the OpenAI docs</see>, although some have been renamed or expanded into single/multiple properties for ease of use.
    /// </summary>
    public class FineTuneRequest
    {
        /// <summary>
        /// ID of the model to use. You can use <see cref="ModelsEndpoint.GetModelsAsync()"/> to see all of your available models, or use a standard model like <see cref="Model.DavinciText"/>.
        /// </summary>
        [JsonProperty("training_file")]
        public string TrainingFile { get; set; } 

        /// <summary>
        /// This is only used for serializing the request into JSON, do not use it directly.
        /// </summary>
        [JsonProperty("validation_file")]
        public string ValidationFile { get; set; }

        /// <summary>
        /// If you are requesting more than one prompt, specify them as an array of strings.
        /// </summary>
        [JsonProperty("model")]
        public Models.Model Model { get; set; } = Models.Model.DavinciText;

        /// <summary>
        /// For convenience, if you are only requesting a single prompt, set it here
        /// </summary>
        [JsonProperty("n_epochs")]
        public int? NumberOfEpochs { get; set; } = 4;

        /// <summary>
        /// The suffix that comes after a completion of inserted text.  Defaults to null.
        /// </summary>
        [JsonProperty("batch_size")]
        public int? BatchSize { get; set; }

        /// <summary>
        /// How many tokens to complete to. Can return fewer if a stop sequence is hit.  Defaults to 16.
        /// </summary>
        [JsonProperty("learning_rate_multiplier")]
        public double? LearningRateMultiplier { get; set; }

        /// <summary>
        /// What sampling temperature to use. Higher values means the model will take more risks. Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer. It is generally recommend to use this or <see cref="TopP"/> but not both.
        /// </summary>
        [JsonProperty("prompt_loss_weight")]
        public double? PromptLossWeight { get; set; }
        
        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered. It is generally recommend to use this or <see cref="Temperature"/> but not both.
        /// </summary>
        [JsonProperty("compute_classification_metrics")]
        public bool ComputeClassificationMetrics { get; set; }

        /// <summary>
        /// The scale of the penalty applied if a token is already present at all.  Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.  Defaults to 0.
        /// </summary>
        [JsonProperty("classification_n_classes")]
        public int? ClassNumberOfClasses { get; set; }

        /// <summary>
        /// The scale of the penalty for how often a token is used.  Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.  Defaults to 0.
        /// </summary>
        [JsonProperty("classification_positive_class")]
        public string ClassPositiveClass { get; set; }

        /// <summary>
        /// How many different choices to request for each prompt.  Defaults to 1.
        /// </summary>
        [JsonProperty("classification_betas")]
        public int[] ClassBetas { get; set; }

        /// <summary>
        /// Specifies where the results should stream and be returned at one time.  Do not set this yourself, use the appropriate methods on <see cref="CompletionEndpoint"/> instead.
        /// </summary>
        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        /// <summary>
        /// Cretes a new, empty <see cref="CompletionRequest"/>
        /// </summary>
        public FineTuneRequest()
        {
            this.Model = OpenAI_API.Models.Model.DefaultModel;
        }

        /// <summary>
        /// Creates a new <see cref="CompletionRequest"/>, inheriting any parameters set in <paramref name="basedOn"/>.
        /// </summary>
        /// <param name="basedOn">The <see cref="CompletionRequest"/> to copy</param>
        public FineTuneRequest(FineTuneRequest basedOn)
        {
            this.TrainingFile = basedOn.TrainingFile;
            this.ValidationFile = basedOn.ValidationFile;
            this.Model = basedOn.Model;
            this.NumberOfEpochs = basedOn.NumberOfEpochs;
            this.BatchSize = basedOn.BatchSize;
            this.LearningRateMultiplier = basedOn.LearningRateMultiplier;
            this.PromptLossWeight = basedOn.PromptLossWeight;
            this.ComputeClassificationMetrics = basedOn.ComputeClassificationMetrics;
            this.ClassNumberOfClasses = basedOn.ClassNumberOfClasses;
            this.ClassPositiveClass = basedOn.ClassPositiveClass;
            this.ClassBetas = basedOn.ClassBetas;
            this.Suffix = basedOn.Suffix;
        }

        /// <summary>
        /// Creates a new <see cref="CompletionRequest"/>, using the specified prompts
        /// </summary>
        /// <param name="prompts">One or more prompts to generate from</param>
        public FineTuneRequest(string trainingFile)
        {
            this.TrainingFile = trainingFile;
        }

        /// <summary>
        /// Creates a new <see cref="CompletionRequest"/> with the specified parameters
        /// </summary>
        /// <param name="prompt">The prompt to generate from</param>
        /// <param name="model">The model to use. You can use <see cref="ModelsEndpoint.GetModelsAsync()"/> to see all of your available models, or use a standard model like <see cref="Model.DavinciText"/>.</param>
        /// <param name="max_tokens">How many tokens to complete to. Can return fewer if a stop sequence is hit.</param>
        /// <param name="temperature">What sampling temperature to use. Higher values means the model will take more risks. Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer. It is generally recommend to use this or <paramref name="top_p"/> but not both.</param>
        /// <param name="suffix">The suffix that comes after a completion of inserted text</param>
        /// <param name="top_p">An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered. It is generally recommend to use this or <paramref name="temperature"/> but not both.</param>
        /// <param name="numOutputs">How many different choices to request for each prompt.</param>
        /// <param name="presencePenalty">The scale of the penalty applied if a token is already present at all.  Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.</param>
        /// <param name="frequencyPenalty">The scale of the penalty for how often a token is used.  Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.</param>
        /// <param name="logProbs">Include the log probabilities on the logprobs most likely tokens, which can be found in <see cref="CompletionResult.Completions"/> -> <see cref="Choice.Logprobs"/>. So for example, if logprobs is 10, the API will return a list of the 10 most likely tokens. If logprobs is supplied, the API will always return the logprob of the sampled token, so there may be up to logprobs+1 elements in the response.</param>
        /// <param name="echo">Echo back the prompt in addition to the completion.</param>
        /// <param name="stopSequences">One or more sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.</param>
        public FineTuneRequest(
            string TrainingFile,
            string ValidationFile = null,
            Models.Model model = null,
            int? NumberOfEpochs = null,
            int? BatchSize = null,
            double? LearningRateMultiplier = null,
            double? PromptLossWeight = null,
            bool ComputeClassificationMetrics = false,
            int? ClassNumberOfClasses = null,
            string ClassPositiveClass = null,
            int[] ClassBetas = null,
            string Suffix = null)
        {
            this.TrainingFile = TrainingFile;
            this.ValidationFile = ValidationFile;
            this.Model = model;
            this.NumberOfEpochs = NumberOfEpochs;
            this.BatchSize = BatchSize;
            this.LearningRateMultiplier = LearningRateMultiplier;
            this.PromptLossWeight = PromptLossWeight;
            this.ComputeClassificationMetrics = ComputeClassificationMetrics;
            this.ClassNumberOfClasses = ClassNumberOfClasses;
            this.ClassPositiveClass = ClassPositiveClass;
            this.ClassBetas = ClassBetas;
            this.Suffix = Suffix;
        }


    }

}