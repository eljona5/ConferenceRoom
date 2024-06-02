using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceRoom.Models;
using ConferenceRoom.Interface;

namespace ConferenceRoom.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
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

        public async Task<IActionResult> Update(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel bookingVM)
        {
            if (ModelState.IsValid)
            {
                await _bookingService.AddBooking(bookingVM);
                return RedirectToAction(nameof(Index));
            }
            return View(bookingVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BookingViewModel bookingVM)
        {
            if (ModelState.IsValid)
            {
                await _bookingService.UpdateBooking(bookingVM);
                return RedirectToAction(nameof(Index));
            }
            return View(bookingVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookingService.DeleteBooking(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
