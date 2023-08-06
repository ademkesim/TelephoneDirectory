using DirectoryService.Api.Core.Domain.Concrete;
using DirectoryService.Api.Core.Domain.Concrete.RequestDTO;
using System.Linq.Expressions;

namespace DirectoryService.Api.Core.Application.Repository
{
    public interface IUserCommunicationRepository
    {
        Task<UserCommunicationInfo> AddUserCommunicationAsync(AddUserCommunicationRequestDTO request);
        Task<UserCommunicationInfo> DeleteUserCommunicationAsync(Guid id);
        IQueryable<UserCommunicationInfo> GetUserCommunications(Expression<Func<UserCommunicationInfo, bool>> predicate = null);
        bool CheckUserCommunication(Guid id);
    }
}
