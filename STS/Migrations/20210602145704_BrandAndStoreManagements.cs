using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class BrandAndStoreManagements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hotline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandManagers",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandManagers", x => x.Username);
                    table.ForeignKey(
                        name: "FK_BrandManagers_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandManagers_Users_Username1",
                        column: x => x.Username1,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Staff_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_Users_Username1",
                        column: x => x.Username1,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId1 = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Brands_BrandId1",
                        column: x => x.BrandId1,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffSkill",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    StaffUsername = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffSkill", x => new { x.StaffId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_StaffSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffSkill_Staff_StaffUsername",
                        column: x => x.StaffUsername,
                        principalTable: "Staff",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShiftAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    StaffUsername = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MealStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MealEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceId = table.Column<int>(type: "int", nullable: false),
                    ShiftReferenceId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftAssignment_ShiftAssignment_ShiftReferenceId",
                        column: x => x.ShiftReferenceId,
                        principalTable: "ShiftAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftAssignment_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftAssignment_Staff_StaffUsername",
                        column: x => x.StaffUsername,
                        principalTable: "Staff",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftAssignment_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreManagers",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreManagers", x => x.Username);
                    table.ForeignKey(
                        name: "FK_StoreManagers_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreManagers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreManagers_Users_Username1",
                        column: x => x.Username1,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreStaff",
                columns: table => new
                {
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StoreId1 = table.Column<int>(type: "int", nullable: true),
                    IsPrimaryStore = table.Column<bool>(type: "bit", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreStaff", x => new { x.StaffId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_StoreStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreStaff_Stores_StoreId1",
                        column: x => x.StoreId1,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeekSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekSchedule_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftRegister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    StaffUsername = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WeekScheduleId = table.Column<int>(type: "int", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferSkill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftRegister_Staff_StaffUsername",
                        column: x => x.StaffUsername,
                        principalTable: "Staff",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftRegister_WeekSchedule_WeekScheduleId",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffScheduleDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekScheduleId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    StaffUsername = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MinHours = table.Column<int>(type: "int", nullable: false),
                    MaxHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffScheduleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffScheduleDetail_Staff_StaffUsername",
                        column: x => x.StaffUsername,
                        principalTable: "Staff",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffScheduleDetail_WeekSchedule_WeekScheduleId",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeekScheduleDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekScheduleId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WorkStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekScheduleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekScheduleDetail_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeekScheduleDetail_WeekSchedule_WeekScheduleId",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandManagers_BrandId",
                table: "BrandManagers",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandManagers_Username1",
                table: "BrandManagers",
                column: "Username1");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignment_ShiftReferenceId",
                table: "ShiftAssignment",
                column: "ShiftReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignment_SkillId",
                table: "ShiftAssignment",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignment_StaffUsername",
                table: "ShiftAssignment",
                column: "StaffUsername");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignment_StoreId",
                table: "ShiftAssignment",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRegister_StaffUsername",
                table: "ShiftRegister",
                column: "StaffUsername");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRegister_WeekScheduleId",
                table: "ShiftRegister",
                column: "WeekScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_BrandId",
                table: "Skill",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_BrandId",
                table: "Staff",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Username1",
                table: "Staff",
                column: "Username1");

            migrationBuilder.CreateIndex(
                name: "IX_StaffScheduleDetail_StaffUsername",
                table: "StaffScheduleDetail",
                column: "StaffUsername");

            migrationBuilder.CreateIndex(
                name: "IX_StaffScheduleDetail_WeekScheduleId",
                table: "StaffScheduleDetail",
                column: "WeekScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffSkill_SkillId",
                table: "StaffSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffSkill_StaffUsername",
                table: "StaffSkill",
                column: "StaffUsername");

            migrationBuilder.CreateIndex(
                name: "IX_StoreManagers_BrandId",
                table: "StoreManagers",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreManagers_StoreId",
                table: "StoreManagers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreManagers_Username1",
                table: "StoreManagers",
                column: "Username1");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_BrandId1",
                table: "Stores",
                column: "BrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStaff_StoreId1",
                table: "StoreStaff",
                column: "StoreId1");

            migrationBuilder.CreateIndex(
                name: "IX_WeekSchedule_StoreId",
                table: "WeekSchedule",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekScheduleDetail_SkillId",
                table: "WeekScheduleDetail",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekScheduleDetail_WeekScheduleId",
                table: "WeekScheduleDetail",
                column: "WeekScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandManagers");

            migrationBuilder.DropTable(
                name: "ShiftAssignment");

            migrationBuilder.DropTable(
                name: "ShiftRegister");

            migrationBuilder.DropTable(
                name: "StaffScheduleDetail");

            migrationBuilder.DropTable(
                name: "StaffSkill");

            migrationBuilder.DropTable(
                name: "StoreManagers");

            migrationBuilder.DropTable(
                name: "StoreStaff");

            migrationBuilder.DropTable(
                name: "WeekScheduleDetail");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "WeekSchedule");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
