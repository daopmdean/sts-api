using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class UpdateAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftAttendances");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Attendances",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Attendances");

            migrationBuilder.CreateTable(
                name: "ShiftAttendances",
                columns: table => new
                {
                    ShiftAssignmentId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TimeCheckIn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TimeCheckOut = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftAttendances", x => x.ShiftAssignmentId);
                    table.ForeignKey(
                        name: "FK_ShiftAttendances_ShiftAssignments_ShiftAssignmentId",
                        column: x => x.ShiftAssignmentId,
                        principalTable: "ShiftAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
