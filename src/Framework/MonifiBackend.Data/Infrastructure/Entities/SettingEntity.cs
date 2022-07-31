using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonifiBackend.Data.Domain.Entities;
using MonifiBackend.Data.Domain.Entities.Configurations;

namespace MonifiBackend.Data.Infrastructure.Entities;

public class SettingEntity : BaseActivityEntity
{
    public string Name { get; set; }
    public int MaxSale { get; set; }
    public int MaxSaleApy { get; set; }
    public int MaxSaleReferance { get; set; }
}
public class SettingEntityConfiguration : IEntityTypeConfiguration<SettingEntity>
{
    public void Configure(EntityTypeBuilder<SettingEntity> builder)
    {
        builder.ToTable("Settings");
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.MaxSale).IsRequired();
        builder.Property(x => x.MaxSaleApy).IsRequired();
        builder.Property(x => x.MaxSaleReferance).IsRequired();

        BaseActivityConfiguration.Configure(builder);
    }
}