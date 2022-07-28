using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetPurchasedMovements;

internal class GetPurchasedMovementsQueryHandler : IQueryHandler<GetPurchasedMovementsQuery, GetPurchasedMovementsQueryResponse>
{
    private readonly IAccountMovementQueryDataPort _accountMovementQueryDataPort;
    private readonly IPackageQueryDataPort _packageQueryDataPort;
    public GetPurchasedMovementsQueryHandler(IAccountMovementQueryDataPort accountMovementQueryDataPort, IPackageQueryDataPort packageQueryDataPort)
    {
        _accountMovementQueryDataPort = accountMovementQueryDataPort;
        _packageQueryDataPort = packageQueryDataPort;
    }
    public async Task<GetPurchasedMovementsQueryResponse> Handle(GetPurchasedMovementsQuery request, CancellationToken cancellationToken)
    {
        var accountMovements = await _accountMovementQueryDataPort.GetPurchasedMovementAsync(request.UserId);
        var packages = await _packageQueryDataPort.GetsAsync();

        foreach (var accountMovement in accountMovements)
        {
            var package = packages.FirstOrDefault(x => x.Details.Any(y => y.Id == accountMovement.PackageDetail.Id));
            accountMovement.PackageDetail.SetPackage(package);
        }
        return new GetPurchasedMovementsQueryResponse(accountMovements);
    }
}