using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;

namespace iD3.Service
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        private Task Worker = null;
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
            if (args[0].Equals("-schedule", StringComparison.OrdinalIgnoreCase))
            {
                Logger.Info("Start Scheduler");
                Worker = Task.Run(() => Work.StartScheduler(Program.ReadPath(), Logger));
            }
            else
            {
                Logger.Info("Start FS Watcher");
                Worker = Task.Run(() => Work.StartFSWatcher(Program.ReadPath(), Logger));
            }
        }

        protected override void OnStop()
        {
            if (Worker.Status == TaskStatus.Running)
            {
                Logger.Info("Stopping Worker Task...");
                Program.Shutdown = true;
                //FSWatcher.Wait();
                //FSWatcher.Dispose();
            }
        }
    }
}
