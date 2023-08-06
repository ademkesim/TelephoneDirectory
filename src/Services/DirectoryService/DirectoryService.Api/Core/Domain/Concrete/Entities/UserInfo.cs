using DirectoryService.Api.Core.Domain.Abstract;
using DirectoryService.Api.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DirectoryService.Api.Core.Domain.Concrete
{
    public class UserInfo : MongoDbEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }
}
