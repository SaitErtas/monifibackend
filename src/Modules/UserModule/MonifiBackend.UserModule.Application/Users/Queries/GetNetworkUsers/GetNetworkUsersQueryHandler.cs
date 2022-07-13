using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetNetworkUsers;

internal class GetNetworkUsersQueryHandler : IQueryHandler<GetNetworkUsersQuery, GetNetworkUsersQueryResponse>
{
    private readonly IUserQueryDataPort _userQueryDataPort;
    public GetNetworkUsersQueryHandler(IUserQueryDataPort userQueryDataPort)
    {
        _userQueryDataPort = userQueryDataPort;
    }
    public async Task<GetNetworkUsersQueryResponse> Handle(GetNetworkUsersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userQueryDataPort.GetAsync(request.UserId);
        var meFirstNetworkUsers = await _userQueryDataPort.GetMeFirstNetworkAsync(request.UserId);
        var networkUserIds = meFirstNetworkUsers.Select(x => x.Id).ToList();
        networkUserIds.Add(request.UserId);
        var networkUsers = await _userQueryDataPort.GetAllNetworkAsync(networkUserIds);

        return new GetNetworkUsersQueryResponse(user, meFirstNetworkUsers, networkUsers);
    }
}