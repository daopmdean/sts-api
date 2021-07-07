using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace STS.Migrations
{
    public partial class UpdateShiftAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftAttendances",
                table: "ShiftAttendances");

            migrationBuilder.DropIndex(
                name: "IX_ShiftAttendances_ShiftAssignmentId",
                table: "ShiftAttendances");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShiftAttendances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftAttendances",
                table: "ShiftAttendances",
                column: "ShiftAssignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftAttendances",
                table: "ShiftAttendances");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShiftAttendances",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftAttendances",
                table: "ShiftAttendances",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAttendances_ShiftAssignmentId",
                table: "ShiftAttendances",
                column: "ShiftAssignmentId",
                unique: true);
        }
    }
}
