using Azure.Messaging.ServiceBus;

namespace PersonalFinance.Domain.Contracts
{
    public interface ICardTransactionProcessor : IAsyncDisposable
    {
        Task StartProcessingMessages();
        Task MessageHandler(ProcessMessageEventArgs args);
        Task ErrorHandler(ProcessErrorEventArgs args);
    }
}
