using MonifiBackend.Core.Domain.Base;

namespace MonifiBackend.WalletModule.Domain.Packages;

public sealed class Package : ReadOnlyBaseDomain<int>
{
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public int Commission { get; private set; }
    public static Package Default() => new();

    public static Package Map(
        int id,
        BaseStatus status,
        DateTime createdAt,
        DateTime modifiedAt,
        string name,
        int duration,
        int commission)
    {
        return new Package()
        {
            Id = id,
            Status = status,
            CreatedAt = createdAt,
            ModifiedAt = modifiedAt,
            Name = name,
            Duration = duration,
            Commission = commission
        };
    }
}