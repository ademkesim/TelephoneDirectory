﻿using EventBus.Base.Abstraction;
using ReportService.Api.Core.Application.Repository;
using ReportService.Api.Core.Constants;
using ReportService.Api.Core.Domain.Concrete.Entities;
using ReportService.Api.IntegrationEvents.Events;

namespace ReportService.Api.IntegrationEvents.EventHandlers
{
    public class RequestReportDetailIntegrationEventHandler : IIntegrationEventHandler<RequestReportDetailIntegrationEvent>
    {
        private readonly ILogger<RequestReportDetailIntegrationEvent> _logger;
        private readonly IReportDetailRepository reportDetailRepository;
        private readonly IReportRepository reportRepository;

        public RequestReportDetailIntegrationEventHandler(ILogger<RequestReportDetailIntegrationEvent> logger, IReportDetailRepository reportDetailRepository, IReportRepository reportRepository)
        {
            this.reportDetailRepository = reportDetailRepository;
            this.reportRepository = reportRepository;
            _logger = logger;
        }

        public async Task Handle(RequestReportDetailIntegrationEvent @event)
        {
            var reportDetails = new List<ReportDetail>();
            
            foreach(var item in @event.RequestReportDetails)
            {
                var reportDetail = new ReportDetail()
                {
                    ReportId = @event.ReportId,
                    LocationInfo = item.LocationInfo,
                    PhoneNumberCount = item.PhoneNumberCount,
                    UserCount = item.UserCount,
                };
                reportDetail = await reportDetailRepository.AddReportDetailAsync(reportDetail);
            }
            var report = await reportRepository.GetReportByIdAsync(@event.ReportId);
            report.ReportStatus = Core.Enums.ReportStatusEnum.Completed;
            await reportRepository.UpdateReportAsync(report);

            _logger.LogInformation(ProjectConst.ReportRequestCompleted);

        }
    }
}
