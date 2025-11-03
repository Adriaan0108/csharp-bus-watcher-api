using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Repositories;

public interface IBusRepository
{
    Task<DeviceBus> CreateDeviceBus(DeviceBus deviceBus);

    Task<IEnumerable<DeviceBus>> GetDeviceBussesByDeviceId(int deviceId);

    Task<DeviceBus> UpdateDeviceBus(DeviceBus deviceBus);
}