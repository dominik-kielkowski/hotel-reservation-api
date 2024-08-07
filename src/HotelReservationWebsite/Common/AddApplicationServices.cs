using Application.Common;
using Core.Common;
using Core.User;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Caching;
using Serilog;
using Infrastructure.Messaging;

namespace API.Common
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<WebsiteDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
            });

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                string connection = config.GetConnectionString("Redis");
                redisOptions.Configuration = connection;
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.ConfigureMediatR();
            services.AddMassTransitWithRabbitMQ();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();

            try
            {
                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<WebsiteDbContext>();
                    dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Database migration failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            return services;
        }

    }
}