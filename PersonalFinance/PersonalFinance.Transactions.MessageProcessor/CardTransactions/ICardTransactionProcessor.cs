using Azure.Messaging.ServiceBus;

namespace PersonalFinance.Transactions.MessageProcessor.CardTransactions
{
    internal interface ICardTransactionProcessor : IAsyncDisposable
    {
        Task StartProcessingMessages();
        Task MessageHandler(ProcessMessageEventArgs args);
        Task ErrorHandler(ProcessErrorEventArgs args);
    }
}
