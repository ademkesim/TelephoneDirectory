using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Core.Application.Repository;
using ReportService.Api.Core.Domain.Concrete.Entities;
using ReportService.Api.Core.Domain.Concrete.RequestDTO;
using ReportService.Api.Core.Domain.Concrete.ResponseDTO;
using ReportService.Api.IntegrationEvents.Events;
using System.Net;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository reportRepository;
        private readonly IReportDetailRepository reportDetailRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportRepository reportRepository, IReportDetailRepository reportDetailRepository, IEventBus eventBus, ILogger<ReportController> logger)
        {
            this.reportRepository = reportRepository;
            this.reportDetailRepository = reportDetailRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        [HttpPost]
        [Route("RequestReport")]
        [ProducesResponseType(typeof(RequestReportResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> RequestReportAsync([FromBody] RequestReportRequestDTO request)
        {
            var report = new Report()
            {
                ReportStatus = Core.Enums.ReportStatusEnum.Pending
            };
            var response = await reportRepository.AddReportAsync(report);

            var eventMessage = new RequestReportIntegrationEvent(response.Id, request.Locations);
            try
            {
                _eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {ReportService.App}", eventMessage.Id);

                throw;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetReports")]
        [ProducesResponseType(typeof(IEnumerable<Report>), (int)HttpStatusCode.OK)]
        public IActionResult GetReports()
        {
            var reports = reportRepository.GetReports();
            return Ok(reports);
        }

        [HttpGet]
        [Route("GetReportDetails/{id}")]
        [ProducesResponseType(typeof(IEnumerable<ReportDetail>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReportDetails(Guid id)
        {
            var report = await reportRepository.GetReportByIdAsync(id);
            if(report == null)
                return NotFound("User Not Found.");

            var reportDetails = reportDetailRepository.GetReportDetail(id);
            return Ok(reportDetails);
        }
    }
}
