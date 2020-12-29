using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace WebJob1.Demos
{
    public class OfflineDemo
    {
        // [Disable]
        // public static void ProcessFileOffline(
        //     [FileTrigger(@"TextFolder\{fileName}", "*", 
        //     WatcherChangeTypes.Created, autoDelete: false)]
        //     TextReader fileInput, string fileName,
        //
        //     TextWriter log)
        // {
        //     log.WriteLine($"Processing file: {fileName}");
        //
        //     //do your processing here
        //     string contents = fileInput.ReadToEnd();
        //
        //     log.WriteLine($"Contents: {contents}");
        // }


        //Ctrl+T webjobs.*triggerAttribute

        //Timers won't overlap
        //cron timer can be used

        //Async can be used in any function.
        private const string CronScheduleEvery5Minutes = "0 */5 * * * *";
        private const string Every5SecondTimeout = "00:00:05";

       public static async Task ProcessTimer(
            [TimerTrigger(Every5SecondTimeout, RunOnStartup = true, UseMonitor = true)] TimerInfo timer, 
            TextWriter logger,
            CancellationToken hostsWantsToShutdown)
        {
            logger.WriteLine("Do something here");
       
            //Execute your code here.
       
            TimeSpan timeout = TimeSpan.FromSeconds(1);
            await Task.Delay(timeout, hostsWantsToShutdown);
          
            logger.WriteLine("Logging something here");
        }
    }
}
