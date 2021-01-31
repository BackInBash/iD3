using iD3.Service.MetadataProvider;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

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
            Discogs disc = new Discogs();
            Local local = new Local();

            try
            {
                var data = await local.GetMetaData(track);

                var discogs = await disc.GetMetadata(data);

                if (discogs != null)
                {
                    logger.Debug("Recived Discogs Metadata");
                    data.genre = discogs;
                    logger.Debug("Discogs Metadata " + JsonConvert.SerializeObject(discogs, Formatting.Indented));
                    await local.SetID3(data, track);
                    logger.Info("Saved Discogs Metadata");
                }
            }
            catch (Exception e)
            {
                logger.Error("Get Metadata for Track {0}. Stack Trace: {1}", e.Message, e.StackTrace);
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
            try
            {
                logger.Info("Create Track " + path);
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    foreach (string file in Helper.GetTrackPaths(path))
                    {
                        try
                        {
                            Work.MergeMetadata(file, logger).GetAwaiter().GetResult();
                        }
                        catch (MetadataException ex)
                        {
                            logger.Error(ex, "Scheduler Metadata Exception Track is missing necessary Metadata" + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Scheduler " + ex.Message);
                        }
                    }
                }
                else
                {
                    if (Helper.IsTrack(path))
                        Work.MergeMetadata(path, logger).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Scheduler " + ex.Message);
            }

            // Sleep for 2 h
            Thread.Sleep(7200000);
        }
    }
}
