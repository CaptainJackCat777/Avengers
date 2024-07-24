using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avengers.Data.Migrations
{
    public partial class SeedSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name", "created", "last_modified" },
                values: new object[,]
                {
                    { 1, "Mathematics", "2024-07-24T22:20:38.3106855Z", "2024-07-24T22:20:38.3106870Z" },
                    { 2, "English", "2024-07-24T22:20:38.3106873Z", "2024-07-24T22:20:38.3106874Z" },
                    { 3, "Geography", "2024-07-24T22:20:38.3106875Z", "2024-07-24T22:20:38.3106875Z" },
                    { 4, "History", "2024-07-24T22:20:38.3106876Z", "2024-07-24T22:20:38.3106877Z" },
                    { 5, "Physics", "2024-07-24T22:20:38.3106878Z", "2024-07-24T22:20:38.3106879Z" },
                    { 6, "Information Technology", "2024-07-24T22:20:38.3106880Z", "2024-07-24T22:20:38.3106881Z" },
                    { 7, "Bulgarian", "2024-07-24T22:20:38.3106882Z", "2024-07-24T22:20:38.3106883Z" },
                    { 8, "Biology", "2024-07-24T22:20:38.3106884Z", "2024-07-24T22:20:38.3106884Z" },
                    { 9, "Physical Education", "2024-07-24T22:20:38.3106885Z", "2024-07-24T22:20:38.3106886Z" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
