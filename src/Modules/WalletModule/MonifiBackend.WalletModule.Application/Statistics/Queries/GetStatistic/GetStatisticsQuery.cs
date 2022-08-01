using FluentValidation;
using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.WalletModule.Application.Statistics.Queries.GetStatistic;

public class GetStatisticsQuery : IQuery<GetStatisticsQueryResponse>
{
}

internal class GetStatisticsQueryValidator : AbstractValidator<GetStatisticsQuery>
{
    public GetStatisticsQueryValidator()
    {

    }
}