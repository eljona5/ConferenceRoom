using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        [Required]
        [Length(4,4, ErrorMessage ="Room code needs to have exactly 4 characters.")]
        public string Code { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Room capacity needs to be between 1 and 100!")]
        public int MaximumCapacity { get; set; }
        
    }
}
