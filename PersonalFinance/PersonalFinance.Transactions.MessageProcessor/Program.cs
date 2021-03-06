// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonalFinance.Domain.Contracts;
using PersonalFinance.Transactions.MessageProcessor;
using PersonalFinance.Transactions.MessageProcessor.CardTransactions;
using PersonalFinance.Transactions.MessageProcessor.Configurations;
using MongoDB.Bson;
using MongoDB.Driver;

ServiceProvider serviceProvider;
IConfiguration configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", false)
.AddEnvironmentVariables()
.AddCommandLine(args)
.Build();

var hostBuilder = new HostBuilder()   
    .ConfigureServices(serviceCollection =>
    {
        RegisterServices(serviceCollection);
        serviceCollection.ConfigureMongoDb(configuration);
        serviceCollection.AddHostedService<ConsoleApplication>();
    });

using (var host = hostBuilder.Build())
{
    await host.RunAsync();
}

void RegisterServices(IServiceCollection services)
{ 
    services.AddAzureClients(builder =>
    {
        builder.AddServiceBusClient(configuration.GetConnectionString("ServiceBus"));
    });
    services.AddSingleton<ICardTransactionProcessor, CardTransactionProcessor>();
    services.AddSingleton<IConfiguration>(configuration);
    services.AddSingleton<ConsoleApplication>();
    services.AddSingleton<MessageProcessor>();
    serviceProvider = services.BuildServiceProvider(true);
}