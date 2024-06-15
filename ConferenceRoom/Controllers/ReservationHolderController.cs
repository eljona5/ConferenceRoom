using ConferenceRoom.Data.Entities;
using ConferenceRoom.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoom.Controllers
{
    public class ReservationHolderController : Controller
    {
        private readonly IReservationHolderService _reservationHolderService;

        public ReservationHolderController(IReservationHolderService reservationHolderService)
        {
            _reservationHolderService = reservationHolderService;
        }

        public async Task<IActionResult> Index()
        {
            List<ReservationHolder> reservationHolders = await _reservationHolderService.GetAllReservationHolders();
            return View(reservationHolders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var reservationHolder = await _reservationHolderService.GetReservationHolderById(id);
            if (reservationHolder == null)
            {
                return NotFound();
            }
            return View(reservationHolder);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationHolder reservationHolder)
        {
            if (ModelState.IsValid)
            {
                await _reservationHolderService.AddReservationHolder(reservationHolder);
                return RedirectToAction(nameof(Index));
            }
            return View(reservationHolder);
        }

        public async Task<IActionResult> Update(int id)
        {
            var reservationHolder = await _reservationHolderService.GetReservationHolderById(id);
            if (reservationHolder == null)
            {
                return NotFound();
            }
            return View(reservationHolder);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ReservationHolder reservationHolder)
        {
            if (ModelState.IsValid)
            {
                await _reservationHolderService.UpdateReservationHolder(reservationHolder);
                return RedirectToAction(nameof(Index));
            }
            return View(reservationHolder);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reservationHolderService.DeleteReservationHolder(id);
            return RedirectToAction(nameof(Index));
        }
    }
}