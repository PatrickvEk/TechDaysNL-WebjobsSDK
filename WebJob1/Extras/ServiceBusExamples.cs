﻿namespace WebJob1.Extras
{
    using System;
    using System.IO;
    using System.Threading;
    using Microsoft.Azure.WebJobs;
    using Microsoft.ServiceBus.Messaging;

    //remove attribute to enable examples
   public class ServiceBusSessionExamples
    {
        //IMPORTANT: make sure responsequeue has "Requires sessions" enabled. (Might require Standard pricing tier for azure service bus)

        //use ServiceBusExplorer to create request and responsequeues
        //https://github.com/paolosalvatori/ServiceBusExplorer/releases


        //optional, make messages expire/deleted early when they are not relevant anymore (e.g. after an hour)
        //using response sessions can be used to offload the work from a httprequest and post back to the right client by using sessioned responses.
        //if requested I can create more example demo's for the community, or make one yourself and create pull request ;)


        //currently there is no implementation to receive from a session queue, only to send to a session.
        //feature request is here https://github.com/Azure/azure-webjobs-sdk/issues/529

        //handle general request and respond to session (this could run on a clientmachine for example)
        public static void HandleRequestAndRespondWithSession(
            [ServiceBusTrigger("requestQueue", AccessRights.Listen)] BrokeredMessage inboundMessage,
            [ServiceBus("responseQueue", AccessRights.Send)] out BrokeredMessage outboundMessage,
            TextWriter log)
        {
            //if body type is unknown or for debugging use GetBody<Stream>();
            string requestString = inboundMessage.GetBody<string>();
            
            //do message handling code here.
            object myResponse = "MyReponseData";

            log.WriteLine(requestString);
            log.WriteLine(myResponse);

            //get sessionID from request message
            string repySession = inboundMessage.ReplyToSessionId;
           
            outboundMessage = new BrokeredMessage(myResponse)
            {
                SessionId = repySession
            };
        }

        [Disable]
        //generates requests for demo purposes
        public static void GenerateRequests(
            [TimerTrigger("00:01:00", RunOnStartup = true, UseMonitor = false)] TimerInfo timer,
            [ServiceBus("requestQueue", AccessRights.Send)] out BrokeredMessage requestMessage,
            TextWriter logger,
            CancellationToken hostsWantsToShutdown)
        {
            object serializableObject = "MyRequestBody"; //could be anything, prefer to use C# object
            requestMessage = new BrokeredMessage(serializableObject);

            //session ID generated by client this is to follow your request and find correlating response
            //can be seen as 'operationID' to track request.
            requestMessage.ReplyToSessionId = Guid.NewGuid().ToString();
        }
    }
}
