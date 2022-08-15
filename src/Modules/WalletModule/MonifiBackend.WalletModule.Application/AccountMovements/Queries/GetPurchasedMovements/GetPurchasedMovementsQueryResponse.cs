using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetPurchasedMovements;

public class GetPurchasedMovementsQueryResponse
{
    public GetPurchasedMovementsQueryResponse(List<AccountMovement> accountMovements)
    {
        Movements = accountMovements.Select(x => new GetPurchasedAccountMovementsSingleQueryResponse(x)).ToList();
    }
    public GetPurchasedMovementsQueryResponse(List<GetPurchasedAccountMovementsSingleQueryResponse> accountMovements)
    {
        Movements = accountMovements;
    }
    public List<GetPurchasedAccountMovementsSingleQueryResponse> Movements { get; set; }
}
public class GetPurchasedAccountMovementsSingleQueryResponse
{
    public GetPurchasedAccountMovementsSingleQueryResponse(int id, string fullName, bool isReferanceUser, decimal amount, DateTime createdAt, DateTime transferTime, TransactionStatus transactionStatus, ActionType actionType, int walletId, string walletAddress, Network cryptoNetwork, int packageDetailId, string packageDetailName, int packageDetailDuration, int packageDetailCommission, Package package)
    {
        FullName = fullName;
        IsReferanceUser = isReferanceUser;
        Id = id;
        Amount = amount;
        CreatedAt = createdAt;
        BlockEndDate = transferTime == default(DateTime) ? null : transferTime.AddMonths(packageDetailDuration);
        TransactionStatus = transactionStatus.ToTransactionStatus();
        ActionType = actionType.ToActionType();
        Wallet = new GetPurchasedWalletResponse(walletId, walletAddress, cryptoNetwork);
        PackageDetail = new GetPurchasedPackageDetailResponse(packageDetailId, packageDetailName, packageDetailDuration, packageDetailCommission, package);
        TotalDay = BlockEndDate == null ? 0 : BlockEndDate.Value.Subtract(transferTime).Days;
        RemainDay = BlockEndDate == null ? 0 : BlockEndDate.Value.Subtract(DateTime.Now).Days;
        PassedDay = TotalDay - RemainDay;
    }
    public GetPurchasedAccountMovementsSingleQueryResponse(AccountMovement accountMovement)
    {
        Id = accountMovement.Id;
        Amount = accountMovement.Amount;
        CreatedAt = accountMovement.CreatedAt;
        BlockEndDate = accountMovement.TransferTime == default(DateTime) ? null : accountMovement.TransferTime.AddMonths(accountMovement.PackageDetail.Duration);
        TransactionStatus = accountMovement.TransactionStatus.ToTransactionStatus();
        ActionType = accountMovement.ActionType.ToActionType();
        Wallet = new GetPurchasedWalletResponse(accountMovement.Wallet.Id, accountMovement.Wallet.WalletAddress, accountMovement.Wallet.CryptoNetwork);
        PackageDetail = new GetPurchasedPackageDetailResponse(accountMovement.PackageDetail.Id, accountMovement.PackageDetail.Name, accountMovement.PackageDetail.Duration, accountMovement.PackageDetail.Commission, accountMovement.PackageDetail.Package);
        TotalDay = BlockEndDate == null ? 0 : BlockEndDate.Value.Subtract(accountMovement.TransferTime).Days;
        RemainDay = BlockEndDate == null ? 0 : BlockEndDate.Value.Subtract(DateTime.Now).Days;
        PassedDay = TotalDay - RemainDay;
    }

    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string TransactionStatus { get; set; }
    public string ActionType { get; set; }
    public string FullName { get; set; }
    public bool IsReferanceUser { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? BlockEndDate { get; set; }
    public GetPurchasedWalletResponse Wallet { get; set; }
    public GetPurchasedPackageDetailResponse PackageDetail { get; set; }
    public int RemainDay { get; set; }
    public int TotalDay { get; set; }
    public int PassedDay { get; set; }
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
    public GetPurchasedPackageDetailResponse(int id, string name, int duration, int commission, Package package)
    {
        Id = id;
        Name = name;
        Duration = duration;
        Commission = commission;
        Package = package == null ? new GetPurchasedPackageResponse() : new GetPurchasedPackageResponse(package.Id, package.Name);

    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public int Commission { get; set; }
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