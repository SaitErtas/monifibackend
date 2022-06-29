using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonifiBackend.Data.Domain.Entities;
using MonifiBackend.Data.Domain.Entities.Configurations;

namespace MonifiBackend.Data.Infrastructure.Entities;

public class PackageEntity : BaseActivityEntity
{
    public string Name { get; set; }
    public int Duration { get; set; }
    public decimal Commission { get; set; }
}
public class PackageEntityConfiguration : IEntityTypeConfiguration<PackageEntity>
{
    public void Configure(EntityTypeBuilder<PackageEntity> builder)
    {
        builder.ToTable("Packages");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Duration).IsRequired();
        builder.Property(x => x.Commission).HasPrecision(3, 2).IsRequired();

        BaseActivityConfiguration.Configure(builder);
    }
}
