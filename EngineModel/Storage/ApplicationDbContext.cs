using System;
using Microsoft.EntityFrameworkCore;
using EngineModel.Storage.EFModels;

namespace EngineModel.Storage
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            // Empty constructor body...
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Room> Rooms { get; set; }

    }
}