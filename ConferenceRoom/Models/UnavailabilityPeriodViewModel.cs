using ConferenceRoom.Data.DBContext;
using ConferenceRoom.Data.Entities;
using ConferenceRoom.Interface;
using System.ComponentModel.DataAnnotations;

namespace ConferenceRoom.Models
{
    public class UnavailabilityPeriodViewModel
    {

        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}

