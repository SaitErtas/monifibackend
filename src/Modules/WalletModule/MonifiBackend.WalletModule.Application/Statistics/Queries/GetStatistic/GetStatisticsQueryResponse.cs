using MonifiBackend.WalletModule.Domain.Settings;

namespace MonifiBackend.WalletModule.Application.Statistics.Queries.GetStatistic;

public class GetStatisticsQueryResponse
{
    public GetStatisticsQueryResponse(Setting setting, decimal totalSale, decimal totalBonus)
    {
        MaximumSalesQuantity = setting.MaximumSalesQuantity;
        MaximumDistributedAPY = setting.MaximumDistributedAPY;
        MaximumReferenceBonus = setting.MaximumReferenceBonus;
        TotalPreSaleQuantity = setting.TotalPreSaleQuantity;
        MonifiPrice = setting.MonifiPrice;
        TotalSale = totalSale;
        TotalBonus = totalBonus;
        TotalDistributedMonifi = totalSale + totalBonus;
    }
    public long MaximumSalesQuantity { get; private set; }
    public long MaximumDistributedAPY { get; private set; }
    public long MaximumReferenceBonus { get; private set; }
    public long TotalPreSaleQuantity { get; private set; }
    public decimal MonifiPrice { get; private set; }
    public decimal TotalSale { get; private set; }
    public decimal TotalBonus { get; private set; }
    public decimal TotalDistributedMonifi { get; private set; }
}
