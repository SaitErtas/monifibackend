using MonifiBackend.Core.Application.Abstractions;

namespace MonifiBackend.WalletModule.Domain.Packages;

public interface IPackageQueryDataPort : IQueryDataPort
{
    Task<List<Package>> GetsAsync();
    Task<bool> GetAsync(int duration);
    Task<Package> GetPackageAsync(int id);
}
