using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Transactions.MessageProcessor
{
    internal class MessageProcessor
    { 
        // the client that owns the connection and can be used to create senders and receivers
        private ServiceBusClient _serviceBusClient;

        // the processor that reads and processes messages from the subscription
        private ServiceBusProcessor _processor;

        public MessageProcessor(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body} ");

            // complete the message. messages is deleted from the subscription. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task SetupProcessingCardTransactions()
        {
            // create a processor that we can use to process the messages
            _processor = _serviceBusClient.CreateProcessor("newcardtransactions", "MQPPRocessingSubscription", new ServiceBusProcessorOptions());

            try
            {
                // add handler to process messages
                _processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                _processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await _processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await _processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await _processor.DisposeAsync();
                await _serviceBusClient.DisposeAsync();
            }
        }
    }
}
