using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace STS.Migrations
{
    public partial class StoreScheduleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "StoreScheduleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeekScheduleId = table.Column<int>(type: "integer", nullable: false),
                    MinDayOff = table.Column<int>(type: "integer", nullable: false),
                    MaxDayOff = table.Column<int>(type: "integer", nullable: false),
                    MinHoursPerWeek = table.Column<float>(type: "real", nullable: false),
                    MaxHoursPerWeek = table.Column<float>(type: "real", nullable: false),
                    MinHoursPerDay = table.Column<float>(type: "real", nullable: false),
                    MaxHoursPerDay = table.Column<float>(type: "real", nullable: false),
                    MinShiftDuration = table.Column<float>(type: "real", nullable: false),
                    MaxShiftDuration = table.Column<float>(type: "real", nullable: false),
                    MaxShiftPerDay = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreScheduleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreScheduleDetails_WeekSchedules_WeekScheduleId",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreScheduleDetails_WeekScheduleId",
                table: "StoreScheduleDetails",
                column: "WeekScheduleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreScheduleDetails");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
