using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Core.Constants;
using DirectoryService.Api.Core.Domain.Concrete;
using DirectoryService.Api.Core.Domain.Concrete.RequestDTO;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DirectoryService.Api.Infrastructure.Repository
{
    public class UserCommunicationRepository : IUserCommunicationRepository
    {
        private readonly ILogger<UserCommunicationRepository> _logger;
        protected readonly IMongoCollection<UserCommunicationInfo> userCommunicationCollection;
        protected readonly IMongoCollection<UserInfo> userCollection;
        private readonly MongoDbSettings settings;

        public UserCommunicationRepository(ILoggerFactory loggerFactory, IOptions<MongoDbSettings> options)
        {
            _logger = loggerFactory.CreateLogger<UserCommunicationRepository>();

            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            var db = client.GetDatabase(this.settings.Database);
            this.userCommunicationCollection = db.GetCollection<UserCommunicationInfo>(typeof(UserCommunicationInfo).Name.ToLowerInvariant());
            this.userCollection = db.GetCollection<UserInfo>(typeof(UserInfo).Name.ToLowerInvariant());
        }

        public async Task<UserCommunicationInfo> AddUserCommunicationAsync(AddUserCommunicationRequestDTO request)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };

            var addedData = new UserCommunicationInfo()
            {
                UserInfoId = request.UserInfoId,
                CommunicationType = request.CommunicationType,
                CommunicationInfo = request.CommunicationInfo,
            };

            await userCommunicationCollection.InsertOneAsync(addedData, options);

            _logger.LogInformation(ProjectConst.AddLogMessage, typeof(UserCommunicationInfo).Name);

            return addedData;
        }

        public async Task<UserCommunicationInfo> DeleteUserCommunicationAsync(Guid id)
        {
            _logger.LogInformation(ProjectConst.DeleteLogMessage, typeof(UserCommunicationInfo).Name);

            return await userCommunicationCollection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public IQueryable<UserCommunicationInfo> GetUserCommunications(Expression<Func<UserCommunicationInfo, bool>> predicate = null)
        {
            return predicate == null
               ? userCommunicationCollection.AsQueryable()
               : userCommunicationCollection.AsQueryable().Where(predicate);
        }
        public bool CheckUserCommunication(Guid id)
        {
            return (userCommunicationCollection.Count(u => u.Id == id) > 0);
        }
    }
}
