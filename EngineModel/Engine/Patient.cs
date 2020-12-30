using System;

namespace EngineModel.Engine
{
    public class Patient
    {
        public Guid PatientId { get; set; }
        public Guid UserId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Symptom { get; set; }
        public DateTime AddmissionDate { get; set; }
        public Room Room { get; set; }
        public Guid RoomId { get; set; }
    }
}