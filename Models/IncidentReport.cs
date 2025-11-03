using csharp_bus_watcher_api.Models.Shared;

namespace csharp_bus_watcher_api.Models;

public class IncidentReport : BaseModel
{
    public int BusId { get; set; }

    public string Description { get; set; }

    public int CreatedByDeviceId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public virtual Bus Bus { get; set; }

    public virtual Device CreatedByDevice { get; set; }

    public virtual ICollection<IncidentReportFeedback> IncidentReportFeedbacks { get; set; }
}