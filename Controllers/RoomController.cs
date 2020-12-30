using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HospitalMS.Models;

using EngineModel.Engine;
using EngineModel.Storage;
using Microsoft.EntityFrameworkCore;

namespace HospitalMS.Controllers
{
    public class RoomController : Controller
    {
        private readonly Hospital _hospital;

        public RoomController(Hospital hospital)
        {
            _hospital = hospital;
        }

        public IActionResult Index()
        {
            var rooms = _hospital.GetAllRooms(UserId());
            return View(rooms);
        }

        public IActionResult Details(Guid roomId) {
            var room = _hospital.GetRoom(roomId, UserId());
            return View(room);
        }

        public IActionResult Create() {
            ViewBag.IsEditing = false;
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomViewModel roomViewModel) {
            if (ModelState.IsValid) {
                var newRoom = new Room() {
                    RoomId = Guid.NewGuid(),
                    DailyPrice = roomViewModel.DailyPrice,
                    Notes = roomViewModel.Notes,
                    Location = roomViewModel.Location,
                    Status = roomViewModel.Status,
                    // DateAdded = roomViewModel.DateAdded,
                    // DaysBetweenCleaning = roomViewModel.DaysBetweenCleaning
                };
                _hospital.CreateRoom(newRoom);
                return RedirectToAction("Index");
            }
            return View("Form");
        }

        public IActionResult Edit(Guid roomId) {
            var existingRoom = _hospital.GetRoom(roomId, UserId());

            var roomViewModel = new RoomViewModel() {
                RoomId = roomId,
                DailyPrice = existingRoom.DailyPrice,
                Notes = existingRoom.Notes,
                Location = existingRoom.Location,
                Status = existingRoom.Status,
                // DateAdded = existingRoom.DateAdded,
                // DaysBetweenCleaning = existingRoom.DaysBetweenCleaning
            };
            ViewBag.IsEditing = true;
            return RedirectToAction("Form", roomViewModel);
        }

        [HttpPost]
        public IActionResult Edit(RoomViewModel updatedRoom) {
            if (ModelState.IsValid) {
                var existingRoom = _hospital.GetRoom(updatedRoom.RoomId.Value, UserId());
                var room = new Room() {
                    RoomId = existingRoom.RoomId,
                    DailyPrice = existingRoom.DailyPrice,
                    Notes = existingRoom.Notes,
                    Location = existingRoom.Location,
                    Status = existingRoom.Status,
                    DateAdded = existingRoom.DateAdded,
                    DaysBetweenCleaning = existingRoom.DaysBetweenCleaning
                };
                _hospital.UpdateRoom(room);
                return RedirectToAction("Details", new { id = existingRoom.RoomId});
            } else {
                ViewBag.IsEditing = true;
                return View("Form", updatedRoom);
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid roomId) {
            _hospital.DeleteRoom(roomId, UserId());
            return RedirectToAction("Details", new { roomId});
        }

        public IActionResult Clean(Guid roomId) {
            _hospital.CleanRoom(roomId, UserId());
            return RedirectToAction("Details", new { roomId});
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Guid UserId() {
            return Guid.Parse("50dc9e44-627c-4da0-aa93-5e45ef4792b8");
        }
    }
}
