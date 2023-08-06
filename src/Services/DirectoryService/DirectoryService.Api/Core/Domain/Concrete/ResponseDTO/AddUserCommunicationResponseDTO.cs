using DirectoryService.Api.Core.Domain.Abstract;

namespace DirectoryService.Api.Core.Domain.Concrete.ResponseDTO
{
    public class AddUserCommunicationResponseDTO : IResponseDTO
    {
        public Guid Id { get; set; }
    }
}
