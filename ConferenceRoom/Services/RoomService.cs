using ConferenceRoom.Data.DBContext;
using ConferenceRoom.Data.Entities;
using ConferenceRoom.Interface;
using ConferenceRoom.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;
        public RoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRoom(string code, int maximumCapacity)
        {
            var rooms = new Room
            {
                Code = code,
                MaximumCapacity = maximumCapacity
            };
            _context.Rooms.Add(rooms);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteRoom(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoomViewModel>> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public async Task<RoomViewModel> GetRoomById(int id)
        {
            throw new NotImplementedException();
            //await _context.Rooms.FindAsync(roomId);

        }

        public async Task UpdateRoom(int id, string code, int maximumCapacity)
        {
            throw new NotImplementedException();
        }

        //public async Task<Room> GetRoomById(int roomId)
        //{
        //    return await _context.Rooms.FindAsync(roomId);
        //}

        //public async Task<IEnumerable<Room>> GetAllRooms()
        //{
        //    return await _context.Rooms.ToListAsync();
        //}

        //public async Task<Room> UpdateRoom(int roomId, string code, int? maximumCapacity)
        //{
        //    var room = await _context.Rooms.FindAsync(roomId);
        //    if (room == null) return null;

        //    if (!string.IsNullOrWhiteSpace(code))
        //    {
        //        room.Code = code;
        //    }

        //    if (maximumCapacity.HasValue)
        //    {
        //        room.MaximumCapacity = maximumCapacity.Value;
        //    }

        //    await _context.SaveChangesAsync();
        //    return room;
        //}

        //public async Task<bool> DeleteRoom(int roomId)
        //{
        //    var room = await _context.Rooms.FindAsync(roomId);
        //    if (room == null) return false;

        //    _context.Rooms.Remove(room);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

    }


}
