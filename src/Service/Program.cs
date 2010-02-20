using System.ServiceProcess;

namespace Dataweb.Dilab.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            var servicesToRun = new ServiceBase[] {
                new MainService()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}