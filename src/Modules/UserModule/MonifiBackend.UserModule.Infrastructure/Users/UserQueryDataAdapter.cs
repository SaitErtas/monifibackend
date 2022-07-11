using Microsoft.EntityFrameworkCore;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Resources;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Core.Infrastructure.Extensions;
using MonifiBackend.Data.Infrastructure.Contexts;
using MonifiBackend.Data.Infrastructure.Entities;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Infrastructure.Extensions.Mappers;
using System.Linq.Expressions;

namespace MonifiBackend.UserModule.Infrastructure.Users;

public class UserQueryDataAdapter : IUserQueryDataPort
{
    private readonly IMonifiBackendDbContext _dbContext;
    public UserQueryDataAdapter(IMonifiBackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<QueryResult<User>> GetListAsync(QueryObject userQuery)
    {
        var query = _dbContext.Users
            .Include(x => x.Phones.Where(q => q.Status == BaseStatus.Active.ToInt()))
             .Where(x => x.Status == BaseStatus.Active.ToInt())
            .AsQueryable();


        var columnsOrder = new Dictionary<string, Expression<Func<UserEntity, object>>>
        {
            ["id"] = x => x.Id
        };

        query = query.ApplyOrdering(userQuery, columnsOrder);
        query = query.ApplyPaging(userQuery);

        var queryResult = new QueryResult<User>(await query.CountAsync(), await query.Select(x => x.Map()).ToListAsync());
        return queryResult;
    }
    public async Task<User> GetAsync(int id)
    {
        var userEntity = await _dbContext.Users
            .Include(x => x.Phones.Where(q => q.Status == BaseStatus.Active.ToInt()))
            .FirstOrDefaultAsync(x => x.Id == id && x.Status == BaseStatus.Active.ToInt());
        return userEntity.Map();
    }
    public async Task<User> GetAsync(string email, string password)
    {
        var userEntity = await _dbContext.Users
            .Include(x => x.Phones.Where(q => q.Status == BaseStatus.Active.ToInt()))
            .FirstOrDefaultAsync(x => x.Email == email && x.Password == password && x.Status == BaseStatus.Active.ToInt());
        return userEntity.Map();
    }
    public async Task<bool> CheckUserEmailAsync(string email)
    {
        return await _dbContext.Users
            .AnyAsync(x => x.Email == email && x.Status == BaseStatus.Active.ToInt());
    }
    public async Task<User> GetReferanceCodeUserAsync(string referanceCode)
    {
        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.ReferanceCode == referanceCode && x.Status == BaseStatus.Active.ToInt());
        return userEntity.Map();
    }
    public async Task<bool> CheckUserReferanceCodeAsync(string referanceCode)
    {
        return await _dbContext.Users
            .AnyAsync(x => x.ReferanceCode == referanceCode && x.Status == BaseStatus.Active.ToInt());
    }
    public async Task<bool> CheckUserConfirmationCodeAsync(string confirmationCode)
    {
        return await _dbContext.Users
            .AnyAsync(x => x.ConfirmationCode == confirmationCode);
    }
    public async Task<User> GetEmailAsync(string email)
    {
        var userEntity = await _dbContext.Users
            .Include(x => x.Phones.Where(q => q.Status == BaseStatus.Active.ToInt()))
            .FirstOrDefaultAsync(x => x.Email == email && x.Status == BaseStatus.Active.ToInt());
        return userEntity.Map();
    }

    public async Task<User> GetUserConfirmationCodeAsync(string confirmationCode)
    {
        var userEntity = await _dbContext.Users
            .Include(x => x.Phones.Where(q => q.Status == BaseStatus.Active.ToInt()))
            .FirstOrDefaultAsync(x => x.ConfirmationCode == confirmationCode);
        return userEntity.Map();
    }
}
