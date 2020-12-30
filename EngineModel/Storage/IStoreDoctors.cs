using System;
using EngineModel.Engine;
using System.Collections.Generic;

namespace EngineModel.Storage
{
    public interface IStoreDoctors
    {
        void InsertDoctor(Doctor newDoctor);
        
        Doctor GetDoctor(Guid doctorId, Guid userId);

        void UpdateDoctor(Doctor updatedDoctor);

        List<Doctor> GetAllDoctors(Guid userId);

        void DeleteDoctor(Guid doctorId, Guid userId);
    }
}