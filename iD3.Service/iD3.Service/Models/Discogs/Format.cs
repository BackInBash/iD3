using Newtonsoft.Json;
using System.Collections.Generic;

namespace iD3.Service.Models.Discogs
{
    public partial class Format
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("qty", NullValueHandling = NullValueHandling.Ignore)]
        public long? Qty { get; set; }

        [JsonProperty("descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Descriptions { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
    }
}
