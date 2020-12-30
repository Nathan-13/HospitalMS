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
    public class PatientController : Controller
    {
        private readonly Hospital _hospital;

        public PatientController(Hospital hospital)
        {
            _hospital = hospital;
        }

        public IActionResult Index()
        {
            var result = _hospital.GetAllPatients(UserId());
            return View(result);
        }

        public IActionResult Details(Guid patientId) {
            var result = _hospital.GetPatient(patientId, UserId());
            return View(result);
        }

        public IActionResult Create() {
            ViewBag.IsEditing = false;
            return View();
        }

        [HttpPost]
        public IActionResult Create(PatientViewModel patientViewModel) {
            if (ModelState.IsValid) {
                var newPatient = new Patient() {
                    PatientId = Guid.NewGuid(),
                    UserId = UserId(),
                    PatientName = patientViewModel.PatientName,
                    DateOfBirth = patientViewModel.DateOfBirth,
                    Address = patientViewModel.Address,
                    PhoneNumber = patientViewModel.PhoneNumber,
                    Weight = patientViewModel.Weight,
                    Height = patientViewModel.Height,
                    Symptom = patientViewModel.Symptom,
                    AddmissionDate = patientViewModel.AddmissionDate,
                    RoomId = patientViewModel.RoomId
                };
                _hospital.CreatePatient(newPatient);
                return RedirectToAction("Index");
            }

            return View("Form");
        }

        public IActionResult Edit(Guid patientId) {
            var existingPatient = _hospital.GetPatient(patientId, UserId());

            var patientViewModel = new PatientViewModel() {
                PatientId = patientId,
                PatientName = existingPatient.PatientName,
                DateOfBirth = existingPatient.DateOfBirth,
                PhoneNumber = existingPatient.PhoneNumber,
                Weight = existingPatient.Weight,
                Height = existingPatient.Height,
                Symptom = existingPatient.Symptom,
                AddmissionDate = existingPatient.AddmissionDate,
                RoomId = existingPatient.RoomId,
            };

            ViewBag.IsEditing = true;
            return View("Form", patientViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PatientViewModel updatedPatient) {
            if (ModelState.IsValid) {
                var existingPatient = _hospital.GetPatient(updatedPatient.PatientId.Value, UserId());
                var patient = new Patient() {
                    PatientId = existingPatient.PatientId,
                    PatientName = existingPatient.PatientName,
                    DateOfBirth = existingPatient.DateOfBirth,
                    Address = existingPatient.Address,
                    PhoneNumber = existingPatient.PhoneNumber,
                    Weight = existingPatient.Weight,
                    Height = existingPatient.Height,
                    Symptom = existingPatient.Symptom,
                    AddmissionDate = existingPatient.AddmissionDate,
                    RoomId = existingPatient.RoomId
                };
                _hospital.UpdatePatient(patient);
                return RedirectToAction("Details", new {id = existingPatient.PatientId});
            } else {
                ViewBag.IsEditing = true;
                return View("Form", updatedPatient);
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid patientId) {
            _hospital.DeletePatient(patientId, UserId());
            return RedirectToAction("Details", new {patientId});
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
