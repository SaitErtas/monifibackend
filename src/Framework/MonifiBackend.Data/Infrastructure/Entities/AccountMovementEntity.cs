using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonifiBackend.Data.Domain.Entities;
using MonifiBackend.Data.Domain.Entities.Configurations;

namespace MonifiBackend.Data.Infrastructure.Entities;

public class AccountMovementEntity : BaseActivityEntity
{
    public int PackageId { get; set; }
    public int WalletId { get; set; }
    public int ActionType { get; set; }
    public int TransactionStatus { get; set; }
    public decimal Amount { get; set; }
    public virtual PackageEntity Package { get; set; }
    public virtual WalletEntity Wallet { get; set; }
}
public class AccountMovementEntityConfiguration : IEntityTypeConfiguration<AccountMovementEntity>
{
    public void Configure(EntityTypeBuilder<AccountMovementEntity> builder)
    {
        builder.ToTable("AccountMovements");
        builder.Property(x => x.PackageId).IsRequired();
        builder.Property(x => x.WalletId).IsRequired();
        builder.Property(x => x.ActionType).IsRequired();
        builder.Property(x => x.TransactionStatus).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(3, 2).IsRequired();


        builder.HasOne(x => x.Wallet).WithMany(x => x.AccountMovements).HasForeignKey(x => x.WalletId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Package).WithMany(x => x.AccountMovements).HasForeignKey(x => x.PackageId).OnDelete(DeleteBehavior.NoAction);

        BaseActivityConfiguration.Configure(builder);
    }
}