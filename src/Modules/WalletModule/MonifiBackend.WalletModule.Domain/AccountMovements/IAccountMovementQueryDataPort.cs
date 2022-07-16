using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.WalletModule.Domain.AccountMovements;

public interface IAccountMovementQueryDataPort : IQueryDataPort
{
    Task<List<AccountMovement>> GetPurchasedMovementAsync(int userId);
    Task<List<AccountMovement>> GetAccountMovementAsync(int userId);
}
