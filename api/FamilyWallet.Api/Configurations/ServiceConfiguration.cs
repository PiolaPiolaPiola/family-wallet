using FamilyWallet.Infraestructure.Repositories;
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
            services.AddScoped<ITopeService, TopeService>();

            #endregion Services

            #region Repositories

            services.AddScoped<ITopeRepository, TopeRepository>();

            #endregion Repositories

            return services;
        }
    }
}
