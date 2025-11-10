using csharp_bus_watcher_api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace csharp_bus_watcher_api.Controllers
{
    [Route("/buses")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;

        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBusesByDeviceId()
        {
            var response = await _busService.GetBusesByDeviceId();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusById(int id)
        {
            var response = await _busService.GetBusById(id);

            return Ok(response);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBusesForDeviceId()
        {
            var response = await _busService.GetAvailableBusesForDeviceId();

            return Ok(response);
        }
    }
}
