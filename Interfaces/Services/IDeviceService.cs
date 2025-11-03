using csharp_bus_watcher_api.Dtos.DeviceDtos;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Services;

public interface IDeviceService
{
    public Task<Device> CreateDevice(CreateDeviceDto createDeviceDto);
}