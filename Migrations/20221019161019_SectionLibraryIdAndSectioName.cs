using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class SectionLibraryIdAndSectioName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "library_id",
                schema: "VmProjectBE",
                table: "section",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<string>(
                name: "section_name",
                schema: "VmProjectBE",
                table: "section",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "library_id",
                schema: "VmProjectBE",
                table: "section");

            migrationBuilder.DropColumn(
                name: "section_name",
                schema: "VmProjectBE",
                table: "section");
        }
    }
}
