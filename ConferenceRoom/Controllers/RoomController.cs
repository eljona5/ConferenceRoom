using ConferenceRoom.Data.Entities;
using ConferenceRoom.Interface;
using ConferenceRoom.Models;
using ConferenceRoom.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _roomService.GetAllRooms());
        }

        public IActionResult Details(int id)
        {
            var room = _roomService.GetRoomById(id);
            return View(room);
        }

        public async Task<IActionResult> Update(int id)
        {
            var room = await _roomService.GetRoomById(id);
            return View(room);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomService.GetRoomById(id);
            return View(room);
        }


        public IActionResult Create()
        {
            return View();
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(RoomViewModel roomVM)
        {
            if (ModelState.IsValid)
            {
                _roomService.AddRoom(roomVM);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(roomVM);
            }
        }


        //  [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(RoomViewModel roomVm)
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

      //  [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(RoomViewModel roomVm)
        {
      
            if (ModelState.IsValid)
            {
                var room = _roomService.DeleteRoom(roomVm); 
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View(roomVm);
            }
        }



    }
}

