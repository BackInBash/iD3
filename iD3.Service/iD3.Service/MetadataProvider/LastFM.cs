using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace iD3.Service.MetadataProvider
{
    public partial class LastFmData
    {
        public Track Track { get; set; }
    }

    public partial class Track
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public long Duration { get; set; }
        public Streamable Streamable { get; set; }
        public long Listeners { get; set; }
        public long Playcount { get; set; }
        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public Toptags Toptags { get; set; }
        public Wiki Wiki { get; set; }
    }

    public partial class Album
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }
        public List<Image> Image { get; set; }
    }

    public partial class Image
    {
        public Uri Text { get; set; }
        public string Size { get; set; }
    }

    public partial class Artist
    {
        public string Name { get; set; }
        public Guid Mbid { get; set; }
        public Uri Url { get; set; }
    }

    public partial class Streamable
    {
        public long Text { get; set; }
        public long Fulltrack { get; set; }
    }

    public partial class Toptags
    {
        public List<Tag> Tag { get; set; }
    }

    public partial class Tag
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
    }

    public partial class Wiki
    {
        public string Published { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
    }
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

                if (data.Track.Artist.Name.Equals(track.artist) && data.Track.Name.Equals(track.title) && data.Track.Album.Title.Equals(track.album))
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