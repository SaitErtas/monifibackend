using MonifiBackend.WalletModule.Domain.Bots;

namespace MonifiBackend.WalletModule.Application.Bots.Queries.GetBots;

public class GetBotsQueryResponse
{
    public GetBotsQueryResponse(List<Bot> bots)
    {
        Bots = bots.Select(s => new GetBotResponse(s)).ToList();
    }
    public List<GetBotResponse> Bots { get; set; }
}
public class GetBotResponse
{
    public GetBotResponse(Bot bot)
    {
        Id = bot.Id;
        Hour = bot.Hour;
        Minute = bot.Minute;
        Day = bot.Day;
        Amount = bot.Amount;
    }
    public int Id { get; private set; }
    public int Hour { get; private set; }
    public int Minute { get; private set; }
    public int Day { get; private set; }
    public int Amount { get; private set; }
}