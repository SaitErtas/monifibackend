using MonifiBackend.Core.Domain.Base;

namespace MonifiBackend.WalletModule.Domain.Settings;

public sealed class Setting : ReadOnlyBaseDomain<int>
{
    public string Name { get; private set; }
    public long MaximumSalesQuantity { get; private set; }
    public long MaximumDistributedAPY { get; private set; }
    public long MaximumReferenceBonus { get; private set; }
    public long TotalPreSaleQuantity { get; private set; }
    public decimal MonifiPrice { get; private set; }

    public static Setting Default() => new();

    public static Setting Map(
        int id,
        BaseStatus status,
        DateTime createdAt,
        DateTime modifiedAt,
        string name,
        long maximumSalesQuantity,
        long maximumDistributedAPY,
        long maximumReferenceBonus,
        long totalPreSaleQuantity,
        decimal monifiPrice)
    {
        return new Setting()
        {
            Id = id,
            Status = status,
            CreatedAt = createdAt,
            ModifiedAt = modifiedAt,
            Name = name,
            MaximumSalesQuantity = maximumSalesQuantity,
            MaximumDistributedAPY = maximumDistributedAPY,
            MaximumReferenceBonus = maximumReferenceBonus,
            TotalPreSaleQuantity = totalPreSaleQuantity,
            MonifiPrice = monifiPrice
        };
    }
}