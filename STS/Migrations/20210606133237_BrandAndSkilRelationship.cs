using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class BrandAndSkilRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Brands_BrandId",
                table: "Skills");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Brands_BrandId",
                table: "Skills",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Brands_BrandId",
                table: "Skills");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Brands_BrandId",
                table: "Skills",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
