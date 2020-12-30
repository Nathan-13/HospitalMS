using System;
using System.Collections.Generic;

namespace EngineModel.Storage.EFModels
{
    public class Doctor
    {
        public Guid DoctorId { get; set; }
        public Guid UserId { get; set; }
        public string DoctorName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialty { get; set; }
        public string Descriptions { get; set; }
        public DateTime DateJoin { get; set; }
        public bool IsDeleted { get; set; }
    }
}