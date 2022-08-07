using Microsoft.EntityFrameworkCore;
using MonifiBackend.Data.Infrastructure.Contexts;
using MonifiBackend.WalletModule.Domain.Users;
using MonifiBackend.WalletModule.Infrastructure.Extensions.Mappers;

namespace MonifiBackend.WalletModule.Infrastructure.Users;

public class UserQueryDataAdapter : IUserQueryDataPort
{
    private readonly IMonifiBackendDbContext _dbContext;
    public UserQueryDataAdapter(IMonifiBackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetUserCountAsync()
    {
        return await _dbContext.Users.CountAsync();
    }
    public async Task<int> GetReferanceCountAsync(int id)
    {
        return await _dbContext.Users.CountAsync(x => x.ReferanceUser == id);
    }

    public async Task<User> GetUserAsync(int id)
    {
        var userEntity = await _dbContext.Users
            .Include(i => i.Wallet)
            .FirstOrDefaultAsync(x => x.Id == id);
        return userEntity.Map();
    }
}