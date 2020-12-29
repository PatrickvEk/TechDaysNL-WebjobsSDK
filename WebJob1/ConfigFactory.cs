namespace WebJob1
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Files;
    using Microsoft.Azure.WebJobs.ServiceBus;

    public class ConfigFactory
    {
        public static JobHostConfiguration CreateConfig()
        {
            var config = new JobHostConfiguration();

            // The environment is determined by the value of the "AzureWebJobsEnv" environment variable. When this
            // is set to "Development", this property will return true.
            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            //check app.config if your connectionstrings are setup properly

            //if the box has no internet connection you can disable the connectionstrings like this, remember you use any internet enabled functions then.
            //config.DashboardConnectionString = null;
            //config.StorageConnectionString = null;

            //normally use AppSetting and ConfigManager to get paths here.
            FilesConfiguration filesConfig = new FilesConfiguration
                {
                    RootPath = @"."
                };
           
            //enable what you like to use
            config.UseFiles(filesConfig);
            config.UseTimers();

            //lots of these extensions can be found on the internet on places like nuget
            //https://www.nuget.org/packages?q=WebJobs.Extensions

            //used for local development without accidently sharing secrets
            string storageString = Environment.GetEnvironmentVariable("AzureStorageAccount");
            
            //set to null to disable azure dashboard connection
            // storageString = null;
                
            config.DashboardConnectionString = storageString;
            config.StorageConnectionString = storageString;
            
            if(storageString == null)
            {
                config.HostId = Guid.NewGuid().ToString("N");
            }
            
            //find servicebusString in Environment variable, otherwise find it in App.config
            string serviceBusConnection = Environment.GetEnvironmentVariable("AzureServiceBus");
            if (!string.IsNullOrWhiteSpace(serviceBusConnection))
            {
                ServiceBusConfiguration sbConfig = new ServiceBusConfiguration
                {
                    ConnectionString = serviceBusConnection
                    //set other options if you like
                };

                config.UseServiceBus(sbConfig);
            }

            return config;
        }
    }
}