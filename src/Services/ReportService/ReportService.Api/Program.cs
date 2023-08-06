using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using RabbitMQ.Client;
using ReportService.Api.Core.Application.Repository;
using ReportService.Api.Core.Infrastructure.Repository;
using ReportService.Api.Extensions;
using ReportService.Api.Extensions.Registration.EventHandlerRegistration;
using ReportService.Api.IntegrationEvents.EventHandlers;
using ReportService.Api.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongoDbSettings(builder.Configuration);
builder.Services.ConfigureEventHandlers();

builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IReportDetailRepository, ReportDetailRepository>();

builder.Services.AddSingleton<IEventBus>(sp =>
{
    EventBusConfig config = new EventBusConfig()
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "IntegrationEvent",
        SubscriberClientAppName = "ReportService",
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
eventBus.Subscribe<RequestReportDetailIntegrationEvent, RequestReportDetailIntegrationEventHandler>();

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