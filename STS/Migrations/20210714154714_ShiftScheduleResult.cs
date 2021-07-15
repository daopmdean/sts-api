using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace STS.Migrations
{
    public partial class ShiftScheduleResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShiftScheduleResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsComplete = table.Column<bool>(type: "boolean", nullable: false),
                    Conflicts = table.Column<long>(type: "bigint", nullable: false),
                    Branches = table.Column<long>(type: "bigint", nullable: false),
                    WallTime = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftScheduleResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShiftScheduleDetailResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false),
                    ShiftScheduleResultId = table.Column<long>(type: "bigint", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MealStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MealEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftScheduleDetailResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftScheduleDetailResults_ShiftScheduleResults_ShiftSchedu~",
                        column: x => x.ShiftScheduleResultId,
                        principalTable: "ShiftScheduleResults",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftScheduleDetailResults_ShiftScheduleResultId",
                table: "ShiftScheduleDetailResults",
                column: "ShiftScheduleResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftScheduleDetailResults");

            migrationBuilder.DropTable(
                name: "ShiftScheduleResults");
        }
    }
}
