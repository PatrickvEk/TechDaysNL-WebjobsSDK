namespace WebJob1.Demos
{
    using System.IO;
    using Microsoft.Azure.WebJobs;

    public class InteractWithAzure
    {
        //Ctrl+T Azure.webjobs.*Attribute

        //demonstration of output bindings
        public static void FileUploadToAzure(
            [FileTrigger(@"Upload\{fileName}", "*")] Stream localInput, string fileName,
            [Blob(@"uploads/{fileName}", FileAccess.Write)] Stream azureOutput,
            TextWriter log)
        {
            log.WriteLine($"Uploading file: {fileName}");

            //this is all it takes to upload a file to Azure
            localInput.CopyTo(azureOutput);

            log.WriteLine($"Uploading finished");
        }








        //optional queue output message as C# object
        //Poco's can be used for numerous output bindings
        public static void FileProcessQueuePocoOutput(
            [FileTrigger(@"Queue\{fileName}", "*")] Stream localInput, string fileName,
            [Queue("azurequeue")] out MyPoco azureQueueMessage,
            TextWriter log)
        {
            azureQueueMessage = null;

            if (localInput.Length > 20)
            {
                azureQueueMessage = new MyPoco
                {
                    Message = "Large file uploaded",
                    FileName = fileName
                };
            }
        }

        public class MyPoco //Poco = Plain old C# object
        {
            public string Message { get; set; }
            public string FileName { get; set; }
        }





        //optional queue output as String
        [Disable]
        public static void FileProcessQueueOutput(
            [FileTrigger(@"Queue\{fileName}", "*")] Stream localInput, string fileName,
            [Queue("azurequeue")] out string azureQueueMessage,
            TextWriter log)
        {
            azureQueueMessage = null;

            if (localInput.Length > 20)
            {
                azureQueueMessage = "Large file uploaded";
            }
        }




        //Azure blob input - Local File output
        public static void GetFilesFromAzure(
            [BlobTrigger("filesforpremise/{fileName}")] Stream azureInput, string fileName,
            [File("download/{fileName}", FileAccess.Write)] Stream localOutput
            )
        {
            //copy to local
            azureInput.CopyTo(localOutput);
        }
    }
}
