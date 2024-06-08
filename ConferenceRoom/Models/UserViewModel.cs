using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
