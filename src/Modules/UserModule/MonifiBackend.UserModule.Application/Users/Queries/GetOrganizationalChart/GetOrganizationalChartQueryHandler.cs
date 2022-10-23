using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Infrastructure.Localize;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetOrganizationalCharts;

internal class GetOrganizationalChartQueryHandler : IQueryHandler<GetOrganizationalChartQuery, GetOrganizationalChartQueryResponse>
{
    private readonly IStringLocalizer<Resource> _stringLocalizer;
    private readonly IUserQueryDataPort _userQueryDataPort;
    public GetOrganizationalChartQueryHandler(IUserQueryDataPort userQueryDataPort, IStringLocalizer<Resource> stringLocalizer)
    {
        _userQueryDataPort = userQueryDataPort;
        _stringLocalizer = stringLocalizer;
    }
    public async Task<GetOrganizationalChartQueryResponse> Handle(GetOrganizationalChartQuery request, CancellationToken cancellationToken)
    {
        var allUsers = await _userQueryDataPort.GetAsync();
        var users = new List<User>();

        //userId tespit ediliyor.
        if (request.UserId > 0)
        {
            users = allUsers.Where(p => p.Id == request.UserId || p.ReferanceUser == request.UserId).ToList();

        }
        else if (!string.IsNullOrEmpty(request.UserEmail))
        {
            int userId = allUsers.Where(p => p.Email == request.UserEmail).FirstOrDefault().Id;

            users = allUsers.Where(p => p.Id == userId || p.ReferanceUser == userId).ToList();

        }
        else if (request.UserId > 0 || !string.IsNullOrEmpty(request.UserEmail) || !string.IsNullOrEmpty(request.UserName))
        {
            int userId = allUsers.Where(p => p.Username == request.UserName).FirstOrDefault().Id;

            users = allUsers.Where(p => p.Id == userId || p.ReferanceUser == userId).ToList();

        }
        else users = allUsers;



        return new GetOrganizationalChartQueryResponse(users);
    }
}
