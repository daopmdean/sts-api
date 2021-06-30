using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class UpdateUserStoreSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffType",
                table: "StoreScheduleDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StaffType",
                table: "StoreScheduleDetails");
        }
    }
}
