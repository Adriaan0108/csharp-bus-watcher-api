using csharp_bus_watcher_api.Models.Shared;

namespace csharp_bus_watcher_api.Models;

public class Area : BaseModel
{
    public string Name { get; set; }

    public virtual ICollection<Stop> Stops { get; set; }
}