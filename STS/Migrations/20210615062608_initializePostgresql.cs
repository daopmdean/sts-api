using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace STS.Migrations
{
    public partial class initializePostgresql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LogoImg = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Hotline = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<byte[]>(type: "bytea", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Dob = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Users_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeekSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    DateStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekSchedules_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShiftAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MealStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MealEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReferenceId = table.Column<int>(type: "integer", nullable: false),
                    ShiftReferenceId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
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
                name: "StaffSkills",
                columns: table => new
                {
                    Username = table.Column<string>(type: "text", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffSkills", x => new { x.Username, x.SkillId });
                    table.ForeignKey(
                        name: "FK_StaffSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffSkills_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "StoreStaffs",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    IsPrimaryStaff = table.Column<bool>(type: "boolean", nullable: false),
                    IsManager = table.Column<bool>(type: "boolean", nullable: false),
                    DateStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreStaffs", x => new { x.Username, x.StoreId });
                    table.ForeignKey(
                        name: "FK_StoreStaffs_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreStaffs_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "ShiftRegisters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    WeekScheduleId = table.Column<int>(type: "integer", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PreferSkill = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftRegisters_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username");
                    table.ForeignKey(
                        name: "FK_ShiftRegisters_WeekSchedules_WeekScheduleId",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffScheduleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeekScheduleId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Username1 = table.Column<string>(type: "text", nullable: true),
                    MinHours = table.Column<int>(type: "integer", nullable: false),
                    MaxHours = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeekScheduleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeekScheduleId = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    WorkStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WorkEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekScheduleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekScheduleDetails_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeekScheduleDetails_WeekSchedules_WeekScheduleId",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShiftAttendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftAssignmentId = table.Column<int>(type: "integer", nullable: false),
                    TimeCheckIn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TimeCheckOut = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftAssignmentId = table.Column<int>(type: "integer", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_Posts_BrandId",
                table: "Posts",
                column: "BrandId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRegisters_Username",
                table: "ShiftRegisters",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRegisters_WeekScheduleId",
                table: "ShiftRegisters",
                column: "WeekScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_BrandId",
                table: "Skills",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffScheduleDetails_Username1",
                table: "StaffScheduleDetails",
                column: "Username1");

            migrationBuilder.CreateIndex(
                name: "IX_StaffScheduleDetails_WeekScheduleId",
                table: "StaffScheduleDetails",
                column: "WeekScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffSkills_SkillId",
                table: "StaffSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_BrandId",
                table: "Stores",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStaffs_StoreId",
                table: "StoreStaffs",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BrandId",
                table: "Users",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekScheduleDetails_SkillId",
                table: "WeekScheduleDetails",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekScheduleDetails_WeekScheduleId",
                table: "WeekScheduleDetails",
                column: "WeekScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekSchedules_StoreId",
                table: "WeekSchedules",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ShiftAttendances");

            migrationBuilder.DropTable(
                name: "ShiftLogs");

            migrationBuilder.DropTable(
                name: "ShiftRegisters");

            migrationBuilder.DropTable(
                name: "StaffScheduleDetails");

            migrationBuilder.DropTable(
                name: "StaffSkills");

            migrationBuilder.DropTable(
                name: "StoreStaffs");

            migrationBuilder.DropTable(
                name: "WeekScheduleDetails");

            migrationBuilder.DropTable(
                name: "ShiftAssignments");

            migrationBuilder.DropTable(
                name: "WeekSchedules");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
