using Newtonsoft.Json;
using System.Collections.Generic;

namespace iD3.Service.Models.Discogs
{
    public partial class SearchResult
    {
        [JsonProperty("pagination", NullValueHandling = NullValueHandling.Ignore)]
        public Pagination Pagination { get; set; }

        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public List<Result> Results { get; set; }
    }
}
