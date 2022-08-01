using Microsoft.EntityFrameworkCore;
using MonifiBackend.Data.Infrastructure.Contexts;
using MonifiBackend.WalletModule.Domain.Users;

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
}