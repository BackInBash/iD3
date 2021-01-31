using iD3.Service.MetadataProvider;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iD3.Service
{
    public class Work
    {
        /// <summary>
        /// Merge ID3 Tags
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public async static Task MergeMetadata(string track, NLog.Logger logger)
        {
            LastFM lastFM = new LastFM();
            Local local = new Local();

            var data = await local.GetMetaData(track);
            var lastFMdata = await lastFM.GetMetadata(data);

            if(lastFMdata != null)
            {
                logger.Debug("Recived LastFM Metadata");
                if (data.genre == null)
                {
                    data.genre = lastFMdata.Track.Toptags.Tag.Select(x => x.Name).ToArray();
                    logger.Debug("LastFM Metadata "+JsonConvert.SerializeObject(lastFMdata, Formatting.Indented));
                    await local.SetID3(data, track);
                    logger.Info("Saved LastFM Metadata");
                }
            }

        }
        /// <summary>
        /// File System Watcher Task
        /// </summary>
        /// <param name="path"></param>
        //[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void StartFSWatcher(string path, NLog.Logger logger)
        {
            try
            {
                using (FileSystemWatcher watcher = new FileSystemWatcher())
                {
                    try
                    {
                        watcher.Path = path;
                        watcher.IncludeSubdirectories = true;

                        // Watch for changes in LastAccess and LastWrite times, and
                        // the renaming of files or directories.
                        watcher.NotifyFilter = NotifyFilters.LastAccess
                                             | NotifyFilters.LastWrite
                                             | NotifyFilters.FileName
                                             | NotifyFilters.DirectoryName;

                        // Only watch text files.
                        watcher.Filter = "*.*";

                        // Add event handlers.
                        watcher.Created += Event.OnCreate;
                        watcher.Renamed += Event.OnRename;

                        // Begin watching.
                        watcher.EnableRaisingEvents = true;
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, e.Message);
                    }
                    while (Program.Shutdown == false)
                    {
                        Thread.Sleep(1);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, ex.Message);
            }
        }

        /// <summary>
        /// Start Scheduler Task
        /// </summary>
        /// <param name="path"></param>
        /// <param name="logger"></param>
        public static void StartScheduler(string path, NLog.Logger logger)
        {

        }
    }
}
