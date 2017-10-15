using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    using Microsoft.Azure.WebJobs;
    using WebJob1;

    public partial class Service1 : ServiceBase
    {
        private readonly JobHost jobHost;

        public Service1()
        {
            InitializeComponent();
            JobHostConfiguration config = ConfigFactory.CreateConfig();
            jobHost = new JobHost(config);
        }

        protected override void OnStart(string[] args)
        {
            jobHost.Start();
        }

        protected override void OnStop()
        {
            jobHost.Stop();
        }
    }
}
