using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Core.Enums;
using DirectoryService.Api.IntegrationEvents.Events;
using DirectoryService.Api.IntegrationEvents.EventHandlers;
using EventBus.Base.Abstraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoryService.Api.Core.Domain.Concrete;
using System;
using System.Linq.Expressions;

namespace Services.UnitTest.DirectoryService
{
    [TestClass]
    public class RequestReportIntegrationEventHandlerTests
    {
        [TestMethod]
        public async Task Handle_ShouldProcessRequestReportEvent()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RequestReportIntegrationEvent>>();
            var eventBusMock = new Mock<IEventBus>();
            var userCommunicationRepositoryMock = new Mock<IUserCommunicationRepository>();

            var handler = new RequestReportIntegrationEventHandler(
                loggerMock.Object,
                eventBusMock.Object,
                userCommunicationRepositoryMock.Object
            );

            var mockUserLocations = new List<UserCommunicationInfo>
        {
            new UserCommunicationInfo { CommunicationType = CommunicationTypeEnum.Location, CommunicationInfo = "Location1", UserInfoId = Guid.NewGuid() },
            new UserCommunicationInfo { CommunicationType = CommunicationTypeEnum.Location, CommunicationInfo = "Location1", UserInfoId = Guid.NewGuid() },
            new UserCommunicationInfo { CommunicationType = CommunicationTypeEnum.Location, CommunicationInfo = "Location2", UserInfoId = Guid.NewGuid() }
        };

            userCommunicationRepositoryMock.Setup(repo => repo.GetUserCommunications(It.IsAny<Expression<Func<UserCommunicationInfo, bool>>>()))
                                          .Returns(mockUserLocations.AsQueryable());

            var requestReportEvent = new RequestReportIntegrationEvent(Guid.NewGuid());

            // Act
            await handler.Handle(requestReportEvent);

            // Assert
            eventBusMock.Verify(bus => bus.Publish(It.IsAny<RequestReportDetailIntegrationEvent>()), Times.Once);
        }
    }
}
