using System;
using System.Threading.Tasks;
using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportService.Api.Controllers;
using ReportService.Api.Core.Application.Repository;
using ReportService.Api.Core.Domain.Concrete.Entities;
using ReportService.Api.Core.Domain.Concrete.RequestDTO;

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

        public ReportControllerTests()
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
            var request = new RequestReportRequestDTO();
            var response = new Report(); 
            mockReportRepository.Setup(repo => repo.AddReportAsync(It.IsAny<Report>())).ReturnsAsync(response);

            var result = await reportController.RequestReportAsync(request);
        }

        [TestMethod]
        public async Task GetReportDetails_ValidId_ReturnsOk()
        {
            var reportId = Guid.NewGuid();
            var report = new Report();
            mockReportRepository.Setup(repo => repo.GetReportByIdAsync(reportId)).ReturnsAsync(report);

            var result = await reportController.GetReportDetails(reportId);

        }
    }
}
