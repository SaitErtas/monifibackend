using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Commands.ConfirmUser;

internal class ConfirmUserCommandHandler : ICommandHandler<ConfirmUserCommand, ConfirmUserCommandResponse>
{
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IUserCommandDataPort _userCommandDataPort;

    public ConfirmUserCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort)
    {
        _userQueryDataPort = userQueryDataPort;
        _userCommandDataPort = userCommandDataPort;
    }

    public async Task<ConfirmUserCommandResponse> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetUserConfirmationCodeAsync(request.ConfirmationCode);
        AppRule.ExistsAndPassive(user, new BusinessValidationException("ConfirmationCode invalid.", $"ConfirmationCode invalid. ConfirmationCode: {request.ConfirmationCode}"));

        user.MarkAsActive();

        var status = await _userCommandDataPort.SaveAsync(user);
        AppRule.True<BusinessValidationException>(status);

        return new ConfirmUserCommandResponse();
    }
}
