
using ReportService.Api.Core.Enums;
using System.Text.Json.Serialization;

namespace ReportService.Api.Core.Domain.Concrete.Entities
{
    public class Report : MongoDbEntity
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReportStatusEnum ReportStatus { get; set; }
    }
}
