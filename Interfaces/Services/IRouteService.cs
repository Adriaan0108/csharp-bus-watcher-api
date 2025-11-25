using csharp_bus_watcher_api.Dtos.RouteDtos;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Services
{
    public interface IRouteService
    {
        Task<IEnumerable<RouteResponseDto>> GetRoutes(string? search = null);
    }
}
