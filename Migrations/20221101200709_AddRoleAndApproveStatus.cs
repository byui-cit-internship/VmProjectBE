using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class AddRoleAndApproveStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "approve_status",
                schema: "VmProjectBE",
                table: "user",
                type: "varchar(15)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AddColumn<string>(
                name: "role",
                schema: "VmProjectBE",
                table: "user",
                type: "varchar(15)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 11);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approve_status",
                schema: "VmProjectBE",
                table: "user");

            migrationBuilder.DropColumn(
                name: "role",
                schema: "VmProjectBE",
                table: "user");
        }
    }
}
