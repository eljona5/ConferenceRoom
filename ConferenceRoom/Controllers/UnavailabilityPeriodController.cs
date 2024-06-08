using ConferenceRoom.Data.DBContext;
using ConferenceRoom.Interface;
using ConferenceRoom.Models;
using ConferenceRoom.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoom.Controllers
{
    public class UnavailabilityPeriodController : Controller
    {
        private readonly IUnavailabilityPeriodService _unavailabilityPeriodService;
        private readonly ApplicationDbContext _context;
        public UnavailabilityPeriodController(IUnavailabilityPeriodService unavailabilityPeriodService, ApplicationDbContext context)
        {
            _unavailabilityPeriodService = unavailabilityPeriodService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _unavailabilityPeriodService.GetAllUnavailabilityPeriods());
        }

        public async Task<IActionResult> Details(int id)
        {
            //var room = _roomService.GetRoomById(id);
            //return View(room);
            return View(await _unavailabilityPeriodService.GetUnavailabilityPeriodById(id));
        }

        public async Task<IActionResult> Update(int id)
        {
            var room = await _unavailabilityPeriodService.GetUnavailabilityPeriodById(id);
            return View(room);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _unavailabilityPeriodService.GetUnavailabilityPeriodById(id);
            return View(room);
        }


        public IActionResult Create()
        {
            return View();
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(UnavailabilityPeriodViewModel unavailabilityPeriodViewModel)
        {
            if (ModelState.IsValid)
            {
                _unavailabilityPeriodService.AddUnavailabilityPeriod(unavailabilityPeriodViewModel);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(unavailabilityPeriodViewModel);
            }
        }


        //  [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(UnavailabilityPeriodViewModel unavailabilityPeriodViewModel )
        {
            if (ModelState.IsValid)
            {
                _unavailabilityPeriodService.UpdateUnavailabilityPeriod(unavailabilityPeriodViewModel);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(unavailabilityPeriodViewModel);
            }
        }

        //  [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(UnavailabilityPeriodViewModel  unavailabilityPeriodViewModel)
        {

            if (ModelState.IsValid)
            {
                var room = _unavailabilityPeriodService.DeleteUnavailabilityPeriod(unavailabilityPeriodViewModel);
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View(unavailabilityPeriodViewModel);
            }
        }

    }
}
