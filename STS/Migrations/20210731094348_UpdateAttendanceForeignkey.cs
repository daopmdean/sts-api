using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class UpdateAttendanceForeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_Username1",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_Username1",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "Username1",
                table: "Attendances");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_Username",
                table: "Attendances",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_Username",
                table: "Attendances",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_Username",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_Username",
                table: "Attendances");

            migrationBuilder.AddColumn<string>(
                name: "Username1",
                table: "Attendances",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_Username1",
                table: "Attendances",
                column: "Username1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_Username1",
                table: "Attendances",
                column: "Username1",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
