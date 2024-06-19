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
        private readonly IReservationHolderService _reservationHolderService;


        public BookingService(ApplicationDbContext context, IReservationHolderService reservationHolderService)
        {
            _context = context;
            _reservationHolderService = reservationHolderService;
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
                //IsConfirmed = booking.IsConfirmed,
                RoomId = booking.RoomId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                IsDeleted = booking.IsDeleted,
            };
        }

        public async Task AddBooking(Booking booking)
        {
            var savedBooking = _context.Bookings.Add(booking);

            await _context.SaveChangesAsync();

            //var booking = ViewModelToEntity(model);
            //_context.Bookings.Add(booking);
            //await _context.SaveChangesAsync();
        }

        public async Task<BookingViewModel> GetBookingById(int id)
        {
            var booking = await _context.Bookings.Include(b => b.ReservationHolder).FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null)
            {
                return null;
            }

            return EntityToViewModel(booking);
        }

        public async Task UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                throw new Exception("Booking not found");
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        //public Task UpdateBooking(BookingViewModel booking)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task UpdateBooking(BookingViewModel vm)
        {
            var bookingExists = await _context.Bookings.AnyAsync(b => b.Id == vm.Id);

            if (!bookingExists)
            {
                throw new Exception("Booking does not exist");
            }

            var bookingEntity = ViewModelToEntity(vm);
            _context.Bookings.Update(bookingEntity);
            await _context.SaveChangesAsync();
        }

        private Booking ViewModelToEntity(BookingViewModel vm)
        {
            return new Booking
            {
                Id = vm.Id,
                Code = vm.Code,
                NumberOfPeople = vm.NumberOfPeople,
                RoomId = vm.RoomId,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                IsDeleted = vm.IsDeleted,
               
            };
        }
    }
}