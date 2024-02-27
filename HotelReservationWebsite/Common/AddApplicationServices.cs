using Application.Common;
using Core.Common;
using Core.User;
using Infrastructure.Data;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Infrastructure.Caching;

namespace HotelReservationWebsite.Common
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<WebsiteDbContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddApplication();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}