using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Data.Entities
{
    public class UnavailabilityPeriod
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
