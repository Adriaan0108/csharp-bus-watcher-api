using csharp_bus_watcher_api.Dtos.IncidentReportDtos;
using csharp_bus_watcher_api.Dtos.NotificationDtos;
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

        private readonly INotificationService _notificationService;

        private readonly IDeviceRepository _deviceRepository;

        public IncidentReportService(IIncidentReportRepository incidentReportRepository, IBusRepository busRepository,
            IDeviceContextService deviceContextService, INotificationService notificationService, IDeviceRepository deviceRepository)
        {
            _incidentReportRepository = incidentReportRepository;
            _busRepository = busRepository;
            _deviceContextService = deviceContextService;
            _notificationService = notificationService;
            _deviceRepository = deviceRepository;
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

            var nextBus = await _busRepository.GetNextBus(bus);

            var report = MappingProfile.ToIncidentReport(createIncidentReportDto);
            report.CreatedByDeviceId = device.Id;

            var createdReport = await _incidentReportRepository.CreateIncidentReport(report);

            await SendIncidentReportNotifications(createdReport, device.Id);

            return createdReport;
        }

        public async Task<IEnumerable<IncidentReport>> GetIncidentReports(DateTime? startDate, DateTime? endDate)
        {
            var device = await _deviceContextService.GetDevice();

            if (startDate.HasValue && endDate.HasValue)
            {
                DateTimeHelper.ValidateDates(startDate.Value, endDate.Value);
            }

            return await _incidentReportRepository.GetIncidentReports(device.Id, startDate, endDate);
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

        private async Task SendIncidentReportNotifications(IncidentReport report, int createdByDeviceId)
        {
            // Get all devices subscribed to this bus
            var subscribedDevices = await _deviceRepository.GetDevicesSubscribedToBus(report.BusId);

            // Filter devices: notifications enabled, not deleted, and exclude the creating device
            var devicesToNotify = subscribedDevices
                .Where(d => d.NotificationsEnabled &&
                           d.DeletedAt == null &&
                           d.Id != createdByDeviceId)
                .ToList();

            if (!devicesToNotify.Any())
            {
                return;
            }

            // Get device tokens for push notifications
            var deviceTokens = devicesToNotify
                .Select(d => d.ExpoPushToken)
                .Where(token => !string.IsNullOrEmpty(token))
                .ToList();

            if (!deviceTokens.Any())
            {
                return;
            }

            var notificationDto = new SendNotificationsDto
            {
                PushTo = deviceTokens,
                PushTitle = "Incident Report",
                PushBody = $"A new incident has been reported for bus {report.Bus.Route.Name}.",
            };

            await _notificationService.SendPushNotifications(notificationDto);
        }
    }
}
