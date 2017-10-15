using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJob1
{
    using Microsoft.Azure.WebJobs;

    class Program
    {
        static void Main()
        {
            JobHostConfiguration config = ConfigFactory.CreateConfig();

            var host = new JobHost(config);

            // If exception occures here, review your configuration. Either App.config or ConfigFactory class
            host.RunAndBlock();
        }
    }
}
