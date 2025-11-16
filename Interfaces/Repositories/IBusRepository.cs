using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Repositories;

public interface IBusRepository
{
    Task<Bus> GetBusById(int id);

    Task<IEnumerable<Bus>> GetAvailableBusesForDevice(int deviceId);

    Task<IEnumerable<Bus>> GetBusesByDeviceId(int deviceId);

    Task<IEnumerable<Bus>> GetAllBuses();
}