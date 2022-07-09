using MonifiBackend.Core.Domain.Base;

namespace MonifiBackend.WalletModule.Domain.Users;

public sealed class User : ReadOnlyBaseDomain<int>
{
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public static User Default() => new();

    public static User Map(
        int id,
        BaseStatus status,
        string userName,
        DateTime createdAt,
        DateTime modifiedAt)
    {
        return new User()
        {
            Id = id,
            Status = status,
            UserName = userName,
            CreatedAt = createdAt,
            ModifiedAt = modifiedAt,
        };
    }
}