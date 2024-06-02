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
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            return View(await _roomService.GetAllRooms());
        }

        public IActionResult Details(int id)
        {
            var room = _roomService.GetRoomById(id);
            return View(room);
        }

        public IActionResult Create()
        {
            return View();
        }
       // [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([Bind("Code,MaximumCapacity")] RoomViewModel roomVM)
        {
            if (ModelState.IsValid)
            {

                _roomService.AddRoom(roomVM);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return StatusCode(500, "Information is invalid");
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] RoomViewModel roomVM)
        //{
        //     await _service.AddRoom(roomVM);
        //    return CreatedAtAction(nameof(Index), new { id = roomVM.Id }, roomVM);
        //}

      //  [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update([Bind(" Code, MaximumCapacity")] RoomViewModel roomVM)
        {
            if (ModelState.IsValid)
            {

                _roomService.UpdateRoom(roomVM);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return StatusCode(500, "Information is invalid");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int Id)
        {
            var room = _roomService.DeleteRoom(Id);
            return RedirectToAction(nameof(Index));

        }
        }
    }


