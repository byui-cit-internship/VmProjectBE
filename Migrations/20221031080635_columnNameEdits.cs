using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class columnNameEdits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email_is_verified",
                schema: "VmProjectBE",
                table: "user",
                newName: "is_verified");

            migrationBuilder.RenameColumn(
                name: "library_id",
                schema: "VmProjectBE",
                table: "section",
                newName: "library_vcenter_id");

            migrationBuilder.RenameColumn(
                name: "library_id",
                schema: "VmProjectBE",
                table: "vm_template",
                newName: "library_vcenter_id");

            migrationBuilder.RenameColumn(
                name: "enrollment_term_id",
                schema: "VmProjectBE",
                table: "semester",
                newName: "enrollment_term_canvas_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                schema: "VmProjectBE",
                table: "semester",
                type: "datetime2(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "semester_year",
                schema: "VmProjectBE",
                table: "semester",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "semester_term",
                schema: "VmProjectBE",
                table: "semester",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                schema: "VmProjectBE",
                table: "semester",
                type: "datetime2(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "enrollment_term_canvas_id",
                schema: "VmProjectBE",
                table: "semester",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<int>(
                name: "section_number",
                schema: "VmProjectBE",
                table: "section",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "section_name",
                schema: "VmProjectBE",
                table: "section",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<int>(
                name: "section_canvas_id",
                schema: "VmProjectBE",
                table: "section",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_verified",
                schema: "VmProjectBE",
                table: "user",
                newName: "email_is_verified");

            migrationBuilder.RenameColumn(
                name: "library_vcenter_id",
                schema: "VmProjectBE",
                table: "section",
                newName: "library_id");

            migrationBuilder.RenameColumn(
                name: "library_vcenter_id",
                schema: "VmProjectBE",
                table: "vm_template",
                newName: "library_id");

            migrationBuilder.RenameColumn(
                name: "enrollment_term_canvas_id",
                schema: "VmProjectBE",
                table: "semester",
                newName: "enrollment_term_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                schema: "VmProjectBE",
                table: "semester",
                type: "datetime2(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "semester_year",
                schema: "VmProjectBE",
                table: "semester",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "semester_term",
                schema: "VmProjectBE",
                table: "semester",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                schema: "VmProjectBE",
                table: "semester",
                type: "datetime2(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<int>(
                name: "enrollment_term_id",
                schema: "VmProjectBE",
                table: "semester",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "section_number",
                schema: "VmProjectBE",
                table: "section",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "section_name",
                schema: "VmProjectBE",
                table: "section",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "section_canvas_id",
                schema: "VmProjectBE",
                table: "section",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);
        }
    }
}
