using csharp_bus_watcher_api.Models.Shared;

namespace csharp_bus_watcher_api.Models;

public class DeviceBus : BaseModel
{
    public int DeviceId { get; set; }

    public int BusId { get; set; }

    public virtual Device Device { get; set; }

    public virtual Bus Bus { get; set; }
}