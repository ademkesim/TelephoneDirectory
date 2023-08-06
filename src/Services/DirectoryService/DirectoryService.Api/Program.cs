using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Extensions;
using DirectoryService.Api.Extensions.Registration.EventHandlerRegistration;
using DirectoryService.Api.Infrastructure.Repository;
using DirectoryService.Api.IntegrationEvents.EventHandlers;
using DirectoryService.Api.IntegrationEvents.Events;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongoDbSettings(builder.Configuration);
builder.Services.AddTransient<IUserCommunicationRepository, UserCommunicationRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.ConfigureEventHandlers();

builder.Services.AddSingleton<IEventBus>(sp =>
{
    EventBusConfig config = new EventBusConfig()
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "IntegrationEvent",
        SubscriberClientAppName = "DirectoryService",
        EventBusType = EventBusType.RabbitMQ,
        Connection = new ConnectionFactory()
        {
            HostName = "c_rabbitmq"
        }
    };

    return EventBusFactory.Create(config, sp);
});


var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<RequestReportIntegrationEvent, RequestReportIntegrationEventHandler>();

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

