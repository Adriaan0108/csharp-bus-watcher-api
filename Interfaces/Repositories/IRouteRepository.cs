using BusRoute = csharp_bus_watcher_api.Models.BusRoute;

namespace csharp_bus_watcher_api.Interfaces.Repositories
{
    public interface IRouteRepository
    {
        Task<IEnumerable<BusRoute>> GetRoutes(string? search = null);
    }
}
