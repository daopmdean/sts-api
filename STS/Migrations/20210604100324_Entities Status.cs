using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class EntitiesStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WeekScheduleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WeekSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StoreManagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StaffSkill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StaffScheduleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BrandManagers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "WeekScheduleDetail");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WeekSchedule");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StoreManagers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StaffSkill");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StaffScheduleDetail");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BrandManagers");
        }
    }
}
