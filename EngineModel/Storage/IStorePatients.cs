using System;
using EngineModel.Engine;
using System.Collections.Generic;

namespace EngineModel.Storage
{
    public interface IStorePatients
    {
        void InsertPatient(Patient newPatient);
        
        Patient GetPatient(Guid patientId, Guid userId);

        void UpdatePatient(Patient updatedPatient);

        List<Patient> GetAllPatients(Guid userId);

        void DeletePatient(Guid patientId, Guid userId);
    }
}