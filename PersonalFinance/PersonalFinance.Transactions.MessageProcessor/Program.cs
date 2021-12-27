// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Transactions.MessageProcessor;

ServiceProvider serviceProvider;
IConfiguration configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables()
.AddCommandLine(args)
.Build();

RegisterServices();
IServiceScope scope = serviceProvider.CreateScope();
scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();
DisposeServices();

void RegisterServices()
{
    var services = new ServiceCollection();
    services.AddAzureClients(builder =>
    {
        builder.AddServiceBusClient(configuration.GetConnectionString("ServiceBus"));
    });
    //services.AddSingleton<ICustomer, Customer>();
    services.AddSingleton<IConfiguration>(configuration);
    services.AddSingleton<ConsoleApplication>();
    serviceProvider = services.BuildServiceProvider(true);
}
void DisposeServices()
{
    if (serviceProvider == null)
    {
        return;
    }
    if (serviceProvider is IDisposable)
    {
        ((IDisposable)serviceProvider).Dispose();
    }
}