namespace HotelReservation.Infrastructure.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsExtension(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                if (environment.IsDevelopment())
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }
                else
                {
                    var allowedOrigins = Environment.GetEnvironmentVariable("Cors")?.Split(",") ??
                                         configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();

                    var allowedMethods = configuration.GetSection("CorsSettings:AllowedMethods").Get<string[]>() ?? new[] { "GET", "POST", "PUT", "DELETE" };
                    var allowedHeaders = configuration.GetSection("CorsSettings:AllowedHeaders").Get<string[]>() ?? new[] { "Content-Type", "Authorization" };

                    if (allowedOrigins?.Length > 0)
                    {
                        policy.WithOrigins(allowedOrigins)
                            .WithMethods(allowedMethods)
                            .WithHeaders(allowedHeaders);

                        if (!allowedOrigins.Contains("*"))
                        {
                            policy.AllowCredentials();
                        }
                    }
                }
            });
        });

        return services;
    }
}