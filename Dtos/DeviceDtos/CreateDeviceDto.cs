namespace csharp_bus_watcher_api.Dtos.DeviceDtos;

public class CreateDeviceDto
{
    public string ExpoPushToken { get; set; }

    public string Platform { get; set; }

    public string HardwareId { get; set; }

    public string Role { get; set; }

    public bool NotificationsEnabled { get; set; }
}