using csharp_bus_watcher_api.Dtos.DeviceBusDtos;
using csharp_bus_watcher_api.Exceptions;
using csharp_bus_watcher_api.Helpers;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Interfaces.Services;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Services;

public class DeviceBusService : IDeviceBusService
{
    private readonly IDeviceBusRepository _deviceBusRepository;

    private readonly IDeviceContextService _deviceContextService;

    public DeviceBusService(IDeviceBusRepository deviceBusRepository, IDeviceContextService deviceContextService)
    {
        _deviceBusRepository = deviceBusRepository;
        _deviceContextService = deviceContextService;
    }

    public async Task<DeviceBus> CreateDeviceBus(CreateDeviceBusDto createDeviceBusDto)
    {
        var device = await _deviceContextService.GetDevice();

        var existingDeviceBus =
            await _deviceBusRepository.GetDeviceBus(device.Id, createDeviceBusDto.BusId);

        if (existingDeviceBus == null)
        {
            var deviceBus = MappingProfile.ToDeviceBus(createDeviceBusDto);
            deviceBus.DeviceId = device.Id;

            return await _deviceBusRepository.CreateDeviceBus(deviceBus);
        }

        return existingDeviceBus;
    }

    public async Task DeleteDeviceBus(int deviceBusId)
    {
        var deviceBus = await _deviceBusRepository.GetDeviceBus(deviceBusId);

        if (deviceBus == null)
        {
            throw HttpExceptionFactory.NotFound($"No Bus Subscription found with Id {deviceBusId}.");
        }

        deviceBus.SetDeletedAt();

        await _deviceBusRepository.UpdateDeviceBus(deviceBus);
    }
}