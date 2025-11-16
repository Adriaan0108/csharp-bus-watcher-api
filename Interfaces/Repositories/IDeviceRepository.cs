using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Repositories;

public interface IDeviceRepository
{
    Task<Device> CreateDevice(Device device);

    Task<Device> GetDeviceById(string id);

    Task<Device> UpdateDevice(Device device);

    Task<Device> GetDeviceByToken(string token);

    Task<Device> GetDeviceByHardwareId(string hardwareId);

    Task<IEnumerable<Device>> GetDevicesSubscribedToBus(int busId);
}