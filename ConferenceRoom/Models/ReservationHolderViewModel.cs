using ConferenceRoom.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Models
{
    public class ReservationHolderViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string IdCardNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public string Notes { get; set; }

        [Required]
        public int BookingId { get; set; }

        public Booking Booking { get; set; }
    }
}