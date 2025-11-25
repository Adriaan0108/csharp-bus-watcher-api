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

    public async Task<IEnumerable<Bus>> GetBusesByDeviceId(int deviceId)
    {
        return await _context.Buses
        .Where(b => _context.DeviceBuses.Any(db => db.BusId == b.Id && db.DeviceId == deviceId && db.DeletedAt == null))
        .Include(b => b.Route)
            .ThenInclude(r => r.OriginStop)
        .Include(b => b.Route)
            .ThenInclude(r => r.DestinationStop)
        .AsSplitQuery()
        .ToListAsync();
    }

    public async Task<IEnumerable<Bus>> GetAvailableBusesForDevice(int deviceId)
    {
        return await _context.Buses
            .Where(b => !_context.DeviceBuses.Any(db => db.BusId == b.Id && db.DeviceId == deviceId && db.DeletedAt == null))
            .Include(b => b.Route)
                .ThenInclude(r => r.OriginStop)
            .Include(b => b.Route)
                .ThenInclude(r => r.DestinationStop)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Bus> GetBusById(int id)
    {
        //return await _context.Buses.FindAsync(id);

        return await _context.Buses
        .Include(b => b.Route)
            .ThenInclude(r => r.OriginStop)
        .Include(b => b.Route)
            .ThenInclude(r => r.DestinationStop)
        .AsSplitQuery()
        .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Bus>> GetAllBuses()
    {
        return await _context.Buses
        .Include(b => b.Route)
            .ThenInclude(r => r.OriginStop)
        .Include(b => b.Route)
            .ThenInclude(r => r.DestinationStop)
        .AsSplitQuery()
        .ToListAsync();
    }

    public async Task<IEnumerable<Bus>> GetBusesByRouteId(int routeId)
    {
        return await _context.Buses.Where(b => b.RouteId == routeId).ToListAsync();
    }

    public async Task<Bus> GetNextBus(Bus currentBus)
    {
        return await _context.Buses
            .Where(b => b.RouteId == currentBus.RouteId
                        && b.Direction == currentBus.Direction
                        && b.DepartTime > currentBus.DepartTime)
            .OrderBy(b => b.DepartTime)
            .FirstOrDefaultAsync();
    }
}