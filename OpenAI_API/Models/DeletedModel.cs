using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenAI_API.Model
{
    public class DeletedModel : ApiResultBase
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("object")]
        public string ObjectType { get; set; }

        [JsonProperty("deleted")]
        public bool isDeleted { get; set; }
    }
}
