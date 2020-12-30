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
    public class DoctorController : Controller
    {
        private Hospital _hospital;

        public DoctorController(Hospital hospital)
        {
            _hospital = hospital;
        }

        public IActionResult Index()
        {
            List<Doctor> doctors = _hospital.GetAllDoctors(UserId());
            return View(doctors);
        }

        public IActionResult Details(Guid id) {
            var result = _hospital.GetDoctor(id, UserId());
            return View(result);
        }

        public IActionResult Form() {
            ViewBag.IsEditing = false;
            return View();
        }

        [HttpPost]
        public IActionResult Create(DoctorViewModel newDoctor) {
            if (ModelState.IsValid) {
                var doctorToCreate = new Doctor() {
                    DoctorId = Guid.NewGuid(),
                    UserId = UserId(),
                    DoctorName = newDoctor.DoctorName,
                    Email = newDoctor.Email,
                    PhoneNumber = newDoctor.PhoneNumber,
                    Specialty = newDoctor.Specialty,
                    Descriptions = newDoctor.Descriptions,
                    DateJoin = newDoctor.DateJoin
                };
                _hospital.CreateDoctor(doctorToCreate);
                return RedirectToAction("Index");
            }
            return View("Form", newDoctor);
        }

        public IActionResult Edit(Guid doctorId) {
            var existingDoctor = _hospital.GetDoctor(doctorId, UserId());

            var doctorViewModel = new DoctorViewModel() {
                DoctorId = doctorId,
                DoctorName = existingDoctor.DoctorName,
                Email = existingDoctor.Email,
                PhoneNumber = existingDoctor.PhoneNumber,
                Specialty = existingDoctor.Specialty,
                Descriptions = existingDoctor.Descriptions,
                DateJoin = existingDoctor.DateJoin
            };

            ViewBag.IsEditing = true;
            return View("Form", doctorViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DoctorViewModel updatedDoctor) {
            if (ModelState.IsValid) {
                var existingDoctor = _hospital.GetDoctor(updatedDoctor.DoctorId.Value, UserId());
                var doctor = new Doctor() {
                    DoctorId = existingDoctor.DoctorId,
                    DoctorName = existingDoctor.DoctorName,
                    Email = existingDoctor.Email,
                    PhoneNumber = existingDoctor.PhoneNumber,
                    Specialty = existingDoctor.Specialty,
                    Descriptions = existingDoctor.Descriptions,
                    DateJoin = existingDoctor.DateJoin
                };
                _hospital.UpdateDoctor(doctor);
                return RedirectToAction("Details", new { id = existingDoctor.DoctorId});
            } else {
                ViewBag.IsEditing = true;
                return View("Form", updatedDoctor);
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid doctorId) {
            _hospital.DeleteDoctor(doctorId, UserId());
            return RedirectToAction("Index");
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
