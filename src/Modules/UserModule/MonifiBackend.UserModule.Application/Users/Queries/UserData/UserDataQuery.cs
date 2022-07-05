using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MonifiBackend.UserModule.Application.Users.Queries.UserData
{
    public class UserDataQuery : IQuery<UserDataQueryResponse>
    {
        public UserDataQuery(int userId)
        {
            UserId = userId;
        }
        [Required]
        [SwaggerSchema(Nullable = false)]
        public int UserId { get; }

    }
    internal class UserDataQueryValidator : AbstractValidator<UserDataQuery>
    {
        public UserDataQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId alanı boş bırakılamaz.");

        }
    }
}
