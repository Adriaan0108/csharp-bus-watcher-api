using csharp_bus_watcher_api.Data;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_bus_watcher_api.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly DataContext _context;

    public DeviceRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Device> CreateDevice(Device device)
    {
        await _context.Devices.AddAsync(device);
        await _context.SaveChangesAsync();
        return device;
    }

    public async Task<Device> GetDeviceById(string id)
    {
        return await _context.Devices.FindAsync(id);
    }

    public async Task<Device> UpdateDevice(Device device)
    {
        _context.Devices.Update(device);
        await _context.SaveChangesAsync();
        return device;
    }

    public async Task<Device> GetDeviceByToken(string token)
    {
        return await _context.Devices.FirstOrDefaultAsync(d => d.ExpoPushToken == token);
    }

    public async Task<Device> GetDeviceByHardwareId(string hardwareId)
    {
        return await _context.Devices.FirstOrDefaultAsync(d => d.HardwareId == hardwareId && d.DeletedAt == null);
    }
}