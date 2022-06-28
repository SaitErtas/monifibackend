using Microsoft.Extensions.DependencyInjection;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.UserModule.Infrastructure.Users;

namespace MonifiBackend.UserModule.Infrastructure
{
    public static class UserInfrastructureSetup
    {
        public static IServiceCollection AddUserServiceInfrastructure(this IServiceCollection services)
        {
            #region Ports-Adapters
            services.AddScoped<IUserCommandDataPort, UserCommandDataAdapter>();
            services.AddScoped<IUserQueryDataPort, UserQueryDataAdapter>();
            services.AddScoped<IJwtUtils, JwtUtils>();
            #endregion
            return services;
        }
    }
}
