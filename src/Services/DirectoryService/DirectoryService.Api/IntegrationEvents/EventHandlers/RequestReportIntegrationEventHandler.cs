using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Core.Enums;
using DirectoryService.Api.IntegrationEvents.Events;
using EventBus.Base.Abstraction;

namespace DirectoryService.Api.IntegrationEvents.EventHandlers
{
    public class RequestReportIntegrationEventHandler : IIntegrationEventHandler<RequestReportIntegrationEvent>
    {
        private readonly IUserRepository userRepository;
        private readonly IUserCommunicationRepository userCommunicationRepository;
        private readonly ILogger<RequestReportIntegrationEvent> _logger;
        private readonly IEventBus _eventBus;

        public RequestReportIntegrationEventHandler(ILogger<RequestReportIntegrationEvent> logger, IEventBus eventBus, IUserRepository userRepository, IUserCommunicationRepository userCommunicationRepository)
        {
            this.userRepository = userRepository;
            this.userCommunicationRepository = userCommunicationRepository;
            _logger = logger;
            _eventBus = eventBus;
        }

        public Task Handle(RequestReportIntegrationEvent @event)
        {
            var userCommunications = userCommunicationRepository.GetUserCommunications().ToList();

            var requestReportDetails = (from userLocation in userCommunications.Where(x => x.CommunicationType == CommunicationTypeEnum.Location)
                      group userLocation by userLocation.CommunicationInfo into userLocationGroup
                      select new RequestReportDetailObject
                      {
                          LocationInfo = userLocationGroup.Key,
                          UserCount = userLocationGroup.Select(x => x.UserInfoId).Distinct().Count(),
                          PhoneNumberCount = userCommunications.Where(x => x.CommunicationType == CommunicationTypeEnum.PhoneNumber
                          && userLocationGroup.Select(u => u.UserInfoId).Contains(x.UserInfoId)).Count()
                      }).ToList();

            var reportDetailIntegrationEvent = new RequestReportDetailIntegrationEvent();
            reportDetailIntegrationEvent.RequestReportDetails = requestReportDetails;
            reportDetailIntegrationEvent.ReportId = @event.ReportId;
            _eventBus.Publish(reportDetailIntegrationEvent);

            return Task.CompletedTask;
        }

    }
}
