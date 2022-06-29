using Microsoft.EntityFrameworkCore;
using MonifiBackend.Core.Domain.Base;
using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.Data.Infrastructure.Contexts;
using MonifiBackend.PackageModule.Domain.Packages;

namespace MonifiBackend.PackageModule.Infrastructure.Packages;

public class PackageQueryDataAdapter : IPackageQueryDataPort
{
    private readonly IMonifiBackendDbContext _dbContext;
    public PackageQueryDataAdapter(IMonifiBackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> GetAsync(int duration, decimal commission)
    {
        return await _dbContext.Packages
            .AnyAsync(x => x.Duration == duration && x.Commission == commission && x.Status != BaseStatus.Deleted.ToInt());
    }
}
