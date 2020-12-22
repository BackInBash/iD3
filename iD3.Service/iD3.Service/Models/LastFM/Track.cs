using System;

namespace iD3.Service.Models.LastFM
{
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
}
