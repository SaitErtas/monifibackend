using MonifiBackend.Data.Infrastructure.Contexts;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Infrastructure.Extensions.Mappers;

namespace MonifiBackend.WalletModule.Infrastructure.AccountMovements;

public class AccountMovementCommandDataAdapter : IAccountMovementCommandDataPort
{
    private readonly IMonifiBackendDbContext _dbContext;
    public AccountMovementCommandDataAdapter(IMonifiBackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> SaveAsync(Wallet wallet)
    {
        _dbContext.Wallets.Update(wallet.Map());
        return (await _dbContext.SaveChangesAsync()) > 0;
    }
}
