using iD3.Service.MetadataProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace iD3.Service
{
    public class Event
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Delete Track Metadata Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnDelete(object source, FileSystemEventArgs e)
        {

        }

        /// <summary>
        /// Update Track Metadata Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnChange(object source, FileSystemEventArgs e)
        {

        }

        /// <summary>
        /// Create new Track Metadata Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnCreate(object source, FileSystemEventArgs e)
        {
            try
            {
                Logger.Info("Create Track " + e.FullPath);
                if (File.GetAttributes(e.FullPath).HasFlag(FileAttributes.Directory))
                {
                    foreach (string path in Helper.GetTrackPaths(e.FullPath))
                    {
                        try
                        {
                            Work.MergeMetadata(path, Logger).GetAwaiter().GetResult();
                        }
                        catch (MetadataException ex)
                        {
                            Logger.Error(ex, "OnRename Metadata Exception Track is missing necessary Metadata" + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex, "OnCreate " + ex.Message);
                        }
                    }
                }
                else
                {
                    Work.MergeMetadata(e.FullPath, Logger).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "OnCreate " + ex.Message);
            }
        }

        /// <summary>
        /// Rename Track Metadata Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnRename(object source, RenamedEventArgs e)
        {
            try
            {
                Logger.Info("Change Track " + e.FullPath);
                if (File.GetAttributes(e.FullPath).HasFlag(FileAttributes.Directory))
                {
                    foreach (string path in Helper.GetTrackPaths(e.FullPath))
                    {
                        try
                        {
                            Work.MergeMetadata(path, Logger).GetAwaiter().GetResult();
                        }
                        catch (MetadataException ex)
                        {
                            Logger.Error(ex, "OnRename Metadata Exception Track is missing necessary Metadata" + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex, "OnRename " + ex.Message);
                        }
                    }
                }
                else
                {
                    Work.MergeMetadata(e.FullPath, Logger).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "OnRename " + ex.Message);
            }
        }
    }
}
