using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.UserModule.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : ICommand<RegisterUserCommandResponse>
    {
        public RegisterUserCommand(string email, string password, string userName, bool terms)
        {
            Email = email;
            Password = password;
            UserName = userName;
            Terms = terms;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool Terms { get; set; }
    }

    internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Mail alanı boş bırakılamaz.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.");
            RuleFor(x => x.Terms)
                .NotEmpty().WithMessage("Terms alanı boş bırakılamaz.");

        }
    }
}
