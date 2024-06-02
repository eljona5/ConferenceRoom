using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceRoom.Data;
using ConferenceRoom.Models;
using ConferenceRoom.Interface;
using Microsoft.EntityFrameworkCore;
using ConferenceRoom.Data.DBContext;
using ConferenceRoom.Data.Entities;

namespace ConferenceRoom.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingViewModel>> GetAllBookings()
        {
            var bookingsVm = new List<BookingViewModel>();

            var bookings = await _context.Bookings.ToListAsync();

            foreach (var booking in bookings)
            {
                bookingsVm.Add(EntityToViewModel(booking));
            }

            return bookingsVm;
        }

        private BookingViewModel EntityToViewModel(Booking booking)
        {
            return new BookingViewModel
            {
                Id = booking.Id,
                Code = booking.Code,
                NumberOfPeople = booking.NumberOfPeople,
                IsConfirmed = booking.IsConfirmed,
                RoomId = booking.RoomId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                IsDeleted = booking.IsDeleted
            };
        }

        public async Task AddBooking(BookingViewModel vm)
        {
            var booking = new Booking
            {
                Code = vm.Code,
                NumberOfPeople = vm.NumberOfPeople,
                IsConfirmed = vm.IsConfirmed,
                RoomId = vm.RoomId,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                IsDeleted = vm.IsDeleted
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<BookingViewModel> GetBookingById(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return null;
            }

            return EntityToViewModel(booking);
        }

        public async Task UpdateBooking(BookingViewModel vm)
        {
            var booking = await _context.Bookings.FindAsync(vm.Id);
            if (booking == null)
            {
                return;
            }

            booking.Code = vm.Code;
            booking.NumberOfPeople = vm.NumberOfPeople;
            booking.IsConfirmed = vm.IsConfirmed;
            booking.RoomId = vm.RoomId;
            booking.StartDate = vm.StartDate;
            booking.EndDate = vm.EndDate;
            booking.IsDeleted = vm.IsDeleted;

            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
