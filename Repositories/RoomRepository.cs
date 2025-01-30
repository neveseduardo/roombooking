using Microsoft.EntityFrameworkCore;
using RoomBooking.Database;
using RoomBooking.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomBooking.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room> GetRoomByIdAsync(int id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public async Task AddRoomAsync(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRoomAsync(Room room)
    {
        room.updated_at = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRoomAsync(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room != null)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}