using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProjectServices.AI
{
    public class GeminiRequest
    {
        [JsonProperty("contents")]
        public List<Content> Contents { get; set; }
    }
}