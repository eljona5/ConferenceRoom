using ConferenceRoom.Data.Entities;
using ConferenceRoom.Models;
using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Models
{
    public class BookingReservationHolderViewModel
    {
        public BookingViewModel Booking { get; set; } = new BookingViewModel();
        public ReservationHolderViewModel ReservationHolder { get; set; } = new ReservationHolderViewModel();
    }
}


