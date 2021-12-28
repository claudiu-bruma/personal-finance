using PersonalFinance.Transactions.MessageProcessor.CardTransactions;

namespace PersonalFinance.Transactions.MessageProcessor
{
    internal class MessageProcessor : IAsyncDisposable
    {
        private readonly ICardTransactionProcessor _cardTransactionProcessor;

        public MessageProcessor(ICardTransactionProcessor cardTransactionProcessor)
        {
            _cardTransactionProcessor = cardTransactionProcessor;
        }

        public async Task SetupProcessingCardTransactions()
        {
            await _cardTransactionProcessor.StartProcessingMessages();
        }

        public async ValueTask DisposeAsync()
        {
            await _cardTransactionProcessor.DisposeAsync();
        }
    }
}
