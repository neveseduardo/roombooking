using RoomBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomBooking.Repositories;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room> GetRoomByIdAsync(int id);
    Task AddRoomAsync(Room room);
    Task UpdateRoomAsync(Room room);
    Task DeleteRoomAsync(int id);
}