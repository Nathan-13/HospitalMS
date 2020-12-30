using System;

namespace EngineModel.Engine
{
    public class Cleaning
    {
        public Guid CleaningId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CleaningDate { get; set; }
        
    }
}