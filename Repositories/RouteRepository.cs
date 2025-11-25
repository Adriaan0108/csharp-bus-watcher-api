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

        public async Task<IEnumerable<BusRoute>> GetRoutes(string? search = null)
        {
            //return await _context.Routes
            //    .Include(r => r.OriginStop)
            //    .Include(r => r.DestinationStop)
            //    .ToListAsync();

            var query = _context.Routes
               .Include(r => r.OriginStop)
               .Include(r => r.DestinationStop)
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(r => r.Name.ToLower().Contains(lowerSearch));
            }

            return await query.ToListAsync();
        }
    }
}
