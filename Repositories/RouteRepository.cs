using csharp_bus_watcher_api.Data;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_bus_watcher_api.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DataContext _context;

        public RouteRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<IEnumerable<BusRoute>> GetRoutes()
        {
            return await _context.Routes
                .Include(r => r.OriginStop)
                .Include(r => r.DestinationStop)
                .ToListAsync();
        }
    }
}
