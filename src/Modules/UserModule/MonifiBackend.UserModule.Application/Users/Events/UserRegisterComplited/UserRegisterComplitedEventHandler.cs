using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.UserModule.Application.Users.Events.UserRegisterComplited;

internal class UserRegisterComplitedEventHandler : IEventHandler<UserRegisterComplitedEvent>
{
    //private readonly IEmailPort _emailPort;
    public UserRegisterComplitedEventHandler(/*IEmailPort emailPort*/)
    {
        //_emailPort = emailPort;
    }
    public async Task Handle(UserRegisterComplitedEvent request, CancellationToken cancellationToken)
    {
        //GetUser

        //Send User Confirmation Code int Mail
        //_emailPort.SendEmailTemplate(1, request.Email, $"{request.Name} {request.Surname}");
    }
}