using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ReportService.Api.Core.Domain.Abstract;

namespace ReportService.Api.Core.Domain.Concrete.Entities
{
    public abstract class MongoDbEntity : IEntity<Guid>
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
