using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class remove_template_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_vm_template_vm_template_id",
                schema: "VmProjectBE",
                table: "vm_instance");

            migrationBuilder.DropTable(
                name: "vm_template_tag",
                schema: "VmProjectBE");

            migrationBuilder.DropTable(
                name: "vm_template",
                schema: "VmProjectBE");

            migrationBuilder.DropIndex(
                name: "IX_vm_instance_vm_template_id",
                schema: "VmProjectBE",
                table: "vm_instance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vm_template",
                schema: "VmProjectBE",
                columns: table => new
                {
                    vm_template_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vm_template_vcenter_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    vm_template_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    vm_template_access_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    library_vcenter_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vm_template", x => x.vm_template_id);
                });

            migrationBuilder.CreateTable(
                name: "vm_template_tag",
                schema: "VmProjectBE",
                columns: table => new
                {
                    vm_template_tag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_id = table.Column<int>(type: "int", nullable: false),
                    vm_template_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vm_template_tag", x => x.vm_template_tag_id);
                    table.ForeignKey(
                        name: "FK_vm_template_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalSchema: "VmProjectBE",
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vm_template_tag_vm_template_vm_template_id",
                        column: x => x.vm_template_id,
                        principalSchema: "VmProjectBE",
                        principalTable: "vm_template",
                        principalColumn: "vm_template_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vm_instance_vm_template_id",
                schema: "VmProjectBE",
                table: "vm_instance",
                column: "vm_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_template_tag_tag_id",
                schema: "VmProjectBE",
                table: "vm_template_tag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_template_tag_vm_template_id",
                schema: "VmProjectBE",
                table: "vm_template_tag",
                column: "vm_template_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_vm_template_vm_template_id",
                schema: "VmProjectBE",
                table: "vm_instance",
                column: "vm_template_id",
                principalSchema: "VmProjectBE",
                principalTable: "vm_template",
                principalColumn: "vm_template_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
