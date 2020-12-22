using Newtonsoft.Json;

namespace iD3.Service.Models.Discogs
{
    public partial class Community
    {
        [JsonProperty("want", NullValueHandling = NullValueHandling.Ignore)]
        public long? Want { get; set; }

        [JsonProperty("have", NullValueHandling = NullValueHandling.Ignore)]
        public long? Have { get; set; }
    }
}
