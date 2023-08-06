using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Core.Domain.Concrete;
using DirectoryService.Api.Core.Domain.Concrete.DomainObjects;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DirectoryService.Api.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        protected readonly IMongoCollection<UserInfo> userCollection;
        protected readonly IMongoCollection<UserCommunicationInfo> userCommunicationCollection;
        private readonly MongoDbSettings settings;

        public UserRepository(ILoggerFactory loggerFactory, IOptions<MongoDbSettings> options)
        {
            _logger = loggerFactory.CreateLogger<UserRepository>();

            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            var db = client.GetDatabase(this.settings.Database);
            this.userCollection = db.GetCollection<UserInfo>(typeof(UserInfo).Name.ToLowerInvariant());
            this.userCommunicationCollection = db.GetCollection<UserCommunicationInfo>(typeof(UserCommunicationInfo).Name.ToLowerInvariant());
        }
        public async Task<UserInfo> AddUserAsync(UserInfo entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await userCollection.InsertOneAsync(entity, options);
            return entity;
        }

        public async Task<UserInfo> DeleteUserAsync(Guid id)
        {
            return await userCollection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public IQueryable<UserInfo> GetUsers(Expression<Func<UserInfo, bool>> predicate = null)
        {
            return predicate == null
                ? userCollection.AsQueryable()
                : userCollection.AsQueryable().Where(predicate);
        }
        public bool CheckUser(Guid id)
        {
            return (userCollection.Count(u => u.Id == id) > 0);
        }

        public UserDetailObject GetUserDetail(Guid id)
        {
            var userDetailObject = new UserDetailObject();
            var user = userCollection.AsQueryable().FirstOrDefault(x => x.Id == id);

            userDetailObject.Id = user.Id;
            userDetailObject.Name = user.Name;
            userDetailObject.Surname = user.Surname;
            userDetailObject.CompanyName = user.CompanyName;
            userDetailObject.Communications = userCommunicationCollection.AsQueryable().Where(x => x.UserInfoId == id).Select(u => new UserDetailCommunicationObject()
            {
                Id = u.Id,
                CommunicationInfo = u.CommunicationInfo,
                CommunicationType = u.CommunicationType,
            }).ToList();

            return userDetailObject;
         
        }
    }
}
