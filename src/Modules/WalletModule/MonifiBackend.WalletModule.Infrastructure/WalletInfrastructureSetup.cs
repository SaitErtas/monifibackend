using Microsoft.Extensions.DependencyInjection;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Domain.Packages;
using MonifiBackend.WalletModule.Domain.Settings;
using MonifiBackend.WalletModule.Infrastructure.AccountMovements;
using MonifiBackend.WalletModule.Infrastructure.Packages;
using MonifiBackend.WalletModule.Infrastructure.Settings;

namespace MonifiBackend.WalletModule.Infrastructure;

public static class WalletInfrastructureSetup
{
    public static IServiceCollection AddWalletServiceInfrastructure(this IServiceCollection services)
    {
        #region Ports-Adapters
        services.AddScoped<IAccountMovementCommandDataPort, AccountMovementCommandDataAdapter>();
        services.AddScoped<IAccountMovementQueryDataPort, AccountMovementQueryDataAdapter>();
        services.AddScoped<IPackageQueryDataPort, PackageQueryDataAdapter>();
        services.AddScoped<ISettingQueryDataPort, SettingQueryDataAdapter>();


        #endregion
        return services;
    }
}