using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Core.Infrastructure.Localize;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetUser;

internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResponse>
{
    private readonly IStringLocalizer<Resource> _stringLocalizer;
    private readonly IUserQueryDataPort _userQueryDataPort;
    public GetUserQueryHandler(IUserQueryDataPort userQueryDataPort, IStringLocalizer<Resource> stringLocalizer)
    {
        _userQueryDataPort = userQueryDataPort;
        _stringLocalizer = stringLocalizer;
    }
    public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetAsync(request.UserId);
        AppRule.ExistsAndActive(user, new BusinessValidationException($"{string.Format(_stringLocalizer["NotFound"], request.UserId)}", $"{string.Format(_stringLocalizer["NotFound"], request.UserId)} UserId: {request.UserId}"));

        return new GetUserQueryResponse(user);
    }
}
