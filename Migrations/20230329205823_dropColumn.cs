using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class dropColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "vm_template_id",
            //     schema: "VmProjectBE",
            //     table: "vm_instance");

            // migrationBuilder.AddColumn<string>(
            //     name: "vm_template_id2",
            //     schema: "VmProjectBE",
            //     table: "vm_instance",
            //     type: "nvarchar(max)",
            //     nullable: false,
            //     defaultValue: "")
            //     .Annotation("Relational:ColumnOrder", 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "vm_template_id2",
            //     schema: "VmProjectBE",
            //     table: "vm_instance");

            // migrationBuilder.AddColumn<string>(
            //     name: "vm_template_id",
            //     schema: "VmProjectBE",
            //     table: "vm_instance",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     defaultValue: "")
            //     .Annotation("Relational:ColumnOrder", 2);
        }
    }
}
