using EventBus.Base.Events;

namespace ReportService.Api.IntegrationEvents.Events
{
    public class RequestReportDetailIntegrationEvent : IntegrationEvent
    {
        public Guid ReportId { get; set; }
        public List<RequestReportDetailObject> RequestReportDetails { get; set; }
    }
    public class RequestReportDetailObject
    {
        public string LocationInfo { get; set; } = string.Empty;
        public long UserCount { get; set; }
        public long PhoneNumberCount { get; set; }
    }
}
