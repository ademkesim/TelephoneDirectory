using DirectoryService.Api.IntegrationEvents.EventHandlers;

namespace DirectoryService.Api.Extensions.Registration.EventHandlerRegistration
{
    public static class EventHandlerRegistration
    {
        public static IServiceCollection ConfigureEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<RequestReportIntegrationEventHandler>();

            return services;
        }
    }
}
