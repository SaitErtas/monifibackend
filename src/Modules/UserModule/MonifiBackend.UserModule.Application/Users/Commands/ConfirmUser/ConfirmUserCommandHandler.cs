using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;
using System.IdentityModel.Tokens.Jwt;

namespace MonifiBackend.UserModule.Application.Users.Commands.ConfirmUser;

internal class ConfirmUserCommandHandler : ICommandHandler<ConfirmUserCommand, ConfirmUserCommandResponse>
{
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IUserCommandDataPort _userCommandDataPort;
    private readonly IJwtUtils _jwtUtils;

    public ConfirmUserCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort, IJwtUtils jwtUtils)
    {
        _userQueryDataPort = userQueryDataPort;
        _userCommandDataPort = userCommandDataPort;
        _jwtUtils = jwtUtils;
    }

    public async Task<ConfirmUserCommandResponse> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetUserConfirmationCodeAsync(request.ConfirmationCode);
        AppRule.ExistsAndPassive(user, new BusinessValidationException("ConfirmationCode invalid.", $"ConfirmationCode invalid. ConfirmationCode: {request.ConfirmationCode}"));

        user.MarkAsActive();

        var status = await _userCommandDataPort.SaveAsync(user);
        AppRule.True<BusinessValidationException>(status);

        // authentication successful so generate jwt token
        JwtSecurityToken jwtSecurityToken = await _jwtUtils.GenerateJwtToken(user);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return new ConfirmUserCommandResponse(user, jwtToken);
    }
}
