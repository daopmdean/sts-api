using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class EntitiesRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftRegister_Users_Username1",
                table: "ShiftRegister");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftRegister_WeekSchedules_WeekScheduleId",
                table: "ShiftRegister");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffSkill_Skills_SkillId",
                table: "StaffSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffSkill_Users_Username1",
                table: "StaffSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreStaff_Stores_StoreId1",
                table: "StoreStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreStaff_Users_Username1",
                table: "StoreStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Brands_BrandId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreStaff",
                table: "StoreStaff");

            migrationBuilder.DropIndex(
                name: "IX_StoreStaff_StoreId1",
                table: "StoreStaff");

            migrationBuilder.DropIndex(
                name: "IX_StoreStaff_Username1",
                table: "StoreStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffSkill",
                table: "StaffSkill");

            migrationBuilder.DropIndex(
                name: "IX_StaffSkill_Username1",
                table: "StaffSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftRegister",
                table: "ShiftRegister");

            migrationBuilder.DropIndex(
                name: "IX_ShiftRegister_Username1",
                table: "ShiftRegister");

            migrationBuilder.DropColumn(
                name: "StoreId1",
                table: "StoreStaff");

            migrationBuilder.DropColumn(
                name: "Username1",
                table: "StoreStaff");

            migrationBuilder.DropColumn(
                name: "Username1",
                table: "StaffSkill");

            migrationBuilder.DropColumn(
                name: "Username1",
                table: "ShiftRegister");

            migrationBuilder.RenameTable(
                name: "StoreStaff",
                newName: "StoreStaffs");

            migrationBuilder.RenameTable(
                name: "StaffSkill",
                newName: "StaffSkills");

            migrationBuilder.RenameTable(
                name: "ShiftRegister",
                newName: "ShiftRegisters");

            migrationBuilder.RenameIndex(
                name: "IX_StaffSkill_SkillId",
                table: "StaffSkills",
                newName: "IX_StaffSkills_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_ShiftRegister_WeekScheduleId",
                table: "ShiftRegisters",
                newName: "IX_ShiftRegisters_WeekScheduleId");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "StoreStaffs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "StaffSkills",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "ShiftRegisters",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreStaffs",
                table: "StoreStaffs",
                columns: new[] { "Username", "StoreId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffSkills",
                table: "StaffSkills",
                columns: new[] { "Username", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftRegisters",
                table: "ShiftRegisters",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ShiftAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    table.PrimaryKey("PK_ShiftAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftAssignments_ShiftAssignments_ShiftReferenceId",
                        column: x => x.ShiftReferenceId,
                        principalTable: "ShiftAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftAssignments_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftAssignments_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftAssignments_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "ShiftAttendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftAssignmentId = table.Column<int>(type: "int", nullable: false),
                    TimeCheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeCheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftAttendances_ShiftAssignments_ShiftAssignmentId",
                        column: x => x.ShiftAssignmentId,
                        principalTable: "ShiftAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftAssignmentId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftLogs_ShiftAssignments_ShiftAssignmentId",
                        column: x => x.ShiftAssignmentId,
                        principalTable: "ShiftAssignments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreStaffs_StoreId",
                table: "StoreStaffs",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRegisters_Username",
                table: "ShiftRegisters",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignments_ShiftReferenceId",
                table: "ShiftAssignments",
                column: "ShiftReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignments_SkillId",
                table: "ShiftAssignments",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignments_StoreId",
                table: "ShiftAssignments",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssignments_Username",
                table: "ShiftAssignments",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAttendances_ShiftAssignmentId",
                table: "ShiftAttendances",
                column: "ShiftAssignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftLogs_ShiftAssignmentId",
                table: "ShiftLogs",
                column: "ShiftAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftRegisters_Users_Username",
                table: "ShiftRegisters",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftRegisters_WeekSchedules_WeekScheduleId",
                table: "ShiftRegisters",
                column: "WeekScheduleId",
                principalTable: "WeekSchedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffSkills_Skills_SkillId",
                table: "StaffSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffSkills_Users_Username",
                table: "StaffSkills",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreStaffs_Stores_StoreId",
                table: "StoreStaffs",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreStaffs_Users_Username",
                table: "StoreStaffs",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Brands_BrandId",
                table: "Users",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftRegisters_Users_Username",
                table: "ShiftRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftRegisters_WeekSchedules_WeekScheduleId",
                table: "ShiftRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffSkills_Skills_SkillId",
                table: "StaffSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffSkills_Users_Username",
                table: "StaffSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreStaffs_Stores_StoreId",
                table: "StoreStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreStaffs_Users_Username",
                table: "StoreStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Brands_BrandId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ShiftAttendances");

            migrationBuilder.DropTable(
                name: "ShiftLogs");

            migrationBuilder.DropTable(
                name: "ShiftAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreStaffs",
                table: "StoreStaffs");

            migrationBuilder.DropIndex(
                name: "IX_StoreStaffs_StoreId",
                table: "StoreStaffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffSkills",
                table: "StaffSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftRegisters",
                table: "ShiftRegisters");

            migrationBuilder.DropIndex(
                name: "IX_ShiftRegisters_Username",
                table: "ShiftRegisters");

            migrationBuilder.RenameTable(
                name: "StoreStaffs",
                newName: "StoreStaff");

            migrationBuilder.RenameTable(
                name: "StaffSkills",
                newName: "StaffSkill");

            migrationBuilder.RenameTable(
                name: "ShiftRegisters",
                newName: "ShiftRegister");

            migrationBuilder.RenameIndex(
                name: "IX_StaffSkills_SkillId",
                table: "StaffSkill",
                newName: "IX_StaffSkill_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_ShiftRegisters_WeekScheduleId",
                table: "ShiftRegister",
                newName: "IX_ShiftRegister_WeekScheduleId");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StoreId",
                table: "StoreStaff",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StoreId1",
                table: "StoreStaff",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username1",
                table: "StoreStaff",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Username",
                table: "StaffSkill",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Username1",
                table: "StaffSkill",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Username",
                table: "ShiftRegister",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username1",
                table: "ShiftRegister",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreStaff",
                table: "StoreStaff",
                columns: new[] { "Username", "StoreId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffSkill",
                table: "StaffSkill",
                columns: new[] { "Username", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftRegister",
                table: "ShiftRegister",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStaff_StoreId1",
                table: "StoreStaff",
                column: "StoreId1");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStaff_Username1",
                table: "StoreStaff",
                column: "Username1");

            migrationBuilder.CreateIndex(
                name: "IX_StaffSkill_Username1",
                table: "StaffSkill",
                column: "Username1");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRegister_Username1",
                table: "ShiftRegister",
                column: "Username1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftRegister_Users_Username1",
                table: "ShiftRegister",
                column: "Username1",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftRegister_WeekSchedules_WeekScheduleId",
                table: "ShiftRegister",
                column: "WeekScheduleId",
                principalTable: "WeekSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffSkill_Skills_SkillId",
                table: "StaffSkill",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffSkill_Users_Username1",
                table: "StaffSkill",
                column: "Username1",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreStaff_Stores_StoreId1",
                table: "StoreStaff",
                column: "StoreId1",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreStaff_Users_Username1",
                table: "StoreStaff",
                column: "Username1",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Brands_BrandId",
                table: "Users",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
