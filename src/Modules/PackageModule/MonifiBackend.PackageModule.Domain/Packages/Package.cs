using MonifiBackend.Core.Domain.Abstractions;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Exceptions;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.PackageDetailModule.Domain.PackageDetails;

namespace MonifiBackend.PackageModule.Domain.Packages;

public sealed class Package : BaseActivityDomain<int>, IAggregateRoot
{
    public string Name { get; private set; }
    public int MinValue { get; private set; }
    public int MaxValue { get; private set; }
    public int ChangePeriodDay { get; private set; }
    public string Icon { get; private set; }
    private List<PackageDetail> _details = new();
    public IReadOnlyCollection<PackageDetail> Details => _details.AsReadOnly();

    public static Package Default() => new();

    public void SetName(string name)
    {
        Name = name;
    }
    public static Package CreateNew(
        string name,
        int duration,
        int commission,
        BaseStatus status)
    {
        AppRule.NotNullOrEmpty(name, new DomainException(DomainExceptionMessageType.NULL_OR_EMPTY, nameof(name), name));
        AppRule.NotNegativeOrZero(duration, new DomainException(DomainExceptionMessageType.NEGATIVE_OR_ZERO, nameof(duration), duration));
        AppRule.NotNegativeOrZero(commission, new DomainException(DomainExceptionMessageType.NEGATIVE_OR_ZERO, nameof(commission), commission));

        return new Package()
        {
            Name = name,
            Status = status
        };
    }
    public static Package Map(
        int id,
        BaseStatus status,
        DateTime createdAt,
        DateTime modifiedAt,
        string name,
        int minValue,
        int maxValue,
        int changePeriodDay,
        string icon,
        List<PackageDetail> details)
    {
        return new Package()
        {
            Id = id,
            Status = status,
            CreatedAt = createdAt,
            ModifiedAt = modifiedAt,
            Name = name,
            MinValue = minValue,
            MaxValue = maxValue,
            ChangePeriodDay = changePeriodDay,
            Icon = icon,
            _details = details
        };
    }
}