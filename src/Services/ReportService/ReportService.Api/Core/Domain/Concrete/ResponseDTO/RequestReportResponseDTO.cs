using ReportService.Api.Core.Domain.Abstract;

namespace ReportService.Api.Core.Domain.Concrete.ResponseDTO
{
    public class RequestReportResponseDTO : IResponseDTO
    {
        public Guid Id { get; set; }
    }
}
