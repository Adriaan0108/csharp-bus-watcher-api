using csharp_bus_watcher_api.Dtos.NotificationDtos;

namespace csharp_bus_watcher_api.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendPushNotifications(SendNotificationsDto sendNotificationsDto);
    }
}
