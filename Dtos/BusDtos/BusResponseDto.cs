namespace csharp_bus_watcher_api.Dtos.BusDtos
{
    public class BusResponseDto
    {
        public int Id { get; set; }

        public int RouteId { get; set; }

        public string Description { get; set; }

        public TimeOnly DepartTime { get; set; }

        public string Direction { get; set; }

        public bool IsSubscribed { get; set; }
    }
}
