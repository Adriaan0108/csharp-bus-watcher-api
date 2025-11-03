using csharp_bus_watcher_api.Dtos.DeviceDtos;
using csharp_bus_watcher_api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace csharp_bus_watcher_api.Controllers;

[Route("/devices")]
[ApiController]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterDevice([FromBody] CreateDeviceDto createDeviceDto)
    {
        var response = await _deviceService.CreateDevice(createDeviceDto);

        return Ok(response);
    }
}