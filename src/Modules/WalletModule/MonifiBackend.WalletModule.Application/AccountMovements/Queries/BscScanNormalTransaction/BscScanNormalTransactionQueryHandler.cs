using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Domain.Accounts;
using MonifiBackend.Core.Domain.BscScans;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Queries.BscScanNormalTransaction;

internal class BscScanNormalTransactionQueryHandler : IQueryHandler<BscScanNormalTransactionQuery, BscScanNormalTransactionQueryResponse>
{
    private readonly IBscScanAccountsDataPort _bscScanAccountsService;
    public BscScanNormalTransactionQueryHandler(IBscScanAccountsDataPort bscScanAccountsService)
    {
        _bscScanAccountsService = bscScanAccountsService;
    }
    public async Task<BscScanNormalTransactionQueryResponse> Handle(BscScanNormalTransactionQuery request, CancellationToken cancellationToken)
    {
        var bnbBalanceRequest = new NormalTransactionsRequest
        {
            Address = request.Address,
        };
        var bnbBalance = await _bscScanAccountsService.GetNormalTransactionsByAddressAsync(bnbBalanceRequest);

        return new BscScanNormalTransactionQueryResponse();
    }
}