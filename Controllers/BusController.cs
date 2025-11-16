using csharp_bus_watcher_api.Dtos.DeviceBusDtos;
using csharp_bus_watcher_api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace csharp_bus_watcher_api.Controllers
{
    [Route("/buses")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;

        private readonly IDeviceBusService _deviceBusService;

        public BusController(IBusService busService, IDeviceBusService deviceBusService)
        {
            _busService = busService;
            _deviceBusService = deviceBusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBusesByDeviceId([FromQuery] bool? subscribed = null)
        {
            var response = await _busService.GetBusesByDeviceId(subscribed);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusById(int id)
        {
            var response = await _busService.GetBusById(id);

            return Ok(response);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> CreateDeviceBus([FromBody] CreateDeviceBusDto createDeviceBusDto)
        {
            var response = await _deviceBusService.CreateDeviceBus(createDeviceBusDto);

            return Ok(response);
        }

        [HttpDelete("{id}/unsubscribe")]
        public async Task<IActionResult> DeleteDeviceBus(int id)
        {
            await _deviceBusService.DeleteDeviceBus(id);

            return NoContent();
        }
    }
}
