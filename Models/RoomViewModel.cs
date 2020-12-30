using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HospitalMS.Models
{
    public class RoomViewModel
    {
        public Guid? RoomId { get; set; }

        [Required]
        public double DailyPrice { get; set; }

        [Required]
        [MaxLength(500)]
        public string Notes { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        [Required]
        public string Status { get; set; }

        // [Required]
        // public DateTime DateAdded { get; set; }

        // [Required]
        // [Range(0,50)]
        // public int DaysBetweenCleaning { get; set; }

    }
}
