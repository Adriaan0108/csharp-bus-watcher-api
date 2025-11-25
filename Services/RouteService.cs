using csharp_bus_watcher_api.Dtos.RouteDtos;
using csharp_bus_watcher_api.Helpers;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Interfaces.Services;

namespace csharp_bus_watcher_api.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;

        private readonly IDeviceBusRepository _deviceBusRepository;

        private readonly IDeviceContextService _deviceContextService;

        public RouteService(IRouteRepository routeRepository, IDeviceBusRepository deviceBusRepository, IDeviceContextService deviceContextService)
        {
            _routeRepository = routeRepository;
            _deviceBusRepository = deviceBusRepository;
            _deviceContextService = deviceContextService;
        }

        public async Task<IEnumerable<RouteResponseDto>> GetRoutes(string? search = null)
        {
            var device = await _deviceContextService.GetDevice();

            var routes = await _routeRepository.GetRoutes(search);

            var subscribedBuses = await _deviceBusRepository.GetBusIdsByDeviceId(device.Id);

            var routeDtos = MappingProfile.ToRouteResponseDtos(routes);

            foreach (var dto in routeDtos)
            {
                dto.HasSubscription = subscribedBuses.Contains(dto.Id);
            }

            return routeDtos;
        }
    }
}
