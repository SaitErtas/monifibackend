using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetUser
{
    public class GetUserQuery : IQuery<GetUserQueryResponse>
    {
        public GetUserQuery(int userId)
        {
            UserId = userId;
        }
        [Required]
        [SwaggerSchema(Nullable = false)]
        public int UserId { get; }

    }
    internal class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId alanı boş bırakılamaz.");

        }
    }
}
