using FamilyWallet.Api.Workers;
using FamilyWallet.Infraestructure.Repositories;
using FamilyWallet.Infraestructure.Repositories.Interfaces;
using FamilyWallet.Infraestructure.Repositories.Interfaces.Messages;
using FamilyWallet.Infraestructure.Services;
using FamilyWallet.Infraestructure.Services.Messages;

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
            services.AddScoped<IEmailService, SendGridEmailService>();

            services.AddScoped<IEmailBulkCommandService, EmailBulkCommandService>();

            // RabbitMQ Services
            services.AddScoped<IProcessBulkService, ProcessBulkService>();
            services.AddHostedService<ProcessBulkWorker>();

            #endregion Services

            #region Repositories

            services.AddScoped<ITopeRepository, TopeRepository>();
            services.AddSingleton<IRabbitMQRepository, RabbitMQRepository>();

            #endregion Repositories

            return services;
        }
    }
}
