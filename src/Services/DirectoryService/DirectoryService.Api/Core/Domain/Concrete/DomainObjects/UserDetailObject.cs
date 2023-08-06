using DirectoryService.Api.Core.Enums;
using System.Text.Json.Serialization;

namespace DirectoryService.Api.Core.Domain.Concrete.DomainObjects
{
    public class UserDetailObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public List<UserDetailCommunicationObject> Communications { get; set; }

    }
    public class UserDetailCommunicationObject
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CommunicationTypeEnum CommunicationType { get; set; }
        public string CommunicationInfo { get; set; }

    }
}
