using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class removeEmailVerificationColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_verified",
                schema: "VmProjectBE",
                table: "user");

            migrationBuilder.DropColumn(
                name: "verification_code",
                schema: "VmProjectBE",
                table: "user");

            migrationBuilder.DropColumn(
                name: "verification_code_expiration",
                schema: "VmProjectBE",
                table: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_verified",
                schema: "VmProjectBE",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "verification_code",
                schema: "VmProjectBE",
                table: "user",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<DateTime>(
                name: "verification_code_expiration",
                schema: "VmProjectBE",
                table: "user",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 10);
        }
    }
}
