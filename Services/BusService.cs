using csharp_bus_watcher_api.Dtos.BusDtos;
using csharp_bus_watcher_api.Exceptions;
using csharp_bus_watcher_api.Helpers;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Interfaces.Services;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Services
{
    public class BusService : IBusService
    {
        private readonly IDeviceContextService _deviceContextService;

        private readonly IBusRepository _busRepository;

        private readonly IDeviceBusRepository _deviceBusRepository;

        public BusService(IDeviceContextService deviceContextService, IBusRepository busRepository, IDeviceBusRepository deviceBusRepository)
        {
            _deviceContextService = deviceContextService;
            _busRepository = busRepository;
            _deviceBusRepository = deviceBusRepository;
        }

        public async Task<Bus> GetBusById(int id)
        {
            var bus = await _busRepository.GetBusById(id);

            if (bus == null)
            {
                throw HttpExceptionFactory.NotFound($"No Bus found for Id {id}.");
            }

            return bus;
        }

        public async Task<IEnumerable<Bus>> GetBusesByDeviceId(bool? subscribed = null)
        {
            var device = await _deviceContextService.GetDevice();

            return subscribed switch
            {
                true => await _busRepository.GetBusesByDeviceId(device.Id),

                false => await _busRepository.GetAvailableBusesForDevice(device.Id),

                null => await _busRepository.GetAllBuses()
            };
        }

        public async Task<IEnumerable<BusResponseDto>> GetBusesByRouteId(int routeId)
        {
            var buses = await _busRepository.GetBusesByRouteId(routeId);

            var device = await _deviceContextService.GetDevice();

            var subscribedBusIds = await _deviceBusRepository.GetBusIdsByDeviceId(device.Id);

            var busDtos = MappingProfile.ToBusResponseDtos(buses);

            return busDtos.Select(dto =>
            {
                dto.IsSubscribed = subscribedBusIds.Contains(dto.Id);
                return dto;
            });
        }
    }
}
