using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Data.Entities
{
    //public class ReservationHolder
    //{
    //    [Key]
    //    [Required]
    //    public int Id { get; set; }
    //    [Required]
    //    public string IdCardNumber { get; set; }
    //    [Required]
    //    public string Name { get; set; }
    //    [Required]
    //    public string Surname { get; set; }
    //    [Required]
    //    public string Email { get; set; }
    //    [Required]
    //    public string PhoneNumber { get; set; }
    //    [Required]
    //    public string Notes { get; set; }
    //    [Required]
    //    public int BookingId { get; set; }

    //    [ForeignKey("BookingId")]
    //    public Booking Booking { get; set; }
    //    public bool? IsDeleted { get; set; } 
    //}


    public class ReservationHolder
    {
        [Key]
        [Required]
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

        [Required]
        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
