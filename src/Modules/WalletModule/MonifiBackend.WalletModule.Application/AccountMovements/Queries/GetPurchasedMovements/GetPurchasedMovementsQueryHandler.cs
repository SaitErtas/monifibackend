using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.WalletModule.Domain.AccountMovements;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetPurchasedMovements;

internal class GetPurchasedMovementsQueryHandler : IQueryHandler<GetPurchasedMovementsQuery, GetPurchasedMovementsQueryResponse>
{
    private readonly IAccountMovementQueryDataPort _accountMovementQueryDataPort;
    public GetPurchasedMovementsQueryHandler(IAccountMovementQueryDataPort accountMovementQueryDataPort)
    {
        _accountMovementQueryDataPort = accountMovementQueryDataPort;
    }
    public async Task<GetPurchasedMovementsQueryResponse> Handle(GetPurchasedMovementsQuery request, CancellationToken cancellationToken)
    {
        var accountMovement = await _accountMovementQueryDataPort.GetPurchasedMovementAsync(request.UserId);

        return new GetPurchasedMovementsQueryResponse(accountMovement);
    }
}