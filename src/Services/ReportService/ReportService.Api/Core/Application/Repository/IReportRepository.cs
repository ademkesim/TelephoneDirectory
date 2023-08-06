using ReportService.Api.Core.Domain.Concrete.Entities;
using System.Linq.Expressions;

namespace ReportService.Api.Core.Application.Repository
{
    public interface IReportRepository
    {
        Task<Report> AddReportAsync(Report report);
        Task<Report> UpdateReportAsync(Report report);
        Task<Report> GetReportByIdAsync(Guid id);
        IQueryable<Report> GetReports(Expression<Func<Report, bool>> predicate = null);
    }
}
