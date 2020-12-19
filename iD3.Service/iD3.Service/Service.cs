using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace iD3.Service
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        private Task FSWatcher = null;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        [DllImport("kernel32")]
        static extern int AllocConsole();

        internal void TestStartupAndStop(string[] args)
        {
            AllocConsole();
            this.OnStart(args);
            Console.Read();
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            FSWatcher = Task.Run(() => Work.StartFSWatcher(Program.ReadPath(), Logger));
        }

        protected override void OnStop()
        {
            if (FSWatcher.Status == TaskStatus.Running)
            {
                Logger.Info("Stopping FS Watcher...");
                Program.Shutdown = true;
                //FSWatcher.Wait();
                //FSWatcher.Dispose();
            }
        }
    }
}
