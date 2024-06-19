using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceRoom.Models;
using ConferenceRoom.Interface;
using ConferenceRoom.Data.DBContext;
using ConferenceRoom.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ConferenceRoom.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;

namespace ConferenceRoom.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IReservationHolderService _reservationHolderService;
        private readonly ApplicationDbContext _context;

        public BookingController(IBookingService bookingService, IRoomService roomService, IReservationHolderService reservationHolderService, ApplicationDbContext context)
        {
            _bookingService = bookingService;
            _roomService = roomService;
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }


            // Fetch the room entity using roomId
            var room = await _context.Rooms.FindAsync(model.Booking.RoomId);
            if (room == null)
            {
                ModelState.AddModelError("Booking.RoomId", "Invalid room.");
                return View(model); // Or any other appropriate action
            }

            string roomCode = room.Code; // Get the room code from the room entity
            string bookingCode = model.Booking.StartDate.ToString("yyyyMMdd") + "-" +
                                 model.Booking.StartDate.ToString("HHmm") + "-" +
                                 model.Booking.EndDate.ToString("HHmm") + "-" +
                                 roomCode;

            var booking = new Booking
            {
                Code = bookingCode,
                NumberOfPeople = model.Booking.NumberOfPeople,
                RoomId = model.Booking.RoomId,
                StartDate = model.Booking.StartDate,
                EndDate = model.Booking.EndDate,
                IsDeleted = model.Booking.IsDeleted,
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            if (model.ReservationHolder != null)
            {
                var reservationHolder = new ReservationHolder
                {
                    IdCardNumber = model.ReservationHolder.IdCardNumber,
                    Name = model.ReservationHolder.Name,
                    Surname = model.ReservationHolder.Surname,
                    Email = model.ReservationHolder.Email,
                    PhoneNumber = model.ReservationHolder.PhoneNumber,
                    Notes = model.ReservationHolder.Notes,
                    BookingId = booking.Id // Set the foreign key to the newly created booking
                };

                _context.ReservationHolders.Add(reservationHolder);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]

        // GET: Booking/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var bookingViewModel = await _bookingService.GetBookingById(id);
            if (bookingViewModel == null)
            {
                return NotFound();
            }

            // Assuming you have a method to get the list of rooms for the dropdown
            ViewBag.Rooms = await _roomService.GetAllRooms();

            var bookingReservationHolderViewModel = new BookingReservationHolderViewModel
            {
                Booking = bookingViewModel,
                ReservationHolder = bookingViewModel.reservationHolder
            };

            return View(bookingReservationHolderViewModel);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, BookingReservationHolderViewModel bookingReservationHolderViewModel)
        {
            if (id != bookingReservationHolderViewModel.Booking.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // Repopulate the rooms in case of an error
                ViewBag.Rooms = await _roomService.GetAllRooms();
                return View(bookingReservationHolderViewModel);
            }

            try
            {
                await _bookingService.UpdateBooking(bookingReservationHolderViewModel.Booking);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                // Repopulate the rooms in case of an error
                ViewBag.Rooms = await _roomService.GetAllRooms();
                return View(bookingReservationHolderViewModel);
            }

        }
        //public async Task<IActionResult> Update(int id)
        //{
        //    var booking = await _bookingService.GetBookingById(id);
        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.Rooms = await _context.Rooms.ToListAsync();
        //    var model = new BookingReservationHolderViewModel
        //    {
        //        Booking = new BookingViewModel
        //        {
        //            Id = booking.Id,
        //            Code = booking.Code,
        //            NumberOfPeople = booking.NumberOfPeople,
        //            //IsConfirmed = booking.IsConfirmed,
        //            RoomId = booking.RoomId,
        //            StartDate = booking.StartDate,
        //            EndDate = booking.EndDate,
        //            IsDeleted = booking.IsDeleted
        //        },
        //        ReservationHolder = booking.reservationHolder
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(BookingReservationHolderViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var booking = await _bookingService.GetBookingById(model.Booking.Id);
        //        if (booking == null)
        //        {
        //            return NotFound();
        //        }

        //        booking.Code = model.Booking.Code;
        //        booking.NumberOfPeople = model.Booking.NumberOfPeople;
        //        //booking.IsConfirmed = model.Booking.IsConfirmed;
        //        booking.RoomId = model.Booking.RoomId;
        //        booking.StartDate = model.Booking.StartDate;
        //        booking.EndDate = model.Booking.EndDate;
        //        booking.IsDeleted = model.Booking.IsDeleted;
        //        booking.reservationHolder = model.ReservationHolder;

        //        await _bookingService.UpdateBooking(booking);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.Rooms = await _context.Rooms.ToListAsync();
        //    return View(model);
        //}


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookingService.DeleteBooking(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var error = new ErrorViewModel
                {
                    ErrorMessage = ex.Message
                };
                return View("Error", error);
            }
        }
    }
}









