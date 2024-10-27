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
            
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices(builder.Configuration, builder.Environment);
            builder.Services.AddIdentityServices(builder.Configuration);
            
            var corsSettings = builder.Configuration.GetSection("CorsSettings");
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(corsSettings.GetSection("AllowedOrigins").Get<string[]>())
                        .WithMethods(corsSettings.GetSection("AllowedMethods").Get<string[]>())
                        .WithHeaders(corsSettings.GetSection("AllowedHeaders").Get<string[]>())
                        .AllowCredentials();
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