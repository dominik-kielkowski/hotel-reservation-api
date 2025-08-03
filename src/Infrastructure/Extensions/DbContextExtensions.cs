using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelReservation.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddHotelDbcontext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        string defaultConnectionString;
        
        if (environment.IsDevelopment())
        {
            defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
        else
        {
            defaultConnectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection environment variable is not set.");
        }
        
        services.AddDbContext<WebsiteDbContext>(opt =>
        {
            opt.UseSqlServer(defaultConnectionString).EnableSensitiveDataLogging();
        });
        
            // var serviceProvider = services.BuildServiceProvider();
            // using (var scope = serviceProvider.CreateScope())
            // {
            //     var dbContext = scope.ServiceProvider.GetRequiredService<WebsiteDbContext>();
            //     dbContext.Database.Migrate();
            // }

        return services;
    }
    
}