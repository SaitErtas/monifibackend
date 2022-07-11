using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.UserModule.Application.Users.Commands.RegisterUser;

public class RegisterUserCommand : ICommand<RegisterUserCommandResponse>
{
    public RegisterUserCommand(string email, string password, bool terms, string referanceCode)
    {
        Email = email;
        Password = password;
        Terms = terms;
        ReferanceCode = referanceCode;
    }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Terms { get; set; }
    public string ReferanceCode { get; set; }
}

internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Mail alanı boş bırakılamaz.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");
        RuleFor(x => x.Terms)
            .NotEmpty().WithMessage("Terms alanı boş bırakılamaz.");
        RuleFor(x => x.ReferanceCode)
            .NotEmpty().WithMessage("Referans Kodu alanı boş bırakılamaz.");

    }
}
