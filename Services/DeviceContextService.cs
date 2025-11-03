using csharp_bus_watcher_api.Exceptions;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Interfaces.Services;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Services;

public class DeviceContextService : IDeviceContextService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeviceContextService(IHttpContextAccessor httpContextAccessor, IDeviceRepository deviceRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _deviceRepository = deviceRepository;
    }

    public string GetHardwareId()
    {
        var context = _httpContextAccessor.HttpContext;

        if (context != null &&
            context.Request.Headers.TryGetValue("hardwareId", out var hardwareId) &&
            !string.IsNullOrEmpty(hardwareId))
        {
            return hardwareId.ToString();
        }

        throw HttpExceptionFactory.BadRequest("Hardware ID is required in request headers.");
    }

    public async Task<Device> GetDevice()
    {
        var hardwareId = GetHardwareId();

        var device = await _deviceRepository.GetDeviceByHardwareId(hardwareId);

        if (device == null)
        {
            throw HttpExceptionFactory.NotFound("Device not found.");
        }

        return device;
    }
}