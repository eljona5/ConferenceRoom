using ConferenceRoom.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Models
{
    public class BookingReservationHolderViewModel
    {
        public BookingViewModel Booking { get; set; }
        public ReservationHolder ReservationHolder { get; set; }
    }
}