using HotelReservation.API;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.MsSql;
using Xunit;

namespace HotelReservation.Tests.Utilities
{
    public class TestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlServerContainer;

        public TestWebAppFactory()
        {
            _sqlServerContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
                .WithPassword("YourStrong@Passw0rd")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                RemoveDbContextOptions<WebsiteDbContext>(services);
                RemoveDbContextOptions<AppIdentityDbContext>(services);

                AddDbContext<WebsiteDbContext>(services);
                AddDbContext<AppIdentityDbContext>(services);
            });
        }

        public async Task InitializeAsync()
        {
            await _sqlServerContainer.StartAsync();

            using (var scope = Services.CreateScope())
            {
                var websiteContext = scope.ServiceProvider.GetRequiredService<WebsiteDbContext>();
                await websiteContext.Database.MigrateAsync();

                var identityContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();
            }
        }

        public async Task DisposeAsync()
        {
            await _sqlServerContainer.StopAsync();
        }

        private void RemoveDbContextOptions<TContext>(IServiceCollection services)
            where TContext : DbContext
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<TContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

        private void AddDbContext<TContext>(IServiceCollection services)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(_sqlServerContainer.GetConnectionString());
            });
        }
    }
}
