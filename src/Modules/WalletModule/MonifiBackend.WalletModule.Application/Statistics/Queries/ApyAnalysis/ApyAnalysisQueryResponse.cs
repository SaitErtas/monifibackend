namespace MonifiBackend.WalletModule.Application.Statistics.Queries.ApyAnalysis;

public class ApyAnalysisQueryResponse
{
    public ApyAnalysisQueryResponse(string referanceCode, decimal? totalEarning, DateTime? firstTime, DateTime? lastTime)
    {
        ReferanceCode = referanceCode;
        TotalEarning = totalEarning;
        FirstTime = firstTime;
        LastTime = lastTime;
    }
    public string ReferanceCode { get; set; }
    public decimal? TotalEarning { get; set; }
    public DateTime? FirstTime { get; set; }
    public DateTime? LastTime { get; set; }
}
