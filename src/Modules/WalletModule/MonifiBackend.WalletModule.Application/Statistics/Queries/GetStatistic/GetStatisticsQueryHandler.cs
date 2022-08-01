using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Settings;

namespace MonifiBackend.WalletModule.Application.Statistics.Queries.GetStatistic;

internal class GetStatisticsQueryHandler : IQueryHandler<GetStatisticsQuery, GetStatisticsQueryResponse>
{
    private const int DEFAULT_SETTING_VALUE = 1;
    private readonly ISettingQueryDataPort _settingQueryDataPort;
    private readonly IAccountMovementQueryDataPort _accountMovementQueryDataPort;
    public GetStatisticsQueryHandler(IAccountMovementQueryDataPort accountMovementQueryDataPort, ISettingQueryDataPort settingQueryDataPort)
    {
        _accountMovementQueryDataPort = accountMovementQueryDataPort;
        _settingQueryDataPort = settingQueryDataPort;
    }
    public async Task<GetStatisticsQueryResponse> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
    {
        var totalSale = await _accountMovementQueryDataPort.GetTotalSaleAsync();
        var totalBonus = await _accountMovementQueryDataPort.GetTotalBonusAsync();
        var setting = await _settingQueryDataPort.GetAsync(DEFAULT_SETTING_VALUE);

        return new GetStatisticsQueryResponse(setting, totalSale, totalBonus);
    }
}