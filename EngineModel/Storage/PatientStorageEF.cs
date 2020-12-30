using System;
using System.Linq;
using EngineModel.Engine;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EngineModel.Storage
{
    public class PatientStorageEF : IStorePatients
    {
        private readonly ApplicationDbContext _context;

        public PatientStorageEF(ApplicationDbContext context) {
            _context = context;
        }
        public void InsertPatient(Patient newPatient) {
            var PatientDb = ConvertToDb(newPatient);
            _context.Patients.Add(PatientDb);
            _context.SaveChanges();
        }
        
        public Patient GetPatient(Guid patientId, Guid userId) {
            var patientDb = _context.Patients
                .AsNoTracking()
                .First(x => x.PatientId == patientId && x.UserId == userId);
            return ConvertFromDb(patientDb);
        }

        public void UpdatePatient(Patient updatedPatient) {
            var patientDb = ConvertToDb(updatedPatient);
            _context.Patients.Update(patientDb);
            _context.SaveChanges();
        }

        public List<Patient> GetAllPatients(Guid userId) {
            var patients = _context.Patients
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.IsDeleted == false)
                .Select(x => ConvertFromDb(x))
                .ToList();
            return patients;
        }

        public void DeletePatient(Guid patientId, Guid userId) {
            var patientDb = _context.Patients
                .AsNoTracking()
                .First(x => x.PatientId == patientId && x.UserId == userId);
            patientDb.IsDeleted = true;
            _context.Patients.Update(patientDb);
            _context.SaveChanges();
        }

        private static EFModels.Patient ConvertToDb(Patient patient) {
            return new EFModels.Patient() {
                PatientId = patient.PatientId,
                UserId = patient.UserId,
                PatientName = patient.PatientName,
                DateOfBirth = patient.DateOfBirth,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                Height = patient.Height,
                Weight = patient.Weight,
                Symptom = patient.Symptom,
                AddmissionDate = patient.AddmissionDate,
                RoomId = patient.RoomId,
                IsDeleted = false
            };
        }

        private static Patient ConvertFromDb(EFModels.Patient patientDb) {
            return new Patient() { 
                PatientId = patientDb.PatientId,
                UserId = patientDb.UserId,
                PatientName = patientDb.PatientName,
                DateOfBirth = patientDb.DateOfBirth,
                Address = patientDb.Address,
                PhoneNumber = patientDb.PhoneNumber,
                Height = patientDb.Height,
                Weight = patientDb.Weight,
                Symptom = patientDb.Symptom,
                AddmissionDate = patientDb.AddmissionDate,
                RoomId = patientDb.RoomId,
            };
            
        }
    }
}