using System.Text;
using Core.User;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace HotelReservation.API.Users
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration config, IHostEnvironment env)
        {
            string identityConnectionString;

            services.AddScoped<ITokenService, TokenService>();
            
            if (env.IsDevelopment())
            {
                identityConnectionString = config.GetConnectionString("IdentityConnection");
            }
            else
            {
                identityConnectionString = Environment.GetEnvironmentVariable("IdentityConnection") 
                    ?? throw new InvalidOperationException("IdentityConnection environment variable is not set.");
            }

            services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                opt.UseSqlServer(identityConnectionString).EnableSensitiveDataLogging();
            });

            services.AddIdentityCore<AppUser>(opt =>
            {
                // Configure identity options if needed
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            var tokenKey = env.IsDevelopment()
                ? config["Token:Key"]
                : Environment.GetEnvironmentVariable("TokenKey") 
                    ?? throw new InvalidOperationException("TokenKey environment variable is not set.");

            var tokenIssuer = env.IsDevelopment()
                ? config["Token:Issuer"]
                : Environment.GetEnvironmentVariable("TokenIssuer") 
                    ?? throw new InvalidOperationException("TokenIssuer environment variable is not set.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                        ValidIssuer = tokenIssuer,
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization();

            try
            {
                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
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
