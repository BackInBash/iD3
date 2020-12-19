using System;
using System.IO;
using System.Threading;

namespace iD3.Service
{
    public class Work
    {
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
                        watcher.Changed += Event.OnChange;
                        watcher.Created += Event.OnCreate;
                        watcher.Deleted += Event.OnDelete;
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
    }
}
