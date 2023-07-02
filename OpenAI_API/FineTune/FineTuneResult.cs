using Newtonsoft.Json;
using OpenAI_API.Embedding;
using OpenAI_API.Files;
using OpenAI_API.Models;
using System.Collections.Generic;

namespace OpenAI_API.FineTune
{
    /// <summary>
    /// Represents a completion choice returned by the FineTune API.  
    /// </summary>
    public class Event : Usage
    {
        /// <summary>
        /// The main text of the completion
        /// </summary>
        [JsonProperty("object")]
        public string Object { get; set; }

        /// <summary>
        /// If multiple completion choices we returned, this is the index withing the various choices
        /// </summary>
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// If the request specified <see cref="FineTuneRequest.Logprobs"/>, this contains the list of the most likely tokens.
        /// </summary>
        [JsonProperty("level")]
        public string level { get; set; }

        /// <summary>
        /// If this is the last segment of the completion result, this specifies why the completion has ended.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets the main text of this completion
        /// </summary>
        public override string ToString()
        {
            return Message;
        }
    }

    /// <summary>
    /// API usage as reported by the OpenAI API for this request
    /// </summary>
    public class HyperParams
    {
        /// <summary>
        /// How many tokens are in the completion(s)
        /// </summary>
        [JsonProperty("batch_size")]
        public int BatchSize { get; set; }
        /// <summary>
        /// How many tokens are in the completion(s)
        /// </summary>
        [JsonProperty("learning_rate_multiplier")]
        public double LearningRateMultiplier { get; set; }
        /// <summary>
        /// How many tokens are in the completion(s)
        /// </summary>
        [JsonProperty("n_epochs")]
        public int NumberOfEpochs { get; set; }
        /// <summary>
        /// How many tokens are in the completion(s)
        /// </summary>
        [JsonProperty("prompt_loss_weight")]
        public double PromptLossWeight { get; set; }
    }

    /// <summary>
    /// Represents a result from calling the FineTune API
    /// </summary>
    public class FineTuneResult : ApiResultBase
    {
        /// <summary>
        /// The identifier of the result, which may be used during troubleshooting
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Object type
        /// </summary>
        [JsonProperty("object")]
        public string ObjectType { get; set; }

        /// <summary>
        /// Model used for a base during fine training
        /// </summary>
        [JsonProperty("model")]
        public string model { get; set; }

        /// <summary>
        /// Time request was processed in unix time
        /// </summary>
        [JsonProperty("created_at")]
        public long createdAt { get; set; }

        /// <summary>
        /// List of events about the fine-tuning
        /// </summary>
        [JsonProperty("events")]
        public IReadOnlyList<Event> Events { get; set; }

        /// <summary>
        /// Model Name
        /// </summary>
        [JsonProperty("fine_tuned_model")]
        public string? FineTunedModel { get; set; }

        /// <summary>
        /// Hyper Parameters
        /// </summary>
        [JsonProperty("hyperparams")]
        public HyperParams HyperParameters { get; set; }

        /// <summary>
        /// organisation id the fine tuning belongs too
        /// </summary>
        [JsonProperty("organization_id")]
        public string? OrganizationId { get; set; }

        /// <summary>
        /// result files of the fine tuning
        /// </summary>
        [JsonProperty("result_files")]
        public List<OpenAI_API.Files.File> ResultFiles { get; set; }

        /// <summary>
        /// status of the tine tuning
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// List of validation files
        /// </summary>
        [JsonProperty("validation_files")]
        public List<Files.File> ValidationFiles { get; set; }

        /// <summary>
        /// List of training files used
        /// </summary>
        [JsonProperty("training_files")]
        public List<Files.File> TrainingFiles { get; set; }

        /// <summary>
        /// Time the fine tune data was updated in unix time
        /// </summary>
        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        /// <summary>
        /// Gets the text of the first completion, representing the main result
        /// </summary>
        public override string ToString()
        {
            if (Status != null)
                return "Name: " + FineTunedModel + ", Status: " + Status.ToString();
            else
                return $"FineTuneResult {Id} has no valid output";
        }
    }

    public class FineTuneResultList : ApiResultBase
    {
        /// <summary>
        /// object type
        /// </summary>
        [JsonProperty("object")]
        public string Object { get; set; }

        /// <summary>
        /// List of fine tune results
        /// </summary>
        [JsonProperty("data")]
        public List<FineTuneResult> data { get; set; }
    }

}