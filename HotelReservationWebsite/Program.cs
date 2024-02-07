using API.Extensions;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WebsiteDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var identityContext = services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();

app.Run();
