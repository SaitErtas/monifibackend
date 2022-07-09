using MonifiBackend.Data.Infrastructure.Contexts;
using MonifiBackend.WalletModule.Domain.AccountMovements;

namespace MonifiBackend.WalletModule.Infrastructure.AccountMovements;

public class AccountMovementQueryDataAdapter : IAccountMovementQueryDataPort
{
    private readonly IMonifiBackendDbContext _dbContext;
    public AccountMovementQueryDataAdapter(IMonifiBackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
