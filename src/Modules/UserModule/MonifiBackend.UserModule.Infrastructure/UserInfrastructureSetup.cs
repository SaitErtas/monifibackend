﻿using Microsoft.Extensions.DependencyInjection;
using MonifiBackend.UserModule.Domain.Localizations;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Domain.Wallets;
using MonifiBackend.UserModule.Infrastructure.Localizations;
using MonifiBackend.UserModule.Infrastructure.Users;
using MonifiBackend.UserModule.Infrastructure.Wallets;

namespace MonifiBackend.UserModule.Infrastructure
{
    public static class UserInfrastructureSetup
    {
        public static IServiceCollection AddUserServiceInfrastructure(this IServiceCollection services)
        {
            #region Ports-Adapters
            services.AddScoped<IUserCommandDataPort, UserCommandDataAdapter>();
            services.AddScoped<IUserQueryDataPort, UserQueryDataAdapter>();
            services.AddScoped<ILocalizationQueryDataPort, LocalizationQueryDataAdapter>();
            services.AddScoped<IWalletQueryDataPort, WalletQueryDataAdapter>();
            services.AddScoped<IJwtUtils, JwtUtils>();
            #endregion
            return services;
        }
    }
}
