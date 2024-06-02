using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceRoom.Models;

namespace ConferenceRoom.Interface
{
    public interface IBookingService
    {
        Task<List<BookingViewModel>> GetAllBookings();
        Task AddBooking(BookingViewModel vm);
        Task<BookingViewModel> GetBookingById(int id);
        Task UpdateBooking(BookingViewModel vm);
        Task DeleteBooking(int id);
    }
}
