using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProjectServices.AI
{
    public class Content
    {
        [JsonProperty("parts")]
        public List<Part> Parts { get; set; }
    }
}