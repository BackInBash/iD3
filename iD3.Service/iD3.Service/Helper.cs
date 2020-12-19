using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iD3.Service
{
    public class Helper
    {
        public static List<string> GetTrackPaths(string Path)
        {
            List<string> files = Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories).ToList();
            var res = from file in files.AsParallel() where file.EndsWith(".mp3") || file.EndsWith(".flac") || file.EndsWith(".wav") select file;
            return res.ToList();
        }
    }
}
