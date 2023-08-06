namespace ReportService.Api.Core.Domain.Concrete.Entities
{
    public class ReportDetail : MongoDbEntity
    {
        public Guid ReportId { get; set; }
        public string LocationInfo { get; set; } = string.Empty;
        public long UserCount { get; set; }
        public long PhoneNumberCount { get; set; }
    }
}
