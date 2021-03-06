﻿using iD3.Service.MetadataProvider;
using iD3.Service.Models.LastFM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace iD3.Service.Test.MetadataProvider
{
    [TestClass]
    public class Test_LastFM
    {
        List<MetaTrack> tracks;

        public Test_LastFM()
        {
            tracks = new List<MetaTrack>();
            tracks.Add(new MetaTrack() { album = "AM", artist = "Arctic Monkeys", artists = null, date = "", duration = 0, genre = null, id = "1234", isrc = "", remixArtist = "", title = "Do I Wanna Know?", year = "" });
        }

        [TestMethod]
        public void Test_Metadata()
        {
            LastFM lastFM = new LastFM();
            var data = lastFM.GetMetadata(tracks[0]).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(data, typeof(LastFmData));
        }
    }
}
