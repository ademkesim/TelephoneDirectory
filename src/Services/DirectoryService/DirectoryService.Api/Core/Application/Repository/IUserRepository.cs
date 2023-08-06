using DirectoryService.Api.Core.Domain.Abstract;
using DirectoryService.Api.Core.Domain.Concrete;
using DirectoryService.Api.Core.Domain.Concrete.DomainObjects;
using System.Linq.Expressions;

namespace DirectoryService.Api.Core.Application.Repository
{
    public interface IUserRepository
    {
        Task<UserInfo> AddUserAsync(UserInfo entity);
        Task<UserInfo> DeleteUserAsync(Guid id);
        IQueryable<UserInfo> GetUsers(Expression<Func<UserInfo, bool>> predicate = null);
        UserDetailObject GetUserDetail(Guid id);
        bool CheckUser(Guid id);
    }
}
