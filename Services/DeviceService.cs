using csharp_bus_watcher_api.Dtos.DeviceDtos;
using csharp_bus_watcher_api.Exceptions;
using csharp_bus_watcher_api.Helpers;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Interfaces.Services;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;

    public DeviceService(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public async Task<Device> CreateDevice(CreateDeviceDto createDeviceDto)
    {
        if (!RoleHelper.IsValidRole(createDeviceDto.Role))
        {
            throw HttpExceptionFactory.BadRequest("Invalid Role provided.");
        }

        if (!PlatformHelper.IsValidPlatform(createDeviceDto.Platform))
        {
            throw HttpExceptionFactory.BadRequest("Invalid Platform provided.");
        }

        var existingDevice = await _deviceRepository.GetDeviceByHardwareId(createDeviceDto.HardwareId);

        if (existingDevice == null) // create device if it doesnt exist
        {
            var device = MappingProfile.ToDevice(createDeviceDto);

            return await _deviceRepository.CreateDevice(device);
        }

        if (existingDevice.ExpoPushToken != createDeviceDto.ExpoPushToken) // only update the token if it changed
        {
            existingDevice.ExpoPushToken = createDeviceDto.ExpoPushToken;

            return await _deviceRepository.UpdateDevice(existingDevice);
        }

        return existingDevice; // return existing device if no changes needed
    }
}