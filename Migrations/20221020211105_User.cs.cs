using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class Usercs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "email_is_verified",
                schema: "VmProjectBE",
                table: "user",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "verification_code",
                schema: "VmProjectBE",
                table: "user",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 9);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_is_verified",
                schema: "VmProjectBE",
                table: "user");

            migrationBuilder.DropColumn(
                name: "verification_code",
                schema: "VmProjectBE",
                table: "user");
        }
    }
}
