using DirectoryService.Api.Core.Domain.Abstract;

namespace DirectoryService.Api.Core.Domain.Concrete.RequestDTO
{
    public class AddUserRequestDTO : IRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }
}
