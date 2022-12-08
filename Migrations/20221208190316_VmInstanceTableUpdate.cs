using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class VmInstanceTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "vm_instance_vcenter_name",
                schema: "VmProjectBE",
                table: "vm_instance",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "vm_instance_expire_date",
                schema: "VmProjectBE",
                table: "vm_instance",
                type: "datetime2(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                schema: "VmProjectBE",
                table: "vm_instance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "vm_instance_create_date",
                schema: "VmProjectBE",
                table: "vm_instance",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectionId",
                schema: "VmProjectBE",
                table: "vm_instance");

            migrationBuilder.DropColumn(
                name: "vm_instance_create_date",
                schema: "VmProjectBE",
                table: "vm_instance");

            migrationBuilder.AlterColumn<string>(
                name: "vm_instance_vcenter_name",
                schema: "VmProjectBE",
                table: "vm_instance",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "vm_instance_expire_date",
                schema: "VmProjectBE",
                table: "vm_instance",
                type: "datetime2(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);
        }
    }
}
