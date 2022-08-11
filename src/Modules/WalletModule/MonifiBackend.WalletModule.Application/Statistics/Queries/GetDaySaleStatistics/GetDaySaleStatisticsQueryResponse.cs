using MonifiBackend.WalletModule.Domain.AccountMovements;

namespace MonifiBackend.WalletModule.Application.Statistics.Queries.GetDaySaleStatistics;

public class GetDaySaleStatisticsQueryResponse
{
    public GetDaySaleStatisticsQueryResponse(List<DaySaleStatistics> daySaleStatistics, decimal totalSale, decimal totalBonus, decimal percentageofChange)
    {
        DaySaleStatistics = daySaleStatistics.Select(x => new GetDaySaleStatisticQueryResponse(x)).ToList();
        TotalMonifi = totalSale;
        PercentageofChange = percentageofChange;
    }
    public decimal PercentageofChange { get; set; }
    public decimal TotalMonifi { get; set; }
    public List<GetDaySaleStatisticQueryResponse> DaySaleStatistics { get; set; }
}
public class GetDaySaleStatisticQueryResponse
{
    public GetDaySaleStatisticQueryResponse(DaySaleStatistics daySaleStatistic)
    {
        Day = daySaleStatistic.Day;
        TotalSales = daySaleStatistic.TotalSales / setting.MonifiPrice;

        Name = Day.ToString("dd/MM");
        Pv = TotalSales;
    }
    public DateTime Day { get; set; }
    public decimal TotalSales { get; set; }
    public string Name { get; set; }
    public decimal Pv { get; set; }
}
