using MonifiBackend.Core.Domain.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.WalletModule.Domain.Packages;
using MonifiBackend.WalletModule.Domain.Users;

namespace MonifiBackend.WalletModule.Domain.AccountMovements;

public sealed class AccountMovement : BaseActivityDomain<int>, IAggregateRoot
{
    public decimal Amount { get; private set; }
    public TransactionStatus TransactionStatus { get; private set; }
    public ActionType ActionType { get; private set; }
    public Package Package { get; protected set; }
    public User User { get; protected set; }

    public static AccountMovement Default() => new();

    public static AccountMovement CreateNew(
        decimal amount,
        BaseStatus status)
    {
        return new AccountMovement()
        {
            Amount = amount,
            Status = status
        };
    }
    public static AccountMovement Map(
        int id,
        BaseStatus status,
        DateTime createdAt,
        DateTime modifiedAt,
        User user,
        Package package)
    {
        return new AccountMovement()
        {
            Id = id,
            Status = status,
            CreatedAt = createdAt,
            ModifiedAt = modifiedAt,
            Package = package,
            User = user
        };
    }

}
