using Newtonsoft.Json;
using System;

namespace iD3.Service.Models.Discogs
{
    public partial class Urls
    {
        [JsonProperty("last", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Last { get; set; }

        [JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Next { get; set; }
    }
}
