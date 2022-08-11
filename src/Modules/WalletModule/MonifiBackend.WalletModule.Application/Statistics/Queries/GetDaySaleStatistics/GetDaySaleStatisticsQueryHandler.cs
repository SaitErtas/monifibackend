using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.WalletModule.Domain.AccountMovements;

namespace MonifiBackend.WalletModule.Application.Statistics.Queries.GetDaySaleStatistics;

internal class GetDaySaleStatisticsQueryHandler : IQueryHandler<GetDaySaleStatisticsQuery, GetDaySaleStatisticsQueryResponse>
{
    private readonly IAccountMovementQueryDataPort _accountMovementQueryDataPort;
    public GetDaySaleStatisticsQueryHandler(IAccountMovementQueryDataPort accountMovementQueryDataPort)
    {
        _accountMovementQueryDataPort = accountMovementQueryDataPort;
    }
    public async Task<GetDaySaleStatisticsQueryResponse> Handle(GetDaySaleStatisticsQuery request, CancellationToken cancellationToken)
    {
        var dayOfSales = await _accountMovementQueryDataPort.GetDayOfSalesAsync();
        var totalSale = await _accountMovementQueryDataPort.GetTotalSaleAsync();
        var totalBonus = await _accountMovementQueryDataPort.GetTotalBonusAsync();


        var today = DateTime.Now.Date;
        var yesterday = DateTime.Now.AddDays(-1).Date;

        var todaySale = dayOfSales.FirstOrDefault(x => x.Day == today);
        var yesterdaySale = dayOfSales.FirstOrDefault(x => x.Day == yesterday);
        var percentageofChange = (((todaySale.TotalSales - yesterdaySale.TotalSales) / yesterdaySale.TotalSales)) * 100;

        return new GetDaySaleStatisticsQueryResponse(dayOfSales, totalSale, totalBonus, percentageofChange);
    }
}