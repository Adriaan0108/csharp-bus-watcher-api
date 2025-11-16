namespace csharp_bus_watcher_api.Dtos.NotificationDtos
{
    public class SendNotificationsDto
    {
        public List<string> PushTo { get; set; } = new();

        public string PushTitle { get; set; }

        public string PushBody { get; set; }

        // optional
        //public object PushData { get; set; }

        // optional
        public string Sound { get; set; } = "default";
    }
}
