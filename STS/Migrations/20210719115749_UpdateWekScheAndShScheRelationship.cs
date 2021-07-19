using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class UpdateWekScheAndShScheRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "WeekSchedules",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WeekScheduleId",
                table: "ShiftScheduleResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftScheduleResults_WeekScheduleId",
                table: "ShiftScheduleResults",
                column: "WeekScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftScheduleResults_WeekSchedules_WeekScheduleId",
                table: "ShiftScheduleResults",
                column: "WeekScheduleId",
                principalTable: "WeekSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftScheduleResults_WeekSchedules_WeekScheduleId",
                table: "ShiftScheduleResults");

            migrationBuilder.DropIndex(
                name: "IX_ShiftScheduleResults_WeekScheduleId",
                table: "ShiftScheduleResults");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "WeekSchedules");

            migrationBuilder.DropColumn(
                name: "WeekScheduleId",
                table: "ShiftScheduleResults");
        }
    }
}
