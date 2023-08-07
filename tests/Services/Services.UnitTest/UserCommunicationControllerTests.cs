using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoryService.Api.Controllers;
using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Core.Domain.Concrete.RequestDTO;
using DirectoryService.Api.Core.Domain.Concrete.ResponseDTO;
using DirectoryService.Api.Core.Domain.Concrete;

namespace Services.UnitTest
{
    [TestClass]
    public class UserCommunicationControllerTests
    {
        private Mock<IUserCommunicationRepository> mockUserCommunicationRepository;
        private Mock<IUserRepository> mockUserRepository;
        private UserCommunicationController userCommunicationController;

        [TestInitialize]
        public void Setup()
        {
            mockUserCommunicationRepository = new Mock<IUserCommunicationRepository>();
            mockUserRepository = new Mock<IUserRepository>();

            userCommunicationController = new UserCommunicationController(
                mockUserCommunicationRepository.Object,
                mockUserRepository.Object
            );
        }

        [TestMethod]
        public async Task AddUserCommunication_ValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new AddUserCommunicationRequestDTO { /* Initialize your request here */ };
            mockUserRepository.Setup(repo => repo.CheckUser(It.IsAny<Guid>())).Returns(true);
            mockUserCommunicationRepository.Setup(repo => repo.AddUserCommunicationAsync(It.IsAny<AddUserCommunicationRequestDTO>())).ReturnsAsync(new UserCommunicationInfo());

            // Act
            var result = await userCommunicationController.AddUserCommunication(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(AddUserCommunicationResponseDTO));
        }

        [TestMethod]
        public async Task DeleteUserCommunication_ExistingId_ReturnsOk()
        {
            // Arrange
            var userCommunicationId = Guid.NewGuid();
            mockUserCommunicationRepository.Setup(repo => repo.CheckUserCommunication(userCommunicationId)).Returns(true);

            // Act
            var result = await userCommunicationController.DeleteUserCommunication(userCommunicationId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

    }
}
