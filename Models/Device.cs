using csharp_bus_watcher_api.Models.Shared;

namespace csharp_bus_watcher_api.Models;

public class Device : BaseModel
{
    public string ExpoPushToken { get; set; }

    public string Platform { get; set; } // ios / android

    public string HardwareId { get; set; }

    public string Role { get; set; }

    public bool NotificationsEnabled { get; set; }

    public virtual ICollection<DeviceBus> DeviceBuses { get; set; }

    public virtual ICollection<IncidentReport> IncidentReports { get; set; }

    public virtual ICollection<IncidentReportFeedback> IncidentReportFeedbacks { get; set; }
}