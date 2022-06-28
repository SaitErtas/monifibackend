using Microsoft.EntityFrameworkCore;
using MonifiBackend.Data.Infrastructure.Entities;

namespace MonifiBackend.Data.Infrastructure.Contexts
{
    public interface IMonifiBackendDbContext : IDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<UserPhoneEntity> UserPhones { get; set; }
    }
}
