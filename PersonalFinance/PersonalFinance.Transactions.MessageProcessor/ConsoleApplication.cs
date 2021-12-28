using Microsoft.Extensions.Hosting;  
namespace PersonalFinance.Transactions.MessageProcessor
{
    internal class ConsoleApplication : IHostedService
    {
        private MessageProcessor _messageProcessor;
        public ConsoleApplication(MessageProcessor messageProcessor )
        {
            _messageProcessor= messageProcessor;
        } 

        public async Task StartAsync(CancellationToken cancellationToken)
        { 
            await _messageProcessor.SetupProcessingCardTransactions();
            
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _messageProcessor.DisposeAsync();
            
        }
    }
}
