using ConferenceRoom.Models;

namespace ConferenceRoom.Interface
{
    public interface IRoomService
    {
        Task AddRoom(string code, int maximumCapacity);
        Task<RoomViewModel> GetRoomById(int id);
        Task<List<RoomViewModel>>GetAllRooms();
        Task UpdateRoom(int id, string code, int maximumCapacity);
        Task DeleteRoom(int id);
    }
}
