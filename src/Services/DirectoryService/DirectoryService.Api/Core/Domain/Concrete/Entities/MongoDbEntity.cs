using DirectoryService.Api.Core.Domain.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DirectoryService.Api.Core.Domain.Concrete
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
