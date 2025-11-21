using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Interfaces.Services;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<IEnumerable<BusRoute>> GetRoutes()
        {
            var routes = await _routeRepository.GetRoutes();

            return routes;
        }
    }
}
