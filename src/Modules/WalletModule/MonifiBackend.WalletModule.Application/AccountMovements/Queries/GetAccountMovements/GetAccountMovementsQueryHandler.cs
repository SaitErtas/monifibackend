using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetAccountMovements;

internal class GetAccountMovementsQueryHandler : IQueryHandler<GetAccountMovementsQuery, GetAccountMovementsQueryResponse>
{
    public Task<GetAccountMovementsQueryResponse> Handle(GetAccountMovementsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}