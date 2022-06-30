using MonifiBackend.Core.Domain.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;

namespace MonifiBackend.PackageModule.Domain.Packages;

public sealed class Package : BaseActivityDomain<int>, IAggregateRoot
{
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public decimal Commission { get; private set; }
    public static Package Default() => new();

    public static Package CreateNew(
        string name,
        int duration,
        decimal commission,
        BaseStatus status)
    {
        AppRule.NotNullOrEmpty(name, new DomainException(DomainExceptionMessageType.NULL_OR_EMPTY, nameof(name), name));
        AppRule.NotNegativeOrZero(duration, new DomainException(DomainExceptionMessageType.NEGATIVE_OR_ZERO, nameof(duration), duration));
        AppRule.NotNegativeOrZero(commission, new DomainException(DomainExceptionMessageType.NEGATIVE_OR_ZERO, nameof(commission), commission));

        return new Package()
        {
            Name = name,
            Status = status,
            Duration = duration,
            Commission = commission,
        };
    }
    public static Package Map(
        int id,
        BaseStatus status,
        DateTime createdAt,
        DateTime modifiedAt,
        string name,
        int duration,
        decimal commission)
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