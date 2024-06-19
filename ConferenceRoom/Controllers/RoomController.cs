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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConferenceRoom.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly ApplicationDbContext _context;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _roomService.GetAllRooms());
        }

        public async Task<IActionResult> Details(int id)
        {     
            return View(await _roomService.GetRoomById(id));
        }
        [Authorize(Roles = Constants.AdminRole)]
        public async Task<IActionResult> Update(int id)
        {
            try
            { 
            var room = await _roomService.GetRoomById(id);
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
                //duhet te therrasesh metoden e delete
                await _roomService.DeleteRoom(id);
                //duhet te kthesh view index
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
        public async Task<IActionResult> Create(RoomViewModel roomVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roomService.AddRoom(roomVM);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(roomVM);
                }
            }
            catch(Exception ex)
            {
                var error = new ErrorViewModel();
                error.ErrorMessage = ex.Message;
                return View("Error", error);
            }
        }


         [Authorize(Roles = Constants.AdminRole)]
        [HttpPost]
        public IActionResult Update(RoomViewModel roomVm)
        {
          try 
          {
                
            if (ModelState.IsValid)
            {
                _roomService.UpdateRoom(roomVm);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(roomVm);
            }
          }
            catch(Exception ex)
            {
                var error = new ErrorViewModel();
        error.ErrorMessage = ex.Message;
                return View("Error", error);
            }
        }


    }
}

