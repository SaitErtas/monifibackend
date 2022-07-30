using Microsoft.Extensions.DependencyInjection;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;
using MonifiBackend.WalletModule.Infrastructure.AccountMovements;
using MonifiBackend.WalletModule.Infrastructure.Packages;

namespace MonifiBackend.WalletModule.Infrastructure;

public static class WalletInfrastructureSetup
{
    public static IServiceCollection AddWalletServiceInfrastructure(this IServiceCollection services)
    {
        #region Ports-Adapters
        services.AddScoped<IAccountMovementCommandDataPort, AccountMovementCommandDataAdapter>();
        services.AddScoped<IAccountMovementQueryDataPort, AccountMovementQueryDataAdapter>();
        services.AddScoped<IPackageQueryDataPort, PackageQueryDataAdapter>();


        #endregion
        return services;
    }
}