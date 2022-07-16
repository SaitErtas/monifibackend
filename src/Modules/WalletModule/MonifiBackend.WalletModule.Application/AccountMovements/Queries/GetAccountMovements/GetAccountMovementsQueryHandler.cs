using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.WalletModule.Domain.AccountMovements;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetAccountMovements;

internal class GetAccountMovementsQueryHandler : IQueryHandler<GetAccountMovementsQuery, GetAccountMovementsQueryResponse>
{
    private readonly IAccountMovementQueryDataPort _accountMovementQueryDataPort;
    public GetAccountMovementsQueryHandler(IAccountMovementQueryDataPort accountMovementQueryDataPort)
    {
        _accountMovementQueryDataPort = accountMovementQueryDataPort;
    }
    public async Task<GetAccountMovementsQueryResponse> Handle(GetAccountMovementsQuery request, CancellationToken cancellationToken)
    {
        var accountMovement = await _accountMovementQueryDataPort.GetAccountMovementAsync(request.UserId);

        return new GetAccountMovementsQueryResponse(accountMovement);
    }
}