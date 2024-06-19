using ConferenceRoom.Data.DBContext;
using ConferenceRoom.Data.Entities;
using ConferenceRoom.Helpers;
using ConferenceRoom.Interface;
using ConferenceRoom.Models;
using ConferenceRoom.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;



namespace ConferenceRoom.Controllers
{
    public class UnavailabilityPeriodController : Controller
    {
        private readonly IUnavailabilityPeriodService _unavailabilityPeriodService;
        private readonly ApplicationDbContext _context;
        public UnavailabilityPeriodController(IUnavailabilityPeriodService unavailabilityPeriodService)
        {
            _unavailabilityPeriodService = unavailabilityPeriodService;
          
        }

        public async Task<IActionResult> Index()
        {

            return View(await _unavailabilityPeriodService.GetAllUnavailabilityPeriods());
        }


        public async Task<IActionResult> Details(int id)
        {
            return View(await _unavailabilityPeriodService.GetUnavailabilityPeriodById(id));
        }


        [Authorize(Roles = Constants.AdminRole)]
        public async Task<IActionResult> Update(int id)
        {
            try 
            { 
            var room = await _unavailabilityPeriodService.GetUnavailabilityPeriodById(id);
            return View(room);
            }

            catch (Exception ex)
            {
                var error = new ErrorViewModel();
                error.ErrorMessage = ex.Message;
                return View("Error", error);
            }
        }


        [Authorize(Roles = Constants.AdminRole)]
        public async Task<IActionResult> Delete(int id)
        {
            try 
            { 
             _unavailabilityPeriodService.DeleteUnavailabilityPeriod(id);

            return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var error = new ErrorViewModel();
                error.ErrorMessage = ex.Message;
                return View("Error", error);
            }
        }


        public IActionResult Create()
        {
            return View();
        }

         [Authorize(Roles = Constants.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Create(UnavailabilityPeriodViewModel unavailabilityPeriodViewModel)
        {
            try 
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
            catch (Exception ex)
            {
                var error = new ErrorViewModel();
                error.ErrorMessage = ex.Message;
                return View("Error", error);
            }
        }


        [Authorize(Roles = Constants.AdminRole)]
        [HttpPost]
        public IActionResult Update(UnavailabilityPeriodViewModel unavailabilityPeriodViewModel )
        {
            try { 
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
            catch (Exception ex)
            {
                var error = new ErrorViewModel();
                error.ErrorMessage = ex.Message;
                return View("Error", error);
            }
        }

        

    }
}
