using System;
using EngineModel.Storage;
using System.Collections.Generic;

namespace EngineModel.Engine
{
    public class Hospital
    {
        private readonly IStoreDoctors _doctorStorage;
        private readonly IStorePatients _patientStorage;
        private readonly IStoreRooms _roomStorage;

        public Hospital (IStoreDoctors doctorStorage, IStorePatients patientStorage, IStoreRooms roomStorage) {
            _doctorStorage = doctorStorage;
            _patientStorage = patientStorage;
            _roomStorage = roomStorage;
        }

        // Method for doctors
        public void CreateDoctor(Doctor newDoctor) {
            _doctorStorage.InsertDoctor(newDoctor);
        }

        public Doctor GetDoctor(Guid doctorId, Guid userId) {
            return _doctorStorage.GetDoctor(doctorId, userId);
        }

        public List<Doctor> GetAllDoctors(Guid userId) {
            return _doctorStorage.GetAllDoctors(userId);
        }

        public void UpdateDoctor(Doctor doctorToUpdate) {
            _doctorStorage.UpdateDoctor(doctorToUpdate);
        }

        public void DeleteDoctor(Guid doctorId, Guid userId) {
            _doctorStorage.DeleteDoctor(doctorId, userId);
        }

        // Methods for patients
        public void CreatePatient(Patient newPatient) {
            _patientStorage.InsertPatient(newPatient);
        }

        public Patient GetPatient(Guid patientId, Guid userId) {
            return _patientStorage.GetPatient(patientId, userId);
        }

        public List<Patient> GetAllPatients(Guid userId) {
            return _patientStorage.GetAllPatients(userId);
        }

        public void UpdatePatient(Patient updatedPatient) {
            _patientStorage.UpdatePatient(updatedPatient);
        }

        public void DeletePatient(Guid patientId, Guid userId) {
            _patientStorage.DeletePatient(patientId, userId);
        }

        // Methods for room 
        public void CreateRoom(Room newRoom) {
            _roomStorage.InsertRoom(newRoom);
        }

        public Room GetRoom(Guid roomId, Guid userId) {
            return _roomStorage.GetRoom(roomId, userId);
        }

        public List<Room> GetAllRooms(Guid userId) {
            return _roomStorage.GetAllRooms(userId);
        }

        public void UpdateRoom(Room updatedRoom) {
            _roomStorage.UpdateRoom(updatedRoom);
        }

        public void DeleteRoom(Guid roomId, Guid userId) {
            _roomStorage.DeleteRoom(roomId, userId);
        }

        public void CleanRoom(Guid roomId, Guid userId) {
            var roomToClean = GetRoom(roomId, userId);
            roomToClean.Clean();
            _roomStorage.UpdateRoom(roomToClean);
        }
    }
}