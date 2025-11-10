using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Services;

public interface IBusService
{
    Task<Bus> GetBusById(int id);

    Task<IEnumerable<Bus>> GetBusesByDeviceId();

    Task<IEnumerable<Bus>> GetAvailableBusesForDeviceId();
}