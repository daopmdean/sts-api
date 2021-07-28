using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace STS.Migrations
{
    public partial class RemoveStaffScheduleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffScheduleDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffScheduleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaxHoursPerDay = table.Column<float>(type: "real", nullable: false),
                    MaxHoursPerWeek = table.Column<float>(type: "real", nullable: false),
                    MaxShiftDuration = table.Column<float>(type: "real", nullable: false),
                    MinHoursPerDay = table.Column<float>(type: "real", nullable: false),
                    MinHoursPerWeek = table.Column<float>(type: "real", nullable: false),
                    MinShiftDuration = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Username1 = table.Column<string>(type: "text", nullable: true),
                    WeekScheduleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffScheduleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffScheduleDetails_Users_Username1",
                        column: x => x.Username1,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffScheduleDetails_WeekSchedules_WeekScheduleId",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffScheduleDetails_Username1",
                table: "StaffScheduleDetails",
                column: "Username1");

            migrationBuilder.CreateIndex(
                name: "IX_StaffScheduleDetails_WeekScheduleId",
                table: "StaffScheduleDetails",
                column: "WeekScheduleId");
        }
    }
}
