using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Core.Constants;
using DirectoryService.Api.Core.Enums;
using DirectoryService.Api.IntegrationEvents.Events;
using EventBus.Base.Abstraction;

namespace DirectoryService.Api.IntegrationEvents.EventHandlers
{
    public class RequestReportIntegrationEventHandler : IIntegrationEventHandler<RequestReportIntegrationEvent>
    {
        private readonly IUserCommunicationRepository userCommunicationRepository;
        private readonly ILogger<RequestReportIntegrationEvent> _logger;
        private readonly IEventBus _eventBus;

        public RequestReportIntegrationEventHandler(ILogger<RequestReportIntegrationEvent> logger, IEventBus eventBus, IUserCommunicationRepository userCommunicationRepository)
        {
            this.userCommunicationRepository = userCommunicationRepository;
            _logger = logger;
            _eventBus = eventBus;
        }

        public Task Handle(RequestReportIntegrationEvent @event)
        {
            var requestReportDetails = new List<RequestReportDetailObject>();
            var userLocations = userCommunicationRepository.GetUserCommunications(x => x.CommunicationType == CommunicationTypeEnum.Location).ToList();

            foreach(var locationGroup in userLocations.GroupBy(x => x.CommunicationInfo))
            {
                var requestReportDetail = new RequestReportDetailObject();
                requestReportDetail.LocationInfo = locationGroup.Key;
                requestReportDetail.UserCount = locationGroup.Select(x => x.UserInfoId).Distinct().Count();
                requestReportDetail.PhoneNumberCount = userCommunicationRepository.GetUserCommunications(x => x.CommunicationType == CommunicationTypeEnum.PhoneNumber &&
                locationGroup.Select(l => l.UserInfoId).Contains(x.UserInfoId)).Count();

                requestReportDetails.Add(requestReportDetail);
            }

            var reportDetailIntegrationEvent = new RequestReportDetailIntegrationEvent();
            reportDetailIntegrationEvent.RequestReportDetails = requestReportDetails;
            reportDetailIntegrationEvent.ReportId = @event.ReportId;
            _eventBus.Publish(reportDetailIntegrationEvent);

            _logger.LogInformation(ProjectConst.SendQueueReportList);

            return Task.CompletedTask;
        }

    }
}
