using ConferenceRoom.Data.Entities;

namespace ConferenceRoom.Interface
{
    public interface IReservationHolderService
    {
        Task<List<ReservationHolder>> GetAllReservationHolders();
        Task<ReservationHolder> GetReservationHolderById(int id);
        Task AddReservationHolder(ReservationHolder reservationHolder);
        Task UpdateReservationHolder(ReservationHolder reservationHolder);
        Task DeleteReservationHolder(int id);
    }
}