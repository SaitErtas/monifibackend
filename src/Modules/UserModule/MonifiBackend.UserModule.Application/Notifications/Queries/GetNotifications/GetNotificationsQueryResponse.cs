using MonifiBackend.UserModule.Domain.Users.Notifications;

namespace MonifiBackend.UserModule.Application.Notifications.Queries.GetNotifications;

public class GetNotificationsQueryResponse
{
    public GetNotificationsQueryResponse(List<UserNotification> notifications)
    {
        Notifications = notifications.Select(x => new GetNotificationQuery(x)).ToList();
    }
    public List<GetNotificationQuery> Notifications { get; set; }
}
public class GetNotificationQuery
{
    public GetNotificationQuery(UserNotification notification)
    {
        Message = notification.Message;
        IsRead = notification.IsRead;
    }
    public string Message { get; set; }
    public bool IsRead { get; set; }
}
