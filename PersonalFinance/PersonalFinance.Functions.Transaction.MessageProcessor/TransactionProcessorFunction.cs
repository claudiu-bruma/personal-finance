using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace PersonalFinance.Functions.Transaction.MessageProcessor
{
    public class TransactionProcessorFunction
    {
        private readonly ILogger<TransactionProcessorFunction> _logger;

        public TransactionProcessorFunction(ILogger<TransactionProcessorFunction> log)
        {
            _logger = log;
        }

        [FunctionName("TransactionProcessorFunction")]
        public void Run([ServiceBusTrigger("newcardtransactions", "FunctionMethodSubscription", Connection = "AsbConnectionString")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
