using csharp_bus_watcher_api.Dtos.IncidentReportDtos;
using csharp_bus_watcher_api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace csharp_bus_watcher_api.Controllers
{
    [Route("/incident-reports")]
    [ApiController]
    public class IncidentReportController : ControllerBase
    {
        private readonly IIncidentReportService _incidentReportService;

        public IncidentReportController(IIncidentReportService incidentReportService)
        {
            _incidentReportService = incidentReportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncidentReports(DateTime? startDate, DateTime? endDate)
        {
            var response = await _incidentReportService.GetIncidentReports(startDate, endDate);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncidentReport([FromBody] CreateIncidentReportDto createIncidentReportDto)
        {
            var response = await _incidentReportService.CreateIncidentReport(createIncidentReportDto);

            return Ok(response);
        }
    }
}
