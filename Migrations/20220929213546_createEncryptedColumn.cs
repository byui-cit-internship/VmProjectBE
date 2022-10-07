using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class createEncryptedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "encrypted_canvas_token",
                schema: "VmProjectBE",
                table: "user",
                type: "varbinary(MAX)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "encrypted_canvas_token",
                schema: "VmProjectBE",
                table: "user");
        }
    }
}
