using MonifiBackend.Core.Domain.Base;

namespace MonifiBackend.WalletModule.Domain.Bots;

public sealed class Bot : ReadOnlyBaseDomain<int>
{
    public int Hour { get; private set; }
    public int Minute { get; private set; }
    public int Day { get; private set; }
    public int Amount { get; private set; }

    public static Bot Default() => new();

    public static Bot CreateNew(
        int hour,
        int minute,
        int day,
        int amount,
        BaseStatus status)
    {
        return new Bot()
        {
            Hour = hour,
            Minute = minute,
            Day = day,
            Amount = amount,
            Status = status,
        };
    }

    public static Bot Map(
        int id,
        BaseStatus status,
        DateTime createdAt,
        DateTime modifiedAt,
        int hour,
        int minute,
        int day,
        int amount)
    {
        return new Bot()
        {
            Id = id,
            Status = status,
            CreatedAt = createdAt,
            ModifiedAt = modifiedAt,
            Hour = hour,
            Minute = minute,
            Day = day,
            Amount = amount,
        };
    }
}
