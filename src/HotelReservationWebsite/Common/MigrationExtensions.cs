using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationWebsite.Common
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using WebsiteDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<WebsiteDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
