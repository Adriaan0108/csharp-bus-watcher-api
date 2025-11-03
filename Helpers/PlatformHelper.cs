namespace csharp_bus_watcher_api.Helpers;

public class PlatformHelper
{
    public static bool IsValidPlatform(string platform)
    {
        return Enum.TryParse<Platform>(platform, true, out _);
    }

    private enum Platform
    {
        Android,
        iOS,
        Windows
    }
}