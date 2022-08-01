using Microsoft.EntityFrameworkCore;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Data.Infrastructure.Contexts;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Infrastructure.Extensions.Mappers;

namespace MonifiBackend.WalletModule.Infrastructure.AccountMovements;

public class AccountMovementQueryDataAdapter : IAccountMovementQueryDataPort
{
    private readonly IMonifiBackendDbContext _dbContext;
    public AccountMovementQueryDataAdapter(IMonifiBackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AccountMovement>> GetAccountMovementAsync(int userId)
    {
        var entity = await _dbContext.AccountMovements
            .Where(w => w.Wallet.UserId == userId)
            .Include(i => i.PackageDetail)
            .ThenInclude(i => i.Package)
            .Include(i => i.Wallet)
            .ThenInclude(i => i.CryptoNetwork)
            .ToListAsync();
        return entity.Select(s => s.Map()).ToList();
    }

    public async Task<List<AccountMovement>> GetPurchasedMovementAsync(int userId)
    {
        var entity = await _dbContext.AccountMovements
            .Where(w => w.Wallet.UserId == userId && w.ActionType == ActionType.Sale.ToInt())
            .Include(i => i.PackageDetail)
            .ThenInclude(i => i.Package)
            .Include(i => i.Wallet)
            .ThenInclude(i => i.CryptoNetwork)
            .ToListAsync();

        return entity.Select(s => s.Map()).ToList();
    }

    public async Task<Wallet> GetUserWalletAsync(int userId)
    {
        var entity = await _dbContext.Wallets
            .Include(i => i.CryptoNetwork)
            .FirstOrDefaultAsync(w => w.UserId == userId);

        return entity.Map();
    }

    public async Task<decimal> GetTotalBonusAsync()
    {
        var totalSale = await _dbContext.AccountMovements
            .Where(w => w.ActionType == ActionType.Bonus.ToInt())
            .SumAsync(s => s.Amount);
        return totalSale;
    }

    public async Task<decimal> GetTotalSaleAsync()
    {
        var totalSale = await _dbContext.AccountMovements
            .Where(w => w.ActionType == ActionType.Sale.ToInt())
            .SumAsync(s => s.Amount);
        return totalSale;
    }
}
