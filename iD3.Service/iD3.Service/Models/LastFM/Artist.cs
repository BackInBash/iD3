using System;

namespace iD3.Service.Models.LastFM
{
    public partial class Artist
    {
        public string Name { get; set; }
        public Guid Mbid { get; set; }
        public Uri Url { get; set; }
    }
}
