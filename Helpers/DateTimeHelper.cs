using csharp_bus_watcher_api.Exceptions;

namespace csharp_bus_watcher_api.Helpers;

public class DateTimeHelper
{
    public static DateTime GetSouthAfricanTime()
    {
        return DateTime.UtcNow.AddHours(2);
    }

    public static void ValidateDates(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw HttpExceptionFactory.BadRequest("Start Date cannot be after End Date.");
        }
    }
}