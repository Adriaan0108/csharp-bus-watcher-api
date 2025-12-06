using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Repositories;

public interface IDeviceBusRepository
{
    Task<DeviceBus> CreateDeviceBus(DeviceBus deviceBus);

    Task<DeviceBus> GetDeviceBus(int deviceId, int busId);

    Task<DeviceBus> GetDeviceBus(int deviceBusId);

    Task<IEnumerable<int>> GetBusIdsByDeviceId(int deviceId);

    Task<IEnumerable<int>> GetAvailableBusIdsByDeviceId(int deviceId);

    Task<IEnumerable<int>> GetAllBusIds();

    Task<IEnumerable<DeviceBus>> GetBusesByDeviceId(int deviceId);

    Task UpdateDeviceBus(DeviceBus deviceBus);
}