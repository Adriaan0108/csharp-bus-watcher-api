using csharp_bus_watcher_api.Dtos.IncidentReportDtos;
using csharp_bus_watcher_api.Exceptions;
using csharp_bus_watcher_api.Helpers;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Interfaces.Services;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Services
{
    public class IncidentReportService : IIncidentReportService
    {
        private readonly IIncidentReportRepository _incidentReportRepository;

        private readonly IBusRepository _busRepository;

        private readonly IDeviceContextService _deviceContextService;

        public IncidentReportService(IIncidentReportRepository incidentReportRepository, IDeviceContextService deviceContextService)
        {
            _incidentReportRepository = incidentReportRepository;
            _deviceContextService = deviceContextService;
        }

        public async Task<IncidentReport> CreateIncidentReport(CreateIncidentReportDto createIncidentReportDto)
        {
            var device = await _deviceContextService.GetDevice();

            var bus = await _busRepository.GetBusById(createIncidentReportDto.BusId);

            if (bus == null)
            {
                throw HttpExceptionFactory.NotFound($"No Bus found for Id {createIncidentReportDto.BusId}.");
            }

            var currentTime = TimeOnly.FromDateTime(DateTimeHelper.GetSouthAfricanTime());

            if (currentTime > bus.DepartTime.AddHours(4))
            {
                throw HttpExceptionFactory.BadRequest("Cannot create incident report for bus that departed 4 or more hours ago.");
            }

            //also check for next buses depart time, cant report when next bus should be on its way, or reports disappear after next bus depart time

            var report = MappingProfile.ToIncidentReport(createIncidentReportDto);
            report.CreatedByDeviceId = device.Id;

            return await _incidentReportRepository.CreateIncidentReport(report);
        }

        public async Task<IEnumerable<IncidentReport>> GetIncidentReports(DateTime? startDate, DateTime? endDate)
        {
            var device = await _deviceContextService.GetDevice();

            if (startDate.HasValue && endDate.HasValue)
            {
                DateTimeHelper.ValidateDates(startDate.Value, endDate.Value);
            }

            return await _incidentReportRepository.GetIncidentReports(device.Id, startDate.Value, endDate.Value);
        }

        public async Task<IncidentReport> GetReportById(int id)
        {
            var report = await _incidentReportRepository.GetReportById(id);

            if (report == null)
            {
                throw HttpExceptionFactory.NotFound($"No Incident Report found for Id {id}.");
            }

            return report;
        }
    }
}
