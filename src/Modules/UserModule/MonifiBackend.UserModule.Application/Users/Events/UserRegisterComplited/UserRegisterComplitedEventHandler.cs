using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Notifications;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Events.UserRegisterComplited;

internal class UserRegisterComplitedEventHandler : IEventHandler<UserRegisterComplitedEvent>
{
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IEmailPort _emailPort;
    public UserRegisterComplitedEventHandler(IUserQueryDataPort userQueryDataPort, IEmailPort emailPort)
    {
        _userQueryDataPort = userQueryDataPort;
        _emailPort = emailPort;
    }
    public async Task Handle(UserRegisterComplitedEvent request, CancellationToken cancellationToken)
    {
        //GetUser
        var user = await _userQueryDataPort.GetAsync(request.UserId);
        SendVerificationMail(user);
    }
    private void SendVerificationMail(User user)
    {
        string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\Templates\\VerificationMail.html";
        StreamReader str = new StreamReader(FilePath);
        string mailText = str.ReadToEnd();
        str.Close();
        mailText = mailText.Replace("[VerificationAddress]", $"https://monifi.io/confirmation/{user.ConfirmationCode}");
        _emailPort.Send(user.Email, "Monofi.io Verification Email", mailText);
    }
}