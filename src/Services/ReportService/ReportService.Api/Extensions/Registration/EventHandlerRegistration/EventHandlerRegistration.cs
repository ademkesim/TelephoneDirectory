using ReportService.Api.IntegrationEvents.EventHandlers;

namespace ReportService.Api.Extensions.Registration.EventHandlerRegistration
{
    public static class EventHandlerRegistration
    {
        public static IServiceCollection ConfigureEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<RequestReportDetailIntegrationEventHandler>();

            return services;
        }
    }
}
