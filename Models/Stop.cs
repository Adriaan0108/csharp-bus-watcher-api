using csharp_bus_watcher_api.Models.Shared;

namespace csharp_bus_watcher_api.Models;

public class Stop : BaseModel
{
    public string Name { get; set; }

    public int AreaId { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual Area Area { get; set; }

    public virtual ICollection<Route> RoutesAsOrigin { get; set; }

    public virtual ICollection<Route> RoutesAsDestination { get; set; }
}