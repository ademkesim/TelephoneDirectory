using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReportService.Api.Core.Application.Repository;
using ReportService.Api.Core.Constants;
using ReportService.Api.Core.Domain.Concrete.Entities;
using System.Linq.Expressions;

namespace ReportService.Api.Core.Infrastructure.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ILogger<ReportRepository> _logger;
        protected readonly IMongoCollection<Report> reportCollection;
        private readonly MongoDbSettings settings;

        public ReportRepository(ILoggerFactory loggerFactory, IOptions<MongoDbSettings> options)
        {
            _logger = loggerFactory.CreateLogger<ReportRepository>();

            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            var db = client.GetDatabase(this.settings.Database);
            this.reportCollection = db.GetCollection<Report>(typeof(Report).Name.ToLowerInvariant());
        }
        public async Task<Report> AddReportAsync(Report report)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await reportCollection.InsertOneAsync(report, options);

            _logger.LogInformation(ProjectConst.AddLogMessage, typeof(Report).Name);

            return report;

        }

        public async Task<Report> UpdateReportAsync(Report report)
        {
            _logger.LogInformation(ProjectConst.UpdateLogMessage, typeof(Report).Name);

            return await reportCollection.FindOneAndReplaceAsync(x => x.Id == report.Id, report);
        }

        public async Task<Report> GetReportByIdAsync(Guid id)
        {
            return await reportCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<Report> GetReports(Expression<Func<Report, bool>> predicate = null)
        {
            return predicate == null
                ? reportCollection.AsQueryable()
                : reportCollection.AsQueryable().Where(predicate);
        }
    }
}
