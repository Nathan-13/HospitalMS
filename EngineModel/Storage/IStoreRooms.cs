using System;
using EngineModel.Engine;
using System.Collections.Generic;

namespace EngineModel.Storage
{
    public interface IStoreRooms
    {
        void InsertRoom(Room newRoom);
        
        Room GetRoom(Guid roomId, Guid userId);

        void UpdateRoom(Room updatedRoom);

        List<Room> GetAllRooms(Guid userId);

        void DeleteRoom(Guid roomId, Guid userId);
    }
}