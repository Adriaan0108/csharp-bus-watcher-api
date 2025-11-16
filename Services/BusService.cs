using csharp_bus_watcher_api.Exceptions;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Interfaces.Services;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Services
{
    public class BusService : IBusService
    {
        private readonly IDeviceContextService _deviceContextService;

        private readonly IBusRepository _busRepository;

        public BusService(IDeviceContextService deviceContextService, IBusRepository busRepository)
        {
            _deviceContextService = deviceContextService;
            _busRepository = busRepository;
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
    }
}
