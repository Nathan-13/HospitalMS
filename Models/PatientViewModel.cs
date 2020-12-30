using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HospitalMS.Models
{
    public class PatientViewModel
    {
        public Guid? PatientId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PatientName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public double Height { get; set; }

        [Required]
        public string Symptom { get; set; }

        [Required]
        public DateTime AddmissionDate { get; set; }

        [Required]
        public Guid RoomId { get; set; }

    }
}
