using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MonifiBackend.UserModule.Application.Users.Queries.UserData
{
    public class UserQuery : IQuery<UserQueryResponse>
    {
        public UserQuery(int userId)
        {
            UserId = userId;
        }
        [Required]
        [SwaggerSchema(Nullable = false)]
        public int UserId { get; }

    }
    internal class UserQueryValidator : AbstractValidator<UserQuery>
    {
        public UserQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId alanı boş bırakılamaz.");

        }
    }
}
