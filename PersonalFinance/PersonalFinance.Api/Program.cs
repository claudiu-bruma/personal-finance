using Microsoft.Extensions.Azure;
using PersonalFinance.Api.Services.MessagingServices;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
ConfigureServices(builder.Services);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


 void ConfigureServices(IServiceCollection services)
{
    services.AddAzureClients(builder =>
    {
        builder.AddServiceBusClient(configuration.GetConnectionString("ServiceBus"));
    });
    services.AddScoped<IMessagingService, AzureServiceBusMessagingService>();
}