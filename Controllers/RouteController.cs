using csharp_bus_watcher_api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace csharp_bus_watcher_api.Controllers
{
    [Route("/routes")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IBusService _busService;

        private readonly IRouteService _routeService;

        public RouteController(IBusService busService, IRouteService routeService)
        {
            _busService = busService;
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoutes([FromQuery] string? search = null, [FromQuery] bool? subscribed = null)
        {
            var response = await _routeService.GetRoutes(search, subscribed);

            return Ok(response);
        }

        [HttpGet("{routeId}/buses")]
        public async Task<IActionResult> GetBusesByRouteId(int routeId)
        {
            var response = await _busService.GetBusesByRouteId(routeId);

            return Ok(response);
        }
    }
}
