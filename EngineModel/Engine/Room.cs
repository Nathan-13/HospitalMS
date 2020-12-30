using System;
using System.Linq;
using System.Collections.Generic;

namespace EngineModel.Engine
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

        public DateTime NextCleaningDate {
            get {
                if (Cleanings.Count > 0) {
                    var lastCleaning = Cleanings.OrderByDescending(x => x.CleaningDate).First();
                    return lastCleaning.CleaningDate.AddDays(DaysBetweenCleaning);
                }
                else {
                    return DateAdded.AddDays(DaysBetweenCleaning);
                }
            }
        }

        public void Clean() {
            var cleaning = new Cleaning() {
                CleaningId = Guid.NewGuid(),
                CleaningDate = DateTime.Now,
                RoomId = RoomId
            };
            Cleanings.Add(cleaning);
        }
    }
}