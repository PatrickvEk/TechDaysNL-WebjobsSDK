using Microsoft.Azure.WebJobs.Extensions.Timers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;

namespace WebJob1
{
    public class ConfigFactory
    {
        public static IHostBuilder CreateConfig()
        {
            var builder = new HostBuilder()
                .UseEnvironment("Development")
                .ConfigureWebJobs(b =>
                {
                    b.AddBuiltInBindings();
                    b.AddTimers();
                    // b.AddAzureStorageCoreServices()
                    //     .AddAzureStorage()
                    //     .AddServiceBus()
                    //     .AddEventHubs();
                })
                .ConfigureAppConfiguration(b =>
                {
                    // Adding command line as a configuration source
                    // b.AddCommandLine(args);
                })
                .ConfigureLogging((context, b) =>
                {
                    b.SetMinimumLevel(LogLevel.Debug);

                    // If this key exists in any config, use it to enable App Insights
                    string appInsightsKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
                    if (!string.IsNullOrEmpty(appInsightsKey))
                    {
                        // b.AddApplicationInsightsWebJobs(o => o.InstrumentationKey = appInsightsKey);
                    }
                    
                    b.AddConsole();
                    
                })
                .ConfigureServices(services =>
                {
                    // services.AddWebJobs(options => { });
                    // add some sample services to demonstrate job class DI
                    // services.AddSingleton<ISampleServiceA, SampleServiceA>();
                    // services.AddSingleton<ISampleServiceB, SampleServiceB>();
                    
                    //override monitor to use local file instead of azure cloud
                    services.AddSingleton<ScheduleMonitor, FileSystemScheduleMonitor>();
                })
                .UseConsoleLifetime();
            
            return builder;
            
            
            // // The environment is determined by the value of the "AzureWebJobsEnv" environment variable. When this
            // // is set to "Development", this property will return true.
            // if (config.IsDevelopment())
            // {
            //     config.UseDevelopmentSettings();
            // }
            //
            // //check app.config if your connectionstrings are setup properly
            //
            // //if the box has no internet connection you can disable the connectionstrings like this, remember you use any internet enabled functions then.
            // //config.DashboardConnectionString = null;
            // //config.StorageConnectionString = null;
            //
            // //normally use AppSetting and ConfigManager to get paths here.
            // FilesConfiguration filesConfig = new FilesConfiguration
            //     {
            //         RootPath = @"."
            //     };
            //
            // //enable what you like to use
            // config.UseFiles(filesConfig);
            // config.UseTimers();
            //
            // //lots of these extensions can be found on the internet on places like nuget
            // //https://www.nuget.org/packages?q=WebJobs.Extensions
            //
            // //used for local development without accidently sharing secrets
            // string storageString = Environment.GetEnvironmentVariable("AzureStorageAccount");
            //
            // //set to null to disable azure dashboard connection
            // // storageString = null;
            //     
            // config.DashboardConnectionString = storageString;
            // config.StorageConnectionString = storageString;
            //
            // if(storageString == null)
            // {
            //     config.HostId = Guid.NewGuid().ToString("N");
            // }
            //
            // //find servicebusString in Environment variable, otherwise find it in App.config
            // string serviceBusConnection = Environment.GetEnvironmentVariable("AzureServiceBus");
            // if (!string.IsNullOrWhiteSpace(serviceBusConnection))
            // {
            //     ServiceBusConfiguration sbConfig = new ServiceBusConfiguration
            //     {
            //         ConnectionString = serviceBusConnection
            //         //set other options if you like
            //     };
            //
            //     config.UseServiceBus(sbConfig);
            // }
            //
            // return config;
        }
    }
}