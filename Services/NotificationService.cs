using csharp_bus_watcher_api.Dtos.NotificationDtos;
using csharp_bus_watcher_api.Helpers;
using csharp_bus_watcher_api.Interfaces.Services;
using Expo.Server.Client;

namespace csharp_bus_watcher_api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly PushApiClient _pushApiClient;

        public NotificationService(PushApiClient pushApiClient)
        {
            _pushApiClient = pushApiClient;
        }

        public async Task SendPushNotifications(SendNotificationsDto sendNotificationsDto)
        {
            if (sendNotificationsDto.PushTo == null || !sendNotificationsDto.PushTo.Any())
            {
                return;
            }

            var payload = MappingProfile.ToPushTicketRequest(sendNotificationsDto);

            await _pushApiClient.PushSendAsync(payload);
        }
    }
}
