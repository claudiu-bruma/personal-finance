using Azure.Messaging.ServiceBus;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using PersonalFinance.Transactions.MessageProcessor.Entities;
using PersonalFinance.Transactions.MessageProcessor.Services.CardTransactionServices;

namespace PersonalFinance.Transactions.MessageProcessor.CardTransactions
{
    internal class CardTransactionProcessor : ICardTransactionProcessor
    {
        private ServiceBusClient _serviceBusClient;
        private ServiceBusProcessor _serviceBusProcessor;
        private ICardTransactionService _cardTransactionService;
        public CardTransactionProcessor(ServiceBusClient serviceBusClient ,ICardTransactionService cardTransactionService)
        {
            _serviceBusClient = serviceBusClient;
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(
                "newcardtransactions", 
                "MQPPRocessingSubscription", 
                new ServiceBusProcessorOptions());
            _serviceBusProcessor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;
            _cardTransactionService = cardTransactionService;

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
            await _cardTransactionService.CreateAsync(deserializedMessage);
            // complete the message. messages is deleted from the subscription. 
            await args.CompleteMessageAsync(args.Message);
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
