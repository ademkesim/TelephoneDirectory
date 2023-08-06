using EventBus.Base.Events;

namespace DirectoryService.Api.IntegrationEvents.Events
{
    public class RequestReportIntegrationEvent : IntegrationEvent
    {
        public List<string>? Locations { get; set; }
        public Guid ReportId { get; set; }
        public RequestReportIntegrationEvent(Guid reportId, List<string>? locations)
        {
            ReportId = reportId;
            Locations = locations;
        }
    }
}
