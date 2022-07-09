using MonifiBackend.Data.Infrastructure.Contexts;
using MonifiBackend.WalletModule.Domain.AccountMovements;

namespace MonifiBackend.WalletModule.Infrastructure.AccountMovements;

public class AccountMovementCommandDataAdapter : IAccountMovementCommandDataPort
{
    private readonly IMonifiBackendDbContext _dbContext;
    public AccountMovementCommandDataAdapter(IMonifiBackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
