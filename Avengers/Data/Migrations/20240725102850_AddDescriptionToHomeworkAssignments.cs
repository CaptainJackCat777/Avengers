using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avengers.Data.Migrations
{
    public partial class AddDescriptionToHomeworkAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HomeworkAssignments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "HomeworkAssignments");
        }
    }
}
