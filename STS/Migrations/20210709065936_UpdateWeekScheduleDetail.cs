using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class UpdateWeekScheduleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "WeekScheduleDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "WeekScheduleDetails");
        }
    }
}
