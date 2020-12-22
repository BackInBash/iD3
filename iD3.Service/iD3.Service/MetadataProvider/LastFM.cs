using iD3.Service.Models.LastFM;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace iD3.Service.MetadataProvider
{
    public class LastFM
    {
        private const string API = "https://ws.audioscrobbler.com/2.0/";
        private const string APIKEY = "0af1393e14d41fb6b9d571038e8003a8";
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36";
        public async Task<LastFmData> GetMetadata(MetaTrack track, NLog.Logger logger = null)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("User-Agent", UserAgent);
                var res = await client.DownloadStringTaskAsync(API + "?method=track.getInfo&api_key=" + APIKEY + "&artist=" + Uri.EscapeDataString(track.artist) + "&track=" + Uri.EscapeDataString(track.title) + "&format=json");
                var data = JsonConvert.DeserializeObject<LastFmData>(res);

                if (data.Track.Artist.Name.Equals(track.artist) && data.Track.Name.Equals(track.title))
                {
                    return data;
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.Error(e, "LastFM Metadata: " + e.Message);
            }
            return null;
        }
    }
}