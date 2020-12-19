using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iD3.Service.MetadataProvider
{
    public class MetadataException : Exception
    {
        public MetadataException()
        {

        }
    }
    public class Local
    {
        /// <summary>
        /// Return MetaData from Track
        /// </summary>
        /// <param name="Path">Path to Track</param>
        /// <returns></returns>
        public async Task<MetaTrack> GetMetaData(string path)
        {
            try
            {
                TagLib.Id3v2.Tag.DefaultVersion = 3;
                TagLib.Id3v2.Tag.ForceDefaultVersion = true;
                MetaTrack track = new MetaTrack();

                var tfile = TagLib.File.Create(path);
                // Set Title
                track.title = tfile.Tag.Title ?? throw new MetadataException();
                track.artist = tfile.Tag.FirstAlbumArtist ?? throw new MetadataException();

                // Set Artist
                track.artists = tfile.Tag.Performers;

                // Set Album Name
                track.album = tfile.Tag.Album;

                // Set Genre
                if (tfile.Tag.Genres.Length > 0)
                {
                    track.genre = tfile.Tag.Genres;
                }

                tfile.Dispose();
                return track;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + path);
            }
        }

        /// <summary>
        /// Set ID3 Tags
        /// </summary>
        public async Task SetID3(MetaTrack meta, string Path)
        {
            TagLib.Id3v2.Tag.DefaultVersion = 3;
            TagLib.Id3v2.Tag.ForceDefaultVersion = true;

            var tfile = TagLib.File.Create(Path);
            // Set Title
            tfile.Tag.Title = meta.title;

            // Set Artist
            if (meta.artists.Length <= 1)
            {
                tfile.Tag.Performers = new[] { meta.artist };
            }
            else
            {
                tfile.Tag.Performers = meta.artists;
            }

            // Set Album Name
            tfile.Tag.Album = meta.album;
            tfile.Tag.AlbumArtists = new[] { meta.artist };

            // Set Genre
            tfile.Tag.Genres = meta.genre ?? new string[0];

            tfile.Save();

        }
    }
}
