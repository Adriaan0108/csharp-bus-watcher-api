using csharp_bus_watcher_api.Models.Shared;

namespace csharp_bus_watcher_api.Models;

public class Bus : BaseModel
{
    public int RouteId { get; set; }

    public string Description { get; set; }

    public TimeOnly DepartTime { get; set; }

    public string Direction { get; set; } // from origin / from destination

    // public virtual ICollection<DeviceBus> DeviceBuses { get; set; }

    public virtual BusRoute Route { get; set; }

    //public virtual ICollection<IncidentReport> IncidentReports { get; set; }
}