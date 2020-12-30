using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalMS.Migrations
{
    public partial class MyFirstMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    DoctorName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Specialty = table.Column<string>(nullable: true),
                    Descriptions = table.Column<string>(nullable: true),
                    DateJoin = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    DailyPrice = table.Column<double>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DaysBetweenCleaning = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Cleaning",
                columns: table => new
                {
                    CleaningId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    CleaningDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cleaning", x => x.CleaningId);
                    table.ForeignKey(
                        name: "FK_Cleaning_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PatientName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Symptom = table.Column<string>(nullable: true),
                    AddmissionDate = table.Column<DateTime>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cleaning_RoomId",
                table: "Cleaning",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_RoomId",
                table: "Patients",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cleaning");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
