using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Data.Infrastructure.Entities;
using MonifiBackend.PackageModule.Domain.Packages;

namespace MonifiBackend.PackageModule.Infrastructure.Extensions.Mappers;

public static partial class DomainMapper
{
    #region Package to PackageEntity 
    public static PackageEntity Map(this Package domain)
    {
        return new PackageEntity()
        {
            Id = domain.Id,
            Name = domain.Name,
            Status = domain.Status.ToInt(),
            CreatedAt = domain.CreatedAt,
            ModifiedAt = domain.ModifiedAt,
            Commission = domain.Commission,
            Duration = domain.Duration
        };
    }
    #endregion
    #region PackageEntity to Package 
    public static Package Map(this PackageEntity entity)
    {
        if (entity == null)
            return Package.Default();

        return Package.Map(entity.Id,
            entity.Status.ToEnum<BaseStatus>(),
            entity.CreatedAt,
            entity.ModifiedAt,
            entity.Name,
            entity.Duration,
            entity.Commission);
    }
    #endregion
}
