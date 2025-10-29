using Microsoft.EntityFrameworkCore;
using FamilyWallet.Infraestructure.Data;

namespace FamilyWallet.Api.Configurations
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<PostgreSQLContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));


            return services;
        }

        public static async Task DatabaseCreatedAsync(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var pgsqlContext = serviceProvider.GetRequiredService<PostgreSQLContext>();
            await pgsqlContext.Database.EnsureCreatedAsync();
        }
    }
}
