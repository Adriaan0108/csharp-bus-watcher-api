using csharp_bus_watcher_api.Data;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_bus_watcher_api.Repositories
{
    public class IncidentReportRepository : IIncidentReportRepository
    {
        private readonly DataContext _context;

        public IncidentReportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IncidentReport> CreateIncidentReport(IncidentReport incidentReport)
        {
            await _context.IncidentReports.AddAsync(incidentReport);
            await _context.SaveChangesAsync();
            return incidentReport;
        }

        public async Task<IEnumerable<IncidentReport>> GetIncidentReports(int deviceId, DateTime? startDate, DateTime? endDate)
        {
            var deviceBusIds = await _context.DeviceBuses
                .Where(db => db.DeviceId == deviceId && db.DeletedAt == null)
                .Select(db => db.BusId)
                .ToListAsync();

            var query = _context.IncidentReports
                .Where(ir => deviceBusIds.Contains(ir.BusId) && ir.DeletedAt == null);

            if (startDate.HasValue)
            {
                query = query.Where(ir => ir.CreatedAt >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(ir => ir.CreatedAt <= endDate.Value);
            }

            query = query.Include(ir => ir.Bus)
                            .ThenInclude(bus => bus.Route);

            return await query.ToListAsync();
        }

        public async Task<IncidentReport> GetReportById(int id)
        {
            //return await _context.IncidentReports.FindAsync(id);

            return await _context.IncidentReports
                .Include(r => r.Bus)
                .Include(r => r.Bus.Route)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
