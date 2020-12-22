using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace iD3.Service.Models.Discogs
{
    public partial class Result
    {
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("year", NullValueHandling = NullValueHandling.Ignore)]
        public long? Year { get; set; }

        [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Format { get; set; }

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Label { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public TypeEnum? Type { get; set; }

        [JsonProperty("genre", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Genre { get; set; }

        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Style { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("barcode", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Barcode { get; set; }

        [JsonProperty("master_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? MasterId { get; set; }

        [JsonProperty("master_url")]
        public Uri MasterUrl { get; set; }

        [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
        public string Uri { get; set; }

        [JsonProperty("catno", NullValueHandling = NullValueHandling.Ignore)]
        public string Catno { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("thumb", NullValueHandling = NullValueHandling.Ignore)]
        public string Thumb { get; set; }

        [JsonProperty("cover_image", NullValueHandling = NullValueHandling.Ignore)]
        public Uri CoverImage { get; set; }

        [JsonProperty("resource_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ResourceUrl { get; set; }

        [JsonProperty("community", NullValueHandling = NullValueHandling.Ignore)]
        public Community Community { get; set; }

        [JsonProperty("format_quantity", NullValueHandling = NullValueHandling.Ignore)]
        public long? FormatQuantity { get; set; }

        [JsonProperty("formats", NullValueHandling = NullValueHandling.Ignore)]
        public List<Format> Formats { get; set; }

        public enum TypeEnum { Release, Master, Artist, Label };
    }
}
