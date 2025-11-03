namespace csharp_bus_watcher_api.Helpers;

public class DateTimeHelper
{
    public static DateTime GetSouthAfricanTime()
    {
        return DateTime.UtcNow.AddHours(2);
    }
}