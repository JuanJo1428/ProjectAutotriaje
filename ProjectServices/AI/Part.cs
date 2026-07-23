using Newtonsoft.Json;

namespace ProjectServices.AI
{
    public class Part
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}