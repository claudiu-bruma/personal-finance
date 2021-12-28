using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Transactions.MessageProcessor.Services.CardTransactionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Transactions.MessageProcessor.Configurations
{
    public static class DatabaseExtensions
    {
        public static void  ConfigureMongoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<CardTransactionStoreDatabaseSettings>(configuration.GetSection("CardTransactionDatabase"));
            serviceCollection.AddScoped<ICardTransactionService, CardTransactionService>();
        }
    }
}
