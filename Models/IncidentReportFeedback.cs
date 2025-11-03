using csharp_bus_watcher_api.Models.Shared;

namespace csharp_bus_watcher_api.Models;

public class IncidentReportFeedback : BaseModel
{
    public int IncidentReportId { get; set; }

    public int CreatedByDeviceId { get; set; }

    public string? Comment { get; set; }

    public string? Vote { get; set; } // upvote / downvote

    public virtual IncidentReport IncidentReport { get; set; }

    public virtual Device CreatedByDevice { get; set; }
}