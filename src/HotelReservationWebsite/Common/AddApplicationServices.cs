using Application.Common;
using Core.Common;
using Core.User;
using Infrastructure.Caching;
using Infrastructure.Data;
using Infrastructure.Messaging;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HotelReservation.API.Common
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
        {
            string defaultConnectionString;
            string redisConnectionString;

            if (env.IsDevelopment())
            {
                defaultConnectionString = config.GetConnectionString("DefaultConnection");
                redisConnectionString = config.GetConnectionString("Redis");
            }
            else
            {
                defaultConnectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection environment variable is not set.");
                redisConnectionString = Environment.GetEnvironmentVariable("Redis") ?? throw new InvalidOperationException("Redis environment variable is not set.");
            }

            services.AddDbContext<WebsiteDbContext>(opt =>
            {
                opt.UseSqlServer(defaultConnectionString).EnableSensitiveDataLogging();
            });

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                redisOptions.Configuration = redisConnectionString;
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
