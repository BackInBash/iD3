using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iD3.Service.MetadataProvider
{
    public class DiscogsRequestLimitException : Exception
    {
        public DiscogsRequestLimitException()
        {
        }

        public DiscogsRequestLimitException(string message)
            : base(message)
        {
        }
    }

    public partial class SearchResult
    {
        [JsonProperty("pagination", NullValueHandling = NullValueHandling.Ignore)]
        public Pagination Pagination { get; set; }

        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public List<Result> Results { get; set; }
    }

    public partial class Pagination
    {
        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public long? Page { get; set; }

        [JsonProperty("pages", NullValueHandling = NullValueHandling.Ignore)]
        public long? Pages { get; set; }

        [JsonProperty("per_page", NullValueHandling = NullValueHandling.Ignore)]
        public long? PerPage { get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public long? Items { get; set; }

        [JsonProperty("urls", NullValueHandling = NullValueHandling.Ignore)]
        public Urls Urls { get; set; }
    }

    public partial class Urls
    {
        [JsonProperty("last", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Last { get; set; }

        [JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Next { get; set; }
    }

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
    }

    public partial class Community
    {
        [JsonProperty("want", NullValueHandling = NullValueHandling.Ignore)]
        public long? Want { get; set; }

        [JsonProperty("have", NullValueHandling = NullValueHandling.Ignore)]
        public long? Have { get; set; }
    }

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

    public enum TypeEnum { Release, Master, Artist, Label };

    public class Discogs
    {
        private const string APIBase = "https://api.discogs.com";
        private const string APIKey = "rAzVUQYRaoFjeBjyWuWZ";
        private const string APISecret = "plxtUTqoCzwxZpqdPysCwGuBSmZNdZVy";
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36";

        /// <summary>
        /// Query Discogs API
        /// </summary>
        /// <param name="track">Track Object</param>
        /// <returns></returns>
        public async Task<SearchResult> Search(MetaTrack track)
        {
            HttpClient webClient = new HttpClient();
            webClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            HttpResponseMessage data = null;

            data = await webClient.GetAsync(APIBase + "/database/search?" +
                  "artist=" + track.artist + "" +
                  "&title=" + track.title + "" +
                  "&key=rAzVUQYRaoFjeBjyWuWZ" +
                  "&secret=plxtUTqoCzwxZpqdPysCwGuBSmZNdZVy" +
                  "&type=release");
            var headers = data.Headers.Concat(data.Content.Headers);
            if (headers.Where(x => x.Key.Equals("X-Discogs-Ratelimit-Remaining")).Select(x => x.Value).First().First() == "0")
            {
                throw new DiscogsRequestLimitException();
            }
            return JsonConvert.DeserializeObject<SearchResult>(await data.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Get Metadata from Discogs
        /// </summary>
        /// <param name="track">Track Object</param>
        /// <returns></returns>
        public async Task<string[]> GetMetadata(MetaTrack track)
        {
            SearchResult data = null;
            try
            {
                data = await Search(track);
            }
            catch (DiscogsRequestLimitException)
            {
                Thread.Sleep(5000);
                data = await Search(track);
            }
            List<string> tags = new List<string>();
            foreach (Result trackres in data.Results)
            {
                if (trackres.Title.Contains(track.artist) && trackres.Title.Contains(track.title) && trackres.Year.Equals(long.Parse(track.year)))
                {
                    tags.AddRange(trackres.Genre);
                    tags.AddRange(trackres.Style);
                }
            }
            return tags.Distinct().ToArray();
        }
    }
}
