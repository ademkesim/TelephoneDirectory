using DirectoryService.Api.Core.Domain.Abstract;
using DirectoryService.Api.Core.Domain.Concrete.DomainObjects;

namespace DirectoryService.Api.Core.Domain.Concrete.ResponseDTO
{
    public class UserDetailResponseDTO : IResponseDTO
    {
        public UserDetailObject UserDetail { get; set; }
    }
}
