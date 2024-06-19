using ConferenceRoom.Data.DBContext;
using ConferenceRoom.Data.Entities;
using ConferenceRoom.Interface;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Services
{
    public class ReservationHolderService : IReservationHolderService
    {
        private readonly ApplicationDbContext _context;

        public ReservationHolderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationHolder>> GetAllReservationHolders()
        {
            return await _context.ReservationHolders.Where(r => (bool)!r.IsDeleted).ToListAsync();
        }

        public async Task<ReservationHolder> GetReservationHolderById(int id)
        {
            return await _context.ReservationHolders.FindAsync(id);
        }

        public async Task AddReservationHolder(ReservationHolder reservationHolder)
        {
            _context.ReservationHolders.Add(reservationHolder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationHolder(ReservationHolder reservationHolder)
        {
            _context.ReservationHolders.Update(reservationHolder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationHolder(int id)
        {
            var reservationHolder = await _context.ReservationHolders.FindAsync(id);
            if (reservationHolder != null)
            {
                reservationHolder.IsDeleted = true;
                var relatedBooking = await _context.Bookings.FindAsync(reservationHolder.BookingId);
                if (relatedBooking != null)
                {
                    relatedBooking.IsDeleted = true;
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}