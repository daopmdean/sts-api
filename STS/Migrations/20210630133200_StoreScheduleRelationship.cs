using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class StoreScheduleRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StoreScheduleDetails_WeekScheduleId",
                table: "StoreScheduleDetails");

            migrationBuilder.CreateIndex(
                name: "IX_StoreScheduleDetails_WeekScheduleId",
                table: "StoreScheduleDetails",
                column: "WeekScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StoreScheduleDetails_WeekScheduleId",
                table: "StoreScheduleDetails");

            migrationBuilder.CreateIndex(
                name: "IX_StoreScheduleDetails_WeekScheduleId",
                table: "StoreScheduleDetails",
                column: "WeekScheduleId",
                unique: true);
        }
    }
}
