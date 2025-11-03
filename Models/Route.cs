using csharp_bus_watcher_api.Models.Shared;

namespace csharp_bus_watcher_api.Models;

public class Route : BaseModel
{
    public string Name { get; set; }

    public int OriginStopId { get; set; }

    public int DestinationStopId { get; set; }

    public virtual Stop OriginStop { get; set; }

    public virtual Stop DestinationStop { get; set; }

    public virtual ICollection<Bus> Buses { get; set; }
}