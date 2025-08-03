using Application.Common;
using Core.Common;
using HotelReservation.API.Extensions;
using HotelReservation.Infrastructure.Extensions;
using Infrastructure.Caching;
using Infrastructure.Data;
using Serilog;

try
{
    Serilog.Debugging.SelfLog.Enable(Console.Error);
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    
    builder.Services.ConfigureMediatR();
    builder.Services.AddSingleton<ICacheService, CacheService>();
    builder.Services.AddRedis(builder.Configuration, builder.Environment);
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddHotelDbcontext(builder.Configuration, builder.Environment);
    
    
    //services.AddMassTransitWithRabbitMQ();
    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    //builder.Services.AddMemoryCache();
    //builder.Services.AddIdentityServices(builder.Configuration, builder.Environment);
    builder.Services.AddCorsExtension(builder.Configuration, builder.Environment);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration)
    );

    var app = builder.Build();

    app.UseJsonExceptionHandler();

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