using Microsoft.EntityFrameworkCore.Migrations;

namespace Database_VmProject.Migrations
{
    public partial class FixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tag)_user_id",
                schema: "VmProjectBE",
                table: "tag_user",
                newName: "tag_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tag_user_id",
                schema: "VmProjectBE",
                table: "tag_user",
                newName: "tag)_user_id");
        }
    }
}
