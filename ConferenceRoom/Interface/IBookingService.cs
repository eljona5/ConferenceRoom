using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceRoom.Data.Entities;
using ConferenceRoom.Models;

namespace ConferenceRoom.Interface
{
    public interface IBookingService
    {
        Task<List<BookingViewModel>> GetAllBookings();
        Task AddBooking(Booking booking);
        Task<BookingViewModel> GetBookingById(int id);
        Task UpdateBooking(Booking booking);
        Task DeleteBooking(int id);
        Task UpdateBooking(BookingViewModel booking);
    }
}