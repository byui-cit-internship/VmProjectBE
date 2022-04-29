using Microsoft.EntityFrameworkCore.Migrations;

namespace Database_VmProject.Migrations
{
    public partial class ResourceUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_resource_group_template_resource_group_template_id",
                schema: "VmProjectBE",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_folder_user_user_UserId",
                schema: "VmProjectBE",
                table: "folder_user");

            migrationBuilder.DropTable(
                name: "resource_group_template",
                schema: "VmProjectBE");

            migrationBuilder.DropIndex(
                name: "IX_folder_user_UserId",
                schema: "VmProjectBE",
                table: "folder_user");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "VmProjectBE",
                table: "folder_user");

            migrationBuilder.RenameColumn(
                name: "resource_group_template_id",
                schema: "VmProjectBE",
                table: "course",
                newName: "resource_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_course_resource_group_template_id",
                schema: "VmProjectBE",
                table: "course",
                newName: "IX_course_resource_group_id");

            migrationBuilder.AddColumn<string>(
                name: "resource_group_name",
                schema: "VmProjectBE",
                table: "resource_group",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "resource_group_vsphere_id",
                schema: "VmProjectBE",
                table: "resource_group",
                type: "varchar(15)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_folder_user_user_id",
                schema: "VmProjectBE",
                table: "folder_user",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_course_resource_group_resource_group_id",
                schema: "VmProjectBE",
                table: "course",
                column: "resource_group_id",
                principalSchema: "VmProjectBE",
                principalTable: "resource_group",
                principalColumn: "resource_group_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_folder_user_user_user_id",
                schema: "VmProjectBE",
                table: "folder_user",
                column: "user_id",
                principalSchema: "VmProjectBE",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_resource_group_resource_group_id",
                schema: "VmProjectBE",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_folder_user_user_user_id",
                schema: "VmProjectBE",
                table: "folder_user");

            migrationBuilder.DropIndex(
                name: "IX_folder_user_user_id",
                schema: "VmProjectBE",
                table: "folder_user");

            migrationBuilder.DropColumn(
                name: "resource_group_name",
                schema: "VmProjectBE",
                table: "resource_group");

            migrationBuilder.DropColumn(
                name: "resource_group_vsphere_id",
                schema: "VmProjectBE",
                table: "resource_group");

            migrationBuilder.RenameColumn(
                name: "resource_group_id",
                schema: "VmProjectBE",
                table: "course",
                newName: "resource_group_template_id");

            migrationBuilder.RenameIndex(
                name: "IX_course_resource_group_id",
                schema: "VmProjectBE",
                table: "course",
                newName: "IX_course_resource_group_template_id");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "VmProjectBE",
                table: "folder_user",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "resource_group_template",
                schema: "VmProjectBE",
                columns: table => new
                {
                    resource_group_template_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cpu = table.Column<double>(type: "float", nullable: false),
                    memory = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_group_template", x => x.resource_group_template_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_folder_user_UserId",
                schema: "VmProjectBE",
                table: "folder_user",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_course_resource_group_template_resource_group_template_id",
                schema: "VmProjectBE",
                table: "course",
                column: "resource_group_template_id",
                principalSchema: "VmProjectBE",
                principalTable: "resource_group_template",
                principalColumn: "resource_group_template_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_folder_user_user_UserId",
                schema: "VmProjectBE",
                table: "folder_user",
                column: "UserId",
                principalSchema: "VmProjectBE",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
