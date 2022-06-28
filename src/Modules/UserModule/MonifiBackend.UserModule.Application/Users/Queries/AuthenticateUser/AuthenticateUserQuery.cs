using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MonifiBackend.UserModule.Application.Users.Queries.AuthenticateUser
{
    public class AuthenticateUserQuery : IQuery<AuthenticateUserQueryResponse>
    {
        public AuthenticateUserQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
        [Required]
        [SwaggerSchema(Nullable = false)]
        public string Email { get; }

        [Required]
        [SwaggerSchema(Nullable = false)]
        public string Password { get; }
    }
    internal class AuthenticateUserQueryValidator : AbstractValidator<AuthenticateUserQuery>
    {
        public AuthenticateUserQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş bırakılamaz.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");
        }
    }
}
