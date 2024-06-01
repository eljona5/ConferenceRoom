using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Data.Entities
{
    public class Booking
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public int NumberOfPeople { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        public int ReservationId { get; set; }

        [ForeignKey("ReservationHolderId")]
        public ReservationHolder ReservationHolder { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
