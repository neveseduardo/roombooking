using RoomBooking.Database.Seeders;
using RoomBooking.Database;

namespace RoomBooking.Extensions;

static class DatabaseSeederExtension
{
    public static void SeedDatabase(this IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();

            var userSeeder = new UserSeeder(context);

            userSeeder.Seed();
        }
    }
}