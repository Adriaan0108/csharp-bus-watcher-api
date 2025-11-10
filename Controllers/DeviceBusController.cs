using csharp_bus_watcher_api.Dtos.DeviceBusDtos;
using csharp_bus_watcher_api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace csharp_bus_watcher_api.Controllers;

[Route("/device-buses")]
[ApiController]
public class DeviceBusController : ControllerBase
{
    private readonly IDeviceBusService _deviceBusService;

    public DeviceBusController(IDeviceBusService deviceBusService)
    {
        _deviceBusService = deviceBusService;
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