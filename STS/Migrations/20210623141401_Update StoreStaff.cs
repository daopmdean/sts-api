using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class UpdateStoreStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPrimaryStaff",
                table: "StoreStaffs",
                newName: "IsPrimaryStore");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPrimaryStore",
                table: "StoreStaffs",
                newName: "IsPrimaryStaff");
        }
    }
}
