using System.ServiceProcess;

namespace WindowsService
{
    using ServiceProcess.Helpers;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //make sure a webjobsSDK service is set to auto-restart (first occurrence, second occurrence, subsequent occurence)
            //this ensures reliability when outages occur (e.g. internet connection)

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            
            //ServiceBase.Run(ServicesToRun);

            //servicehelper extentions
            ServicesToRun.LoadServices(); 
        }
    }
}
