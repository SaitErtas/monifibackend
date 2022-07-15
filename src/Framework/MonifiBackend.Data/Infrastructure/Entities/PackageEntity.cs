using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonifiBackend.Data.Domain.Entities;
using MonifiBackend.Data.Domain.Entities.Configurations;

namespace MonifiBackend.Data.Infrastructure.Entities;

public class PackageEntity : BaseActivityEntity
{
    public string Name { get; set; }
    public virtual ICollection<AccountMovementEntity> AccountMovements { get; set; }
    public virtual ICollection<PackageDetailEntity> PackageDetails { get; set; }
}
public class PackageEntityConfiguration : IEntityTypeConfiguration<PackageEntity>
{
    public void Configure(EntityTypeBuilder<PackageEntity> builder)
    {
        builder.ToTable("Packages");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

        builder.HasMany(x => x.AccountMovements).WithOne(x => x.Package);
        builder.HasMany(x => x.PackageDetails).WithOne(x => x.Package);

        BaseActivityConfiguration.Configure(builder);
    }
}
