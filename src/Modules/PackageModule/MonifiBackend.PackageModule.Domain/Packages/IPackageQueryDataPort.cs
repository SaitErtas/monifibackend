using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.PackageModule.Domain.Packages;

public interface IPackageQueryDataPort : IQueryDataPort
{
    Task<bool> GetAsync(int duration, decimal commission);
}
