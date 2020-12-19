using NLog;
using System;
using System.Configuration;
using System.ServiceProcess;

namespace iD3.Service
{
    static class Program
    {
        public static string ReadPath()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings["Path"] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                return null;
            }
        }

        private static void LogSetup()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "IndexService.log", Layout = "${longdate} ${message} ${exception:format=tostring}" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);
            // Apply config           
            NLog.LogManager.Configuration = config;
        }

        public static bool Shutdown = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            LogSetup();
            NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
            Logger.Info("Start Service...");

            if (Environment.UserInteractive)
            {
                Service service1 = new Service();
                service1.TestStartupAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                new Service()
            };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
