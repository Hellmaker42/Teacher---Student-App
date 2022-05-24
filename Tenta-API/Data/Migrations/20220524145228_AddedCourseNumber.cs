using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tenta_API.Data.Migrations
{
    public partial class AddedCourseNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Courses");
        }
    }
}
