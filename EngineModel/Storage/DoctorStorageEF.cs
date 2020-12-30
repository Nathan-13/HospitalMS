using System;
using System.Linq;
using EngineModel.Engine;
using EngineModel.Storage;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EngineModel.Storage
{
    public class DoctorStorageEF : IStoreDoctors
    {
        private ApplicationDbContext _context;

        public DoctorStorageEF(ApplicationDbContext context) {
            _context = context;
        }

        public void InsertDoctor(Doctor newDoctor) {
            var doctorDb = ConvertToDb(newDoctor);
            _context.Doctors.Add(doctorDb);
            _context.SaveChanges();
        }
        
        public Doctor GetDoctor(Guid doctorId, Guid userId) {
            var doctorDb = _context.Doctors
                .AsNoTracking()
                .First(x => x.DoctorId == doctorId && x.UserId == userId);
            return ConvertFromDb(doctorDb);
        }

        public void UpdateDoctor(Doctor updatedDoctor) {
            var doctorDb = ConvertToDb(updatedDoctor);
            _context.Doctors.Add(doctorDb);
            _context.SaveChanges();
        }

        public List<Doctor> GetAllDoctors(Guid userId) {
            var doctors = _context.Doctors
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.IsDeleted == false)
                .Select(x => ConvertFromDb(x))
                .ToList();
            return doctors;
        }

        public void DeleteDoctor(Guid doctorId, Guid userId) {
            var doctorDb = _context.Doctors
                .AsNoTracking()
                .First(x => x.DoctorId == doctorId && x.UserId == userId);
            doctorDb.IsDeleted = true;
            _context.Doctors.Update(doctorDb);
            _context.SaveChanges();
        }

        private static EFModels.Doctor ConvertToDb(Doctor doctor) {
            return new EFModels.Doctor() {
                DoctorId = doctor.DoctorId,
                UserId = doctor.UserId,
                DoctorName = doctor.DoctorName,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                Specialty = doctor.Specialty,
                Descriptions = doctor.Descriptions,
                DateJoin = doctor.DateJoin,
                IsDeleted = false,
            };
        }

        private static Doctor ConvertFromDb(EFModels.Doctor doctorDb) {
            return new Doctor() {
                DoctorId = doctorDb.DoctorId,
                UserId = doctorDb.UserId,
                DoctorName = doctorDb.DoctorName,
                Email = doctorDb.Email,
                PhoneNumber = doctorDb.PhoneNumber,
                Specialty = doctorDb.Specialty,
                Descriptions = doctorDb.Descriptions,
                DateJoin = doctorDb.DateJoin,
            };
        }

    }

}