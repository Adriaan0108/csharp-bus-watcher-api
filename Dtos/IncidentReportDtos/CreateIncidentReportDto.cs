namespace csharp_bus_watcher_api.Dtos.IncidentReportDtos
{
    public class CreateIncidentReportDto
    {
        public int BusId { get; set; }

        public string Description { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
