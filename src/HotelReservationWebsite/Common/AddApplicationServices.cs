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


            

            return services;
        }
    }
}
