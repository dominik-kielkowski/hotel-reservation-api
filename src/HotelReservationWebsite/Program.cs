using HotelReservation.API.Common;
using HotelReservation.API.Users;
using Serilog;

namespace HotelReservation.API;

public partial class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices(builder.Configuration, builder.Environment);
            builder.Services.AddIdentityServices(builder.Configuration, builder.Environment);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    if (builder.Environment.IsDevelopment())
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    }
                    else
                    {
                        var allowedOrigins = Environment.GetEnvironmentVariable("Cors")?.Split(",") ?? builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();
                        var allowedMethods = builder.Configuration.GetSection("CorsSettings:AllowedMethods").Get<string[]>() ?? new[] { "GET", "POST", "PUT", "DELETE" };
                        var allowedHeaders = builder.Configuration.GetSection("CorsSettings:AllowedHeaders").Get<string[]>() ?? new[] { "Content-Type", "Authorization" };

                        if (allowedOrigins?.Length > 0)
                        {
                            policy.WithOrigins(allowedOrigins)
                                .WithMethods(allowedMethods)
                                .WithHeaders(allowedHeaders)
                                .AllowCredentials();
                        }
                    }
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
            );

            var app = builder.Build();

            app.UseSerilogRequestLogging();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.MapControllers();
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application startup failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
