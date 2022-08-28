using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Domain.Users.Notifications;

namespace MonifiBackend.UserModule.Application.Notifications.Queries.GetNotifications;

public class GetNotificationsQueryResponse
{
    public GetNotificationsQueryResponse(List<UserNotification> notifications, User user)
    {
        Notifications = notifications.Select(x => new GetNotificationQuery(x, user)).ToList();
    }
    public List<GetNotificationQuery> Notifications { get; set; }
}
public class GetNotificationQuery
{
    public GetNotificationQuery(UserNotification notification, User user)
    {
        Message = notification.Message;
        IsRead = notification.IsRead;
        Id = notification.Id;
        CustomerName = notification.CustomerName == user.FullName ? null : notification.CustomerName;
        Price = notification.Price == default(decimal) ? null : notification.Price;
        CreatedAt = notification.CreatedAt;
    }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public decimal? Price { get; set; }
    public DateTime CreatedAt { get; set; }
}
