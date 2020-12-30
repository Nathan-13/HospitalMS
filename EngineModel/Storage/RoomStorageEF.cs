using System;
using System.Linq;
using EngineModel.Engine;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EngineModel.Storage
{
    public class RoomStorageEF : IStoreRooms
    {
        private readonly ApplicationDbContext _context;

        public RoomStorageEF(ApplicationDbContext context) {
            _context = context;
        }
        public void InsertRoom(Room newRoom) {
            var roomDb = ConvertToDb(newRoom);
            _context.Rooms.Add(roomDb);
            _context.SaveChanges();
        }
        
        public Room GetRoom(Guid roomId, Guid userId) {
            var roomDb = _context.Rooms
                .AsNoTracking()
                .First(x => x.RoomId == roomId && x.UserId == userId);
            return ConvertFromDb(roomDb);
        }

        public void UpdateRoom(Room updatedRoom) {
            var roomDb = ConvertToDb(updatedRoom);
            _context.Rooms.Add(roomDb);
            _context.SaveChanges();
        }

        public List<Room> GetAllRooms(Guid userId) {
            var rooms = _context.Rooms
                .AsNoTracking()
                .Include(x => x.Cleanings)
                .Where(x => x.UserId == userId && x.IsDeleted == false)
                .Select(x => ConvertFromDb(x))
                .ToList();
            return rooms;
        }
        
        public void DeleteRoom(Guid roomId, Guid userId) {
            var roomDb = _context.Rooms
                .AsNoTracking()
                .First(x => x.RoomId == roomId && x.UserId == userId);
            roomDb.IsDeleted = true;
            _context.Rooms.Update(roomDb);
            _context.SaveChanges();
        }

        private static EFModels.Room ConvertToDb(Room room) {
            return new EFModels.Room() {
                RoomId = room.RoomId,
                UserId = room.UserId,
                DailyPrice = room.DailyPrice,
                Notes = room.Notes,
                Location = room.Location,
                Status = room.Status,
                DaysBetweenCleaning = room.DaysBetweenCleaning,
                IsDeleted = false,
                Cleanings = room.Cleanings
                    .Select(x => ConvertToDb(x))
                    .ToList(),
            };
        }

        private static Room ConvertFromDb(EFModels.Room roomDb) {
            return new Room() {
                RoomId = roomDb.RoomId,
                UserId = roomDb.UserId,
                DailyPrice = roomDb.DailyPrice,
                Notes = roomDb.Notes,
                Location = roomDb.Location,
                Status = roomDb.Status,
                DaysBetweenCleaning = roomDb.DaysBetweenCleaning,
                Cleanings = roomDb.Cleanings
                    .Select(x => ConvertFromDb(x))
                    .ToList(),
            };
        }

        private static EFModels.Cleaning ConvertToDb(Cleaning cleaning) {
            return new EFModels.Cleaning() {
                CleaningId = cleaning.CleaningId,
                RoomId = cleaning.RoomId,
                CleaningDate = cleaning.CleaningDate,    
            };
        }

        private static Cleaning ConvertFromDb(EFModels.Cleaning cleaningDb) {
            return new Cleaning() {
                CleaningId = cleaningDb.CleaningId,
                RoomId = cleaningDb.RoomId,
                CleaningDate = cleaningDb.CleaningDate,
            };
        }
    }
}