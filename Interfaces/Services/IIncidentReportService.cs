using csharp_bus_watcher_api.Dtos.IncidentReportDtos;
using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Interfaces.Services
{
    public interface IIncidentReportService
    {
        Task<IncidentReport> CreateIncidentReport(CreateIncidentReportDto createIncidentReportDto);

        Task<IEnumerable<IncidentReport>> GetIncidentReports(DateTime? startDate, DateTime? endDate);

        Task<IncidentReport> GetReportById(int id);
    }
}
