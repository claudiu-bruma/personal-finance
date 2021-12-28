using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Transactions.MessageProcessor.Configurations
{
    public class CardTransactionStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CardTransactionCollectionName { get; set; } = null!;
    }
}
