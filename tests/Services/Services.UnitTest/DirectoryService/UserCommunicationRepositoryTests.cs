using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Core.Domain.Concrete;
using DirectoryService.Api.Core.Domain.Concrete.RequestDTO;
using MongoDB.Driver;
using DirectoryService.Api.Infrastructure.Repository;
using DirectoryService.Api.Core.Enums;

namespace Services.UnitTest.DirectoryService
{
    [TestClass]
    public class UserCommunicationRepositoryTests
    {
        private Mock<ILoggerFactory> loggerFactoryMock;
        private Mock<ILogger<UserCommunicationRepository>> loggerMock;
        private Mock<IOptions<MongoDbSettings>> optionsMock;
        private Mock<IMongoCollection<UserCommunicationInfo>> userCommunicationCollectionMock;

        [TestInitialize]
        public void Setup()
        {
            loggerFactoryMock = new Mock<ILoggerFactory>();
            loggerMock = new Mock<ILogger<UserCommunicationRepository>>();
            userCommunicationCollectionMock = new Mock<IMongoCollection<UserCommunicationInfo>>();
            loggerFactoryMock.Setup(factory => factory.CreateLogger(It.IsAny<string>())).Returns(loggerMock.Object);
            optionsMock = new Mock<IOptions<MongoDbSettings>>();
            optionsMock.Setup(options => options.Value).Returns(new MongoDbSettings()
            {
                ConnectionString = "mongodb://localhost:27017/",
                Database = "telephone_directory"
            });
        }

        [TestMethod]
        public async Task AddUserCommunicationAsync_ShouldAddCommunicationInfo()
        {
            userCommunicationCollectionMock
                .Setup(col => col.InsertOneAsync(It.IsAny<UserCommunicationInfo>(), null, default))
                .Returns(Task.CompletedTask);

            var repository = new UserCommunicationRepository(loggerFactoryMock.Object, optionsMock.Object);

            var request = new AddUserCommunicationRequestDTO
            {
                UserInfoId = Guid.NewGuid(),
                CommunicationType = CommunicationTypeEnum.Email,
                CommunicationInfo = "adem@gmail.com"
            };

            // Act
            var result = await repository.AddUserCommunicationAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.UserInfoId, result.UserInfoId);
            Assert.AreEqual(request.CommunicationType, result.CommunicationType);
            Assert.AreEqual(request.CommunicationInfo, result.CommunicationInfo);
        }

        [TestMethod]
        public void CheckUserCommunication_ShouldReturnTrueIfExist()
        {
            // Arrange

            var repository = new UserCommunicationRepository(loggerFactoryMock.Object, optionsMock.Object);

            var idToCheck = repository.GetUserCommunications().FirstOrDefault().Id;

            // Act
            var result = repository.CheckUserCommunication(idToCheck);

            // Assert
            Assert.IsTrue(result);
            Assert.IsInstanceOfType(result, typeof(Boolean));
        }

        [TestMethod]
        public async Task DeleteUserCommunicationAsync_ShouldDeleteCommunicationInfo()
        {
            // Arrange

            var repository = new UserCommunicationRepository(loggerFactoryMock.Object, optionsMock.Object);

            var idToDelete = repository.GetUserCommunications().FirstOrDefault().Id;

            // Act
            var result = await repository.DeleteUserCommunicationAsync(idToDelete);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UserCommunicationInfo));
        }
    }
}
