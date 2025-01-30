using RoomBooking.Helpers;
using RoomBooking.Models;
using RoomBooking.Database;

namespace RoomBooking.Database.Seeders;

public class UserSeeder
{
    private readonly ApplicationDbContext _context;

    public UserSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (!_context.Users.Any(a => a.email == "email@email.com"))
        {
            var password = PasswordHelper.HashPassword("Senh@123");
            var User = new User { name = "Admin", email = "email@email.com", password = "" };
            User.password = password;

            _context.Users.Add(User);
            _context.SaveChanges();
        }
    }
}