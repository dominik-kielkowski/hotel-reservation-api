using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;

namespace HotelReservation.Tests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        protected TestServer server;
        protected HttpClient client;
        private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithPortBinding(62848)
                .WithDatabase("HotelReservationWebsite")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .Build();

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

        public Task InitializeAsync()
        {
            return _postgresContainer.StartAsync();
        }

        public new Task DisposeAsync()
        {
            return _postgresContainer.StopAsync();
        }

        private void RemoveDbContextOptions<TContext>(IServiceCollection services)
            where TContext : DbContext
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<TContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }
        }

        private void AddDbContext<TContext>(IServiceCollection services)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(_postgresContainer.GetConnectionString());
            });
        }
    }
}
