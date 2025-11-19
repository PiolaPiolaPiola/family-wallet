using FamilyWallet.Infraestructure.Repositories.Interfaces;
using FamilyWallet.Infraestructure.Services;

namespace FamilyWallet.Api.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            #region Services

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IPersonService, PersonService>();

            #endregion Services

            return services;
        }
    }
}
