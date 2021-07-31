using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace STS.Migrations
{
    public partial class UpdateAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftAttendances_ShiftAssignments_ShiftAssignmentId",
                table: "ShiftAttendances");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftAssignmentId",
                table: "ShiftAttendances",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ShiftAssignmentId1",
                table: "ShiftAttendances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Attendances",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAttendances_ShiftAssignmentId1",
                table: "ShiftAttendances",
                column: "ShiftAssignmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftAttendances_ShiftAssignments_ShiftAssignmentId1",
                table: "ShiftAttendances",
                column: "ShiftAssignmentId1",
                principalTable: "ShiftAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftAttendances_ShiftAssignments_ShiftAssignmentId1",
                table: "ShiftAttendances");

            migrationBuilder.DropIndex(
                name: "IX_ShiftAttendances_ShiftAssignmentId1",
                table: "ShiftAttendances");

            migrationBuilder.DropColumn(
                name: "ShiftAssignmentId1",
                table: "ShiftAttendances");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Attendances");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftAssignmentId",
                table: "ShiftAttendances",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftAttendances_ShiftAssignments_ShiftAssignmentId",
                table: "ShiftAttendances",
                column: "ShiftAssignmentId",
                principalTable: "ShiftAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
