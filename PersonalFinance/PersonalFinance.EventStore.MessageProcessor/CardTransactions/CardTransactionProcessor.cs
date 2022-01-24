using Azure.Messaging.ServiceBus;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using PersonalFinance.Domain.Contracts;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Transactions.MessageProcessor.Entities;

namespace PersonalFinance.Transactions.MessageProcessor.CardTransactions
{
    internal class CardTransactionProcessor : ICardTransactionProcessor
    {
        private ServiceBusClient _serviceBusClient;
        private ServiceBusProcessor _serviceBusProcessor; 
        public CardTransactionProcessor(ServiceBusClient serviceBusClient )
        {
            _serviceBusClient = serviceBusClient;
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(
                "newcardtransactions",
                "MQPPRocessingSubscription",
                new ServiceBusProcessorOptions());
            _serviceBusProcessor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            _serviceBusProcessor.ProcessErrorAsync += ErrorHandler; 

        }

        public async Task StartProcessingMessages()
        {
            // start processing 
            await _serviceBusProcessor.StartProcessingAsync();
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body} ");

            var deserializedMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<CardTransactionEntity>(body);
            if (deserializedMessage is not null)
            {
                 
                // complete the message. messages is deleted from the subscription. 
                await args.CompleteMessageAsync(args.Message);
            }

        }

        // handle any errors when receiving messages
        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await _serviceBusProcessor.StopProcessingAsync();
            await _serviceBusProcessor.DisposeAsync();
            await _serviceBusClient.DisposeAsync();
        }
    }
}
