using ReportService.Api.Core.Domain.Abstract;

namespace ReportService.Api.Core.Domain.Concrete.RequestDTO
{
    public class RequestReportRequestDTO : IRequestDTO
    {
        public List<string>? Locations { get; set; }
    }
}
