using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetUser;

internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResponse>
{
    private readonly IUserQueryDataPort _userQueryDataPort;
    private readonly IJwtUtils _jwtUtils;
    public GetUserQueryHandler(IUserQueryDataPort userQueryDataPort, IJwtUtils jwtUtils)
    {
        _userQueryDataPort = userQueryDataPort;
        _jwtUtils = jwtUtils;
    }
    public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetAsync(request.UserId);
        AppRule.ExistsAndActive(user, new BusinessValidationException("User not found exception.", $"User not found exception. Email: {request.UserId}"));

        return new GetUserQueryResponse(user);
    }
}
