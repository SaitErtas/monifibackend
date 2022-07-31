using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.UserModule.Application.Notifications.Events.CreateNotification;

public class CreateNotificationEvent : IEvent
{
    public CreateNotificationEvent(int userId, string message)
    {
        UserId = userId;
        Message = message;
    }
    public int UserId { get; set; }
    public string Message { get; set; }
}