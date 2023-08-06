using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReportService.Api.Core.Application.Repository;
using ReportService.Api.Core.Domain.Concrete.Entities;

namespace ReportService.Api.Core.Infrastructure.Repository
{
    public class ReportDetailRepository : IReportDetailRepository
    {
        private readonly ILogger<ReportRepository> _logger;
        protected readonly IMongoCollection<ReportDetail> reportDetailCollection;
        private readonly MongoDbSettings settings;

        public ReportDetailRepository(ILoggerFactory loggerFactory, IOptions<MongoDbSettings> options)
        {
            _logger = loggerFactory.CreateLogger<ReportRepository>();

            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            var db = client.GetDatabase(this.settings.Database);
            this.reportDetailCollection = db.GetCollection<ReportDetail>(typeof(ReportDetail).Name.ToLowerInvariant());
        }

        public async Task<ReportDetail> AddReportDetailAsync(ReportDetail reportDetail)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await reportDetailCollection.InsertOneAsync(reportDetail, options);

            return reportDetail;
        }

        public IQueryable<ReportDetail> GetReportDetail(Guid reportId)
        {
            return reportDetailCollection.AsQueryable().Where(x => x.ReportId == reportId);
        }
    }
}
