using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class UpdateEntityAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftAssignments_ShiftAssignments_ShiftReferenceId",
                table: "ShiftAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffScheduleDetails_WeekSchedules_WeekScheduleId",
                table: "StaffScheduleDetails");

            migrationBuilder.DropIndex(
                name: "IX_ShiftAssignments_ShiftReferenceId",
                table: "ShiftAssignments");

            migrationBuilder.DropColumn(
                name: "MaxHours",
                table: "StaffScheduleDetails");

            migrationBuilder.DropColumn(
                name: "MinHours",
                table: "StaffScheduleDetails");

            migrationBuilder.DropColumn(
                name: "ShiftReferenceId",
                table: "ShiftAssignments");

            migrationBuilder.AddColumn<float>(
                name: "MaxHoursPerDay",
                table: "StaffScheduleDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxHoursPerWeek",
                table: "StaffScheduleDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxShiftDuration",
                table: "StaffScheduleDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MinHoursPerDay",
                table: "StaffScheduleDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MinHoursPerWeek",
                table: "StaffScheduleDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MinShiftDuration",
                table: "StaffScheduleDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffScheduleDetails_WeekSchedules_WeekScheduleId",
                table: "StaffScheduleDetails",
                column: "WeekScheduleId",
                principalTable: "WeekSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffScheduleDetails_WeekSchedules_WeekScheduleId",
                table: "StaffScheduleDetails");

            migrationBuilder.DropColumn(
                name: "MaxHoursPerDay",
                table: "StaffScheduleDetails");

            migrationBuilder.DropColumn(
                name: "MaxHoursPerWeek",
                table: "StaffScheduleDetails");

            migrationBuilder.DropColumn(
                name: "MaxShiftDuration",
                table: "StaffScheduleDetails");

            migrationBuilder.DropColumn(
                name: "MinHoursPerDay",
                table: "StaffScheduleDetails");

            migrationBuilder.DropColumn(
                name: "MinHoursPerWeek",
                table: "StaffScheduleDetails");

            migrationBuilder.DropColumn(
                name: "MinShiftDuration",
                table: "StaffScheduleDetails");

            migrationBuilder.AddColumn<int>(
                name: "MaxHours",
                table: "StaffScheduleDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinHours",
                table: "StaffScheduleDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShiftReferenceId",
                table: "ShiftAssignments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignments_ShiftReferenceId",
                table: "ShiftAssignments",
                column: "ShiftReferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftAssignments_ShiftAssignments_ShiftReferenceId",
                table: "ShiftAssignments",
                column: "ShiftReferenceId",
                principalTable: "ShiftAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffScheduleDetails_WeekSchedules_WeekScheduleId",
                table: "StaffScheduleDetails",
                column: "WeekScheduleId",
                principalTable: "WeekSchedules",
                principalColumn: "Id");
        }
    }
}
