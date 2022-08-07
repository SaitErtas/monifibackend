using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.WalletModule.Domain.AccountMovements;

public interface IAccountMovementQueryDataPort : IQueryDataPort
{
    Task<List<AccountMovement>> GetAllMovementAsync(TransactionStatus transactionStatus);
    Task<List<AccountMovement>> GetAllMovementAsync(int userId, TransactionStatus transactionStatus);
    Task<List<AccountMovement>> GetUserMovementAsync(int userId);
    Task<List<AccountMovement>> GetPurchasedMovementAsync(int userId);
    Task<List<AccountMovement>> GetAccountMovementAsync(int userId);
    Task<Wallet> GetUserWalletAsync(int userId);
    Task<decimal> GetTotalSaleAsync();
    Task<decimal> GetTotalBonusAsync();
}
