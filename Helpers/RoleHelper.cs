namespace csharp_bus_watcher_api.Helpers;

public class RoleHelper
{
    public static bool IsValidRole(string role)
    {
        return Enum.TryParse<Role>(role, true, out _);
    }

    private enum Role
    {
        User,
        Admin,
        Superadmin
    }
}