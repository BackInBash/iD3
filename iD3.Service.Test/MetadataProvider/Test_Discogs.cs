using iD3.Service.MetadataProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iD3.Service.Test.MetadataProvider
{
    [TestClass]
    public class Test_Discogs
    {
        List<MetaTrack> tracks;

        public Test_Discogs()
        {
            tracks = new List<MetaTrack>();
            tracks.Add(new MetaTrack() { album = "AM", artist = "Arctic Monkeys", artists = null, date = "", duration = 0, genre = null, id = "1234", isrc = "", remixArtist = "", title = "Do I Wanna Know?", year = "2013" });
        }

        [TestMethod]
        public void Test_Search()
        {
            Discogs discogs = new Discogs();
            var data = discogs.Search(tracks.First()).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(data, typeof(SearchResult));
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void Test_Metadata()
        {
            Discogs discogs = new Discogs();
            var data = discogs.GetMetadata(tracks.First()).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(data, typeof(string[]));
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void Test_Search_Exception()
        {
            Discogs discogs = new Discogs();
            try
            {
                for (int i = 0; i < 62; i++)
                {
                    _ = discogs.Search(tracks.First()).GetAwaiter().GetResult();
                }
            }
            catch(Exception)
            {
                Assert.IsTrue(true);
            }
            Assert.IsTrue(false);
        }
    }
}
