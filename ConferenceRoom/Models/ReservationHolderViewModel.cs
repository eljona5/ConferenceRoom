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
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string Notes { get; set; }
    }

}