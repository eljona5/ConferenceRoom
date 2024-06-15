using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceRoom.Models;
using ConferenceRoom.Interface;
using ConferenceRoom.Data.DBContext;
using ConferenceRoom.Data.Entities;
using Microsoft.EntityFrameworkCore;    
using ConferenceRoom.Services;

namespace ConferenceRoom.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IReservationHolderService _reservationHolderService;
        private readonly ApplicationDbContext _context;

        public BookingController(IBookingService bookingService, IRoomService roomService, IReservationHolderService reservationHolderService, ApplicationDbContext context)
        {
            _bookingService = bookingService;
            _reservationHolderService = reservationHolderService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<BookingViewModel> bookings = await _bookingService.GetAllBookings();
            return View(bookings);
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Rooms = await _context.Rooms.ToListAsync();
            return View(new BookingReservationHolderViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingReservationHolderViewModel model)
        {

            Boolean isValid = ModelState.IsValid;

            if (!isValid)
            {
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }

            if (isValid)
            {
                try
                {
                    var booking = new Booking
                    {
                        Code = model.Booking.Code,
                        NumberOfPeople = model.Booking.NumberOfPeople,
                        RoomId = model.Booking.RoomId,
                        StartDate = model.Booking.StartDate,
                        EndDate = model.Booking.EndDate,
                        IsDeleted = model.Booking.IsDeleted,
                        ReservationHolder = model.ReservationHolder
                    };

                    _context.Bookings.Add(booking);
                    int result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        // Successfully saved to the database
                        Console.WriteLine("Save successful. Number of state entries written: " + result);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // No entries were written to the database
                        Console.WriteLine("Save failed. No state entries were written to the database.");
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // Optionally, log the exception or display an error message to the user
                }
            }
            ViewBag.Rooms = await _context.Rooms.ToListAsync();
            Console.WriteLine("Hello World");
            // Re-populate ViewBag.Rooms in case of validation error
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewBag.Rooms = await _context.Rooms.ToListAsync();
            var model = new BookingReservationHolderViewModel
            {
                Booking = new BookingViewModel
                {
                    Id = booking.Id,
                    Code = booking.Code,
                    NumberOfPeople = booking.NumberOfPeople,
                    //IsConfirmed = booking.IsConfirmed,
                    RoomId = booking.RoomId,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                    IsDeleted = booking.IsDeleted
                },
                ReservationHolder = booking.ReservationHolder
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BookingReservationHolderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var booking = await _bookingService.GetBookingById(model.Booking.Id);
                if (booking == null)
                {
                    return NotFound();
                }

                booking.Code = model.Booking.Code;
                booking.NumberOfPeople = model.Booking.NumberOfPeople;
                //booking.IsConfirmed = model.Booking.IsConfirmed;
                booking.RoomId = model.Booking.RoomId;
                booking.StartDate = model.Booking.StartDate;
                booking.EndDate = model.Booking.EndDate;
                booking.IsDeleted = model.Booking.IsDeleted;
                booking.ReservationHolder = model.ReservationHolder;

                await _bookingService.UpdateBooking(booking);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Rooms = await _context.Rooms.ToListAsync();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookingService.DeleteBooking(id);
            return RedirectToAction(nameof(Index));
        }
    }
}