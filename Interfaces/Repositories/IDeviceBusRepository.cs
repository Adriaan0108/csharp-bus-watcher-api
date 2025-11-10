using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Repositories;

public interface IDeviceBusRepository
{
    Task<DeviceBus> CreateDeviceBus(DeviceBus deviceBus);

    Task<DeviceBus> GetDeviceBus(int deviceId, int busId);

    Task<DeviceBus> GetDeviceBus(int deviceBusId);

    Task UpdateDeviceBus(DeviceBus deviceBus);
}