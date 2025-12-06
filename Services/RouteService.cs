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

        private readonly IBusRepository _busRepository;

        public RouteService(IRouteRepository routeRepository, IDeviceBusRepository deviceBusRepository, 
            IDeviceContextService deviceContextService, IBusRepository busRepository)
        {
            _routeRepository = routeRepository;
            _deviceBusRepository = deviceBusRepository;
            _deviceContextService = deviceContextService;
            _busRepository = busRepository;
        }

        public async Task<IEnumerable<RouteResponseDto>> GetRoutes(string? search = null, bool? subscribed = null)
        {
            var device = await _deviceContextService.GetDevice();

            var routes = await _routeRepository.GetRoutes(search);

            var routeIds = routes.Select(r => r.Id).ToList();

            IEnumerable<int> busIds;

            if (subscribed == true)
            {
                busIds = await _deviceBusRepository.GetBusIdsByDeviceId(device.Id);
            }
            else if (subscribed == false)
            {
                busIds = await _deviceBusRepository.GetAvailableBusIdsByDeviceId(device.Id);
            }
            else
            {
                busIds = await _deviceBusRepository.GetAllBusIds();
            }

            var routeDtos = MappingProfile.ToRouteResponseDtos(routes);

            var buses = await _busRepository.GetBusesByRouteIds(routeIds);

            foreach (var dto in routeDtos)
            {
                // Check if ANY bus in this route is in the subscribed buses list
                dto.HasSubscription = buses
                    .Where(b => b.RouteId == dto.Id)
                    .Any(b => busIds.Contains(b.Id));
            }

            return routeDtos;
        }
    }
}
