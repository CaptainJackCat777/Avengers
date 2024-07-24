using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avengers.Data.Migrations
{
    public partial class HomeworkCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "text",
                table: "HomeworkCreations",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "HomeworkCreations",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "last_modified",
                table: "HomeworkCreations",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "file_path",
                table: "HomeworkCreations",
                newName: "FilePath");

            migrationBuilder.AddColumn<int>(
                name: "HomeworkAssignmentId",
                table: "HomeworkCreations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkCreations_HomeworkAssignmentId",
                table: "HomeworkCreations",
                column: "HomeworkAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeworkCreations_HomeworkAssignments_HomeworkAssignmentId",
                table: "HomeworkCreations",
                column: "HomeworkAssignmentId",
                principalTable: "HomeworkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeworkCreations_HomeworkAssignments_HomeworkAssignmentId",
                table: "HomeworkCreations");

            migrationBuilder.DropIndex(
                name: "IX_HomeworkCreations_HomeworkAssignmentId",
                table: "HomeworkCreations");

            migrationBuilder.DropColumn(
                name: "HomeworkAssignmentId",
                table: "HomeworkCreations");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "HomeworkCreations",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "HomeworkCreations",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "HomeworkCreations",
                newName: "last_modified");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "HomeworkCreations",
                newName: "file_path");
        }
    }
}
