using ReportService.Api.Core.Domain.Concrete.Entities;

namespace ReportService.Api.Core.Application.Repository
{
    public interface IReportDetailRepository
    {
        Task<ReportDetail> AddReportDetailAsync(ReportDetail reportDetail);
        IQueryable<ReportDetail> GetReportDetail(Guid reportId);
    }
}
