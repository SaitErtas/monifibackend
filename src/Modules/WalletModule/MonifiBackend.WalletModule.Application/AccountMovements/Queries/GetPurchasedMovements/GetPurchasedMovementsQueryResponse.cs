using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetPurchasedMovements;

public class GetPurchasedMovementsQueryResponse
{
    public GetPurchasedMovementsQueryResponse(List<AccountMovement> accountMovements)
    {
        Movements = accountMovements.Select(x => new GetPurchasedAccountMovementsSingleQueryResponse(x)).ToList();
    }
    public List<GetPurchasedAccountMovementsSingleQueryResponse> Movements { get; set; }
}
public class GetPurchasedAccountMovementsSingleQueryResponse
{
    public GetPurchasedAccountMovementsSingleQueryResponse(AccountMovement accountMovement)
    {
        Id = accountMovement.Id;
        Amount = accountMovement.Amount;
        TransactionStatus = accountMovement.TransactionStatus.ToTransactionStatus();
        ActionType = accountMovement.ActionType.ToActionType();
        Wallet = new GetPurchasedWalletResponse(accountMovement.Wallet.Id, accountMovement.Wallet.WalletAddress, accountMovement.Wallet.CryptoNetwork);
        PackageDetail = new GetPurchasedPackageDetailResponse(accountMovement.PackageDetail.Id, accountMovement.PackageDetail.Name, accountMovement.PackageDetail.Package);
    }

    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string TransactionStatus { get; set; }
    public string ActionType { get; set; }
    public GetPurchasedWalletResponse Wallet { get; set; }
    public GetPurchasedPackageDetailResponse PackageDetail { get; set; }
}
public class GetPurchasedWalletResponse
{
    public GetPurchasedWalletResponse(int id, string walletAddress, Network network)
    {
        Id = id;
        WalletAddress = walletAddress;
        Network = new GetPurchasedNetworkResponse(network.Id, network.Name);
    }

    public int Id { get; set; }
    public string WalletAddress { get; set; }
    public GetPurchasedNetworkResponse Network { get; set; }
}
public class GetPurchasedNetworkResponse
{
    public GetPurchasedNetworkResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}
public class GetPurchasedPackageDetailResponse
{
    public GetPurchasedPackageDetailResponse(int id, string name, Package package)
    {
        Id = id;
        Name = name;
        Package = package == null ? new GetPurchasedPackageResponse() : new GetPurchasedPackageResponse(package.Id, package.Name);

    }
    public int Id { get; set; }
    public string Name { get; set; }
    public GetPurchasedPackageResponse Package { get; set; }
}
public class GetPurchasedPackageResponse
{
    public GetPurchasedPackageResponse()
    {

    }
    public GetPurchasedPackageResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; }
}