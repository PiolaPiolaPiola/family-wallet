using Microsoft.EntityFrameworkCore;
using FamilyWallet.Infraestructure.Data;
using Microsoft.Extensions.Configuration;

namespace FamilyWallet.Api.Configurations
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PostgreSQLContext>(options =>
                options.UseNpgsql(connectionString));

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
