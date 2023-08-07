using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportService.Api.Controllers;
using ReportService.Api.Core.Application.Repository;
using ReportService.Api.Core.Domain.Concrete.Entities;
using ReportService.Api.Core.Domain.Concrete.RequestDTO;
using ReportService.Api.Core.Domain.Concrete.ResponseDTO;
using ReportService.Api.Core.Enums;
using ReportService.Api.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Services.UnitTest
{
    [TestClass]
    public class ReportControllerTests
    {
        private Mock<IReportRepository> mockReportRepository;
        private Mock<IReportDetailRepository> mockReportDetailRepository;
        private Mock<IEventBus> mockEventBus;
        private Mock<ILogger<ReportController>> mockLogger;
        private ReportController reportController;

        [TestInitialize]
        public void Setup()
        {
            mockReportRepository = new Mock<IReportRepository>();
            mockReportDetailRepository = new Mock<IReportDetailRepository>();
            mockEventBus = new Mock<IEventBus>();
            mockLogger = new Mock<ILogger<ReportController>>();

            reportController = new ReportController(
                mockReportRepository.Object,
                mockReportDetailRepository.Object,
                mockEventBus.Object,
                mockLogger.Object
            );
        }

        [TestMethod]
        public async Task RequestReportAsync_ValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new RequestReportRequestDTO();
            var response = new Report { Id = Guid.NewGuid() };

            mockReportRepository.Setup(repo => repo.AddReportAsync(It.IsAny<Report>()))
                .ReturnsAsync(response);

            mockEventBus.Setup(eventBus => eventBus.Publish(It.IsAny<RequestReportIntegrationEvent>()));

            // Act
            var result = await reportController.RequestReportAsync(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(response, okResult.Value);
        }

        [TestMethod]
        public async Task GetReportDetails_NonExistingReportId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingReportId = Guid.NewGuid();

            mockReportRepository.Setup(repo => repo.GetReportByIdAsync(nonExistingReportId))
                .ReturnsAsync((Report)null);

            // Act
            var result = await reportController.GetReportDetails(nonExistingReportId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
            Assert.AreEqual("User Not Found.", notFoundResult.Value);
        }

    }
}
