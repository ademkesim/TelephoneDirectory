using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DirectoryService.Api.Controllers;
using DirectoryService.Api.Core.Application.Repository;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Services.UnitTest
{
    [TestClass]
    public class UserControllerTests
    {
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IUserCommunicationRepository> mockUserCommunicationRepository;
        private UserController userController;

        [TestInitialize]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockUserCommunicationRepository = new Mock<IUserCommunicationRepository>();

            userController = new UserController(
                mockUserRepository.Object,
                mockUserCommunicationRepository.Object
            );
        }

        [TestMethod]
        public async Task DeleteUser_ExistingUserId_ReturnsOk()
        {
            // Arrange
            var existingUserId = Guid.NewGuid();

            mockUserRepository.Setup(repo => repo.CheckUser(existingUserId))
                .Returns(true);

            // Act
            var result = await userController.DeleteUser(existingUserId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            var okResult = result as OkResult;
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task DeleteUser_NonExistingUserId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingUserId = Guid.NewGuid();

            mockUserRepository.Setup(repo => repo.CheckUser(nonExistingUserId))
                .Returns(false);

            // Act
            var result = await userController.DeleteUser(nonExistingUserId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
            Assert.AreEqual("User Not Found.", notFoundResult.Value);
        }

    }
}
