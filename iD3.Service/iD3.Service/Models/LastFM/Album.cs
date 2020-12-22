using System;
using System.Collections.Generic;

namespace iD3.Service.Models.LastFM
{
    public partial class Album
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }
        public List<Image> Image { get; set; }
    }
}
