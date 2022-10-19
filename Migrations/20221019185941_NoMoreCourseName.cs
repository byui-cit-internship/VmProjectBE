using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class NoMoreCourseName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "course_name",
                schema: "VmProjectBE",
                table: "course");

            migrationBuilder.AlterColumn<int>(
                name: "resource_group_id",
                schema: "VmProjectBE",
                table: "course",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "resource_group_id",
                schema: "VmProjectBE",
                table: "course",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<string>(
                name: "course_name",
                schema: "VmProjectBE",
                table: "course",
                type: "varchar(75)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 3);
        }
    }
}
