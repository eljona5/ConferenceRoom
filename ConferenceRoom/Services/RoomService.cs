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

        public async Task AddRoom(RoomViewModel vm)
        {
            var rooms = new Room ();
            
            var roomExist = _context.Rooms.Where(p => p.Id == vm.Id ||
                                                     p.Code==vm.Code )
            .FirstOrDefault();

            
                if (roomExist != null)
                {
                    throw new Exception("Room  exist");
                }


                _context.Rooms.Add(rooms);
                await _context.SaveChangesAsync();
         
        }

        public async Task DeleteRoom(int id)
        {

            var room = await _context.Rooms.FindAsync(id);
            if (room == null) 
                throw new Exception ("Room does not exist");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
             
        }

        public async Task<List<RoomViewModel>> GetAllRooms()
        { 
            var roomsVm = new List<RoomViewModel>();

            var rooms = await _context.Rooms.ToListAsync();

            foreach (var room in rooms)
            {
                roomsVm.Add(EntityToViewModel(room));
            }

            return roomsVm;

        }

        public async Task<RoomViewModel> GetRoomById(int id)
        {  

            var roomViewModel = await _context.Rooms.FindAsync(id);
            return EntityToViewModel(roomViewModel);

        }

        public async Task  UpdateRoom(RoomViewModel vm)
        {
            var room = await _context.Rooms.FindAsync(vm.Id);
            if (room == null)
            {
                throw new Exception("Room does not exist");
            }
            if (vm.Id == null)
            {
                throw new Exception("can not have negative value");
            }
            else 
            { 
                room.Id = vm.Id;
            }
            if (!string.IsNullOrWhiteSpace(vm.Code))
            {
                room.Code =vm.Code;
            }
            if (vm.MaximumCapacity == null)
            {
                throw new Exception("can not have negative value");
            }
            else
            {
                room.MaximumCapacity = vm.MaximumCapacity;
            }
            await _context.SaveChangesAsync();
        }



        private Room ViewModelToEntity(RoomViewModel vm)
        {
            var room = new Room()
            {
                Id = vm.Id,
                Code = vm.Code,
                MaximumCapacity = vm.MaximumCapacity
            };
            return room;
        }

        private RoomViewModel EntityToViewModel(Room entity)
        {
            var roomViewModel = new RoomViewModel()
            {
                Id = entity.Id,
                Code = entity.Code,
                MaximumCapacity = entity.MaximumCapacity
            };
            return roomViewModel;

        }
    }
}