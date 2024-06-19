using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Data.Entities
{
    public class Room
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int MaximumCapacity { get; set; }

    }
}
