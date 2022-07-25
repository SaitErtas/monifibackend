using MediatR;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Application.Users.Events.ResetPasswordMail;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Commands.ResetPassword;

internal class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ResetPasswordCommandResponse>
{
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IUserCommandDataPort _userCommandDataPort;
    private readonly IMediator _mediator;

    public ResetPasswordCommandHandler(IUserQueryDataPort userQueryDataPort, IUserCommandDataPort userCommandDataPort, IMediator mediator)
    {
        _userQueryDataPort = userQueryDataPort;
        _userCommandDataPort = userCommandDataPort;
        _mediator = mediator;
    }

    public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetEmailAsync(request.Email);
        AppRule.ExistsAndActive(user, new BusinessValidationException("User not found exception.", $"User not found exception. Email: {request.Email}"));

        var resetPasswordCode = await GenerateResetPasswordCode();
        user.SetResetPasswordCode(resetPasswordCode);

        var result = await _userCommandDataPort.SaveAsync(user);
        AppRule.True(result, new BusinessValidationException("User Not Updated Exception.", $"User already exist. UserId: {user.Id}"));

        var registerComplitedEvent = new ResetPasswordMailEvent(user.Id);
        await _mediator.Publish(registerComplitedEvent);

        return new ResetPasswordCommandResponse();
    }
    private async Task<string> GenerateResetPasswordCode()
    {
        string resetPasswordCode;
    TekrarOlustur:
        resetPasswordCode = RandomKeyGenerator.RandomKey(6);
        //Böyle bir referans kodu var mı?
        var isResetPasswordCode = await _userQueryDataPort.CheckUserResetPasswordCodeAsync(resetPasswordCode);
        if (!isResetPasswordCode)
            return resetPasswordCode;
        else
            goto TekrarOlustur;
    }
}
