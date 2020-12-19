namespace iD3.Service
{
    public class MetaTrack
    {
        /// <summary>
        /// Deezer SongID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Track Titel
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Artist
        /// </summary>
        public string artist { get; set; }

        /// <summary>
        /// Duration in Sec.
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// Genre
        /// </summary>
        public string[]? genre { get; set; }

        /// <summary>
        /// Remix Artist
        /// </summary>
        public string? remixArtist { get; set; }

        /// <summary>
        /// Release Year
        /// </summary>
        public string year { get; set; }

        /// <summary>
        /// Release Date
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// Album Name
        /// </summary>
        public string album { get; set; }

        /// <summary>
        /// Artsts
        /// </summary>
        public string[]? artists { get; set; }

        /// <summary>
        /// Album Art
        /// </summary>
        //public string albumart { get; set; }

        /// <summary>
        /// ISRC ID
        /// </summary>
        public string isrc { get; set; }
    }
}
