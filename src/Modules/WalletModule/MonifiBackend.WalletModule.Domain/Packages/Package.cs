using MonifiBackend.Core.Domain.Base;

namespace MonifiBackend.WalletModule.Domain.Packages;

public sealed class Package : ReadOnlyBaseDomain<int>
{
    public string Name { get; private set; }
    private List<PackageDetail> _details = new();
    public IReadOnlyCollection<PackageDetail> Details => _details.AsReadOnly();
    public static Package Default() => new();

    public static Package Map(
    int id,
    BaseStatus status,
    DateTime createdAt,
    DateTime modifiedAt,
    string name)
    {
        return new Package()
        {
            Id = id,
            Status = status,
            CreatedAt = createdAt,
            ModifiedAt = modifiedAt,
            Name = name,
        };
    }
}