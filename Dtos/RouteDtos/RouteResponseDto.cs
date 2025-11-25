using csharp_bus_watcher_api.Models;

namespace csharp_bus_watcher_api.Dtos.RouteDtos
{
    public class RouteResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int OriginStopId { get; set; }

        public int DestinationStopId { get; set; }

        public bool HasSubscription { get; set; }

        public virtual Stop OriginStop { get; set; }

        public virtual Stop DestinationStop { get; set; }
    }
}
