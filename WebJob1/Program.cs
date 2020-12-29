using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WebJob1
{
    class Program
    {
        static void Main()
        {
            IHostBuilder builder = ConfigFactory.CreateConfig();
            
            IHost host = builder.Build();
            host.Run();
        }
    }
}
