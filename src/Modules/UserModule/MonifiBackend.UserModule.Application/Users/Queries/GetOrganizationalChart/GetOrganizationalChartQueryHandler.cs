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
        var users = await _userQueryDataPort.GetAsync();


        return new GetOrganizationalChartQueryResponse(users);
    }
}
