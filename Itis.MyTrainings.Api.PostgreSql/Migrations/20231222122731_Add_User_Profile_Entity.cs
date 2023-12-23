using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class Add_User_Profile_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4e556bc3-d6b7-4be0-910f-8e7da06c27d0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("528e5a8a-d42b-4d57-9745-2e5982c5d259"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("92c909e1-19fd-4583-ad58-1fcacaf11bcb"));

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Height = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: true),
                    Goals = table.Column<List<string>>(type: "text[]", nullable: true),
                    TrainingPreference = table.Column<List<string>>(type: "text[]", nullable: true),
                    PreferredWorkoutTypes = table.Column<List<string>>(type: "text[]", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WeeklyTrainingFrequency = table.Column<int>(type: "integer", nullable: true),
                    MedicalSickness = table.Column<string>(type: "text", nullable: true),
                    DietaryPreferences = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("30ef6e8c-9f6a-4230-abf1-60956ad2bd59"), null, "User", "USER" },
                    { new Guid("d76f77c2-8178-4ad4-abd1-6e88e67fc6ee"), null, "Coach", "COACH" },
                    { new Guid("fba695d3-ff4f-4ff5-aeb2-a046d1ec4724"), null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("30ef6e8c-9f6a-4230-abf1-60956ad2bd59"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d76f77c2-8178-4ad4-abd1-6e88e67fc6ee"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fba695d3-ff4f-4ff5-aeb2-a046d1ec4724"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4e556bc3-d6b7-4be0-910f-8e7da06c27d0"), null, "Coach", "COACH" },
                    { new Guid("528e5a8a-d42b-4d57-9745-2e5982c5d259"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("92c909e1-19fd-4583-ad58-1fcacaf11bcb"), null, "User", "USER" }
                });
        }
    }
}
