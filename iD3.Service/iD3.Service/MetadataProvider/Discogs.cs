using iD3.Service.Models.Discogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

    public class Discogs
    {
        private const string APIBase = "https://api.discogs.com";
        private const string APIKey = "rAzVUQYRaoFjeBjyWuWZ";
        private const string APISecret = "plxtUTqoCzwxZpqdPysCwGuBSmZNdZVy";
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36";
        private NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
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
                Logger.Debug("Discdigs Request Limit Triggered!");
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
