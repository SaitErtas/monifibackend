using Microsoft.EntityFrameworkCore;
using MonifiBackend.Data.Domain.Entities;
using MonifiBackend.Data.Infrastructure.Entities;

namespace MonifiBackend.Data.Infrastructure.Contexts
{
    public class MonifiBackendDbContext : DbContext, IMonifiBackendDbContext
    {
        public MonifiBackendDbContext(DbContextOptions<MonifiBackendDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<UserPhoneEntity>().ToTable("UserPhones");
            modelBuilder.Entity<AccountMovementEntity>().ToTable("AccountMovements");
            modelBuilder.Entity<PackageEntity>().ToTable("Packages");

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserPhoneEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountMovementEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PackageEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseActivityEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entityEntry in entries)
            {
                ((BaseActivityEntity)entityEntry.Entity).ModifiedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                    ((BaseActivityEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                else
                    entityEntry.Property("CreatedAt").IsModified = false;
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseActivityEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entityEntry in entries)
            {
                ((BaseActivityEntity)entityEntry.Entity).ModifiedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                    ((BaseActivityEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                else
                    entityEntry.Property("CreatedAt").IsModified = false;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserPhoneEntity> UserPhones { get; set; }
        public DbSet<PackageEntity> Packages { get; set; }
    }
}
