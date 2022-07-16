using Microsoft.Extensions.DependencyInjection;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Infrastructure.AccountMovements;

namespace MonifiBackend.WalletModule.Infrastructure;

public static class WalletInfrastructureSetup
{
    public static IServiceCollection AddWalletServiceInfrastructure(this IServiceCollection services)
    {
        #region Ports-Adapters
        services.AddScoped<IAccountMovementCommandDataPort, AccountMovementCommandDataAdapter>();
        services.AddScoped<IAccountMovementQueryDataPort, AccountMovementQueryDataAdapter>();
        #endregion
        return services;
    }
}