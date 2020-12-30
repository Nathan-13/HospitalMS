using System;
using EngineModel.Engine;

namespace EngineModel.Storage.EFModels
{
    public class Cleaning
    {
        public Guid CleaningId { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime CleaningDate { get; set; }
    }
}