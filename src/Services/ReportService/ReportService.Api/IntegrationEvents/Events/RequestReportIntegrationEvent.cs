using EventBus.Base.Events;

namespace ReportService.Api.IntegrationEvents.Events
{
    public class RequestReportIntegrationEvent : IntegrationEvent
    {
        public Guid ReportId { get; set; }
        public RequestReportIntegrationEvent(Guid reportId)
        {
            ReportId = reportId;
        }
    }
}
