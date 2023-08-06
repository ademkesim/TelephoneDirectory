using DirectoryService.Api.Core.Domain.Abstract;
using DirectoryService.Api.Core.Enums;

namespace DirectoryService.Api.Core.Domain.Concrete.RequestDTO
{
    public class AddUserCommunicationRequestDTO : IRequestDTO
    {
        public CommunicationTypeEnum CommunicationType { get; set; }
        public string CommunicationInfo { get; set; } = string.Empty;
        public Guid UserInfoId { get; set; }
    }
}
