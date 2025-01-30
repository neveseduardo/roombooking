using RoomBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomBooking.Repositories;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAllBookingsAsync();
    Task<Booking> GetBookingByIdAsync(int id);
    Task AddBookingAsync(Booking booking);
    Task UpdateBookingAsync(Booking booking);
    Task DeleteBookingAsync(int id);
}