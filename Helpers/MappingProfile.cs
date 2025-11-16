using csharp_bus_watcher_api.Dtos.DeviceBusDtos;
using csharp_bus_watcher_api.Dtos.DeviceDtos;
using csharp_bus_watcher_api.Dtos.IncidentReportDtos;
using csharp_bus_watcher_api.Dtos.NotificationDtos;
using csharp_bus_watcher_api.Models;
using Expo.Server.Models;
using Riok.Mapperly.Abstractions;

namespace csharp_bus_watcher_api.Helpers;

[Mapper]
public static partial class MappingProfile
{
    public static partial Device ToDevice(CreateDeviceDto createDeviceDto);

    public static partial DeviceBus ToDeviceBus(CreateDeviceBusDto createDeviceBusDto);

    public static partial IncidentReport ToIncidentReport(CreateIncidentReportDto createIncidentReportDto);

    public static partial PushTicketRequest ToPushTicketRequest(SendNotificationsDto sendNotificationsDto);
}