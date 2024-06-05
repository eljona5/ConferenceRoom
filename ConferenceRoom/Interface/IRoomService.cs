using ConferenceRoom.Models;

namespace ConferenceRoom.Interface
{
    public interface IRoomService
    {
        Task AddRoom(RoomViewModel vm);
        Task<RoomViewModel> GetRoomById(int id);
        Task<List<RoomViewModel>>GetAllRooms();
        Task UpdateRoom(RoomViewModel vm);
        Task DeleteRoom(RoomViewModel vm);
    }
}
