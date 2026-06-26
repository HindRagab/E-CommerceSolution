using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.IdentityData.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContextServices = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var pendingMigrations = await dbContextServices.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await dbContextServices.Database.MigrateAsync();
            return app;
        }

        public static async Task<WebApplication> MigrateIdentityDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContextServices = scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
            var pendingMigrations = await dbContextServices.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await dbContextServices.Database.MigrateAsync();
            return app;
        }

        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var DataInitializerServices = scope.ServiceProvider.GetRequiredKeyedService<IDataInitializer>("Default");
            await DataInitializerServices.InitializeAsync();
            return app;
        }

        public static async Task<WebApplication> SeedIdentityDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var DataInitializerServices = scope.ServiceProvider.GetRequiredKeyedService<IDataInitializer>("Identity");
            await DataInitializerServices.InitializeAsync();
            return app;
        }
    }
}
