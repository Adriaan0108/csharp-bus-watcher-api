using csharp_bus_watcher_api.Data;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_bus_watcher_api.Repositories;

public class DeviceBusRepository : IDeviceBusRepository
{
    private readonly DataContext _context;

    public DeviceBusRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<DeviceBus> CreateDeviceBus(DeviceBus deviceBus)
    {
        await _context.DeviceBuses.AddAsync(deviceBus);
        await _context.SaveChangesAsync();
        return deviceBus;
    }

    public async Task<DeviceBus> GetDeviceBus(int deviceId, int busId)
    {
        return await _context.DeviceBuses.FirstOrDefaultAsync(d =>
            d.DeviceId == deviceId && d.BusId == busId && d.DeletedAt == null);
    }

    public async Task<DeviceBus> GetDeviceBus(int deviceBusId)
    {
        //return await _context.DeviceBuses.FindAsync(deviceBusId);

        return await _context.DeviceBuses
           .Where(db => db.Id == deviceBusId && db.DeletedAt == null)
           .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<int>> GetBusIdsByDeviceId(int deviceId)
    {
        return await _context.DeviceBuses
            .Where(db => db.DeviceId == deviceId && db.DeletedAt == null)
            .Select(db => db.BusId)
            .ToListAsync();
    }

    public async Task UpdateDeviceBus(DeviceBus deviceBus)
    {
        _context.DeviceBuses.Update(deviceBus);
        await _context.SaveChangesAsync();
    }
}