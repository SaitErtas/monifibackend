using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.UserModule.Application.Users.Commands.ConfirmUser;

public class ConfirmUserCommand : ICommand<ConfirmUserCommandResponse>
{
    public ConfirmUserCommand(string confirmationCode)
    {
        ConfirmationCode = confirmationCode;
    }
    public string ConfirmationCode { get; set; }
}

internal class ConfirmUserCommandValidator : AbstractValidator<ConfirmUserCommand>
{
    public ConfirmUserCommandValidator()
    {
        RuleFor(x => x.ConfirmationCode)
            .NotEmpty().WithMessage("Confirmation Code alanı boş bırakılamaz.");
    }
}