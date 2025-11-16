using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Repositories
{
    public interface IIncidentReportRepository
    {
        Task<IncidentReport> CreateIncidentReport(IncidentReport incidentReport);

        Task<IEnumerable<IncidentReport>> GetIncidentReports(int deviceId, DateTime? startDate, DateTime? endDate);

        Task<IncidentReport> GetReportById(int id);
    }
}
