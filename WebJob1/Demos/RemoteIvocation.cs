using System.Diagnostics;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace WebJob1.Demos
{
    public class RemoteIvocation
    {
        [NoAutomaticTrigger]
        public static void InvokeCommand(string myParam, TextWriter log)
        {
            log.WriteLine($"My received parameter is: {myParam}");
        }


        [NoAutomaticTrigger]
        public static void InvokeCommandViaJson(string commandString, TextWriter log)
        {
            MyDto command = JsonConvert.DeserializeObject<MyDto>(commandString);

            log.WriteLine($"Id:{command.Id}");
            log.WriteLine($"Name:{command.Name}");
        }












        [NoAutomaticTrigger]
        public static void InvokeTerminalCommand(string command, TraceWriter log)
        {
            log.Verbose($"Starting command: {command}");

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = $"/c \"{command}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string outputLine = proc.StandardOutput.ReadLine();
                log.Info(outputLine);
            }

            //if (!proc.StandardError.EndOfStream)
            {
                string errorOutput = proc.StandardError.ReadToEnd();
                log.Error(errorOutput);
            }

            log.Verbose($"Finished command: {command}");
        }



        //won't work, webjobs won't parse the string to a c# object, you'll have to do that yourself like above
        [Disable]
        public static void InvokeCommandViaDtoError(MyDto command, TextWriter log)
        {
            log.WriteLine($"Id:{command.Id}");
            log.WriteLine($"Name:{command.Name}");
        }

        public class MyDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}