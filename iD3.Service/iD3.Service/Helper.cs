using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        /// <summary>
        /// Check for Audio File Ending
        /// </summary>
        /// <param name="path">Path to posible Audio File</param>
        /// <returns></returns>
        public static bool IsTrack(string path)
        {
            List<string> FileEndings = new List<string>() { ".mp3", ".flac", ".wav" };
            if(FileEndings.Exists(x=> path.EndsWith(x)))
            {
                return true;
            }
            return false;
        }
    }
}
