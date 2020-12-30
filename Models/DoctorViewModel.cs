using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalMS.Models
{
    public class DoctorViewModel
    {
        public Guid? DoctorId { get; set; }

        [Required]
        [MaxLength(50)]
        public string DoctorName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Specialty { get; set; }

        [Required]
        [MaxLength(800)]
        public string Descriptions { get; set; }

        [Required]
        public DateTime DateJoin { get; set; }
    }
}
