using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class UpdateShiftAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekScheduleId",
                table: "ShiftAssignments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignments_WeekScheduleId",
                table: "ShiftAssignments",
                column: "WeekScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftAssignments_WeekSchedules_WeekScheduleId",
                table: "ShiftAssignments",
                column: "WeekScheduleId",
                principalTable: "WeekSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftAssignments_WeekSchedules_WeekScheduleId",
                table: "ShiftAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ShiftAssignments_WeekScheduleId",
                table: "ShiftAssignments");

            migrationBuilder.DropColumn(
                name: "WeekScheduleId",
                table: "ShiftAssignments");
        }
    }
}
