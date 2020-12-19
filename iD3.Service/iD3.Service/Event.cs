using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iD3.Service
{
    public class Event
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Delete Track from DB Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnDelete(object source, FileSystemEventArgs e)
        {
           
        }

        /// <summary>
        /// Update Track in DB Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnChange(object source, FileSystemEventArgs e)
        {
            
        }

        /// <summary>
        /// Create new Track in DB Event
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
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "OnCreate " + ex.Message);
            }
        }

        /// <summary>
        /// Rename Track in DB Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnRename(object source, RenamedEventArgs e)
        {
        }
    }
}
