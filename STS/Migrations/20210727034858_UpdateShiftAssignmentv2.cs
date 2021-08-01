﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STS.Migrations
{
    public partial class UpdateShiftAssignmentv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealEnd",
                table: "ShiftAssignments");

            migrationBuilder.DropColumn(
                name: "MealStart",
                table: "ShiftAssignments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MealEnd",
                table: "ShiftAssignments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MealStart",
                table: "ShiftAssignments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
