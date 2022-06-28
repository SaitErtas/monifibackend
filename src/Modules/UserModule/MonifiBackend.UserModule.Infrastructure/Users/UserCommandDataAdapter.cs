using MonifiBackend.Data.Infrastructure.Contexts;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Infrastructure.Extensions.Mappers;

namespace MonifiBackend.UserModule.Infrastructure.Users
{
    public class UserCommandDataAdapter : IUserCommandDataPort
    {
        private readonly IMonifiBackendDbContext _dbContext;
        public UserCommandDataAdapter(IMonifiBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateAsync(User user)
        {
            var userEntity = user.Map();
            await _dbContext.Users.AddAsync(userEntity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? userEntity.Id : 0;
        }

        public async Task<bool> SaveAsync(User user)
        {
            _dbContext.Users.Update(user.Map());
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}
