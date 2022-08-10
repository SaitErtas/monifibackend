using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetDaySaleStatistics;

public class GetDaySaleStatisticsQuery : IQuery<GetDaySaleStatisticsQueryResponse>
{

}
internal class GetDaySaleStatisticsQueryValidator : AbstractValidator<GetDaySaleStatisticsQuery>
{
    public GetDaySaleStatisticsQueryValidator()
    {
    }
}