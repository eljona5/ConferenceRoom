using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Booking code needs to have between 4 and 10 characters.")]
        public string Code { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Number of people needs to be between 1 and 100.")]
        public int NumberOfPeople { get; set; }

        public bool IsConfirmed { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
