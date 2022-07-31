using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Notifications.Commands.CreateNotification;

internal class CreateNotificationCommandHandler : ICommandHandler<CreateNotificationCommand, CreateNotificationCommandResponse>
{
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IUserCommandDataPort _userCommandDataPort;

    public CreateNotificationCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort)
    {
        _userQueryDataPort = userQueryDataPort;
        _userCommandDataPort = userCommandDataPort;
    }

    public async Task<CreateNotificationCommandResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetAsync(request.UserId);
        AppRule.ExistsAndActive(user, new BusinessValidationException("User not found.", $"User not found. UserId: {request.UserId}"));

        user.AddNotification(request.Message);

        var result = await _userCommandDataPort.SaveAsync(user);
        AppRule.True(result, new BusinessValidationException("User Not Updated Exception.", $"User Not Updated Exception. UserId: {user.Id}"));

        return new CreateNotificationCommandResponse();
    }
}