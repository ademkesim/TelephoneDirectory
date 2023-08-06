using DirectoryService.Api.Core.Enums;

namespace DirectoryService.Api.Core.Domain.Concrete
{
    public class UserCommunicationInfo : MongoDbEntity
    {
        public CommunicationTypeEnum CommunicationType { get; set; }
        public string CommunicationInfo { get; set; } = string.Empty;
        public Guid UserInfoId { get; set; }
    }
}
