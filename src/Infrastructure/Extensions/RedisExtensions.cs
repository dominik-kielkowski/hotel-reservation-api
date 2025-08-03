using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelReservation.Infrastructure.Extensions;

public static class RedisExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        string redisConnectionString;

        if (environment.IsDevelopment())
        {
            redisConnectionString = configuration.GetConnectionString("Redis");
        }
        else
        {
            redisConnectionString = Environment.GetEnvironmentVariable("Redis") ?? throw new InvalidOperationException("Redis environment variable is not set.");
        }

        services.AddStackExchangeRedisCache(redisOptions =>
        {
            redisOptions.Configuration = redisConnectionString;
        });
        
        return services;
    }
}