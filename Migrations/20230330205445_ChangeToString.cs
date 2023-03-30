using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class ChangeToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex (
                name: "IX_vm_instance_vm_template_id",
                schema: "VmProjectBE",
                table: "vm_instance"
            );

            migrationBuilder.AlterColumn<string>(
                name: "vm_template_id",
                schema: "VmProjectBE",
                table: "vm_instance",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int"
                );

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
