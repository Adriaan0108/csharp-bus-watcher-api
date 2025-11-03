using csharp_bus_watcher_api.Data;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_bus_watcher_api.Repositories;

public class BusRepository : IBusRepository
{
    private readonly DataContext _context;

    public BusRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<DeviceBus> CreateDeviceBus(DeviceBus deviceBus)
    {
        await _context.DeviceBuses.AddAsync(deviceBus);
        await _context.SaveChangesAsync();
        return deviceBus;
    }

    public Task<DeviceBus> UpdateDeviceBus(DeviceBus deviceBus)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<DeviceBus>> GetDeviceBussesByDeviceId(int deviceId)
    {
        return await _context.DeviceBuses.Where(d => d.DeviceId == deviceId)
            .Include(db => db.Bus)
            .ThenInclude(b => b.Route)
            .ToListAsync();
    }
}