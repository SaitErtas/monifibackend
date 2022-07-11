using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.UserModule.Application.Users.Events.UserRegisterComplited;

internal class UserRegisterComplitedEvent : IEvent
{
    public UserRegisterComplitedEvent(string email)
    {
        Email = email;
    }
    public string Email { get; set; }
}