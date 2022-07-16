using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetAccountMovements;

public class GetAccountMovementsQueryResponse
{
    public GetAccountMovementsQueryResponse(List<AccountMovement> accountMovements)
    {
        Movements = accountMovements.Select(x => new GetAccountMovementsSingleQueryResponse(x)).ToList();
    }
    public List<GetAccountMovementsSingleQueryResponse> Movements { get; set; }
}
public class GetAccountMovementsSingleQueryResponse
{
    public GetAccountMovementsSingleQueryResponse(AccountMovement accountMovement)
    {
        Id = accountMovement.Id;
        Amount = accountMovement.Amount;
        TransactionStatus = accountMovement.TransactionStatus.ToTransactionStatus();
        ActionType = accountMovement.ActionType.ToActionType();
        Wallet = new GetMovementsWalletResponse(accountMovement.Wallet.Id, accountMovement.Wallet.WalletAddress, accountMovement.Wallet.CryptoNetwork);
        PackageDetail = new GetMovementsPackageDetailResponse(accountMovement.PackageDetail.Id, accountMovement.PackageDetail.Name, accountMovement.PackageDetail.Package);
    }

    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string TransactionStatus { get; set; }
    public string ActionType { get; set; }
    public GetMovementsWalletResponse Wallet { get; set; }
    public GetMovementsPackageDetailResponse PackageDetail { get; set; }
}
public class GetMovementsWalletResponse
{
    public GetMovementsWalletResponse(int id, string walletAddress, Network network)
    {
        Id = id;
        WalletAddress = walletAddress;
        Network = new GetMovementsNetworkResponse(network.Id, network.Name);
    }

    public int Id { get; set; }
    public string WalletAddress { get; set; }
    public GetMovementsNetworkResponse Network { get; set; }
}
public class GetMovementsNetworkResponse
{
    public GetMovementsNetworkResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}
public class GetMovementsPackageDetailResponse
{
    public GetMovementsPackageDetailResponse(int id, string name, Package package)
    {
        Id = id;
        Name = name;
        Package = new GetMovementsPackageResponse(package.Id, package.Name);
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public GetMovementsPackageResponse Package { get; set; }
}
public class GetMovementsPackageResponse
{
    public GetMovementsPackageResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; }
}