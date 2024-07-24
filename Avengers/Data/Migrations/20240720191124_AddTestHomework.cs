using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Avengers.Data.Migrations
{
    public partial class AddTestHomework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert a Subject
            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Mathematics" });

            // Insert a Teacher
            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name", "SubjectId" },
                values: new object[] { 1, "Mr. Smith", 1 });

            // Insert a HomeworkAssignment
            migrationBuilder.InsertData(
                table: "HomeworkAssignments",
                columns: new[] { "Id", "Title", "DueDate", "SubjectId", "TeacherId", "Grade" },
                values: new object[] { 1, "Test Homework", "2024-08-01", 1, 1, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the HomeworkAssignment
            migrationBuilder.DeleteData(
                table: "HomeworkAssignments",
                keyColumn: "Id",
                keyValue: 1);

            // Remove the Teacher
            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1);

            // Remove the Subject
            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
