using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using PersonalFinance.Api.Models.Transactions;

namespace PersonalFinance.Api.Services.MessagingServices
{
    public interface IMessagingService
    {
        Task PublishNewCardTransaction(CardTransation cardTransation);
    }
    public class AzureServiceBusMessagingService : IMessagingService
    {
        private readonly ServiceBusClient _serviceBusClient; 
        private readonly ServiceBusSender _serviceBusSender;
        public AzureServiceBusMessagingService(ServiceBusClient serviceBusClient )
        {
            _serviceBusClient = serviceBusClient;
            _serviceBusSender = _serviceBusClient.CreateSender("newcardtransactions");
        }
        
        public async Task PublishNewCardTransaction(CardTransation cardTransation)
        {
            string messageBody = JsonConvert.SerializeObject(cardTransation);
            var cardTransactionSerilizedServiceBusMessage = new ServiceBusMessage(messageBody);
            await _serviceBusSender.SendMessageAsync(cardTransactionSerilizedServiceBusMessage);
        }

    }
}
