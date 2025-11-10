using csharp_bus_watcher_api.Dtos.DeviceBusDtos;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Services;

public interface IDeviceBusService
{
    Task<DeviceBus> CreateDeviceBus(CreateDeviceBusDto createDeviceBusDto);

    Task DeleteDeviceBus(int deviceBusId);
}