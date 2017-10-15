namespace WebJob1
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Files;

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
                    RootPath = @"C:\Users\p.vanek\Desktop\TechDays\FileWatch"
                };
           
            //enable what you like to use
            config.UseFiles(filesConfig);
            config.UseTimers();

            config.UseServiceBus();

            //lots of these extensions can be found on the internet on places like nuget
            //https://www.nuget.org/packages?q=WebJobs.Extensions

            return config;
        }
    }
}