using RoomBooking.Models;

namespace RoomBooking.Repository
{
    public interface IAuthenticationRepository
    {
        Task<User?> ValidateUserAsync(string email, string password);

        string CreateToken(User user);

        Task<dynamic?> GetUserAsync(int id);
    }
}