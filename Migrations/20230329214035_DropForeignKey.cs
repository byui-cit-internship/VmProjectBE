using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class DropForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_vm_instance_vm_template_vm_template_id","vm_instance", "VmProjectBE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
