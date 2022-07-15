using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetNetworkUsers;

public class GetNetworkUsersQuery : IQuery<GetNetworkUsersQueryResponse>
{
    public GetNetworkUsersQuery(int userId)
    {
        UserId = userId;
    }
    public int UserId { get; }

}
internal class GetNetworkUsersQueryValidator : AbstractValidator<GetNetworkUsersQuery>
{
    public GetNetworkUsersQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId alanı boş bırakılamaz.");
    }
}