using MonifiBackend.Core.Domain.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.WalletModule.Domain.Packages;

namespace MonifiBackend.WalletModule.Domain.AccountMovements;

public sealed class AccountMovement : BaseActivityDomain<int>, IAggregateRoot
{
    public decimal Amount { get; private set; }
    public TransactionStatus TransactionStatus { get; private set; }
    public ActionType ActionType { get; private set; }
    public PackageDetail PackageDetail { get; protected set; }
    public Wallet Wallet { get; protected set; }

    public static AccountMovement Default() => new();

    public static AccountMovement CreateNew(
        decimal amount,
        BaseStatus status,
        TransactionStatus transactionStatus,
        ActionType actionType,
        PackageDetail packageDetail,
        Wallet wallet)
    {
        return new AccountMovement()
        {
            Amount = amount,
            Status = status,
            ActionType = actionType,
            TransactionStatus = transactionStatus,
            PackageDetail = packageDetail,
            Wallet = wallet
        };
    }
    public static AccountMovement Map(
        int id,
        BaseStatus status,
        DateTime createdAt,
        DateTime modifiedAt,
        decimal amount,
        TransactionStatus transactionStatus,
        ActionType actionType,
        PackageDetail packageDetail,
        Wallet wallet)
    {
        return new AccountMovement()
        {
            Id = id,
            Status = status,
            CreatedAt = createdAt,
            ModifiedAt = modifiedAt,
            PackageDetail = packageDetail,
            Amount = amount,
            ActionType = actionType,
            TransactionStatus = transactionStatus,
            Wallet = wallet
        };
    }

}
