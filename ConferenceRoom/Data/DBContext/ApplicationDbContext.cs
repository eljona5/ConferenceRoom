using ConferenceRoom.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Data.DBContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ReservationHolder> ReservationHolders { get; set; }
        public DbSet<UnavailabilityPeriod> UnavailabilityPeriods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ReservationHolder)
                .WithOne(r => r.Booking)
                .HasForeignKey<ReservationHolder>(r => r.BookingId)
                .IsRequired(false); // Make the relationship optional

        }
    }
    
}
