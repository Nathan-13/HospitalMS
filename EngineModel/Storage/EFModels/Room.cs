using System;
using EngineModel.Engine;
using System.Collections.Generic;

namespace EngineModel.Storage.EFModels
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public double DailyPrice { get; set; }
        public string Notes { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public int DaysBetweenCleaning { get; set; }
        public List<Cleaning> Cleanings { get; set; }
        public bool IsDeleted { get; set; }
    }
}